using System;

namespace Bludk
{
    public class CancelToken : ICancelToken, IDisposable
    {
        public bool IsCancelled { get; private set; }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public void Dispose()
        {
            Cancel();
        }
    }
}