using System;
using System.Collections.Generic;

namespace BluToolbox
{
    public static class ListExtensions
    {
        private static readonly Random _random = new();

        public static void Shuffle<T>(this List<T> l)
        {
            l.Shuffle(_random);
        }

        public static void Shuffle<T>(this List<T> l, Random random)
        {
            for (int i = 0; i <= l.Count - 2; i++)
            {
                int j = random.Next(i, l.Count);
                l.Swap(i, j);
            }
        }

        public static T Random<T>(this List<T> l)
        {
            return l.Random(_random);
        }

        public static T Random<T>(this List<T> l, Random random)
        {
            return l[random.Next(0, l.Count - 1)];
        }

        public static void Swap<T>(this List<T> l, int i, int j)
        {
            (l[i], l[j]) = (l[j], l[i]);
        }
    }
}