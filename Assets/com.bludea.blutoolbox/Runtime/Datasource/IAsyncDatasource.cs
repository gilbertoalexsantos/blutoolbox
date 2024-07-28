using UnityEngine;

namespace BluToolbox
{
  public interface IAsyncDatasource<T>
  {
    Awaitable<T> LoadAsync();
  }
}