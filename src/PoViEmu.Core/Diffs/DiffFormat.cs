using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Core.Diffs
{
    public static class DiffFormat
    {
        public static string ToStr(this DiffValue d)
        {
            var vOld = d.Old ?? "_";
            var vNew = d.New ?? "_";
            return $"{d.Key} = {vOld} --> {vNew}";
        }

        public static string[] ToStr(this IEnumerable<DiffValue> d)
        {
            return d.Select(x => x.ToStr()).ToArray();
        }
    }
}