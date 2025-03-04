using System;
using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public class DisposableRegistry : IDisposable
  {
    private readonly List<DisposableHolder> _holders = new();

    public IDisposable Register(object obj)
    {
      DisposableHolder holder = new(obj, toRemove =>
      {
        _holders.Remove(toRemove);
      });
      _holders.Add(holder);
      return holder;
    }

    public void Dispose()
    {
      foreach (DisposableHolder holder in _holders.ToList())
      {
        holder.Dispose();
      }

      _holders.Clear();
    }

    public IEnumerable<T> Enumerate<T>()
    {
      return new FilteredEnumerable<T>(_holders);
    }
  }
}
