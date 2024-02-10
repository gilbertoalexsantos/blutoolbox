using System.Threading.Tasks;

namespace BluToolbox
{
  public class AsyncDatasourceAdapter<T> : IAsyncDatasource<T>
  {
    private readonly ISyncDatasource<T> _datasource;

    public AsyncDatasourceAdapter(ISyncDatasource<T> datasource)
    {
      _datasource = datasource;
    }

    public Task<T> LoadAsync()
    {
      return Task.FromResult(_datasource.LoadSync());
    }
  }
}