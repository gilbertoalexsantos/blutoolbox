using System;

namespace Bludk
{
  public readonly struct Either<L, R>
  {
    private readonly Maybe<L> _left;
    private readonly Maybe<R> _right;

    public bool IsLeft => _left.HasValue;
    public bool IsRight => _right.HasValue;

    public L Left
    {
      get
      {
        if (!IsLeft)
        {
          throw new Exception("You can't unpack because it's not a left one");
        }

        return _left.Value;
      }
    }

    public R Right
    {
      get
      {
        if (!IsRight)
        {
          throw new Exception("You can't unpack because it's not a right one");
        }

        return _right.Value;
      }
    }

    internal Either(Maybe<L> left, Maybe<R> right)
    {
      if (!left.HasValue && !right.HasValue)
      {
        throw new ArgumentException("Either left or right should have a valid value");
      }

      if (left.HasValue && right.HasValue)
      {
        throw new ArgumentException("Only left or only right should have a valid value");
      }

      _left = left;
      _right = right;
    }
  }
}