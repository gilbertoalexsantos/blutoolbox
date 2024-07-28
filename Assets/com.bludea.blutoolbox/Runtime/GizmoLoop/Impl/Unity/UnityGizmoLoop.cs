using System;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGizmoLoop : MonoBehaviour, IGizmoLoop
  {
    private readonly DisposableManager<IGizmoLoopListener> _disposableManager = new();

    public void OnDrawGizmos()
    {
      foreach (IGizmoLoopListener gizmoLoopListener in _disposableManager)
      {
        gizmoLoopListener.OnDrawGizmosEvent();
      }
    }

    public IDisposable Register(IGizmoLoopListener listener)
    {
      return _disposableManager.Register(listener);
    }
  }
}