using System;

namespace BluToolbox
{
  public interface ITween : IDisposable
  {
    bool IsCompleted { get; }
    void SkipToEnd();
  }
}
