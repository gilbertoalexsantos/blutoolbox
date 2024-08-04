using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public class DisposableTracker : IDisposable
  {
    private readonly HashSet<IDisposable> _disposables = new();

    public void Add(IDisposable disposable)
    {
      _disposables.Add(disposable);
    }

    public void Remove(IDisposable disposable)
    {
      _disposables.Remove(disposable);
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