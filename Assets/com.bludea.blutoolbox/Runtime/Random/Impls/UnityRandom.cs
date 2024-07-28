namespace BluToolbox
{
  public class UnityRandom : IRandom
  {
    public int IntRange(int min, int max)
    {
      return UnityEngine.Random.Range(min, max);
    }

    public float FloatRange(float min, float max)
    {
      return UnityEngine.Random.Range(min, max);
    }
  }
}