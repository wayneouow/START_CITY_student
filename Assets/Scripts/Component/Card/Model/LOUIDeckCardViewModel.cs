
using LO.Manager;
using LO.Meta;

namespace LO.Model {

    public interface ILOUIDeckCardViewModel {

        int Index { get; }
        string ImageName { get; }
        string DisplayName { get; }
        int Number { get; }
    }

    public class LOUIDeckCardViewModel : LOUIBaseViewModel, ILOUIDeckCardViewModel {

        public static ILOUIDeckCardViewModel Create(ILOCardModelBase cardModel, int index, ILOLocaleManager localeManager) {
            return new LOUIDeckCardViewModel(cardModel, index, localeManager);
        }

        public int Index { get; private set; }
        public string ImageName { get => m_CardModel.ImageName; }
        public string DisplayName { get => Display(m_CardModel.NameKey, m_CardModel.Name); }
        public int Number { get => m_CardModel.Number; }

        ILOCardModelBase m_CardModel;

        private LOUIDeckCardViewModel(ILOCardModelBase cardModel, int index, ILOLocaleManager localeManager) : base(localeManager) {

            Index = index;
            m_CardModel = cardModel;
        }
    }
}