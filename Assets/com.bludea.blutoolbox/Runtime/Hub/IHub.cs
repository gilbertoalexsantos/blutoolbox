using System;

namespace BluToolbox
{
  public interface IHub
  {
    IHubEventDisposable Register<T>(Action<T> cb) where T : IHubEvent;
    void Call<T>(T hubEvent) where T : IHubEvent;
  }
}