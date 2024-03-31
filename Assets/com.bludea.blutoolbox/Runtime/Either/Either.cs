using System;

namespace BluToolbox
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

    internal Either(L left)
    {
      _left = Maybe.Some(left);
      _right = Maybe.None<R>();
    }

    internal Either(R right)
    {
      _left = Maybe.None<L>();
      _right = Maybe.Some(right);
    }

    public static implicit operator Either<L, R>(L left)
    {
      return left.AsLeft<L, R>();
    }

    public static implicit operator Either<L, R>(R right)
    {
      return right.AsRight<L, R>();
    }
  }
}