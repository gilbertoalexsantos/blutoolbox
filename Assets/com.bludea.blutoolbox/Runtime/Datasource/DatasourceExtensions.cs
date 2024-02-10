namespace BluToolbox
{
  public static class DatasourceExtensions
  {
    public static IAsyncDatasource<T> ToAsyncDatasource<T>(this ISyncDatasource<T> datasource)
    {
      return new AsyncDatasourceAdapter<T>(datasource);
    }
  }
}