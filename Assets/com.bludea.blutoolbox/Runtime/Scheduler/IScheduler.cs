using System;

namespace BluToolbox
{
  public interface IScheduler : IDisposable, IHardReload
  {
    IDisposable Schedule(float delay, float seconds, Action callback);
    IDisposable ScheduleOnce(float delay, Action callback);
    IDisposable ScheduleEveryFrame(Action callback);
  }
}