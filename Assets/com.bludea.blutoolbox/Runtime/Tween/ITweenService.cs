using System;

namespace BluToolbox
{
  public interface ITweenService : IDisposable
  {
    ITween Do(float from, float to, float duration, Action<float> onUpdate, Action onComplete = null);
  }
}
