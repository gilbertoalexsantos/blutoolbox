using System;

namespace BluToolbox
{
  public interface IHub : IDisposable, IHardReload
  {
    IHubEventDisposable Register<T>(Action<T> cb) where T : IHubEvent;
    void Call<T>(T hubEvent) where T : IHubEvent;
  }
}