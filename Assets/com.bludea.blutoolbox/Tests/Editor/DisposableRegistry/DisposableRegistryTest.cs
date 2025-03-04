using System;
using System.Linq;
using NUnit.Framework;

namespace BluToolbox.Tests
{
  public class DisposableRegistryTest
  {
    private class TestDisposable : IDisposable
    {
      public bool IsDisposed { get; private set; }

      public void Dispose()
      {
        IsDisposed = true;
      }
    }

    private class AnotherDisposable : IDisposable
    {
      public bool IsDisposed { get; private set; }

      public void Dispose()
      {
        IsDisposed = true;
      }
    }

    [Test]
    public void TestDisposeWithSuccess()
    {
      DisposableRegistry disposableRegistry = new();

      TestDisposable t1 = new();
      TestDisposable t2 = new();

      IDisposable t1Disposable = disposableRegistry.Register(t1);
      IDisposable t2Disposable = disposableRegistry.Register(t2);

      Assert.Contains(t1, disposableRegistry.Enumerate<TestDisposable>().ToList());
      Assert.Contains(t2, disposableRegistry.Enumerate<TestDisposable>().ToList());
      Assert.AreEqual(2, disposableRegistry.Enumerate<TestDisposable>().Count());

      t1Disposable.Dispose();

      CollectionAssert.DoesNotContain(disposableRegistry.Enumerate<TestDisposable>().ToList(), t1);
      Assert.Contains(t2, disposableRegistry.Enumerate<TestDisposable>().ToList());
      Assert.AreEqual(1, disposableRegistry.Enumerate<TestDisposable>().Count());

      t2Disposable.Dispose();

      CollectionAssert.DoesNotContain(disposableRegistry.Enumerate<TestDisposable>().ToList(), t1);
      CollectionAssert.DoesNotContain(disposableRegistry.Enumerate<TestDisposable>().ToList(), t2);
      Assert.AreEqual(0, disposableRegistry.Enumerate<TestDisposable>().Count());
    }

    [Test]
    public void TestDisposeWhileEnumerating()
    {
      DisposableRegistry disposableRegistry = new();

      TestDisposable t1 = new();
      TestDisposable t2 = new();
      TestDisposable t3 = new();
      TestDisposable t4 = new();

      disposableRegistry.Register(t1);
      IDisposable t2Disposable = disposableRegistry.Register(t2);
      disposableRegistry.Register(t3);
      disposableRegistry.Register(t4);

      int idx = 0;
      bool hasSeenT1 = false;
      bool hasSeenT2 = false;
      bool hasSeenT3 = false;
      bool hasSeenT4 = false;
      foreach (TestDisposable testDisposable in disposableRegistry.Enumerate<TestDisposable>())
      {
        if (t1 == testDisposable)
        {
          hasSeenT1 = !hasSeenT1;
          t2Disposable.Dispose();
        }
        else if (t2 == testDisposable)
        {
          hasSeenT2 = !hasSeenT2;
        }
        else if (t3 == testDisposable)
        {
          hasSeenT3 = !hasSeenT3;
        }
        else if (t4 == testDisposable)
        {
          hasSeenT4 = !hasSeenT4;
        }

        idx++;
      }

      Assert.IsTrue(hasSeenT1);
      Assert.IsTrue(hasSeenT2);
      Assert.IsTrue(hasSeenT3);
      Assert.IsTrue(hasSeenT4);
      Assert.AreEqual(4, idx);
    }

    [Test]
    public void TestRegisteringNull()
    {
      DisposableRegistry disposableRegistry = new();
      Assert.DoesNotThrow(() => disposableRegistry.Register(null));
      Assert.AreEqual(0, disposableRegistry.Enumerate<object>().Count());
    }

    [Test]
    public void TestDisposeRegistry()
    {
      DisposableRegistry disposableRegistry = new();

      TestDisposable t1 = new();
      TestDisposable t2 = new();

      disposableRegistry.Register(t1);
      disposableRegistry.Register(t2);

      disposableRegistry.Dispose();

      Assert.AreEqual(0, disposableRegistry.Enumerate<TestDisposable>().Count());
    }

    [Test]
    public void TestRegisteringAndDisposingMultipleTypes()
    {
      DisposableRegistry disposableRegistry = new();

      TestDisposable t1 = new();
      AnotherDisposable a1 = new();

      IDisposable t1Disposable = disposableRegistry.Register(t1);
      IDisposable a1Disposable = disposableRegistry.Register(a1);

      Assert.AreEqual(1, disposableRegistry.Enumerate<TestDisposable>().Count());
      Assert.AreEqual(1, disposableRegistry.Enumerate<AnotherDisposable>().Count());

      t1Disposable.Dispose();

      Assert.AreEqual(0, disposableRegistry.Enumerate<TestDisposable>().Count());
      Assert.AreEqual(1, disposableRegistry.Enumerate<AnotherDisposable>().Count());

      a1Disposable.Dispose();

      Assert.AreEqual(0, disposableRegistry.Enumerate<TestDisposable>().Count());
      Assert.AreEqual(0, disposableRegistry.Enumerate<AnotherDisposable>().Count());
    }

    [Test]
    public void TestDisposingAlreadyDisposedObject()
    {
      DisposableRegistry disposableRegistry = new();

      TestDisposable t1 = new();
      IDisposable t1Disposable = disposableRegistry.Register(t1);

      t1Disposable.Dispose();
      Assert.DoesNotThrow(() => t1Disposable.Dispose());

      Assert.AreEqual(0, disposableRegistry.Enumerate<TestDisposable>().Count());
    }

    [Test]
    public void TestEnumerateOnEmptyRegistry()
    {
      DisposableRegistry disposableRegistry = new();

      Assert.AreEqual(0, disposableRegistry.Enumerate<TestDisposable>().Count());
    }
  }
}
