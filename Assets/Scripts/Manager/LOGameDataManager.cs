using LO.Event;

namespace LO.Manager {

    public class LOGameDataManager : ILOGameDataManager {

        public static ILOGameDataManager Create() {
            return new LOGameDataManager();
        }

        public ILOLocaleManager LocaleManager { get; private set; }
        public ILOCardDataManager CardDataManager { get; private set; }
        public ILOTechDataManager TechDataManager { get; private set; }
        public ILOUserDataManager UserDataManager { get; private set; }

        const int RELOAD_COUNT = 4;
        int m_ReloadDoneCount = 0;

        private LOGameDataManager() {

            // init sub manager
            LocaleManager = LOLocaleManager.Create(this);
            CardDataManager = LOCardDataManager.Create(this);
            TechDataManager = LOTechDataManager.Create(this);
            UserDataManager = LOUserDataManager.Create(this);
        }

        public void Reload(LOSimpleEvent complete = null) {

            m_ReloadDoneCount = 0;

            LocaleManager.Reload(() => { CheckReady(complete); });
            CardDataManager.Reload(() => { CheckReady(complete); });
            TechDataManager.Reload(() => { CheckReady(complete); });
            UserDataManager.Reload(() => { CheckReady(complete); });
        }

        void CheckReady(LOSimpleEvent complete = null) {

            m_ReloadDoneCount++;

            if (m_ReloadDoneCount == RELOAD_COUNT) {

                LOLogger.DI("LOGameDataManager", "Ready");
                complete?.Invoke();
            }
        }
    }
}
