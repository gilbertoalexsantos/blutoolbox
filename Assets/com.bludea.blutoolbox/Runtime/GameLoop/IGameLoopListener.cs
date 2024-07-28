namespace BluToolbox
{
  public interface IGameLoopListener
  {
    void OnUpdate(float deltaTime);
    void OnLateUpdate(float deltaTime);
    void OnFixedUpdate(float fixedDeltaTime);
  }
}