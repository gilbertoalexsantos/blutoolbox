using System;

namespace BluToolbox
{
  public static class SchedulerExtensions
  {
    public static IDisposable ScheduleEvery(this IScheduler scheduler, TimeSpan timeSpan, Action callback)
    {
      float seconds = (float) timeSpan.TotalSeconds;
      return scheduler.Schedule(seconds, seconds, callback);
    }
  }
}