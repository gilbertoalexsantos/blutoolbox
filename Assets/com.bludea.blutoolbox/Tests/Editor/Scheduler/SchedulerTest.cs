using System;
using NUnit.Framework;

namespace BluToolbox.Tests
{
  public class SchedulerTest
  {
    private TestGameLoop _gameLoop;
    private Scheduler _scheduler;

    [SetUp]
    public void SetUp()
    {
      _gameLoop = new TestGameLoop();
      _scheduler = new Scheduler(_gameLoop);
    }

    [Test]
    public void TestSchedule()
    {
      int value = 0;
      _scheduler.Schedule(delay: 1f, seconds: 1f, () => { value++; });
      WaitSeconds(0.5f);
      Assert.AreEqual(0, value);
      WaitSeconds(0.6f);
      Assert.AreEqual(1, value);
      WaitSeconds(2.2f);
      Assert.AreEqual(3, value);
    }

    [Test]
    public void TestScheduleDispose()
    {
      int value = 0;
      IDisposable schedule = _scheduler.Schedule(delay: 1f, seconds: 1f, () => { value++; });
      WaitSeconds(1.1f);
      Assert.AreEqual(1, value);
      schedule.Dispose();
      WaitSeconds(2f);
      Assert.AreEqual(1, value);

      int value2 = 0;
      IDisposable schedule2 = _scheduler.Schedule(delay: 1f, seconds: 1f, () => { value2++; });
      schedule2.Dispose();
      WaitSeconds(2f);
      Assert.AreEqual(value2, 0);
    }

    [Test]
    public void TestScheduleOnce()
    {
      int value = 0;
      _scheduler.ScheduleOnce(1f, () => { value++; });
      WaitSeconds(0.5f);
      Assert.AreEqual(0, value);
      WaitSeconds(0.6f);
      Assert.AreEqual(1, value);
      WaitSeconds(1.1f);
      Assert.AreEqual(1, value);
    }

    [Test]
    public void TestScheduleOnceDispose()
    {
      int value = 0;
      IDisposable schedule = _scheduler.ScheduleOnce(1f, () => { value++; });
      WaitSeconds(0.5f);
      Assert.AreEqual(0, value);
      schedule.Dispose();
      WaitSeconds(1f);
      Assert.AreEqual(0, value);

      int value2 = 0;
      IDisposable schedule2 = _scheduler.ScheduleOnce(1f, () => { value2++; });
      schedule2.Dispose();
      WaitSeconds(1.5f);
      Assert.AreEqual(0, value2);
    }

    [Test]
    public void TestScheduleEveryFrame()
    {
      int value = 0;
      _scheduler.ScheduleEveryFrame(() => { value++; });
      int framesPassed = WaitSeconds(1f);
      Assert.IsTrue(value == framesPassed || value == framesPassed - 1 || value == framesPassed + 1);
    }

    [Test]
    public void TestScheduleEveryFrameDispose()
    {
      int value = 0;
      IDisposable schedule = _scheduler.ScheduleEveryFrame(() => { value++; });
      int framesPassed = WaitSeconds(1f);
      Assert.IsTrue(value == framesPassed || value == framesPassed - 1 || value == framesPassed + 1);
      schedule.Dispose();
      framesPassed = WaitSeconds(1f);
      Assert.IsTrue(value == framesPassed || value == framesPassed - 1 || value == framesPassed + 1);

      int value2 = 0;
      IDisposable schedule2 = _scheduler.ScheduleEveryFrame(() => { value2++; });
      schedule2.Dispose();
      WaitSeconds(1f);
      Assert.AreEqual(0, value2);
    }

    private int WaitSeconds(float seconds)
    {
      return TestHelper.WaitSeconds(seconds, _gameLoop);
    }
  }
}