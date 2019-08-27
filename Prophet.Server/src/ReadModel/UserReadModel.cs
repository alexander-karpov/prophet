using System.Linq;
using Prophet.Entities;
using static Prophet.Prelude;

namespace Prophet.ReadModel
{
    public class UserReadModel
    {
        readonly ProphetContext _db;

        public UserReadModel(ProphetContext db)
        {
            _db = db;
        }

        public bool IsExists(string userId)
        {
            return _db.Users.Where(u => u.UserId == userId).Count() == 1;
        }

        public Maybe<User> ById(string userId)
        {
            return Maybe(
                _db.Users.Where(u => u.UserId == userId).FirstOrDefault()
            );
        }
    }
}
