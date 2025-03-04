using System;

namespace BluToolbox
{
  public class Tween : ITween
  {
    public bool IsCompleted { get; private set; }

    private readonly float _from;
    private readonly float _to;
    private readonly float _duration;
    private readonly Action<float> _onUpdate;
    private readonly Action _onComplete;

    private float _timePassed;

    public Tween(float from, float to, float duration, Action<float> onUpdate, Action onComplete = null)
    {
      _from = from;
      _to = to;
      _duration = duration;
      _onUpdate = onUpdate;
      _onComplete = onComplete;
    }

    public void Update(float deltaTime)
    {
      if (_timePassed >= _duration)
      {
        _onUpdate(_to);
        IsCompleted = true;
        _onComplete?.Invoke();
        return;
      }

      _onUpdate(_from + (_to - _from) * (_timePassed / _duration));
      _timePassed += deltaTime;
    }

    public void SkipToEnd()
    {
      _onUpdate(_to);
      IsCompleted = true;
      _onComplete?.Invoke();
    }

    public void Dispose()
    {
      IsCompleted = true;
    }
  }
}
