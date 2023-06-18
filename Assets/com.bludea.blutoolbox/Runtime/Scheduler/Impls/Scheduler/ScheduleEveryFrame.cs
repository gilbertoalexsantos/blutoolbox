using System;

namespace BluToolbox
{
    internal class ScheduleEveryFrame : ISchedule
    {
        private int _lastFrameCall;

        public bool IsCancelled { get; private set; }
        public Action Callback { get; private set; }
        public bool ShouldRemove { get; private set; }

        public ScheduleEveryFrame(int frame, Action callback)
        {
            _lastFrameCall = frame;
            Callback = callback;
        }

        public bool ShouldCall(float secondsSinceStartup, int frame)
        {
            if (IsCancelled || ShouldRemove)
            {
                return false;
            }

            return _lastFrameCall != frame;
        }

        public void Call(float secondsSinceStartup, int frame)
        {
            _lastFrameCall = frame;
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