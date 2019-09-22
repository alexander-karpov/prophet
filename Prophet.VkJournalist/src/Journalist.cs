using System;
using System.Linq;
using Prophet.VkJournalist.Model;
using System.Threading.Tasks;

namespace Prophet.VkJournalist
{
    class Journalist : IDisposable
    {
        const string POSTS_EXCHANGE = "posts";

        // TODO: Переделать на запрос до нех пор,
        // пока не считаем все новые сообщения
        const int FETCH_POSTS_COUNT = 32;

        readonly VkService _vk = new VkService();
        readonly JournalistContext _ctx = new JournalistContext();
        readonly MessageQueue _mq = new MessageQueue();
        readonly RoundRobinSequence<Owner> _owners;
        readonly RoundRobinSequence<string> _apiKeys = new RoundRobinSequence<string>(
            new[] {
                "f575f5e3f575f5e3f575f5e3a5f51e6cecff575f575f5e3a8f0f1e74c639563bffd1f9f", // Ежедневный пророк
                "5652e91a5652e91a5652e91a3e563e12dc556525652e91a0bd7e3e9eae7a43eaf3e5818" //Помощник ежедневного пророка
            }
        );

        public Journalist()
        {
            _ctx.Database.EnsureCreated();

            _ctx.EnsureOwner("41946361"); // Дмитрий Емец
            _ctx.EnsureOwner("2222944"); // Андрей Ромашко
            _ctx.EnsureOwner("19458733"); // Степан Берёзкин
            _ctx.EnsureOwner("1152487"); // Владимир Киняйкин
            _ctx.EnsureOwner("562314067"); // Я

            _owners = new RoundRobinSequence<Owner>(_ctx.Owners);
        }

        public async Task PullAndPublishUpdates(object _)
        {
            if (
                _owners.Next() is Just<Owner> owner &&
                _apiKeys.Next() is Just<string> key
            )
            {
                try
                {
                    var posts = await _vk.WallGet(
                    apiKey: key,
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
                catch (Exception e)
                {
                    Console.Error.WriteLine($"{nameof(this.PullAndPublishUpdates)} failed. ${e.Message}");
                }
            }
        }

        void Publish(Post post)
        {
            _mq.PublishPost(
                message: $"Источник {post.OwnerId}: {post.Text}",
                routingKey: $"vk/{post.OwnerId}");
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _mq.Dispose();
        }
    }
}
