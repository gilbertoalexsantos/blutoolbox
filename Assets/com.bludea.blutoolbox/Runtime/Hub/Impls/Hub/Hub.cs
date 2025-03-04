using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public class Hub : IHub
  {
    private readonly Dictionary<Type, DisposableRegistry> _actions = new();

    public IDisposable Register<T>(Action<T> cb) where T : IHubEvent
    {
      _ = cb ?? throw new ArgumentNullException($"{nameof(cb)} cannot be null");

      Type type = typeof(Action<T>);
      if (_actions.ContainsKey(type) == false)
      {
        _actions[type] = new DisposableRegistry();
      }

      return _actions[type].Register(cb);
    }

    public void Call<T>(T hubEvent) where T : IHubEvent
    {
      if (_actions.TryGetValue(typeof(Action<T>), out DisposableRegistry disposableManager))
      {
        foreach (Action<T> cb in disposableManager.Enumerate<Action<T>>())
        {
          cb(hubEvent);
        }
      }
    }

    public void Dispose()
    {
      foreach (KeyValuePair<Type, DisposableRegistry> pair in _actions)
      {
        pair.Value.Dispose();
      }
      _actions.Clear();
    }
  }
}
