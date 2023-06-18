using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BluToolbox.Tests
{
    public class SchedulerTest
    {
        private Scheduler _scheduler;

        [SetUp]
        public void SetUp()
        {
            IHardReloadManager hardReloadManager = new HardReloadManager();

            UnityMonoCallbacks unityMonoCallbacks = new GameObject("MonoCallbacks").AddComponent<UnityMonoCallbacks>();
            unityMonoCallbacks.Constructor(hardReloadManager);

            IClock clock = new UnityClock();

            _scheduler = new Scheduler(unityMonoCallbacks, clock, hardReloadManager);
        }

        [UnityTest]
        public IEnumerator TestSchedule()
        {
            int value = 0;
            _scheduler.Schedule(1f, () => { value++; });
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(0, value);
            yield return new WaitForSeconds(0.6f);
            Assert.AreEqual(1, value);
            yield return new WaitForSeconds(2.2f);
            Assert.AreEqual(3, value);
        }

        [UnityTest]
        public IEnumerator TestScheduleDispose()
        {
            int value = 0;
            IDisposable schedule = _scheduler.Schedule(1f, () => { value++; });
            yield return new WaitForSeconds(1.1f);
            Assert.AreEqual(1, value);
            schedule.Dispose();
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(1, value);

            int value2 = 0;
            IDisposable schedule2 = _scheduler.Schedule(1f, () => { value2++; });
            schedule2.Dispose();
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(value2, 0);
        }

        [UnityTest]
        public IEnumerator TestScheduleOnce()
        {
            int value = 0;
            _scheduler.ScheduleOnce(1f, () => { value++; });
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(0, value);
            yield return new WaitForSeconds(0.6f);
            Assert.AreEqual(1, value);
            yield return new WaitForSeconds(1.1f);
            Assert.AreEqual(1, value);
        }

        [UnityTest]
        public IEnumerator TestScheduleOnceDispose()
        {
            int value = 0;
            IDisposable schedule = _scheduler.ScheduleOnce(1f, () => { value++; });
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(0, value);
            schedule.Dispose();
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(0, value);

            int value2 = 0;
            IDisposable schedule2 = _scheduler.ScheduleOnce(1f, () => { value2++; });
            schedule2.Dispose();
            yield return new WaitForSeconds(1.5f);
            Assert.AreEqual(0, value2);
        }

        [UnityTest]
        public IEnumerator TestScheduleEveryFrame()
        {
            int value = 0;
            int startFrame = Time.frameCount;
            _scheduler.ScheduleEveryFrame(() => { value++; });
            yield return new WaitForSeconds(1f);
            int framesPassed = Time.frameCount - startFrame;
            Assert.IsTrue(value == framesPassed || value == framesPassed - 1 || value == framesPassed + 1);
        }

        [UnityTest]
        public IEnumerator TestScheduleEveryFrameDispose()
        {
            int value = 0;
            int startFrame = Time.frameCount;
            IDisposable schedule = _scheduler.ScheduleEveryFrame(() => { value++; });
            yield return new WaitForSeconds(1f);
            int framesPassed = Time.frameCount - startFrame;
            Assert.IsTrue(value == framesPassed || value == framesPassed - 1 || value == framesPassed + 1);
            schedule.Dispose();
            yield return new WaitForSeconds(1f);
            Assert.IsTrue(value == framesPassed || value == framesPassed - 1 || value == framesPassed + 1);

            int value2 = 0;
            IDisposable schedule2 = _scheduler.ScheduleEveryFrame(() => { value2++; });
            schedule2.Dispose();
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(0, value2);
        }
    }
}