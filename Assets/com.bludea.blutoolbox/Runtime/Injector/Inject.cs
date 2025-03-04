using System;

namespace BluToolbox.Injector
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
  public class Inject : Attribute
  {
  }
}