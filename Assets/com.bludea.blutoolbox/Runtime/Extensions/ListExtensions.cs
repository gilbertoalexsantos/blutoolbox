using System.Collections.Generic;

namespace BluToolbox
{
  public static class ListExtensions
  {
    private static readonly SystemRandom _systemRandom = new();

    public static void Shuffle<T>(this List<T> l)
    {
      l.Shuffle(_systemRandom);
    }

    public static void Shuffle<T>(this List<T> l, IRandom unityRandom)
    {
      for (int i = 0; i <= l.Count - 2; i++)
      {
        int j = unityRandom.IntRange(i, l.Count);
        l.Swap(i, j);
      }
    }

    public static T Random<T>(this List<T> l)
    {
      return l.Random(_systemRandom);
    }

    public static T Random<T>(this List<T> l, IRandom unityRandom)
    {
      return l[unityRandom.IntRange(0, l.Count - 1)];
    }

    public static void Swap<T>(this List<T> l, int i, int j)
    {
      (l[i], l[j]) = (l[j], l[i]);
    }
  }
}