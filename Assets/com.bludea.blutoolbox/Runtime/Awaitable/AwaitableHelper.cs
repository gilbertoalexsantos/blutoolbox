using System;
using System.Threading;
using UnityEngine;

namespace BluToolbox
{
  public static class AwaitableHelper
  {
    public static async void FireAndForget(Func<Awaitable> taskFn, CancellationToken token)
    {
      if (token.IsCancellationRequested)
      {
        token.ThrowIfCancellationRequested();
      }

      await taskFn();
    }
  }
}