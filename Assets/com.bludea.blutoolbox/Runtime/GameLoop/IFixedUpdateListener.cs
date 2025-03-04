namespace BluToolbox
{
  public interface IFixedUpdateListener : IGameLoopListener
  {
    void OnFixedUpdate(float fixedDeltaTime);
  }
}
