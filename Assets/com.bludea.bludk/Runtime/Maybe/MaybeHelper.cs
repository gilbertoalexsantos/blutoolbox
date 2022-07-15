namespace Bludk
{
    public class Maybe
    {
        public static Maybe<T> Some<T>(T value) => new Maybe<T>(value, true);
        public static Maybe<T> None<T>() => new Maybe<T>(default(T), false);
    }
}