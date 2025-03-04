namespace BluToolbox
{
  public interface IUpdateListener : IGameLoopListener
  {
    void OnUpdate(float deltaTime);
  }
}
