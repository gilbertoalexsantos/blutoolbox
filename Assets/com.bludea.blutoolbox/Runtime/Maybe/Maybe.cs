using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public readonly struct Maybe<T> : IEquatable<Maybe<T>>, IComparable<Maybe<T>>
  {
    private readonly T _value;
    private readonly bool _hasValue;

    public bool HasValue => _hasValue;

    public T Value
    {
      get
      {
        if (!HasValue)
        {
          throw new Exception("Maybe does not have a value");
        }

        return _value;
      }
    }

    internal Maybe(T value, bool hasValue)
    {
      _value = value;
      _hasValue = hasValue;
    }

    public bool Equals(Maybe<T> other)
    {
      if (!HasValue && !other.HasValue)
      {
        return true;
      }

      if (_hasValue && other.HasValue)
      {
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
      }

      return false;
    }

    public override bool Equals(object obj)
    {
      return obj is Maybe<T> maybe && Equals(maybe);
    }

    public override int GetHashCode()
    {
      if (HasValue)
      {
        return Value == null ? 1 : Value.GetHashCode();
      }

      return 1;
    }

    public int CompareTo(Maybe<T> other)
    {
      if (HasValue && !other.HasValue) return 1;
      if (!HasValue && other.HasValue) return -1;
      return Comparer<T>.Default.Compare(Value, other.Value);
    }

    public override string ToString()
    {
      if (HasValue)
      {
        return Value == null ? "Some (null)" : $"Some ({Value})";
      }
      else
      {
        return "None";
      }
    }

    public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);
    public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);
    public static bool operator <(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) < 0;
    public static bool operator <=(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) <= 0;
    public static bool operator >(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) > 0;
    public static bool operator >=(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) >= 0;

    public static implicit operator T(Maybe<T> maybe)
    {
      return maybe.HasValue ? maybe.Value : default;
    }

    public static implicit operator Maybe<T>(T value)
    {
      return value == null ? Maybe.None<T>() : Maybe.Some(value);
    }
  }
}