namespace BluToolbox.Tests
{
  public static class TestHelper
  {
    public static void WaitSeconds(float seconds, TestGameLoop gameLoop, TestClock clock)
    {
      float deltaTime = 1f / 60f;
      float start = clock.SecondsSinceStartup;
      for (float time = start; time <= start + seconds; time += deltaTime)
      {
        clock.SecondsSinceStartup += deltaTime;
        clock.FrameCount++;
        gameLoop.Update();
      }
    }
  }
}