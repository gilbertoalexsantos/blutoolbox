using System.Threading.Tasks;

namespace BluToolbox
{
  public interface IAsyncDatasource<T>
  {
    Task<T> LoadAsync();
  }
}