using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public class Scheduler : IScheduler, IGameLoopListener
  {
    private readonly IClock _clock;

    private readonly List<ISchedule> _schedules = new();
    private readonly IGameLoopHandlerDisposable _gameLoopHandlerDisposable;
    private readonly IHardReloadDisposable _hardReloadDisposable;

    public Scheduler(IClock clock, IGameLoop gameLoop)
    {
      _clock = clock;
      _gameLoopHandlerDisposable = gameLoop.Register(this);
    }

    public void OnUpdate()
    {
      float secondsSinceStartup = _clock.SecondsSinceStartup;
      int frame = _clock.FrameCount;
      for (int i = _schedules.Count - 1; i >= 0; i--)
      {
        ISchedule schedule = _schedules[i];
        if (schedule.ShouldRemove)
        {
          _schedules.RemoveAt(i);
          continue;
        }

        while (!schedule.IsCancelled && !schedule.ShouldRemove && schedule.ShouldCall(secondsSinceStartup, frame))
        {
          schedule.Call(secondsSinceStartup, frame);
        }

        if (schedule.ShouldRemove)
        {
          _schedules.RemoveAt(i);
        }
      }
    }

    public void OnLateUpdate()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public IDisposable Schedule(float delay, float seconds, Action callback)
    {
      ScheduleSeconds scheduleSeconds = new(delay, seconds, repeat: true, _clock.SecondsSinceStartup, callback);
      _schedules.Add(scheduleSeconds);
      return scheduleSeconds;
    }

    public IDisposable ScheduleOnce(float delay, Action callback)
    {
      ScheduleSeconds scheduleSeconds = new(delay, seconds: 0f, repeat: false, _clock.SecondsSinceStartup, callback);
      _schedules.Add(scheduleSeconds);
      return scheduleSeconds;
    }

    public IDisposable ScheduleEveryFrame(Action callback)
    {
      ScheduleEveryFrame scheduleEveryFrame = new(_clock.FrameCount, callback);
      _schedules.Add(scheduleEveryFrame);
      return scheduleEveryFrame;
    }

    public void Dispose()
    {
      _schedules.Clear();
      _hardReloadDisposable.Dispose();
      _gameLoopHandlerDisposable.Dispose();
    }

    public void OnHardReload()
    {
      _schedules.Clear();
    }
  }
}