using UnityEngine;

namespace BluToolbox
{
  public class TransformInterpolator
  {
    private readonly Transform _target;

    public Vector3 Position { get; private set; }
    public Quaternion Rotation { get; private set; }

    public TransformInterpolator(Transform transform)
    {
      _target = transform;
      Position = _target.position;
      Rotation = _target.rotation;
    }

    public void Update(float deltaTime)
    {
      Position = Vector3.MoveTowards(Position, _target.position, deltaTime);
      Rotation = Quaternion.RotateTowards(Rotation, _target.rotation, deltaTime);
    }
  }
}