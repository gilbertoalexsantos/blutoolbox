using System;
using System.Threading;
using UnityEngine;

namespace BluToolbox
{
  public static class AwaitableExtensions
  {
    public static async Awaitable<T> FromResult<T>(this T result)
    {
      await Awaitable.MainThreadAsync();
      return result;
    }

    public static async Awaitable WaitUntil(
      Func<bool> condition,
      CancellationToken cancellationToken = default
    )
    {
      while (!condition())
        await Awaitable.EndOfFrameAsync(cancellationToken);
    }
  }
}