using UnityEngine;

namespace BluToolbox
{
  public static class RectExtensions
  {
    public static Rect Shrink(this Rect r, float left = 0f, float top = 0f, float right = 0f, float bottom = 0f)
    {
      float xMin = r.xMin + left;
      float xMax = r.xMax + right;
      float yMin = r.yMin + top;
      float yMax = r.yMax + bottom;
      return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }
  }
}