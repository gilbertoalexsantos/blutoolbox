using System;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGameLoop : MonoBehaviour, IGameLoop
  {
    private readonly DisposableManager<IGameLoopListener> _disposableManager = new();

    public void Dispose()
    {
      _disposableManager.Dispose();
    }

    public IDisposable Register(IGameLoopListener listener)
    {
      return _disposableManager.Register(listener);
    }

    private void Update()
    {
      foreach (IGameLoopListener handler in _disposableManager)
      {
        handler.OnUpdate(Time.deltaTime);
      }
    }

    private void LateUpdate()
    {
      foreach (IGameLoopListener handler in _disposableManager)
      {
        handler.OnLateUpdate(Time.deltaTime);
      }
    }

    private void FixedUpdate()
    {
      foreach (IGameLoopListener handler in _disposableManager)
      {
        handler.OnFixedUpdate(Time.fixedDeltaTime);
      }
    }
  }
}