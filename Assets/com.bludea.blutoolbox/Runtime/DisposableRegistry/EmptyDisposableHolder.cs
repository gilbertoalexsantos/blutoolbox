using System;

namespace BluToolbox
{
  public class EmptyDisposableHolder : IDisposable
  {
    private readonly Action _onDispose;

    public EmptyDisposableHolder(Action onDispose)
    {
      _onDispose = onDispose;
    }

    public void Dispose()
    {
      _onDispose();
    }
  }
}
