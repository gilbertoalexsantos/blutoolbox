namespace Bludk
{
    public static class MaybeExtensions
    {
        public static Maybe<T> Some<T>(this T value) => Maybe.Some(value);
        public static Maybe<T> None<T>(this T value) => Maybe.None<T>();
    }
}