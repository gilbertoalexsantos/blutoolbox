using System;
using System.Collections;

namespace Bludk
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