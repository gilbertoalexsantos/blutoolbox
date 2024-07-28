using BluToolbox.Injector;
using NUnit.Framework;

namespace BluToolbox.Tests
{
  public class InjectorTest
  {
    private class MyClass
    {
    }

    private class MyClassWithDependency
    {
      public MyClass Dependency { get; private set; }

      public MyClassWithDependency(MyClass dependency)
      {
        Dependency = dependency;
      }
    }

    private class CircularDependencyA
    {
      public CircularDependencyB B { get; private set; }

      public CircularDependencyA(CircularDependencyB b)
      {
        B = b;
      }
    }

    private class CircularDependencyB
    {
      public CircularDependencyA A { get; private set; }

      public CircularDependencyB(CircularDependencyA a)
      {
        A = a;
      }
    }

    private interface IMyInterface
    {
    }

    private class MyImplementation : IMyInterface
    {
    }

    [Test]
    public void TestResolveSimpleBinding()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsTransient();
      MyClass instance = binder.Resolve<MyClass>();
      Assert.IsNotNull(instance);
      Assert.IsInstanceOf<MyClass>(instance);
    }

    [Test]
    public void TestSingletonBinding()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsSingleton();
      MyClass instance1 = binder.Resolve<MyClass>();
      MyClass instance2 = binder.Resolve<MyClass>();
      Assert.AreSame(instance1, instance2);
    }

    [Test]
    public void TestTransientBinding()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsTransient();
      MyClass instance1 = binder.Resolve<MyClass>();
      MyClass instance2 = binder.Resolve<MyClass>();
      Assert.AreNotSame(instance1, instance2);
    }

    [Test]
    public void TestResolveWithDependency()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsTransient();
      binder.Bind<MyClassWithDependency>().AsTransient();
      MyClassWithDependency instance = binder.Resolve<MyClassWithDependency>();
      Assert.IsNotNull(instance);
      Assert.IsNotNull(instance.Dependency);
      Assert.IsInstanceOf<MyClass>(instance.Dependency);
    }

    [Test]
    public void TestAsValue()
    {
      InjectorBinder binder = new();
      MyClass myClass = new();
      binder.Bind<MyClass>().AsValue(myClass);
      MyClass resolvedInstance = binder.Resolve<MyClass>();
      Assert.AreSame(myClass, resolvedInstance);
    }

    [Test]
    public void TestCircularDependency()
    {
      InjectorBinder binder = new();
      binder.Bind<CircularDependencyA>().AsTransient();
      binder.Bind<CircularDependencyB>().AsTransient();

      Assert.Throws<InjectorException>(() => binder.Resolve<CircularDependencyA>(), "Circular dependency detected");
    }

    [Test]
    public void TestMissingBinding()
    {
      InjectorBinder binder = new();
      Assert.Throws<InjectorException>(() => binder.Resolve<MyClass>(), "No binding found for type");
    }

    [Test]
    public void TestMultipleImplementations()
    {
      InjectorBinder binder = new();
      binder.Bind<IMyInterface>().To<MyImplementation>().AsTransient();
      IMyInterface instance = binder.Resolve<IMyInterface>();
      Assert.IsNotNull(instance);
      Assert.IsInstanceOf<MyImplementation>(instance);
    }

    [Test]
    public void TestSingletonResolution()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsSingleton();
      MyClass instance1 = binder.Resolve<MyClass>();
      MyClass instance2 = binder.Resolve<MyClass>();
      Assert.AreSame(instance1, instance2);
    }

    [Test]
    public void TestInterfaceBinding()
    {
      InjectorBinder binder = new();
      binder.Bind<IMyInterface>().To<MyImplementation>().AsTransient();
      IMyInterface instance = binder.Resolve<IMyInterface>();
      Assert.IsNotNull(instance);
      Assert.IsInstanceOf<MyImplementation>(instance);
    }

    [Test]
    public void TestResolvingUnboundType()
    {
      InjectorBinder binder = new();
      Assert.Throws<InjectorException>(() => binder.Resolve<MyClass>(), "No binding found for type");
    }

    [Test]
    public void TestRecursiveDependencyResolution()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsSingleton();
      binder.Bind<MyClassWithDependency>().AsTransient();
      MyClassWithDependency instance1 = binder.Resolve<MyClassWithDependency>();
      MyClassWithDependency instance2 = binder.Resolve<MyClassWithDependency>();
      Assert.AreNotSame(instance1, instance2);
      Assert.AreSame(instance1.Dependency, instance2.Dependency);
    }
  }
}