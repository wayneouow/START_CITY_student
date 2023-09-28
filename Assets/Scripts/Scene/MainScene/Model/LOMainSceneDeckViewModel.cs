using System.Collections.Generic;
using LO.Manager;
using LO.Meta;

namespace LO.Model {

    public interface ILOMainSceneDeckViewModel {

        void Reload();
        Dictionary<LOCardType, List<ILOCardModelBase>> AllCardDic { get; }
        List<ILOCardModelBase> UserCardDeck { get; }
        int GetNumberByType(LOCardType type);
        string GetTransByType(LOCardType type);
    }

    public class LOMainSceneDeckViewModel : ILOMainSceneDeckViewModel {

        public static ILOMainSceneDeckViewModel Create(ILOCardDataManager cardDataManager, ILOLocaleManager localeManager) {
            return new LOMainSceneDeckViewModel(cardDataManager, localeManager);
        }

        public Dictionary<LOCardType, List<ILOCardModelBase>> AllCardDic { get; private set; }
        public List<ILOCardModelBase> UserCardDeck { get; private set; }

        ILOCardDataManager m_CardDataManager;
        ILOLocaleManager m_LocaleManager;

        private LOMainSceneDeckViewModel(ILOCardDataManager cardDataManager, ILOLocaleManager localeManager) {

            m_CardDataManager = cardDataManager;
            m_LocaleManager = localeManager;
            Reload();
        }

        public void Reload() {

            UserCardDeck = m_CardDataManager.UserCardDeck;
            AllCardDic = m_CardDataManager.AllCardDic;
        }

        public int GetNumberByType(LOCardType type) {

            int number = 0;
            var cardList = AllCardDic[type];
            foreach (var card in cardList) {
                number += card.Number;
            }

            return number;
        }

        public string GetTransByType(LOCardType type) {

            // TODO : refactor this
            switch (type) {
                case LOCardType.Building: return "建築";
                case LOCardType.Production: return "生產";
                case LOCardType.Functional: return "功能";
                case LOCardType.Other: return "其他";
                default: return "";
            }
        }
    }
}