using System;
using NUnit.Framework;

namespace BluToolbox.Tests
{
  public class HubTest
  {
    private IHub _hub;

    private class TestEvent : IHubEvent
    {
      public int Value { get; }

      public TestEvent(int value)
      {
        Value = value;
      }
    }

    [SetUp]
    public void Setup()
    {
      _hub = new Hub();
    }

    [Test]
    public void TestRegisterWithSuccess()
    {
      Action<TestEvent> action = _ => { };
      IHubEventDisposable disposable = _hub.Register(action);
      Assert.IsNotNull(disposable);
    }

    [Test]
    public void TestCallWithSuccess()
    {
      bool actionCalled = false;
      Action<TestEvent> action = value =>
      {
        actionCalled = true;
        Assert.AreEqual(5, value.Value);
      };
      _hub.Register(action);
      _hub.Call(new TestEvent(5));
      Assert.IsTrue(actionCalled);
    }

    [Test]
    public void TestCallTwiceWithSuccess()
    {
      int cnt = 0;
      Action<TestEvent> action = value =>
      {
        cnt++;
        Assert.AreEqual(5, value.Value);
      };
      _hub.Register(action);
      _hub.Call(new TestEvent(5));
      Assert.AreEqual(1, cnt);
      _hub.Call(new TestEvent(5));
      Assert.AreEqual(2, cnt);
    }
    
    [Test]
    public void TestTwoHandlersPointingToTheSameCallbackWillStillCall()
    {
      int cnt = 0;
      Action<TestEvent> action = value =>
      {
        cnt++;
        Assert.AreEqual(5, value.Value);
      };

      IDisposable foo = _hub.Register(action);
      IDisposable bar = _hub.Register(action);

      _hub.Call(new TestEvent(5));
      Assert.AreEqual(2, cnt);

      foo.Dispose();

      _hub.Call(new TestEvent(5));
      Assert.AreEqual(3, cnt);
      
      bar.Dispose();

      _hub.Call(new TestEvent(5));
      Assert.AreEqual(3, cnt);
    }

    [Test]
    public void TestNullParameterThrowsException()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        _hub.Register<TestEvent>(null);
      });
    }

    [Test]
    public void TestUnregisterWithSuccess()
    {
      bool actionCalled = false;
      Action<TestEvent> action = _ =>
      {
        actionCalled = true;
      };
      IHubEventDisposable disposable = _hub.Register(action);
      disposable.Dispose();
      _hub.Call(new TestEvent(5));
      Assert.IsFalse(actionCalled);
    }
  }
}