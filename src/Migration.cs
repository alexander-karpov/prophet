using System;
using System.Linq;
using Prophet.Entities;

namespace Prophet
{
    /**
     * Накатывает на базу изменения последовательно
     * начиная с указанной версии
     */
    public class Mirgation
    {
        readonly ProphetContext _db;

        public Mirgation(ProphetContext db)
        {
            _db = db;
        }

        public void Migrate()
        {
            EnsureDatabaseExists();

            var currentVersion = CurrentVersion();

            if (currentVersion < 1)
            {
                _db.Feeds.Add(
                     new Feed { Platform = "VK", AuthorId = "41946361", AuthorName = "Дмитрий Емец", LastPulledOn = DateTime.UtcNow }
                 );

                _db.Feeds.Add(
                    new Feed { Platform = "VK", AuthorId = "2222944", AuthorName = "Андрей Ромашко", LastPulledOn = DateTime.UtcNow }
                );

                _db.Feeds.Add(
                    new Feed { Platform = "VK", AuthorId = "19458733", AuthorName = "Степан Берёзкин", LastPulledOn = DateTime.UtcNow }
                );

                _db.Migrations.Update(new Migration { Version = 1 });
                _db.SaveChanges();
            }
        }

        private int CurrentVersion()
        {
            var migration = _db.Migrations.OrderByDescending(m => m.Version).FirstOrDefault();
            return migration?.Version ?? 0;
        }

        private void EnsureDatabaseExists()
        {
            _db.Database.EnsureCreated();
        }
    }
}
