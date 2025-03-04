using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public class Scheduler : IScheduler, IUpdateListener
  {
    private readonly List<ISchedule> _schedules = new();
    private readonly IDisposable _gameLoopHandlerDisposable;

    private float _secondsPassed;
    private int _frameCount;

    public Scheduler(IGameLoop gameLoop)
    {
      _gameLoopHandlerDisposable = gameLoop.Register(this);
    }

    public void OnUpdate(float deltaTime)
    {
      _secondsPassed += deltaTime;
      int frame = _frameCount;
      for (int i = _schedules.Count - 1; i >= 0; i--)
      {
        ISchedule schedule = _schedules[i];
        if (schedule.ShouldRemove)
        {
          _schedules.RemoveAt(i);
          continue;
        }

        while (!schedule.IsCancelled && !schedule.ShouldRemove && schedule.ShouldCall(_secondsPassed, frame))
        {
          schedule.Call(_secondsPassed, frame);
        }

        if (schedule.ShouldRemove)
        {
          _schedules.RemoveAt(i);
        }
      }
      _frameCount++;
    }

    public IDisposable Schedule(float delay, float seconds, Action callback)
    {
      ScheduleSeconds scheduleSeconds = new(delay, seconds, repeat: true, _secondsPassed, callback);
      _schedules.Add(scheduleSeconds);
      return scheduleSeconds;
    }

    public IDisposable ScheduleOnce(float delay, Action callback)
    {
      ScheduleSeconds scheduleSeconds = new(delay, seconds: 0f, repeat: false, _secondsPassed, callback);
      _schedules.Add(scheduleSeconds);
      return scheduleSeconds;
    }

    public IDisposable ScheduleEveryFrame(Action callback)
    {
      ScheduleEveryFrame scheduleEveryFrame = new(_frameCount, callback);
      _schedules.Add(scheduleEveryFrame);
      return scheduleEveryFrame;
    }

    public void Dispose()
    {
      _schedules.Clear();
      _gameLoopHandlerDisposable.Dispose();
    }
  }
}
