using Microsoft.EntityFrameworkCore;
using Prophet.Entities;

namespace Prophet
{
    public class ProphetContext : DbContext
    {
        public ProphetContext(DbContextOptions<ProphetContext> options)
            : base(options)
        { }

        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFeed> UserFeeds { get; set; }
        public DbSet<Migration> Migrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasIndex(a => new { a.ArticleOwnId, a.FeedId })
                .IsUnique();

            modelBuilder.Entity<Article>()
                .HasIndex(a => a.FeedId)
                .IsUnique();

            modelBuilder.Entity<UserFeed>()
                .HasIndex(uf => uf.UserId)
                .IsUnique();

            modelBuilder.Entity<UserFeed>()
                .HasIndex(uf => uf.FeedId)
                .IsUnique();
        }
    }
}
