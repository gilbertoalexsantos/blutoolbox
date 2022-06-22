using UnityEngine;

namespace Bludk
{
    public class Prefs : IPrefs
    {
        private readonly ISerializer _serializer;

        public Prefs(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Save(string key, object obj)
        {
            SaveString(key, _serializer.Serialize(obj));
        }

        public T Load<T>(string key)
        {
            string data = LoadString(key);
            return _serializer.Deserialize<T>(data);
        }

        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public string LoadString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public bool Has(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}