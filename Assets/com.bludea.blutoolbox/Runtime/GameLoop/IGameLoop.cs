using System;

namespace BluToolbox
{
  public interface IGameLoop : IDisposable
  {
    IDisposable Register(IGameLoopListener listener);
  }
}
