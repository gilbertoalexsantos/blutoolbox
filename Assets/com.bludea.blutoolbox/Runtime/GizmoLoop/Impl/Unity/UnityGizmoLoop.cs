using System;
using UnityEngine;

namespace BluToolbox
{
  public class UnityGizmoLoop : MonoBehaviour, IGizmoLoop
  {
    private readonly DisposableRegistry _disposableRegistry = new();

    public void OnDrawGizmos()
    {
      foreach (IGizmoLoopListener gizmoLoopListener in _disposableRegistry.Enumerate<IGizmoLoopListener>())
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
