using UnityEngine;

namespace BluToolbox
{
  public static class CameraExtensions
  {
    public static Bounds GetOrthographicBounds(this Camera camera)
    {
      float screenAspect = (float) Screen.width / (float) Screen.height;
      float cameraHeight = camera.orthographicSize * 2;
      Bounds bounds = new(
        camera.transform.position + Vector3.forward * camera.nearClipPlane,
        new Vector3(cameraHeight * screenAspect, cameraHeight, camera.farClipPlane - camera.nearClipPlane)
      );
      return bounds;
    }
  }
}