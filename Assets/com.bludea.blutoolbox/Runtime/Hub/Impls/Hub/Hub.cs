using System;
using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public class Hub : IHub
  {
    private readonly Dictionary<Type, List<HubEventDisposable>> _actions = new();

    public IHubEventDisposable Register<T>(Action<T> cb) where T : IHubEvent
    {
      if (cb == null)
      {
        throw new ArgumentNullException($"{nameof(cb)} cannot be null");
      }

      HubEventDisposable handler = new(cb, Remove);

      Type type = typeof(Action<T>);
      if (_actions.TryGetValue(type, out List<HubEventDisposable> handlers))
      {
        handlers.Add(handler);
      }
      else
      {
        _actions[type] = new List<HubEventDisposable>
        {
          handler
        };
      }

      return handler;
    }

    public void Call<T>(T hubEvent) where T : IHubEvent
    {
      if (_actions.TryGetValue(typeof(Action<T>), out List<HubEventDisposable> handlers))
      {
        foreach (HubEventDisposable handler in handlers.ToList())
        {
          ((Action<T>) handler.Callback)(hubEvent);
        }
      }
    }

    private void Remove(HubEventDisposable handler)
    {
      Type type = handler.Callback.GetType();
      if (_actions.TryGetValue(type, out List<HubEventDisposable> handlers))
      {
        handlers.Remove(handler);
      }
    }
  }
}