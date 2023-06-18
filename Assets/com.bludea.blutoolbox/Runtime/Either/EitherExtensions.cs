namespace BluToolbox
{
  public static class EitherExtensions
  {
    public static Either<L, R> AsLeft<L, R>(this L value)
    {
      return new Either<L, R>(value.Some(), Maybe.None<R>());
    }

    public static Either<L, R> AsRight<L, R>(this R value)
    {
      return new Either<L, R>(Maybe.None<L>(), value.Some());
    }
  }
}