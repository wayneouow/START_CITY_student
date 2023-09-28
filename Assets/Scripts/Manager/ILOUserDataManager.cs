using LO.Model;
using LO.Event;

namespace LO.Manager {

    public interface ILOUserDataManager : ILOManagerBase {

        ILOUserModel UserModel { get; }
        void UpdateUser();

        event LOUserModelEvent UserModelUpdateEvent;

        // NOTE : Editor mode only
        void AddUserCoin(int coin);
        void AddUserTechPoint(int techPoint);
        void AddUserExperience(int experience);
        void AddUserLevel(int level);
    }
}
