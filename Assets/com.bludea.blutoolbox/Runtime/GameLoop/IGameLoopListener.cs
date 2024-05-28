namespace BluToolbox
{
  public interface IGameLoopListener
  {
    void OnUpdate();
    void OnLateUpdate();
    void OnFixedUpdate();
  }
}