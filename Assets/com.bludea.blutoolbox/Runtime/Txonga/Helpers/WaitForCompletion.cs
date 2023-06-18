using System.Collections;
using System.Collections.Generic;

namespace BluToolbox
{
  public class WaitForCompletion
  {
    public bool IsComplete { get; private set; }

    public void Complete()
    {
      IsComplete = true;
    }

    public void Reset()
    {
      IsComplete = false;
    }

    public IEnumerator Await()
    {
      while (!IsComplete)
      {
        yield return null;
      }
    }
  }

  public class WaitForCompletion<T>
  {
    public bool IsComplete { get; private set; }
    public T Result { get; private set; }

    public void Complete(T result)
    {
      IsComplete = true;
      Result = result;
    }

    public void Reset()
    {
      IsComplete = false;
      Result = default;
    }

    public IEnumerator<T> Await()
    {
      while (!IsComplete)
      {
        yield return default;
      }

      yield return Result;
    }
  }
}