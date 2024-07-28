using System;

namespace BluToolbox.Tests
{
  public class TestGameLoop : IGameLoop
  {
    private readonly DisposableRegistry<IGameLoopListener> _disposableRegistry = new();

    public IDisposable Register(IGameLoopListener listener)
    {
      return _disposableRegistry.Register(listener);
    }

    public void Dispose()
    {
      _disposableRegistry.Dispose();
    }

    public void Update(float deltaTime)
    {
      foreach (IGameLoopListener listener in _disposableRegistry)
      {
        listener.OnUpdate(deltaTime);
      }
    }

    public void LateUpdate(float deltaTime)
    {
      foreach (IGameLoopListener listener in _disposableRegistry)
      {
        listener.OnLateUpdate(deltaTime);
      }
    }

    public void FixedUpdate(float deltaTime)
    {
      foreach (IGameLoopListener listener in _disposableRegistry)
      {
        listener.OnFixedUpdate(deltaTime);
      }
    }
  }
}