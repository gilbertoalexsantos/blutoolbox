using System;

namespace BluToolbox
{
  public class HardReloadDisposable : IHardReloadDisposable
  {
    private readonly Action<HardReloadDisposable> _onDispose;

    public IHardReload Obj { get; }

    public HardReloadDisposable(IHardReload obj, Action<HardReloadDisposable> onDispose)
    {
      Obj = obj;
      _onDispose = onDispose;
    }

    public void Dispose()
    {
      _onDispose?.Invoke(this);
    }
  }
}