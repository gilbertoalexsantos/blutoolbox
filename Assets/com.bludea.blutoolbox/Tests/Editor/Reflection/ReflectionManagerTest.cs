using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace BluToolbox.Tests
{
  public class ReflectionManagerTest
  {
    private class ZeroConstructor
    {
    }

    private class OneConstructorAndZeroParameter
    {
      public OneConstructorAndZeroParameter()
      {
      }
    }

    private class OneConstructorAndOneParameter
    {
      public OneConstructorAndOneParameter(int foo)
      {
      }
    }
    
    private class TwoConstructorZeroParameterAndOneParameter
    {
      public TwoConstructorZeroParameterAndOneParameter()
      {
      }
      
      public TwoConstructorZeroParameterAndOneParameter(string foo)
      {
      }

      private TwoConstructorZeroParameterAndOneParameter(int foo)
      {
        
      }
    }

    [Test]
    public void TestGetConstructors()
    {
      ReflectionManager reflectionManager = new();

      List<ConstructorInfo> constructors = reflectionManager.GetPublicConstructors(typeof(OneConstructorAndZeroParameter));
      Assert.AreEqual(1, constructors.Count);
      ConstructorInfo constructorInfo = constructors[0];
      Assert.AreEqual(constructorInfo.GetParameters().Length, 0);

      constructors = reflectionManager.GetPublicConstructors(typeof(OneConstructorAndOneParameter));
      Assert.AreEqual(1, constructors.Count);
      constructorInfo = constructors[0];
      Assert.AreEqual(constructorInfo.GetParameters().Length, 1);
      Assert.AreEqual(constructorInfo.GetParameters()[0].ParameterType, typeof(int));

      constructors = reflectionManager.GetPublicConstructors(typeof(TwoConstructorZeroParameterAndOneParameter));
      Assert.AreEqual(2, constructors.Count);
      constructorInfo = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);
      Assert.IsNotNull(constructorInfo);
      constructorInfo = constructors.FirstOrDefault(c => c.GetParameters().Length == 1);
      Assert.IsNotNull(constructorInfo);
      Assert.AreEqual(constructorInfo.GetParameters()[0].ParameterType, typeof(string));
      
      constructors = reflectionManager.GetPublicConstructors(typeof(ZeroConstructor));
      Assert.AreEqual(1, constructors.Count);
      constructorInfo = constructors[0];
      Assert.AreEqual(constructorInfo.GetParameters().Length, 0);
    }
  }
}