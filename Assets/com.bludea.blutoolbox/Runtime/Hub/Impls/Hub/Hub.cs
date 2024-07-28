using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public class Hub : IHub
  {
    private readonly Dictionary<Type, DisposableRegistry<object>> _actions = new();

    public IDisposable Register<T>(Action<T> cb) where T : IHubEvent
    {
      _ = cb ?? throw new ArgumentNullException($"{nameof(cb)} cannot be null");

      Type type = typeof(Action<T>);
      if (_actions.ContainsKey(type) == false)
      {
        _actions[type] = new DisposableRegistry<object>();
      }

      return _actions[type].Register(cb);
    }

    public void Call<T>(T hubEvent) where T : IHubEvent
    {
      if (_actions.TryGetValue(typeof(Action<T>), out DisposableRegistry<object> disposableManager))
      {
        foreach (object obj in disposableManager)
        {
          ((Action<T>) obj)(hubEvent);
        }
      }
    }

    public void Dispose()
    {
      foreach (KeyValuePair<Type, DisposableRegistry<object>> pair in _actions)
      {
        pair.Value.Dispose();
      }
      _actions.Clear();
    }
  }
}