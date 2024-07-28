using UnityEngine;

namespace BluToolbox
{
  public static class VectorExtensions
  {
    public static Vector2 ToXY(this Vector3 v)
    {
      return new Vector2(v.x, v.y);
    }

    public static Vector3 DropX(this Vector3 v)
    {
      return new Vector3(0f, v.y, v.z);
    }

    public static Vector3 DropY(this Vector3 v)
    {
      return new Vector3(v.x, 0f, v.z);
    }

    public static Vector3 DropZ(this Vector3 v)
    {
      return new Vector3(v.x, v.y, 0f);
    }

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
      float radians = degrees * Mathf.Deg2Rad;
      float sin = Mathf.Sin(radians);
      float cos = Mathf.Cos(radians);
      return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }
  }
}