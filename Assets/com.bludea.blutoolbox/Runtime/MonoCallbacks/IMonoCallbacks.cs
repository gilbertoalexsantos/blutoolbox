using System;

namespace BluToolbox
{
  public interface IMonoCallbacks
  {
    void AddOnUpdate(Action action);
    void RemoveOnUpdate(Action action);
    void AddOnLateUpdate(Action action);
    void RemoveOnLateUpdate(Action action);
    void AddOnFixedUpdate(Action action);
    void RemoveOnFixedUpdate(Action action);
  }
}