using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public class DisposableRegistry<T> : IEnumerable<T>, IDisposable
  {
    private readonly HashSet<DisposableHolder<T>> _holders = new();

    public IDisposable Register(T obj)
    {
      DisposableHolder<T> holder = new(obj, toRemove =>
      {
        _holders.Remove(toRemove);
      });
      _holders.Add(holder);
      return holder;
    }

    public void Dispose()
    {
      foreach (DisposableHolder<T> holder in _holders.ToList())
      {
        holder.Dispose();
      }

      _holders.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
      return _holders.Select(holder => holder.Obj).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}