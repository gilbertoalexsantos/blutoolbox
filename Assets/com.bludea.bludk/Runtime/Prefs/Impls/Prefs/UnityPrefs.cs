using UnityEngine;

namespace Bludk
{
    public class UnityPrefs : IPrefs
    {
        private readonly ISerializer _serializer;

        public UnityPrefs(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Save(string key, object obj)
        {
            SaveString(key, _serializer.Serialize(obj));
        }

        public Maybe<T> Load<T>(string key)
        {
            if (!Has(key))
            {
                return Maybe.None<T>();
            }

            T value = Load(key, default(T));
            return Maybe.Some(value);
        }

        public T Load<T>(string key, T defaultValue)
        {
            if (!Has(key))
            {
                return defaultValue;
            }

            string data = LoadString(key);
            return _serializer.Deserialize<T>(data);
        }

        public bool Has(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        private void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        private string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }
    }
}