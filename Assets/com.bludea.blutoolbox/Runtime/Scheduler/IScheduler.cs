using System;

namespace BluToolbox
{
  public interface IScheduler
  {
    IDisposable Schedule(float seconds, Action callback);
    IDisposable ScheduleOnce(float delay, Action callback);
    IDisposable ScheduleEveryFrame(Action callback);
  }
}