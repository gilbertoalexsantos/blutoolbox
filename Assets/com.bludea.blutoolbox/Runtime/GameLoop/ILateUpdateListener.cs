namespace BluToolbox
{
  public interface ILateUpdateListener : IGameLoopListener
  {
    void OnLateUpdate(float deltaTime);
  }
}
