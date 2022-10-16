using System.Collections.Generic;

namespace Bludk
{
    public interface IAsyncDatasource<out T>
    {
        IEnumerator<T> LoadAsync();
    }
}