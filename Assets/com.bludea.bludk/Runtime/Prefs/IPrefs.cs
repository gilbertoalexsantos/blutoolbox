namespace Bludk
{
    public interface IPrefs
    {
        void Save(string key, object obj);
        Maybe<T> Load<T>(string key);
        T Load<T>(string key, T defaultValue);
        bool Has(string key);
    }
}