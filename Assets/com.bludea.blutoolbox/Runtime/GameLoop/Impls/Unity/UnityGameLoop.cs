using System.Collections.Generic;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGameLoop : MonoBehaviour, IGameLoop
  {
    private readonly HashSet<GameLoopHandlerDisposable> _handlers = new();
    private IHardReloadDisposable _hardReloadDisposable;

    public void Constructor(IHardReloadManager hardReloadManager)
    {
      _hardReloadDisposable = hardReloadManager.Register(this);
    }

    public IGameLoopHandlerDisposable Register(IGameLoopListener listener)
    {
      GameLoopHandlerDisposable disposable = new(listener, Remove);
      _handlers.Add(disposable);
      return disposable;
    }

    private void Update()
    {
      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnUpdate();
      }
    }

    private void LateUpdate()
    {
      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnLateUpdate();
      }
    }

    private void FixedUpdate()
    {
      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnFixedUpdate();
      }
    }

    private void Remove(GameLoopHandlerDisposable handler)
    {
      _handlers.Remove(handler);
    }

    public void OnHardReload()
    {
      Dispose();
    }

    public void Dispose()
    {
      foreach (GameLoopHandlerDisposable monoBehaviourEventHandlerDisposable in _handlers)
      {
        monoBehaviourEventHandlerDisposable.Dispose();
      }
      _hardReloadDisposable.Dispose();
    }
  }
}