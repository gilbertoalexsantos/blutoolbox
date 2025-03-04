using System;

namespace BluToolbox
{
  public static class ReflectionUtils
  {
    public static bool ImplementsOrInherits(Type t, Type u)
    {
      return u.IsAssignableFrom(t);
    }
  }
}
