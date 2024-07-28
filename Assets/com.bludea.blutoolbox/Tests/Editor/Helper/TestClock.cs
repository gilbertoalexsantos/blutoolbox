namespace BluToolbox.Tests
{
  public class TestClock : IClock
  {
    public float SecondsSinceStartup { get; set; }
    public int FrameCount { get; set; }
  }
}