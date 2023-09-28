using LO.Event;

namespace LO.Manager {

    public class LOTechDataManager : ILOTechDataManager {

        public static ILOTechDataManager Create(ILOGameDataManager gameDataManager) {

            return new LOTechDataManager(gameDataManager);
        }

        ILOGameDataManager m_GameDataManager;

        private LOTechDataManager(ILOGameDataManager gameDataManager) {

            m_GameDataManager = gameDataManager;
        }

        public void Reload(LOSimpleEvent complete = null) {

            // TODO : load tech meta by addressable
            complete?.Invoke();
        }
    }
}
