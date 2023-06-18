namespace BluToolbox
{
  public interface IHardReloadManager
  {
    void HardReload();
    void AddOnHardReload(IHardReload obj);
    void RemoveOnHardReload(IHardReload obj);
  }
}