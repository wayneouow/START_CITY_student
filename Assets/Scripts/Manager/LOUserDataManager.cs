using LO.Event;
using LO.Model;

namespace LO.Manager {

    public class LOUserDataManager : ILOUserDataManager {

        public static ILOUserDataManager Create(ILOGameDataManager gameDataManager) {

            return new LOUserDataManager(gameDataManager);
        }

        public ILOUserModel UserModel { get; private set; }
        public event LOUserModelEvent UserModelUpdateEvent;

        ILOGameDataManager m_GameDataManager;

        private LOUserDataManager(ILOGameDataManager gameDataManager) {

            m_GameDataManager = gameDataManager;
        }

        public void Reload(LOSimpleEvent complete = null) {

            // TODO : fetch from server
            UserModel = LOUserModel.CreateFake();

            // TODO : create temp user model
            complete?.Invoke();

            UpdateUser();
        }

        public void UpdateUser() {

            UserModelUpdateEvent?.Invoke(UserModel);
        }

        #region Editor only

        public void AddUserCoin(int coin) {

            UserModel = LOUserModel.Create(UserModel.UserId, UserModel.Level, UserModel.Experience, UserModel.Coin + coin, UserModel.TechPoint);

            UpdateUser();
        }

        public void AddUserTechPoint(int techPoint) {

            UserModel = LOUserModel.Create(UserModel.UserId, UserModel.Level, UserModel.Experience, UserModel.Coin, UserModel.TechPoint + techPoint);

            UpdateUser();
        }

        public void AddUserExperience(int experience) {

            UserModel = LOUserModel.Create(UserModel.UserId, UserModel.Level, UserModel.Experience + experience, UserModel.Coin, UserModel.TechPoint);

            UpdateUser();
        }

        public void AddUserLevel(int level) {

            UserModel = LOUserModel.Create(UserModel.UserId, UserModel.Level + level, UserModel.Experience, UserModel.Coin, UserModel.TechPoint);

            UpdateUser();
        }

        #endregion
    }
}
