using System;

namespace Bludk
{
    public interface IMonoCallbacks
    {
        void AddOnUpdate(Action action);
        void RemoveOnUpdate(Action action);
        void AddOnLateUpdate(Action action);
        void RemoveOnLateUpdate(Action action);
    }
}