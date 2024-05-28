using System;

namespace BluToolbox
{
  public interface IGameLoop : IDisposable, IHardReload
  {
    IGameLoopHandlerDisposable Register(IGameLoopListener listener);
  }
}