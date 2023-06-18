using System.Collections.Generic;

namespace BluToolbox
{
  public interface IAsyncDatasource<out T>
  {
    IEnumerator<T> LoadAsync();
  }
}