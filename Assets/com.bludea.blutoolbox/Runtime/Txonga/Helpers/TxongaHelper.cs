using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BluToolbox
{
  public static class TxongaHelper
  {
    private static readonly EmptyEnumerator _emptyEnumerator = new ();

    public static IEnumerator Empty => _emptyEnumerator;

    public static IEnumerable<IEnumerator> All(params IEnumerator[] enumerators)
    {
      return enumerators;
    }

    public static IEnumerable<IEnumerator> All(IEnumerable<IEnumerator> enumerators)
    {
      return All(enumerators.ToArray());
    }
  }
}