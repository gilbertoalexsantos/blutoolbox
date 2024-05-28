using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public class HardReloadManager : IHardReloadManager
  {
    private readonly HashSet<HardReloadDisposable> _handlers = new();

    public void HardReload()
    {
      foreach (HardReloadDisposable handler in _handlers.ToList())
      {
        handler.Obj.OnHardReload();
      }
      _handlers.Clear();
    }

    public IHardReloadDisposable Register(IHardReload obj)
    {
      HardReloadDisposable disposable = new(obj, Remove);
      _handlers.Add(disposable);
      return disposable;
    }

    private void Remove(HardReloadDisposable disposable)
    {
      _handlers.Remove(disposable);
    }
  }
}