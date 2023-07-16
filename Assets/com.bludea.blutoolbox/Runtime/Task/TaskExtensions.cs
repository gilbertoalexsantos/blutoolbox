using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BluToolbox
{
  public static class TaskExtensions
  {
    public static Task RunOnMainThread(Func<Task> taskFn, CancellationToken token)
    {
      return Task.Run(async () =>
      {
        if (token.IsCancellationRequested)
        {
          return;
        }

        await Awaitable.MainThreadAsync();

        if (!token.IsCancellationRequested)
        {
          await taskFn(); 
        }
      }, token);
    }
  }
}