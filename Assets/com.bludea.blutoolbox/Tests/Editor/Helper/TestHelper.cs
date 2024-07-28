namespace BluToolbox.Tests
{
  public static class TestHelper
  {
    public static int WaitSeconds(float seconds, TestGameLoop gameLoop)
    {
      int frameCount = 0;
      float deltaTime = 1f / 60f;
      for (float time = 0; time <= seconds; time += deltaTime)
      {
        gameLoop.Update(deltaTime);
        frameCount++;
      }
      return frameCount;
    }
  }
}