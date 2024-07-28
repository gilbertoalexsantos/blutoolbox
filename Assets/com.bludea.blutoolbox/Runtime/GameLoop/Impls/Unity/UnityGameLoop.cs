using System;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGameLoop : MonoBehaviour, IGameLoop
  {
    private readonly DisposableRegistry<IGameLoopListener> _disposableRegistry = new();

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
      foreach (IGameLoopListener handler in _disposableRegistry)
      {
        handler.OnUpdate(Time.deltaTime);
      }
    }

    private void LateUpdate()
    {
      foreach (IGameLoopListener handler in _disposableRegistry)
      {
        handler.OnLateUpdate(Time.deltaTime);
      }
    }

    private void FixedUpdate()
    {
      foreach (IGameLoopListener handler in _disposableRegistry)
      {
        handler.OnFixedUpdate(Time.fixedDeltaTime);
      }
    }
  }
}