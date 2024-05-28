using Unity.Plastic.Newtonsoft.Json;

namespace BluToolbox
{
  public class JsonNetSerializer : ISerializer
  {
    public string Serialize(object obj)
    {
      return JsonConvert.SerializeObject(obj);
    }

    public T Deserialize<T>(string data)
    {
      return JsonConvert.DeserializeObject<T>(data);
    }
  }
}