using System;

namespace BluToolbox
{
  public class DisposableHolder : IDisposable
  {
    private readonly Action<DisposableHolder> _onDispose;

    public object Obj { get; }

    public DisposableHolder(object obj, Action<DisposableHolder> onDispose)
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
