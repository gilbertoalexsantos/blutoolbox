namespace Bludk
{
    public interface IPrefs
    {
        void Save(string key, object obj);
        T Load<T>(string key);
        void SaveString(string key, string value);
        string LoadString(string key);
        string LoadString(string key, string defaultValue);
        bool Has(string key);
    }
}