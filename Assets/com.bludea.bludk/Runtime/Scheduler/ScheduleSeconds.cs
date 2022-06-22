using System;

namespace Bludk
{
    internal class ScheduleSeconds : ISchedule
    {
        private float _nextSecondToExecute;
        private float _seconds;
        private bool _repeat;

        public bool IsCancelled { get; private set; }
        public Action Callback { get; private set; }
        public bool ShouldRemove { get; private set; }

        public ScheduleSeconds(float seconds, bool repeat, float secondsSinceStartup, Action callback)
        {
            _seconds = seconds;
            _repeat = repeat;
            _nextSecondToExecute = secondsSinceStartup + seconds;
            Callback = callback;
        }

        public bool ShouldCall(float secondsSinceStartup, int frame)
        {
            if (IsCancelled || ShouldRemove)
            {
                return false;
            }

            return secondsSinceStartup >= _nextSecondToExecute;
        }

        public void Call(float secondsSinceStartup, int frame)
        {
            _nextSecondToExecute += _seconds;

            if (!_repeat)
            {
                ShouldRemove = true;
            }

            Callback();
        }

        public void Dispose()
        {
            IsCancelled = true;
            ShouldRemove = true;
            Callback = null;
        }
    }
}