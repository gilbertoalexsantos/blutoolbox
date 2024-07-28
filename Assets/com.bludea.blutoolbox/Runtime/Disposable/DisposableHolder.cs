using System;

namespace BluToolbox
{
  public class DisposableHolder<T> : IDisposable
  {
    private readonly Action<DisposableHolder<T>> _onDispose;

    public T Obj { get; }

    public DisposableHolder(T obj, Action<DisposableHolder<T>> onDispose)
    {
      Obj = obj;
      _onDispose = onDispose;
    }

    public void Dispose()
    {
      _onDispose(this);
    }
  }
}