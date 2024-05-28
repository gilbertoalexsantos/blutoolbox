using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BluToolbox
{
  public static class TaskHelper
  {
    public static async void FireAndForget(Func<Task> taskFn, CancellationToken token)
    {
      if (token.IsCancellationRequested)
      {
        token.ThrowIfCancellationRequested();
      }

      await Awaitable.MainThreadAsync();

      if (token.IsCancellationRequested)
      {
        token.ThrowIfCancellationRequested();
      }

      await taskFn();
    }

    public static async Task RunOnMainThread(Func<Task> taskFn, CancellationToken token = default)
    {
      if (token.IsCancellationRequested)
      {
        token.ThrowIfCancellationRequested();
      }

      await Awaitable.MainThreadAsync();

      if (token.IsCancellationRequested)
      {
        token.ThrowIfCancellationRequested();
      }

      await taskFn();
    }

    public static async Task<T> RunOnMainThread<T>(Func<Task<T>> taskFn, CancellationToken token = default)
    {
      if (token.IsCancellationRequested)
      {
        token.ThrowIfCancellationRequested();
      }

      await Awaitable.MainThreadAsync();

      if (token.IsCancellationRequested)
      {
        token.ThrowIfCancellationRequested();
      }

      return await taskFn();
    }
  }
}