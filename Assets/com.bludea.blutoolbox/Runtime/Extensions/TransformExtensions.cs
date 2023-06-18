using UnityEngine;

namespace BluToolbox
{
  public static class TransformExtensions
  {
    public static Transform GetChildWithName(this Transform t, string name)
    {
      foreach (Transform child in t.GetComponentsInChildren<Transform>(true))
      {
        if (child.name == name)
        {
          return child;
        }
      }

      return null;
    }

    public static void SetPositionX(this Transform t, float x)
    {
      t.position = new Vector3(x, t.position.y, t.position.z);
    }

    public static void SetPositionY(this Transform t, float y)
    {
      t.position = new Vector3(t.position.x, y, t.position.z);
    }

    public static void SetPositionZ(this Transform t, float z)
    {
      t.position = new Vector3(t.position.x, t.position.y, z);
    }

    public static void SetLocalPositionX(this Transform t, float x)
    {
      t.localPosition = new Vector3(x, t.localPosition.y, t.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform t, float y)
    {
      t.localPosition = new Vector3(t.localPosition.x, y, t.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform t, float z)
    {
      t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, z);
    }

    public static void SetScaleX(this Transform t, float x)
    {
      t.localScale = new Vector3(x, t.localScale.y, t.localScale.z);
    }

    public static void SetScaleY(this Transform t, float y)
    {
      t.localScale = new Vector3(t.localScale.x, y, t.localScale.z);
    }

    public static void SetScaleZ(this Transform t, float z)
    {
      t.localScale = new Vector3(t.localScale.x, t.localScale.y, z);
    }
  }
}