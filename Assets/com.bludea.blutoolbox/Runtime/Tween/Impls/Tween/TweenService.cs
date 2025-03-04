using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public class TweenService : ITweenService, IUpdateListener
  {
    private readonly IGameLoop _gameLoop;
    private readonly List<Tween> _tweens = new();

    private IDisposable _gameLoopDisposable;

    public TweenService(IGameLoop gameLoop)
    {
      _gameLoop = gameLoop;
    }

    public ITween Do(float from, float to, float duration, Action<float> onUpdate, Action onComplete = null)
    {
      if (_gameLoopDisposable == null)
      {
        _gameLoopDisposable = _gameLoop.Register(this);
      }

      Tween tween = new(from, to, duration, onUpdate, onComplete);
      _tweens.Add(tween);
      return tween;
    }

    public void OnUpdate(float deltaTime)
    {
      for (int i = _tweens.Count - 1; i >= 0; i--)
      {
        if (_tweens[i].IsCompleted)
        {
          _tweens.RemoveAt(i);
          continue;
        }

        _tweens[i].Update(deltaTime);

        if (_tweens[i].IsCompleted)
        {
          _tweens.RemoveAt(i);
        }
      }

      if (_tweens.Count == 0)
      {
        _gameLoopDisposable.Dispose();
        _gameLoopDisposable = null;
      }
    }

    public void Dispose()
    {
      _gameLoopDisposable?.Dispose();
    }
  }
}
