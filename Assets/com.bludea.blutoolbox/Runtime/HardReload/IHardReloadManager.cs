namespace BluToolbox
{
  public interface IHardReloadManager
  {
    void HardReload();
    IHardReloadDisposable Register(IHardReload obj);
  }
}