namespace Prophet.Entities
{
    /**
     * Накатывает на базу изменения последовательно
     * начиная с указанной версии
     */
    public class CreateUserEntityOperation
    {
        readonly ProphetContext _storage;

        public CreateUserEntityOperation(ProphetContext storage)
        {
            _storage = storage;
        }

        public User New(string userId, int lastNewsId)
        {
            var user = new User { UserId = userId, LastReceivedArticleId = lastNewsId };
            _storage.Users.Add(user);
            _storage.SaveChanges();

            return user;
        }
    }
}
