using Microsoft.EntityFrameworkCore;

namespace Prophet.VkJournalist
{
    public class VkJournalistContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Article> Article { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=/data/VkJournalist.db");
        }
    }

    public class User
    {
        public string UserId { get; set; }
    }

    public class Article
    {
        public string ArticleId { get; set; }
    }
}
