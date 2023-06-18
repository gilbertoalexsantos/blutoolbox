using System.Collections;
using UnityEngine;

namespace BluToolbox
{
  public class TxongaWaitForSeconds : IEnumerator
  {
    private float _seconds;
    private float _startedSeconds;
    private bool _started;

    public object Current => null;

    public TxongaWaitForSeconds(float seconds)
    {
      _seconds = seconds;
    }

    public bool MoveNext()
    {
      if (!_started)
      {
        _startedSeconds = Time.time;
        _started = true;
      }

      float timePassed = Time.time - _startedSeconds;
      return timePassed < _seconds;
    }

    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public override string ToString()
    {
      return $"WaitForSeconds {_seconds}";
    }
  }
}