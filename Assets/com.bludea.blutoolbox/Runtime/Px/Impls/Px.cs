using UnityEngine;

namespace BluToolbox
{
  public class Px : IPx
  {
    private readonly IRandom _random;

    public Px(IRandom random)
    {
      _random = random;
    }

    public Vector3 GetRandomDirection()
    {
      // Generate a random angle between 0 and 2π
      float angle = _random.FloatRange(0f, Mathf.PI * 2);

      // Generate a random radius with uniform distribution
      float radius = Mathf.Sqrt(_random.FloatRange(0f, 1f));

      // Convert polar coordinates to Cartesian coordinates
      float x = radius * Mathf.Cos(angle);
      float y = radius * Mathf.Sin(angle);

      Vector2 v = new Vector2(x, y).normalized;
      return v == Vector2.zero ? Vector2.up : v;
    }
  }
}