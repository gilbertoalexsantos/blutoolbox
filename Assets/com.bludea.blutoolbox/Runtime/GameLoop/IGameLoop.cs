using System;

namespace BluToolbox
{
  public interface IGameLoop : IDisposable
  {
    IGameLoopHandlerDisposable Register(IGameLoopListener listener);
  }
}