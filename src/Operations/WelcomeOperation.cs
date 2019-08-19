using System;
using Prophet.Entities;
using Prophet.Exceptions;
using Prophet.ReadModel;

namespace Prophet.Operations
{
    /**
     * Пользователь пришёл в первый раз
     */
    public class WelcomeOperation
    {
        readonly UserReadModel _userReadModel;
        readonly ArticleReadModel _articleReadModel;

        readonly CreateUserEntityOperation _createUserEntityOperation;

        public WelcomeOperation(
            UserReadModel userReadModel,
            ArticleReadModel articleReadModel,
            CreateUserEntityOperation createUserEntityOperation
        )
        {
            _userReadModel = userReadModel;
            _articleReadModel = articleReadModel;
            _createUserEntityOperation = createUserEntityOperation;
        }

        public void Welcome(string userId)
        {
            if (_userReadModel.IsUserExists(userId))
            {
                throw new AlreadyExistsException($"Пользователь {userId} уже существует");
            }

            _createUserEntityOperation.Create(
                userId,
                _articleReadModel.LastArticleId()
            );
        }
    }
}
