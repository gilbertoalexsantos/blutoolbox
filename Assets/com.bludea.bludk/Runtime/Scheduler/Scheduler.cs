using System;
using System.Collections.Generic;

namespace Bludk
{
    public class Scheduler : IScheduler, IHardReload, IDisposable
    {
        private readonly IMonoCallbacks _monoCallbacks;
        private readonly IClock _clock;

        private readonly List<ISchedule> _schedules;

        public Scheduler(IMonoCallbacks monoCallbacks, IClock clock, IHardReloadManager hardReloadManager)
        {
            _monoCallbacks = monoCallbacks;
            _clock = clock;
            _schedules = new List<ISchedule>();
            _monoCallbacks.AddOnUpdate(Update);
            hardReloadManager.AddOnHardReload(this);
        }

        private void Update()
        {
            float secondsSinceStartup = _clock.SecondsSinceStartup;
            int frame = _clock.FrameCount;
            for (int i = _schedules.Count - 1; i >= 0; i--)
            {
                var schedule = _schedules[i];
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

        public IDisposable Schedule(float seconds, Action callback)
        {
            ScheduleSeconds scheduleSeconds = new ScheduleSeconds(seconds, true, _clock.SecondsSinceStartup, callback);
            _schedules.Add(scheduleSeconds);
            return scheduleSeconds;
        }

        public IDisposable ScheduleOnce(float seconds, Action callback)
        {
            ScheduleSeconds scheduleSeconds = new ScheduleSeconds(seconds, false, _clock.SecondsSinceStartup, callback);
            _schedules.Add(scheduleSeconds);
            return scheduleSeconds;
        }

        public IDisposable ScheduleEveryFrame(Action callback)
        {
            ScheduleEveryFrame scheduleEveryFrame = new ScheduleEveryFrame(_clock.FrameCount, callback);
            _schedules.Add(scheduleEveryFrame);
            return scheduleEveryFrame;
        }

        public void Dispose()
        {
            _monoCallbacks.RemoveOnUpdate(Update);
            _schedules.Clear();
        }

        public void OnHardReload()
        {
            _schedules.Clear();
        }
    }
}