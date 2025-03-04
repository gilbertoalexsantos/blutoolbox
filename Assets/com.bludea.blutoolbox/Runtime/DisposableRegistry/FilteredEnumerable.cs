using System;
using System.Collections;
using System.Collections.Generic;

namespace BluToolbox
{
  public class FilteredEnumerable<T> : IEnumerable<T>
  {
    private readonly List<DisposableHolder> _list;

    public FilteredEnumerable(List<DisposableHolder> list)
    {
      _list = list ?? throw new ArgumentNullException(nameof(list));
    }

    public IEnumerator<T> GetEnumerator()
    {
      DisposableHolder[] snapshot = _list.ToArray();
      for (int i = 0; i < snapshot.Length; i++)
      {
        DisposableHolder holder = snapshot[i];
        if (holder.Obj is T item)
        {
          yield return item;
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
