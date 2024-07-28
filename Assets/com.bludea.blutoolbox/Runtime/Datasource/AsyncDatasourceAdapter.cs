using UnityEngine;

namespace BluToolbox
{
  public class AsyncDatasourceAdapter<T> : IAsyncDatasource<T>
  {
    private readonly ISyncDatasource<T> _datasource;

    public AsyncDatasourceAdapter(ISyncDatasource<T> datasource)
    {
      _datasource = datasource;
    }

    public async Awaitable<T> LoadAsync()
    {
      return await _datasource.LoadSync().FromResult();
    }
  }
}