using Prophet.ReadModel;

namespace Prophet.Operations
{
    /**
     * Пользователь вернулся
     */
    public class СomebackOperation
    {
        readonly UserReadModel _userReadModel;

        public СomebackOperation(UserReadModel userReadModel)
        {
            _userReadModel = userReadModel;
        }

        public bool IsСomeback(string userId)
        {
            return _userReadModel.IsUserExists(userId);
        }
    }
}
