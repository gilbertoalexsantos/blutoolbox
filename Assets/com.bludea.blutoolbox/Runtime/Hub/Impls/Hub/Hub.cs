using System;
using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public class Hub : IHub
  {
    private readonly Dictionary<Type, List<object>> _actions = new();

    public IHubEventDisposable Register<T>(Action<T> action) where T : IHubEvent
    {
      if (action == null)
      {
        throw new ArgumentNullException($"{nameof(action)} cannot be null");
      }

      HubEventDisposable disposable = new(action, (HubEventDisposable hubEventDisposable) =>
      {
        Remove(action);
      });

      Type type = typeof(T);
      if (_actions.TryGetValue(typeof(T), out List<object> actions))
      {
        actions.Add(action);
      }
      else
      {
        _actions[type] = new List<object>
        {
          action
        };
      }

      return disposable;
    }

    public void Call<T>(T hubEvent) where T : IHubEvent
    {
      if (_actions.TryGetValue(typeof(T), out List<object> actions))
      {
        foreach (object action in actions.ToList())
        {
          ((Action<T>) action)(hubEvent);
        }
      }
    }

    private void Remove<T>(Action<T> action) where T : IHubEvent
    {
      Type type = typeof(T);
      if (_actions.TryGetValue(type, out List<object> actions))
      {
        actions.Remove(action);
      }
    }
  }
}