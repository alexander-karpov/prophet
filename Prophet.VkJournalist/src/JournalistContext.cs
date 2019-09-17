using Microsoft.EntityFrameworkCore;
using System.Linq;
using Prophet.VkJournalist.Model;

namespace Prophet.VkJournalist
{
    public class JournalistContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }

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

        public void EnsureOwner(string id)
        {
            if (Owners.Count(owner => owner.Id == id) != 0)
            {
                return;
            }

            Owners.Add(new Owner { Id = id });
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
