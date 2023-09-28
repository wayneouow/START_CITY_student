using System;
using System.Collections.Generic;
using LO.Event;

namespace LO.Manager {

    public class LOLocaleManager : ILOLocaleManager {

        public static ILOLocaleManager Create(ILOGameDataManager gameDataManager) {

            return new LOLocaleManager(gameDataManager);
        }

        Dictionary<string, Dictionary<string, string>> m_LocaleDics;
        string m_DefaultLocale { get => LOConst.LocaleList[0]; }
        string m_CurLocale;

        Dictionary<string, string> m_CurLocaleDic {
            get =>
                m_LocaleDics.ContainsKey(m_CurLocale) ?
                m_LocaleDics[m_CurLocale] :
                m_LocaleDics[m_DefaultLocale];
        }

        ILOGameDataManager m_GameDataManager;

        private LOLocaleManager(ILOGameDataManager gameDataManager) {

            m_GameDataManager = gameDataManager;
        }

        void Init() {

            // default locale is TW
            m_CurLocale = m_DefaultLocale;
            m_LocaleDics = new Dictionary<string, Dictionary<string, string>>();

            var localeList = LOConst.LocaleList;
            foreach (var locale in localeList) {

                m_LocaleDics.Add(locale, new Dictionary<string, string>());
            }
        }

        public void Reload(LOSimpleEvent complete = null) {

            Init();
            // TODO : read the json and create the locale dictionary 
            complete?.Invoke();
        }

        public void SetLocale(string newLocale) {

            // check whether the new locale is valid
            if (Array.IndexOf(LOConst.LocaleList, newLocale) < 0) { return; }

            m_CurLocale = newLocale;
        }

        /// <summary>
        /// Translate by the key
        /// </summary>
        /// <param name="key">locale key</param>
        /// <returns></returns>
        public string T(string key) {

            // if the key is not in the dictionary, just return the key
            return m_CurLocaleDic.ContainsKey(key) ? m_CurLocaleDic[key] : key;
        }

        /// <summary>
        /// if use NO_LOCALE_MODE symbol, then the locale manager will just return the temp value and without translate
        /// </summary>
        /// <param name="key">locale key</param>
        /// <param name="tempValue">temp locale value</param>
        /// <returns></returns>
        public string T(string key, string tempValue) {
#if NO_LOCALE_MODE
            return tempValue;
#else
            return T(key);
#endif
        }
    }
}
