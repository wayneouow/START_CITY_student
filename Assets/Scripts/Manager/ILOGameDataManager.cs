namespace LO.Manager {

    public interface ILOGameDataManager : ILOManagerBase {

        ILOLocaleManager LocaleManager { get; }
        ILOCardDataManager CardDataManager { get; }
        ILOTechDataManager TechDataManager { get; }
        ILOUserDataManager UserDataManager { get; }
    }
}