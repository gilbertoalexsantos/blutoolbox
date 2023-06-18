namespace BluToolbox
{
  public static class Maybe
  {
    public static Maybe<T> Some<T>(T value) => new(value, true);
    public static Maybe<T> None<T>() => new(default, false);
  }
}