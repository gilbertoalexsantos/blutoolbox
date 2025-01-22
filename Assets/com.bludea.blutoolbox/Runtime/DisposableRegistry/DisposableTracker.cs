using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public class DisposableTracker : IDisposable
  {
    private readonly HashSet<IDisposable> _disposables = new();

    public void Register(params IDisposable[] disposables)
    {
      foreach (IDisposable disposable in disposables)
      {
        _disposables.Add(disposable);
      }
    }

    public void Dispose()
    {
      foreach (IDisposable disposable in _disposables)
      {
        disposable.Dispose();
      }

      _disposables.Clear();
    }
  }
}
