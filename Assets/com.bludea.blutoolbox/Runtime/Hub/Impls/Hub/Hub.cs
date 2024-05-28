using System;
using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public class Hub : IHub
  {
    private readonly Dictionary<Type, HashSet<HubEventDisposable>> _actions = new();

    public IHubEventDisposable Register<T>(Action<T> cb) where T : IHubEvent
    {
      _ = cb ?? throw new ArgumentNullException($"{nameof(cb)} cannot be null");

      HubEventDisposable handler = new(cb, Remove);

      Type type = typeof(Action<T>);
      if (_actions.TryGetValue(type, out HashSet<HubEventDisposable> handlers))
      {
        handlers.Add(handler);
      }
      else
      {
        _actions[type] = new HashSet<HubEventDisposable>
        {
          handler
        };
      }

      return handler;
    }

    public void Call<T>(T hubEvent) where T : IHubEvent
    {
      if (_actions.TryGetValue(typeof(Action<T>), out HashSet<HubEventDisposable> handlers))
      {
        foreach (HubEventDisposable handler in handlers.ToList())
        {
          ((Action<T>) handler.Callback)(hubEvent);
        }
      }
    }

    public void Dispose()
    {
      foreach (HashSet<HubEventDisposable> handlers in _actions.Values)
      {
        foreach (HubEventDisposable handler in handlers)
        {
          handler.Dispose();
        }
      }

      _actions.Clear();
    }

    public void OnHardReload()
    {
      Dispose();
    }

    private void Remove(HubEventDisposable handler)
    {
      Type type = handler.Callback.GetType();
      if (_actions.TryGetValue(type, out HashSet<HubEventDisposable> handlers))
      {
        handlers.Remove(handler);
      }
    }
  }
}