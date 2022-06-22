using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bludk
{
    public class BluBehaviour : MonoBehaviour
    {
        private readonly Dictionary<Type, object> _cachedComponents = new();

        protected T GetCached<T>() where T : MonoBehaviour
        {
            Type type = typeof(T);
            if (_cachedComponents.ContainsKey(type))
            {
                return (T) _cachedComponents[type];
            }

            T component = GetComponentInChildren<T>(true);
            _cachedComponents[type] = component;

            return component;
        }
    }
}