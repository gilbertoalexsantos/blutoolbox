using System;

namespace BluToolbox.Injector
{
  public class InjectorException : Exception
  {
    public InjectorException(string message) : base(message)
    {
    }
  }
}