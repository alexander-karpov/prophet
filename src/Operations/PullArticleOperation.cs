// using Prophet.Entities;
// using VK.OpenApi;
// using System.Threading.Tasks;

// namespace Prophet.Operations
// {
//     /**
//      * Накатывает на базу изменения последовательно
//      * начиная с указанной версии
//      */
//     public class PullArticleOperation
//     {
//         readonly ReadModel _readModel;
//         readonly CreateUserEntityOperation _createUserEntityOperation;
//         readonly VkOpenApiClient _vk;
//         readonly SaveArticleEntityOperation _saveArticleEntityOperation;

//         public PullArticleOperation(
//             ProphetContext storage,
//             ReadModel readModel,
//             CreateUserEntityOperation createUserEntityOperation,
//             VkOpenApiClient vk,
//             SaveArticleEntityOperation saveArticleEntityOperation
//         )
//         {
//             _readModel = readModel;
//             _createUserEntityOperation = createUserEntityOperation;
//             _vk = vk;
//             _saveArticleEntityOperation = saveArticleEntityOperation;
//         }

//         public async Task<Maybe<Article>> Pull(string @userId)
//         {
//             var user = EnsureUser(userId);

//             if (_readModel.FreshArticle(userId: user) is Just<Article> article)
//             {
//                 return article;
//             }

//             await FetchArticles();

//             return _readModel.FreshArticle(userId: user);
//         }

//         private User EnsureUser(string userId)
//         {
//             if (_readModel.UserById(userId) is Just<User> user)
//             {
//                 return user;
//             }

//             return _createUserEntityOperation.New(userId, _readModel.LastArticleId());
//         }

//         private async Task FetchArticles()
//         {
//             // TODO Тут должен опрашиваться сначала самый старый фид
//             // до получения первого результата
//             foreach (var feed in _readModel.Feeds())
//             {
//                 var articlesData = await _vk.WallGet(feed.AuthorId, 10, 0);
//                 var articleAdded = false;

//                 foreach (var article in articlesData)
//                 {
//                     if (!_readModel.IsArticleExists(article.id, feed))
//                     {
//                         _saveArticleEntityOperation.SaveArticle(article, feed);
//                         articleAdded = true;
//                     }
//                 }

//                 if (articleAdded)
//                 {
//                     return;
//                 }
//             }
//         }
//     }
// }
