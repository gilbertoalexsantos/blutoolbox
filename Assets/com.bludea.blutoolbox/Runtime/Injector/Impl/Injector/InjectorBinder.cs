using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BluToolbox.Injector
{
  public class InjectorBinder : IInjectorBinder
  {
    private class Binding
    {
      public Type BindType;
      public bool IsSingleton;
    }

    private readonly Dictionary<Type, Binding> _bindings = new();
    private readonly HashSet<Type> resolvingTypes = new();
    private readonly Dictionary<Type, object> _singletonInstances = new();
    private Maybe<Type> _currentBindType;

    public IInjectorBinder Bind<T>()
    {
      if (_currentBindType.HasValue)
      {
        throw new InjectorException("Cannot bind a type while another type is being bound");
      }

      Type currentBindType = typeof(T);

      if (_bindings.ContainsKey(currentBindType))
      {
        throw new InjectorException($"Type {currentBindType.FullName} is already bound");
      }

      _bindings[currentBindType] = new Binding
      {
        BindType = currentBindType
      };

      _currentBindType = currentBindType;

      return this;
    }

    public IInjectorBinder To<T>()
    {
      if (!_currentBindType.TryGetValue(out Type currentBindType))
      {
        throw new InjectorException("Cannot set the bind type while no type is being bound");
      }

      _bindings[currentBindType].BindType = typeof(T);

      return this;
    }

    public T Resolve<T>()
    {
      return (T) Resolve(typeof(T));
    }

    public T Create<T>()
    {
      return (T) CreateInstance(typeof(T));
    }

    public T Inject<T>(T obj)
    {
      Type type = obj.GetType();
      BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

      var injectMethods = type.GetMethods(flags)
        .Where(m => m.GetCustomAttribute<Inject>() != null);
      foreach (MethodInfo injectMethod in injectMethods)
      {
        ParameterInfo[] parameters = injectMethod.GetParameters();
        object[] parameterInstances = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
        injectMethod.Invoke(obj, parameterInstances);
      }

      var injectFields = type.GetFields(flags)
        .Where(f => f.GetCustomAttribute<Inject>() != null);
      foreach (FieldInfo injectField in injectFields)
      {
        object fieldValue = Resolve(injectField.FieldType);
        injectField.SetValue(obj, fieldValue);
      }

      var injectProperties = type.GetProperties(flags)
        .Where(p => p.GetCustomAttribute<Inject>() != null && p.CanWrite);
      foreach (PropertyInfo injectProperty in injectProperties)
      {
        object propertyValue = Resolve(injectProperty.PropertyType);
        injectProperty.SetValue(obj, propertyValue);
      }

      return obj;
    }

    public void AsValue<T>(T value)
    {
      if (!_currentBindType.TryGetValue(out Type currentBindType))
      {
        throw new InjectorException("Cannot set the bind value while no type is being bound");
      }

      if (!_bindings.TryGetValue(currentBindType, out Binding binding))
      {
        throw new InjectorException($"No binding found for type {currentBindType.FullName}");
      }

      if (!ReflectionUtils.ImplementsOrInherits(typeof(T), binding.BindType))
      {
        throw new InjectorException(
          $"Cannot bind value of type {typeof(T).FullName} to type {currentBindType.FullName}");
      }

      _singletonInstances[binding.BindType] = value;
      binding.IsSingleton = true;

      _currentBindType = Maybe.None<Type>();
    }

    public void AsSingleton()
    {
      if (!_currentBindType.TryGetValue(out Type currentBindType))
      {
        throw new InjectorException("Cannot set the bind value while no type is being bound");
      }

      if (!_bindings.TryGetValue(currentBindType, out Binding binding))
      {
        throw new InjectorException($"No binding found for type {currentBindType.FullName}");
      }

      binding.IsSingleton = true;

      _currentBindType = Maybe.None<Type>();
    }

    public void AsTransient()
    {
      if (!_currentBindType.TryGetValue(out Type currentBindType))
      {
        throw new InjectorException("Cannot set the bind value while no type is being bound");
      }

      if (!_bindings.TryGetValue(currentBindType, out Binding binding))
      {
        throw new InjectorException($"No binding found for type {currentBindType.FullName}");
      }

      binding.IsSingleton = false;

      _currentBindType = Maybe.None<Type>();
    }

    private object Resolve(Type targetType)
    {
      if (resolvingTypes.Contains(targetType))
      {
        throw new InjectorException($"Circular dependency detected for type {targetType.FullName}");
      }

      if (!_bindings.TryGetValue(targetType, out Binding binding))
      {
        throw new InjectorException($"No binding found for type {targetType.FullName}");
      }

      if (binding.IsSingleton && _singletonInstances.TryGetValue(binding.BindType, out object value))
      {
        return value;
      }

      resolvingTypes.Add(targetType);
      object instance = CreateInstance(binding.BindType);
      resolvingTypes.Remove(targetType);

      if (binding.IsSingleton)
      {
        _singletonInstances[binding.BindType] = instance;
      }

      return instance;
    }

    private object CreateInstance(Type type)
    {
      ConstructorInfo constructor = GetCreateInstanceConstructor(type);

      ParameterInfo[] parameters = constructor.GetParameters();
      object[] parameterInstances = new object[parameters.Length];
      for (int i = 0; i < parameters.Length; i++)
      {
        parameterInstances[i] = Resolve(parameters[i].ParameterType);
      }

      return Inject(Activator.CreateInstance(type, parameterInstances));
    }

    private ConstructorInfo GetCreateInstanceConstructor(Type type)
    {
      ConstructorInfo[] constructors = type.GetConstructors();
      if (constructors.Length == 0)
      {
        throw new InjectorException($"No public constructors found for type {type.FullName}");
      }

      ConstructorInfo constructorInfo = constructors.FirstOrDefault(c => c.GetCustomAttribute<Construct>() != null);
      if (constructorInfo != null)
      {
        return constructorInfo;
      }

      constructorInfo = constructors
        .OrderByDescending(c => c.GetParameters().Length)
        .First();

      return constructorInfo;
    }
  }
}
