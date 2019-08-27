using System;
using Prophet.Entities;
using Prophet.EntityOperations;
using Prophet.Exceptions;
using Prophet.ReadModel;

namespace Prophet.Operations
{
    public abstract class StartOperationResult { }

    public class Newcomer : StartOperationResult { }

    public class UserWaitArticle : StartOperationResult { }

    public class UserHasArticle : StartOperationResult
    {
        public Article Article { get; set; }

        public UserHasArticle(Article article)
        {
            Article = article;
        }
    }

    /**
     * Пользователь пришёл в первый раз
     */
    public class StartOperation
    {
        readonly UserReadModel _userReadModel;
        readonly ArticleReadModel _articleReadModel;
        readonly CreateUserEntityOperation _createUserEntityOperation;

        public StartOperation(
            UserReadModel userReadModel,
            ArticleReadModel articleReadModel,
            CreateUserEntityOperation createUserEntityOperation
        )
        {
            _userReadModel = userReadModel;
            _articleReadModel = articleReadModel;
            _createUserEntityOperation = createUserEntityOperation;
        }

        public StartOperationResult Start(string userId)
        {
            var muser = _userReadModel.ById(userId);

            if (muser is Just<User> user)
            {
                var marticle = _articleReadModel.NextArticle(user);

                if (marticle is Just<Article> article)
                {
                    return new UserHasArticle(article);
                }

                return new UserWaitArticle();
            }

            _createUserEntityOperation.Create(
                userId,
                _articleReadModel.LastArticleId()
            );

            return new Newcomer();
        }
    }

}
