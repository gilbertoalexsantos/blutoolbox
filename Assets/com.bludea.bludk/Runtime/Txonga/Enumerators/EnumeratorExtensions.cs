using System.Collections.Generic;

namespace Bludk
{
    public static class EnumeratorExtensions
    {
        public static IEnumerator<T> ToEnumerator<T>(this T value)
        {
            return new ValueEnumerator<T>(value);
        }
    }
}