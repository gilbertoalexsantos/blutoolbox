namespace BluToolbox
{
  public interface ISyncDatasource<out T>
  {
    T LoadSync();
  }
}