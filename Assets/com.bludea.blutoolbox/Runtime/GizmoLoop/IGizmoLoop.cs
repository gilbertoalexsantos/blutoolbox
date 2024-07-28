using System;

namespace BluToolbox
{
  public interface IGizmoLoop
  {
    IDisposable Register(IGizmoLoopListener listener);
  }
}