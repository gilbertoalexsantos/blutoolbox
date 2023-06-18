using System;
using System.Collections;

namespace BluToolbox
{
  internal class EmptyEnumerator : IEnumerator
  {
    public object Current => null;

    public bool MoveNext()
    {
      return false;
    }

    public void Reset()
    {
      throw new NotImplementedException();
    }
  }
}