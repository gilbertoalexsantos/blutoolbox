using System;

namespace BluToolbox
{
  internal interface ISchedule : IDisposable
  {
    bool IsCancelled { get; }
    Action Callback { get; }
    bool ShouldRemove { get; }
    bool ShouldCall(float secondsSinceStartup, int frame);
    void Call(float secondsSinceStartup, int frame);
  }
}