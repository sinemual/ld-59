using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Random<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static bool AddIfNotContains<T>(this IList<T> list, T value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
                return true;
            }

            return false;
        }
    }
}