using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Core.Hardware
{
    public static class CollFun
    {
        public static void Push<T>(this List<T> list, T item)
        {
            list.Add(item);
        }

        public static T Pop<T>(this List<T> list)
        {
            var last = list.Last();
            list.RemoveAt(list.Count - 1);
            return last;
        }
    }
}