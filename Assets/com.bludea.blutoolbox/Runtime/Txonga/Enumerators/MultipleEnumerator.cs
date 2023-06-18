using System;
using System.Collections;
using System.Collections.Generic;

namespace BluToolbox
{
    internal class MultipleEnumerator : IEnumerator
    {
        private readonly IEnumerable<IEnumerator> _enumerators;

        public object Current => null;

        public MultipleEnumerator(IEnumerable<IEnumerator> enumerators)
        {
            _enumerators = enumerators;
        }

        public bool MoveNext()
        {
            bool hasMove = false;
            foreach (IEnumerator enumerator in _enumerators)
            {
                hasMove |= enumerator.MoveNext();
            }

            return hasMove;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}