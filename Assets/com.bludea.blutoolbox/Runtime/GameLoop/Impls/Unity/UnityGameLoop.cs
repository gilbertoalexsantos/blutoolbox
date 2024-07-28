using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGameLoop : MonoBehaviour, IGameLoop
  {
    private readonly HashSet<GameLoopHandlerDisposable> _handlers = new();

    public void Dispose()
    {
      foreach (GameLoopHandlerDisposable monoBehaviourEventHandlerDisposable in _handlers.ToList())
      {
        monoBehaviourEventHandlerDisposable.Dispose();
      }
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
        handler.Obj.OnUpdate(Time.deltaTime);
      }
    }

    private void LateUpdate()
    {
      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnLateUpdate(Time.deltaTime);
      }
    }

    private void FixedUpdate()
    {
      foreach (GameLoopHandlerDisposable handler in _handlers)
      {
        handler.Obj.OnFixedUpdate(Time.fixedDeltaTime);
      }
    }

    private void Remove(GameLoopHandlerDisposable handler)
    {
      _handlers.Remove(handler);
    }
  }
}