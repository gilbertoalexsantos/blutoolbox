using System;

namespace BluToolbox.Tests
{
  public class TestGameLoop : IGameLoop
  {
    private readonly DisposableManager<IGameLoopListener> _disposableManager = new();

    public IDisposable Register(IGameLoopListener listener)
    {
      return _disposableManager.Register(listener);
    }

    public void Dispose()
    {
      _disposableManager.Dispose();
    }

    public void Update(float deltaTime)
    {
      foreach (IGameLoopListener listener in _disposableManager)
      {
        listener.OnUpdate(deltaTime);
      }
    }

    public void LateUpdate(float deltaTime)
    {
      foreach (IGameLoopListener listener in _disposableManager)
      {
        listener.OnLateUpdate(deltaTime);
      }
    }

    public void FixedUpdate(float deltaTime)
    {
      foreach (IGameLoopListener listener in _disposableManager)
      {
        listener.OnFixedUpdate(deltaTime);
      }
    }
  }
}