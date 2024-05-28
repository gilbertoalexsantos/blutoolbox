using System;
using System.Collections.Generic;

namespace BluToolbox.Tests
{
  public class TestGameLoop : IGameLoop
  {
    private class HandlerDisposable : IGameLoopHandlerDisposable
    {
      private readonly IGameLoopListener _listener;
      private readonly Action<IGameLoopListener> _onDispose;

      public HandlerDisposable(IGameLoopListener listener, Action<IGameLoopListener> onDispose)
      {
        _listener = listener;
        _onDispose = onDispose;
      }

      public void Dispose()
      {
        _onDispose(_listener);
      }
    }

    private readonly HashSet<IGameLoopListener> _listeners = new();

    public IGameLoopHandlerDisposable Register(IGameLoopListener listener)
    {
      _listeners.Add(listener);
      HandlerDisposable disposable = new(listener, (toRemoveListener) =>
      {
        _listeners.Remove(toRemoveListener);
      });
      return disposable;
    }

    public void Dispose()
    {
      foreach (IGameLoopListener listener in _listeners)
      {
        if (listener is IDisposable disposable)
        {
          disposable.Dispose();
        }
      }

      _listeners.Clear();
    }

    public void OnHardReload()
    {
      Dispose();
    }

    public void Update()
    {
      foreach (IGameLoopListener listener in _listeners)
      {
        listener.OnUpdate();
      }
    }

    public void LateUpdate()
    {
      foreach (IGameLoopListener listener in _listeners)
      {
        listener.OnLateUpdate();
      }
    }

    public void FixedUpdate()
    {
      foreach (IGameLoopListener listener in _listeners)
      {
        listener.OnFixedUpdate();
      }
    }
  }
}