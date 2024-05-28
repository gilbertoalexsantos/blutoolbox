using System;
using System.Collections.Generic;

namespace BluToolbox
{
  public readonly struct Maybe<T> : IEquatable<Maybe<T>>, IComparable<Maybe<T>>
  {
    private readonly T _value;
    private readonly bool _hasValue;

    public bool HasValue => _hasValue;

    private T Value
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

    public bool TryGetValue(out T value)
    {
      if (HasValue)
      {
        value = Value;
        return true;
      }

      value = default;
      return false;
    }

    public T ValueOr(T defaultValue)
    {
      return TryGetValue(out T v) ? v : defaultValue;
    }

    public bool Equals(Maybe<T> other)
    {
      if (!HasValue || !other.HasValue)
      {
        return false;
      }

      return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
      return obj is Maybe<T> maybe && Equals(maybe);
    }

    public override int GetHashCode()
    {
      return TryGetValue(out T v) ? v.GetHashCode() : 0;
    }

    public int CompareTo(Maybe<T> other)
    {
      if (!HasValue || !other.HasValue) return 0;
      return Comparer<T>.Default.Compare(Value, other.Value);
    }

    public override string ToString()
    {
      if (HasValue)
      {
        return Value == null ? "<<null>>" : Value.ToString();
      }

      return "<<null>>";
    }

    public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);
    public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);
    public static bool operator <(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) < 0;
    public static bool operator <=(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) <= 0;
    public static bool operator >(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) > 0;
    public static bool operator >=(Maybe<T> left, Maybe<T> right) => left.CompareTo(right) >= 0;

    public static implicit operator Maybe<T>(T value)
    {
      return value == null ? Maybe.None<T>() : Maybe.Some(value);
    }
  }
}