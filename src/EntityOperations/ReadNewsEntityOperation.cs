using Prophet.Entities;

namespace Prophet.EntityOperations
{
    /**
     * Накатывает на базу изменения последовательно
     * начиная с указанной версии
     */
    public class ReadNewsEntityOperation
    {
        readonly ProphetContext _storage;

        public ReadNewsEntityOperation(ProphetContext storage)
        {
            _storage = storage;
        }

        public void Read(User user, Article news)
        {
            user.LastReceivedArticleId = news.ArticleId;
            _storage.Update(user);
            _storage.SaveChanges();
        }
    }
}
