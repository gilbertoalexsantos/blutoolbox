using UnityEngine;

namespace BluToolbox
{
  public interface ICommandAsync
  {
    Awaitable Execute();
  }

  public interface ICommandAsync<T>
  {
    Awaitable<T> Execute();
  }
}