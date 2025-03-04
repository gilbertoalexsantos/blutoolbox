using System;

namespace BluToolbox.Tests
{
  public class TestGameLoop : IGameLoop
  {
    private readonly DisposableRegistry _disposableRegistry = new();

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
      foreach (IUpdateListener listener in _disposableRegistry.Enumerate<IUpdateListener>())
      {
        listener.OnUpdate(deltaTime);
      }
    }

    public void LateUpdate(float deltaTime)
    {
      foreach (ILateUpdateListener listener in _disposableRegistry.Enumerate<ILateUpdateListener>())
      {
        listener.OnLateUpdate(deltaTime);
      }
    }

    public void FixedUpdate(float deltaTime)
    {
      foreach (IFixedUpdateListener listener in _disposableRegistry.Enumerate<IFixedUpdateListener>())
      {
        listener.OnFixedUpdate(deltaTime);
      }
    }
  }
}
