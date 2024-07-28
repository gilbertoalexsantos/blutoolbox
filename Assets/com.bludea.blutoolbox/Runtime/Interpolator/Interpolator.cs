using UnityEngine;

namespace BluToolbox
{
  public class Interpolator
  {
    private readonly float _speed;

    private Vector3 _actualPoint;

    public Interpolator(float speed, Vector3 initialActualPoint)
    {
      _speed = speed;
      _actualPoint = initialActualPoint;
    }

    public Vector3 Update(Vector3 target, float deltaTime)
    {
      _actualPoint = Vector3.MoveTowards(_actualPoint, target, _speed * deltaTime);
      return _actualPoint;
    }
  }
}