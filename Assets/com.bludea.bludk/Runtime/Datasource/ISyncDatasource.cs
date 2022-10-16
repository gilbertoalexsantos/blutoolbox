namespace Bludk
{
    public interface ISyncDatasource<out T>
    {
        T LoadSync();
    }
}