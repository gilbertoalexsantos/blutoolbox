using System.Collections;
using System.Collections.Generic;

namespace BluToolbox
{
  public interface ICommandAsync
  {
    IEnumerator Execute();
  }

  public interface ICommandAsync<out T>
  {
    IEnumerator<T> Execute();
  }
}