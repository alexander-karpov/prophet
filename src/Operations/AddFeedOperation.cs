using Prophet.Entities;
using VK.OpenApi;
using System.Threading.Tasks;
using static Prophet.Prelude;

namespace Prophet.Operations
{
    /**
     * Накатывает на базу изменения последовательно
     * начиная с указанной версии
     */
    public class AddFeedOperation
    {
        readonly ReadModel _readModel;
        readonly CreateUserEntityOperation _newUserEntityOperation;
        readonly ReadNewsEntityOperation _readNewsEntityOperation;
        readonly VkOpenApiClient _vk;
        readonly SaveArticleEntityOperation _saveArticleEntityOperation;

        public AddFeedOperation(
            ProphetContext storage,
            ReadModel readModel,
            CreateUserEntityOperation newUserEntityOperation,
            ReadNewsEntityOperation readNewsEntityOperation,
            VkOpenApiClient vk,
            SaveArticleEntityOperation saveArticleEntityOperation
        )
        {
            _readModel = readModel;
            _newUserEntityOperation = newUserEntityOperation;
            _readNewsEntityOperation = readNewsEntityOperation;
            _vk = vk;
            _saveArticleEntityOperation = saveArticleEntityOperation;
        }

        public async Task AddFeed(string feedId)
        {


        }

        private User EnsureUser(string userId)
        {
            return _readModel.UserById(userId) switch
            {
                User user => user,
                _ => _newUserEntityOperation.New(userId, _readModel.LastArticleId())
            };
        }

        private async Task FetchArticles()
        {
            // TODO Тут должен опрашиваться сначала самый старый фид
            // до получения первого результата
            foreach (var feed in _readModel.Feeds())
            {
                var articlesData = await _vk.WallGet(feed.AuthorId, 10, 0);
                var articleAdded = false;

                foreach (var article in articlesData)
                {
                    if (!_readModel.IsArticleExists(article.id, feed))
                    {
                        _saveArticleEntityOperation.SaveArticle(article, feed);
                        articleAdded = true;
                    }
                }

                if (articleAdded)
                {
                    return;
                }
            }
        }
    }
}
