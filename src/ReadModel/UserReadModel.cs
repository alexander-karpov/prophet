using System.Linq;

namespace Prophet.ReadModel
{
    public class UserReadModel
    {
        readonly ProphetContext _db;

        public UserReadModel(ProphetContext db)
        {
            _db = db;
        }

        public bool IsUserExists(string userId)
        {
            return _db.Users.Where(u => u.UserId == userId).Count() == 1;
        }
    }
}
