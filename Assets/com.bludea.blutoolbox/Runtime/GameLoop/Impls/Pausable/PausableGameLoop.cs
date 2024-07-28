using System;

namespace BluToolbox
{
  public class PausableGameLoop : IGameLoop, IGameLoopListener
  {
    private readonly DisposableRegistry<IGameLoopListener> _disposableRegistry = new();
    private readonly IDisposable _gameLoopDisposable;

    private bool _paused;

    public PausableGameLoop(IGameLoop gameLoop)
    {
      _gameLoopDisposable = gameLoop.Register(this);
    }

    public void Dispose()
    {
      _gameLoopDisposable.Dispose();
      _disposableRegistry.Dispose();
    }

    public void TogglePause()
    {
      _paused = !_paused;
    }

    public IDisposable Register(IGameLoopListener listener)
    {
      return _disposableRegistry.Register(listener);
    }

    public void OnUpdate(float deltaTime)
    {
      if (_paused)
      {
        return;
      }

      foreach (IGameLoopListener handler in _disposableRegistry)
      {
        handler.OnUpdate(deltaTime);
      }
    }

    public void OnLateUpdate(float deltaTime)
    {
      if (_paused)
      {
        return;
      }

      foreach (IGameLoopListener handler in _disposableRegistry)
      {
        handler.OnLateUpdate(deltaTime);
      }
    }

    public void OnFixedUpdate(float fixedDeltaTime)
    {
      if (_paused)
      {
        return;
      }


      foreach (IGameLoopListener handler in _disposableRegistry)
      {
        handler.OnFixedUpdate(fixedDeltaTime);
      }
    }
  }
}