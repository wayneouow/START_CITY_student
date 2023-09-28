namespace LO.Manager {

    public interface ILOLocaleManager : ILOManagerBase {

        void SetLocale(string locale);
        string T(string key);
        string T(string key, string tempValue);
    }
}
