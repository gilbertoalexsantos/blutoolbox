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

    private class MyClassWithInjectMethod
    {
      public MyClass Dependency { get; private set; }

      [Inject]
      public void InjectDependency(MyClass dependency)
      {
        Dependency = dependency;
      }
    }

    private interface IInterface1
    {
    }

    private interface IInterface2
    {
    }

    private class MyClassWithTwoInterfaces : IInterface1, IInterface2
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
    public void TestAsValueDifferentType()
    {
      InjectorBinder binder = new();
      MyImplementation impl = new();
      binder.Bind<IMyInterface>().AsValue(impl);
      IMyInterface resolvedInstance = binder.Resolve<IMyInterface>();
      Assert.AreSame(impl, resolvedInstance);
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

    [Test]
    public void TestInjectMethod()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsTransient();
      MyClassWithInjectMethod obj = new();
      binder.Inject(obj);
      Assert.IsNotNull(obj.Dependency);
      Assert.IsInstanceOf<MyClass>(obj.Dependency);
    }

    [Test]
    public void TestInjectMethodWithMultipleParameters()
    {
      InjectorBinder binder = new();
      binder.Bind<MyClass>().AsTransient();
      binder.Bind<MyClassWithDependency>().AsTransient();

      var obj = new MyClassWithInjectMethod();

      binder.Inject(obj);

      Assert.IsNotNull(obj.Dependency);
      Assert.IsInstanceOf<MyClass>(obj.Dependency);
    }

    [Test]
    public void TestInjectTwoInterfacesToSameSingleton()
    {
      InjectorBinder binder = new();
      binder.Bind<IInterface1>().To<MyClassWithTwoInterfaces>().AsSingleton();
      binder.Bind<IInterface2>().To<MyClassWithTwoInterfaces>().AsSingleton();

      var obj1 = binder.Resolve<IInterface1>();
      var obj2 = binder.Resolve<IInterface2>();
      Assert.AreSame(obj1, obj2);
    }
  }
}
