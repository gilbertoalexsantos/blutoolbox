using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BluToolbox.Tests
{
  public class TxongaTest
  {
    private const float DELTA = 0.1f;

    [UnityTest]
    public IEnumerator TestThenAction()
    {
      var now = DateTime.Now;
      float secondsToWait = 0.5f;
      yield return AwaitConstants.WithSeconds(secondsToWait)
        .Then(() => { })
        .Then(() => { })
        .Then(() => { })
        .StartAndAwait();
      var totalSeconds = (DateTime.Now - now).TotalSeconds;
      Assert.AreEqual(secondsToWait, totalSeconds, 0.1);
    }

    [UnityTest]
    public IEnumerator TestThenActionWithInputWithMultipleYield()
    {
      int yieldValue = 15;
      yield return AwaitConstants.WithSeconds(0.1f)
        .Then(() => YieldValue(yieldValue))
        .Then(value => { Assert.AreEqual(yieldValue, value); })
        .StartAndAwait();
    }

    [UnityTest]
    public IEnumerator TestThenActionWithInputAndNormalYield()
    {
      int yieldValue = 15;
      yield return AwaitConstants.WithSeconds(0.1f)
        .Then(() => yieldValue.Yield())
        .Then(value => { Assert.AreEqual(yieldValue, value); })
        .StartAndAwait();
    }

    [UnityTest]
    public IEnumerator TestInAndOut()
    {
      int yieldValue = 15;
      yield return AwaitConstants.WithSeconds(0.1f)
        .Then(() => yieldValue.Yield())
        .Then(value => (value * 2).Yield())
        .Then(value => Assert.AreEqual(value, yieldValue * 2))
        .StartAndAwait();
    }

    [UnityTest]
    public IEnumerator TestThenEnumerator()
    {
      var now = DateTime.Now;
      float secondsToWait = 0.5f;
      yield return AwaitConstants.WithSeconds(secondsToWait)
        .Then(() => AwaitConstants.WithSeconds(secondsToWait))
        .Then(() => AwaitConstants.WithSeconds(secondsToWait))
        .StartAndAwait();
      var totalSeconds = (DateTime.Now - now).TotalSeconds;
      Assert.AreEqual(secondsToWait * 3f, totalSeconds, DELTA);
    }

    [UnityTest]
    public IEnumerator TestThenAllEnumerator()
    {
      var now = DateTime.Now;
      yield return AwaitConstants.WithSeconds(0.5f)
        .ThenAll(() => TxongaHelper.All(
          AwaitConstants.WithSeconds(0.1f), 
          AwaitConstants.WithSeconds(0.2f), 
          AwaitConstants.WithSeconds(0.5f), 
          AwaitConstants.WithSeconds(1f))
        )
        .Then(() => AwaitConstants.WithSeconds(0.5f))
        .StartAndAwait();
      var totalSeconds = (DateTime.Now - now).TotalSeconds;
      Assert.AreEqual(2f, totalSeconds, DELTA);
    }

    [UnityTest]
    public IEnumerator TestCancel()
    {
      float duration = 1f;
      int value = 1;
      WaitForCompletion waitForCompletion = new WaitForCompletion();
      Txonga txonga = AwaitConstants.WithSeconds(0.1f)
        .Then(() => waitForCompletion.Complete())
        .Then(() => AwaitConstants.WithSeconds(duration))
        .Then(() => value = 2)
        .Start();
      yield return waitForCompletion.Await();
      txonga.Cancel();
      yield return new WaitForSeconds(duration * 2f);
      Assert.AreEqual(1, value);
      Assert.IsTrue(txonga.IsCancelled);
      Assert.IsFalse(txonga.IsCompleted);
    }

    [UnityTest]
    public IEnumerator TestTxongaReturnValue()
    {
      Txonga<int> txonga = AwaitConstants.WithSeconds(0.1f)
        .Then(() => 2.Yield())
        .Then(value => (value * 5).Yield())
        .Start();
      yield return txonga.Await();
      Assert.AreEqual(10, txonga.Result);
    }

    [UnityTest]
    public IEnumerator TextCancelToken()
    {
      CancelToken token = new CancelToken();
      bool afterCancel = false;
      yield return AwaitConstants.WithSeconds(0.25f)
        .Then(() => token.Cancel())
        .Then(() => afterCancel = true)
        .StartAndAwait(token);
      Assert.AreEqual(false, afterCancel);
    }

    [UnityTest]
    public IEnumerator TestInnerIEnumerator()
    {
      var now = DateTime.Now;
      float duration = 2f;
      yield return InnerIEnumerator1(duration)
        .StartAndAwait();
      var totalSeconds = (DateTime.Now - now).TotalSeconds;
      Assert.AreEqual(duration, totalSeconds, DELTA);
    }

    private IEnumerator InnerIEnumerator1(float totalSeconds)
    {
      float t = totalSeconds / 3f;
      yield return AwaitConstants.WithSeconds(t);
      yield return InnerEnumerator2((t));
      yield return AwaitConstants.WithSeconds(t);
    }

    private IEnumerator InnerEnumerator2(float seconds)
    {
      float t = seconds / 2f;
      yield return InnerEnumerator3(t);
      yield return AwaitConstants.WithSeconds(t);
    }

    private IEnumerator InnerEnumerator3(float seconds)
    {
      yield return AwaitConstants.WithSeconds(seconds);
    }

    private IEnumerator<int> YieldValue(int value)
    {
      yield return -1;
      yield return value;
      yield return -1;
      yield return value;
    }
  }
}