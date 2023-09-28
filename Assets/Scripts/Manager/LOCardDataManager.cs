using System.Collections.Generic;
using LO.Event;
using LO.Meta;
using LO.Model;
using LO.Utils;

namespace LO.Manager {

    public class LOCardDataManager : ILOCardDataManager {

        public static ILOCardDataManager Create(ILOGameDataManager gameDataManager) {

            return new LOCardDataManager(gameDataManager);
        }

        public Dictionary<LOCardType, List<ILOCardModelBase>> AllCardDic { get; private set; }
        public List<ILOCardModelBase> UserCardDeck { get; private set; }

        LOCardBookMeta m_CardBook;
        ILOGameDataManager m_GameDataManager;

        private LOCardDataManager(ILOGameDataManager gameDataManager) {

            m_GameDataManager = gameDataManager;
        }

        #region ILOCardDataManager

        public void Reload(LOSimpleEvent complete = null) {

            LOLogger.DI("LOCardDataManager Reload Book Meta", "start");

            AllCardDic = new Dictionary<LOCardType, List<ILOCardModelBase>>();

            LOAddressable.Load<LOCardBookMeta>(
                LOConst.CardBookKey,
                (book) => {

                    m_CardBook = book;

                    UpdateAllCardDicByMeta();

                    complete?.Invoke();

                    LOLogger.DI("LOCardDataManager Reload Book Meta", "complete");
                }
            );
        }

        public void UpdateUserCardDeck(List<ILOCardModelBase> newCardDeck) {

            // TODO : check the new card deck is available. compare with user card meta
            UserCardDeck = newCardDeck;
        }

        void UpdateAllCardDicByMeta() {

            foreach (var card in m_CardBook.CardList) {

                var type = card.CardType;
                if (!AllCardDic.ContainsKey(type)) {

                    AllCardDic.Add(type, new List<ILOCardModelBase>());
                }
                // TODO : refine this fake user card server data after backend setup
                AllCardDic[type].Add(LOCardModelBase.CreateFake(card));
            }
        }

        #endregion
    }
}