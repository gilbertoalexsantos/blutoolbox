using System;
using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public class PausableGameLoop : IGameLoop, IGameLoopListener
  {
    private readonly HashSet<GameLoopHandlerDisposable> _handlers = new();
    private readonly IDisposable _gameLoopDisposable;

    private bool _paused;

    public PausableGameLoop(IGameLoop gameLoop)
    {
      _gameLoopDisposable = gameLoop.Register(this);
    }

    public void Dispose()
    {
      _gameLoopDisposable.Dispose();

      foreach (GameLoopHandlerDisposable disposable in _handlers.ToList())
      {
        disposable.Dispose();
      }
      _handlers.Clear();
    }

    public void TogglePause()
    {
      _paused = !_paused;
    }

    public IGameLoopHandlerDisposable Register(IGameLoopListener listener)
    {
      GameLoopHandlerDisposable disposable = new(listener, Remove);
      _handlers.Add(disposable);
      return disposable;
    }

    private void Remove(GameLoopHandlerDisposable handler)
    {
      _handlers.Remove(handler);
    }

    public void OnUpdate(float deltaTime)
    {
      if (_paused)
      {
        return;
      }

      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnUpdate(deltaTime);
      }
    }

    public void OnLateUpdate(float deltaTime)
    {
      if (_paused)
      {
        return;
      }

      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnLateUpdate(deltaTime);
      }
    }

    public void OnFixedUpdate(float fixedDeltaTime)
    {
      if (_paused)
      {
        return;
      }

      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnFixedUpdate(fixedDeltaTime);
      }
    }
  }
}