using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BluToolbox
{
  public class ReflectionManager
  {
    public List<ConstructorInfo> GetPublicConstructors(Type type)
    {
      return type.GetConstructors().ToList();
    }
  }
}