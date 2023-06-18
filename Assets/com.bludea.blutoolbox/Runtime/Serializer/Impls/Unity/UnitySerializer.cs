using UnityEngine;

namespace BluToolbox
{
  public class UnitySerializer : ISerializer
  {
    public string Serialize(object obj)
    {
      return JsonUtility.ToJson(obj);
    }

    public T Deserialize<T>(string data)
    {
      return JsonUtility.FromJson<T>(data);
    }
  }
}