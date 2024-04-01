using System;

namespace BluToolbox
{
  public class HubEventDisposable : IHubEventDisposable
  {
    private readonly Action<HubEventDisposable> _onDispose;

    public object Callback { get; }

    public HubEventDisposable(object callback, Action<HubEventDisposable> onDispose)
    {
      Callback = callback;
      _onDispose = onDispose;
    }

    public void Dispose()
    {
      _onDispose?.Invoke(this);
    }
  }
}