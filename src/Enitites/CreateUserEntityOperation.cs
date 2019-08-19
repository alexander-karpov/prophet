namespace Prophet.Entities
{
    /**
     * Накатывает на базу изменения последовательно
     * начиная с указанной версии
     */
    public class CreateUserEntityOperation
    {
        readonly ProphetContext _db;

        public CreateUserEntityOperation(ProphetContext db)
        {
            _db = db;
        }

        public void Create(string userId, int lastNewsId)
        {
            _db.Users.Add(
                new User { UserId = userId, LastReceivedArticleId = lastNewsId }
            );

            _db.SaveChanges();
        }
    }
}
