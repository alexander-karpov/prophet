using Microsoft.EntityFrameworkCore;
using System.Linq;
using Prophet.VkJournalist.Model;

namespace Prophet.VkJournalist
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<PublishedPost> PublishedPosts { get; set; }

        public bool IsPublished(Post post)
        {
            return PublishedPosts.Count(published => published.Id == PostPublishMark(post)) != 0;
        }

        public void MarkAsPublished(Post post)
        {
            PublishedPosts.Add(new PublishedPost { Id = PostPublishMark(post) });
            SaveChanges();
        }

        private string PostPublishMark(Post post)
        {
            return $"{post.OwnerId}/{post.Id}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=/data/VkJournalist.db");
        }
    }

}
