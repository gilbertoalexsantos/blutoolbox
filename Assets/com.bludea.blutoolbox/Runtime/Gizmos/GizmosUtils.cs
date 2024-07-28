using UnityEngine;

namespace BluToolbox
{
  public static class GizmosUtils
  {
    public static void DrawCircle(Vector3 position, float radius, Color color)
    {
      Color oldColor = Gizmos.color;
      Gizmos.color = color;
      Gizmos.DrawWireSphere(position, radius);
      Gizmos.color = oldColor;
    }
  }
}