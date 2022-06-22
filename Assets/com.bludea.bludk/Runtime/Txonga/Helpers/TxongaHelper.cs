using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bludk
{
    public static class TxongaHelper
    {
        public static IEnumerable<IEnumerator> All(params IEnumerator[] enumerators)
        {
            return enumerators;
        }

        public static IEnumerable<IEnumerator> All(IEnumerable<IEnumerator> enumerators)
        {
            return All(enumerators.ToArray());
        }

        public static IEnumerator Empty() => new EmptyEnumerator();
    }
}