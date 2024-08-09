using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Common
{
    public static class CollHelper
    {
        public static IEnumerable<T> ToMax<T>(this IEnumerable<T> items, int maxCount)
        {
            return maxCount >= 1 ? items.Take(maxCount) : items;
        }
    }
}