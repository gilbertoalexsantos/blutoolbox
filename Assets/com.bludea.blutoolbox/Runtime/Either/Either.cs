using System;

namespace BluToolbox
{
  public readonly struct Either<L, R>
  {
    private readonly Maybe<L> _left;
    private readonly Maybe<R> _right;

    public bool IsLeft => _left.HasValue;
    public bool IsRight => _right.HasValue;

    private L Left
    {
      get
      {
        if (_left.TryGetValue(out L v))
        {
          return v;
        }

        throw new Exception("You can't unpack because it's not a left one");
      }
    }

    private R Right
    {
      get
      {
        if (_right.TryGetValue(out R v))
        {
          return v;
        }

        throw new Exception("You can't unpack because it's not a right one");
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

    public bool TryGetLeft(out L value)
    {
      return _left.TryGetValue(out value);
    }

    public bool TryGetRight(out R value)
    {
      return _right.TryGetValue(out value);
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