using System.Collections;
using System.Collections.Generic;

namespace Bludk
{
    public class ValueEnumerator<T> : IEnumerator<T>
    {
        private readonly T _value;

        private bool _moved;

        public T Current => _moved ? _value : default;

        object IEnumerator.Current => Current;

        public ValueEnumerator(T value)
        {
            _value = value;
            _moved = false;
        }

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
            _moved = false;
        }

        public void Dispose()
        {
        }
    }
}