using UnityEngine;

namespace BluToolbox
{
  public static class BoundsExtensions
  {
    public static bool IsTargetInside(this Bounds source, Bounds target)
    {
      return source.Contains(target.min) && source.Contains(target.max);
    }

    public static float MinDistanceTo(this Bounds bounds, Vector3 point)
    {
      return Vector3.Distance(bounds.ClosestPoint(point), point);
    }
  }
}