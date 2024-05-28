using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace BluToolbox.Tests
{
  public class TaskTest
  {
    [Test]
    public async Task TestRunOnMainThreadWithReturn()
    {
      int valueToReturn = 5;
      int returnedValue = await TaskHelper.RunOnMainThread(() => ReturnValue(valueToReturn));
      Assert.AreEqual(valueToReturn, returnedValue);
    }

    [Test]
    public async Task TestRunOnMainThread()
    {
      int startFrame = Time.frameCount;
      await TaskHelper.RunOnMainThread(WaitForNextFrame);
      Assert.AreEqual(startFrame + 1, Time.frameCount);
    }

    [Test]
    public void TestCancellationToken()
    {
      CancellationTokenSource source = new();
      source.Cancel();
      int startFrame = Time.frameCount;
      Assert.ThrowsAsync<TaskCanceledException>(async () =>
      {
        await TaskHelper.RunOnMainThread(async () =>
        {
          await WaitForNextFrame();
          source.Cancel();
        }, source.Token);
      });
      Assert.AreEqual(startFrame, Time.frameCount);
    }

    [Test]
    public async Task TestException()
    {
      bool hasException = false;
      int startFrame = Time.frameCount;
      try
      {
        await TaskHelper.RunOnMainThread(async () =>
        {
          await WaitForNextFrame();
          throw new ArgumentException("Test exception");
        });
      }
      catch (Exception e)
      {
        hasException = e is ArgumentException;
      }
      Assert.AreEqual(startFrame + 1, Time.frameCount);
      Assert.IsTrue(hasException);
    }

    private async Task WaitForNextFrame()
    {
      await Awaitable.NextFrameAsync();
    }

    private async Task<int> ReturnValue(int value)
    {
      await Awaitable.NextFrameAsync();
      return value;
    }
  }
}