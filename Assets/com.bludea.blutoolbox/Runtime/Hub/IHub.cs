using System;

namespace BluToolbox
{
  public interface IHub
  {
    IHubEventDisposable Register<T>(Action<T> action) where T : IHubEvent;
    void Call<T>(T hubEvent) where T : IHubEvent;
  }
}