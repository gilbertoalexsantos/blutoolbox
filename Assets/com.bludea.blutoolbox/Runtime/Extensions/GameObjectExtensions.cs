using System.Threading;
using UnityEngine;

namespace BluToolbox
{
  public static class GameObjectExtensions
  {
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
      T component = obj.GetComponent<T>();
      if (component == null)
      {
        component = obj.AddComponent<T>();
      }

      return component;
    }

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
      return component.gameObject.GetOrAddComponent<T>();
    }

    public static CancellationToken CreateCancellationToken(this Component component)
    {
      return component.gameObject.CreateCancellationToken();
    }

    public static CancellationToken CreateCancellationToken(this GameObject obj)
    {
      return obj.GetOrAddComponent<OnDestroyBh>().Token;
    }
  }
}