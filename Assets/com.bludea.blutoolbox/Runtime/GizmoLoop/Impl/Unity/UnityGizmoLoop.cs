using System;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGizmoLoop : MonoBehaviour, IGizmoLoop
  {
    private readonly DisposableRegistry<IGizmoLoopListener> _disposableRegistry = new();

    public void OnDrawGizmos()
    {
      foreach (IGizmoLoopListener gizmoLoopListener in _disposableRegistry)
      {
        gizmoLoopListener.OnDrawGizmosEvent();
      }
    }

    public IDisposable Register(IGizmoLoopListener listener)
    {
      return _disposableRegistry.Register(listener);
    }
  }
}