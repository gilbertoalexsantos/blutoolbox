using System;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGameLoop : MonoBehaviour, IGameLoop
  {
    private readonly DisposableRegistry _disposableRegistry = new();

    public void Dispose()
    {
      _disposableRegistry.Dispose();
    }

    public IDisposable Register(IGameLoopListener listener)
    {
      return _disposableRegistry.Register(listener);
    }

    private void Update()
    {
      foreach (IUpdateListener handler in _disposableRegistry.Enumerate<IUpdateListener>())
      {
        handler.OnUpdate(Time.deltaTime);
      }
    }

    private void LateUpdate()
    {
      foreach (ILateUpdateListener handler in _disposableRegistry.Enumerate<ILateUpdateListener>())
      {
        handler.OnLateUpdate(Time.deltaTime);
      }
    }

    private void FixedUpdate()
    {
      foreach (IFixedUpdateListener handler in _disposableRegistry.Enumerate<IFixedUpdateListener>())
      {
        handler.OnFixedUpdate(Time.fixedDeltaTime);
      }
    }
  }
}
