using System;

namespace BluToolbox
{
  public interface IHub : IDisposable
  {
    IHubEventDisposable Register<T>(Action<T> cb) where T : IHubEvent;
    void Call<T>(T hubEvent) where T : IHubEvent;
  }
}