using LO.Manager;

namespace LO.Controller {

    public interface ILOBaseController {
        // manager
        ILOGameDataManager GameDataManager { get; }
        ILOLocaleManager LocaleManager { get; }
        ILOUserDataManager UserDataManager { get; }
        ILOCardDataManager CardDataManager { get; }
        ILOTechDataManager TechDataManager { get; }
        // controller
        ILOCommonUIController CommonUIController { get; }

        void Init();
        void Release();
        string T(string key);
    }

    public class LOBaseController : ILOBaseController {

        public ILOGameDataManager GameDataManager { get => LOApplication.Instance.GameDataManager; }
        public ILOLocaleManager LocaleManager { get => GameDataManager.LocaleManager; }
        public ILOUserDataManager UserDataManager { get => GameDataManager.UserDataManager; }
        public ILOCardDataManager CardDataManager { get => GameDataManager.CardDataManager; }
        public ILOTechDataManager TechDataManager { get => GameDataManager.TechDataManager; }

        public virtual ILOCommonUIController CommonUIController { get; }

        protected LOBaseController() { }

        public virtual void Init() { }
        public virtual void Release() { }

        /// <summary>
        /// Just a fast locale function so every controller can use easily.
        /// </summary>
        /// <param name="key">locale key</param>
        /// <returns></returns>
        public string T(string key) {
            return LocaleManager.T(key);
        }
    }
}