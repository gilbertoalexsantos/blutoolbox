namespace BluToolbox
{
  public static class MaybeExtensions
  {
    public static Maybe<T> Some<T>(this T value) => Maybe.Some(value);
    public static Maybe<T> Some<T>(this Maybe<T> value) => value.TryGetValue(out T v) ? Maybe.Some(v) : Maybe.None<T>();
    public static Maybe<T> None<T>(this T _) => Maybe.None<T>();
    public static Maybe<T> None<T>(this Maybe<T> _) => Maybe.None<T>();
  }
}