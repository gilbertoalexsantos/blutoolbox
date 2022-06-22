using System;
using System.Collections;

namespace Bludk
{
    internal class EmptyEnumerator : IEnumerator
    {
        public object Current => null;
        private bool _moved;

        public bool MoveNext()
        {
            if (_moved)
            {
                return false;
            }

            _moved = true;
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}