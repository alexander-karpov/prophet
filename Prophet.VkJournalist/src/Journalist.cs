using System;
using System.Text;
using System.Threading;
using System.Linq;
using RabbitMQ.Client;
using Prophet.VkJournalist.Model;

namespace Prophet.VkJournalist
{
    class Journalist : IDisposable
    {
        readonly VkService _vk = new VkService();
        readonly Context _ctx = new Context();
        readonly Timer _timer;

        const string ARTICLES_EXCHANGE = "articles";

        public Journalist()
        {
            _ctx.Database.EnsureCreated();
            _timer = new Timer(PullUpdates, null, 0, 10000);
        }

        public void WatchAndPublish()
        {
            var userId = "41946361";

            var factory = new ConnectionFactory()
            {
                HostName = "dialogs.kukuruku.name",
                Port = 5672,
                UserName = "kukuruku",
                Password = "function-tingle-casebook-drier"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: ARTICLES_EXCHANGE, type: "direct");

            var body = Encoding.UTF8.GetBytes("message");

            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicPublish(exchange: ARTICLES_EXCHANGE,
                                 routingKey: $"vk/{userId}",
                                 basicProperties: props,
                                 body: body);
        }

        public async void PullUpdates(object state)
        {
            var userId = "41946361";

            Console.WriteLine("Pull updates…");
            var posts = await _vk.WallGet(ownerId: userId, count: 3, offset: 0);
            var notPublished = posts.Where(p => !_ctx.IsPublished(p));

            foreach (var post in notPublished.OrderBy(p => p.Date))
            {
                Publish(post);
                _ctx.MarkAsPublished(post);
            }
        }

        public void Publish(Post post)
        {
            Console.WriteLine("Publishing… " + post.Text);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
