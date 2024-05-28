using System;

namespace BluToolbox
{
  public class GameLoopHandlerDisposable : IGameLoopHandlerDisposable
  {
    private readonly Action<GameLoopHandlerDisposable> _onDispose;

    public IGameLoopListener Obj { get; }

    public GameLoopHandlerDisposable(IGameLoopListener obj, Action<GameLoopHandlerDisposable> onDispose)
    {
      Obj = obj;
      _onDispose = onDispose;
    }

    public void Dispose()
    {
      _onDispose?.Invoke(this);
    }
  }
}