using LO.Manager;

namespace LO.Model {

    public interface ILOUIBaseViewModel {

        string Display(string key, string tempValue);
    }

    public class LOUIBaseViewModel : ILOUIBaseViewModel {


        ILOLocaleManager m_LocaleManager;

        protected LOUIBaseViewModel(ILOLocaleManager localeManager) {

            m_LocaleManager = localeManager;
        }

        public string Display(string key, string tempValue) {
            return m_LocaleManager.T(key, tempValue);
        }
    }
}