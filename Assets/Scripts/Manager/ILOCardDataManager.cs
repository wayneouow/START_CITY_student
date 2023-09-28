using System.Collections.Generic;
using LO.Meta;
using LO.Model;

namespace LO.Manager {

    public interface ILOCardDataManager : ILOManagerBase {

        // LOCardBookMeta CardBook { get; }
        Dictionary<LOCardType, List<ILOCardModelBase>> AllCardDic { get; }
        List<ILOCardModelBase> UserCardDeck { get; }

        void UpdateUserCardDeck(List<ILOCardModelBase> newCardDeck);
    }
}