using System;
using System.Text;
using System.Threading;
using System.Linq;
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
        readonly JournalistContext _ctx = new JournalistContext();
        readonly Timer _timer;

        readonly OwnersSequence _owners;

        public Journalist()
        {
            _ctx.Database.EnsureCreated();

            _ctx.EnsureOwner("41946361"); // Дмитрий Емец
            _ctx.EnsureOwner("2222944"); // Андрей Ромашко
            _ctx.EnsureOwner("19458733"); // Степан Берёзкин
            _ctx.EnsureOwner("1152487"); // Владимир Киняйкин

            _owners = new OwnersSequence(_ctx.Owners);
            _timer = new Timer(PullUpdates, null, 10000, 10000);
        }

        public void WatchAndPublish()
        {
            var userId = "41946361";




        }

        async void PullUpdates(object _)
        {
            if (_owners.Next() is Just<Owner> owner)
            {
                Console.WriteLine("Pull " + owner.Value.Id);

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
