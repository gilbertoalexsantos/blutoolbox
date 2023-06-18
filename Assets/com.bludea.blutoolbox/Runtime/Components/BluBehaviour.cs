using System;
using System.Collections.Generic;
using UnityEngine;

namespace BluToolbox
{
  public abstract class BluBehaviour : MonoBehaviour
  {
    private readonly Dictionary<Type, object> _cachedComponents = new();

    protected T GetCached<T>() where T : MonoBehaviour
    {
      Type type = typeof(T);
      if (_cachedComponents.TryGetValue(type, out var cachedComponent))
      {
        return (T) cachedComponent;
      }

      T component = GetComponentInChildren<T>(true);
      _cachedComponents[type] = component;

      return component;
    }
  }
}