namespace BluToolbox
{
  public class SystemRandom : IRandom
  {
    private readonly System.Random _random = new();
    
    public int IntRange(int min, int max)
    {
      return _random.Next(min, max);
    }

    public float FloatRange(float min, float max)
    {
      return (float) (_random.NextDouble() * (max - min) + min);
    }
  }
}