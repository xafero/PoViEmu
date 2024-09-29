using System.Collections.Generic;

namespace PoViEmu.Common
{
    public static class CollHelper
    {
        public static IEnumerable<(T first, T second)> SplitTwo<T>(this IEnumerable<T> items)
        {
            using var it = items.GetEnumerator();
            while (it.MoveNext())
            {
                var first = it.Current;
                if (!it.MoveNext())
                    continue;
                var second = it.Current;
                yield return (first, second);
            }
        }

        public static IEnumerable<T[]> SplitIt<T>(this IEnumerable<T> items, int count)
        {
            var res = new List<T>();
            foreach (var item in items)
            {
                res.Add(item);
                if (res.Count != count)
                    continue;
                var array = res.ToArray();
                res.Clear();
                yield return array;
            }
            if (res.Count == 0)
                yield break;
            var last = res.ToArray();
            res.Clear();
            yield return last;
        }

        public static bool IsContained(this byte[] first, byte[] second)
        {
            for (var i = 0; i < first.Length && i < second.Length; i++)
            {
                var one = first[i];
                var two = second[i];
                if (one != two)
                    return false;
            }
            return true;
        }
    }
}