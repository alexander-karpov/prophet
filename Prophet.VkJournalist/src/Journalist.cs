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
        const string POSTS_EXCHANGE = "posts";

        // TODO: Переделать на запрос до нех пор,
        // пока не считаем все новые сообщения
        const int FETCH_POSTS_COUNT = 32;

        readonly VkService _vk = new VkService();
        readonly Context _ctx = new Context();
        readonly Timer _timer;

        readonly OwnersSequence _owners;

        public Journalist()
        {
            _ctx.Database.EnsureCreated();
            _owners = new OwnersSequence(_ctx.Owners);
            _timer = new Timer(PullUpdates, null, 10000, 10000);
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

            channel.ExchangeDeclare(exchange: POSTS_EXCHANGE, type: "direct");

            var body = Encoding.UTF8.GetBytes("message");

            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicPublish(exchange: POSTS_EXCHANGE,
                                 routingKey: $"vk/{userId}",
                                 basicProperties: props,
                                 body: body);
        }

        async void PullUpdates(object state)
        {
            if (_owners.Next() is Just<Owner> owner)
            {
                var posts = await _vk.WallGet(
                    ownerId: owner.Value.Id,
                    count: FETCH_POSTS_COUNT,
                    offset: 0);

                var notPublished = posts.Where(p => !_ctx.IsPublished(p));

                foreach (var post in notPublished.OrderBy(p => p.Date))
                {
                    Publish(post);
                    _ctx.MarkAsPublished(post);
                }
            }
        }

        void Publish(Post post)
        {
            Console.WriteLine("Publishing… " + post.Text);
        }

        public void Dispose()
        {
            _timer.Dispose();
            _ctx.Dispose();
        }
    }
}
