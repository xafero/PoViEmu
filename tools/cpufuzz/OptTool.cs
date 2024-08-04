using System.Collections.Generic;
using System.Linq;

namespace CpuFuzzer
{
    public static class OptUtil
    {
        private static IDictionary<string, List<string>> OptimizeOpDict(this IDictionary<string, List<string>> opRaw)
        {
            var opDict = new SortedDictionary<string, List<string>>();
            foreach (var entry in opRaw)
            {
                var key = $"{entry.Key} !";
                var vals = GetCommonChars(entry.Value);
                opDict[key] = [new string(vals)];
            }
            return opDict;
        }

        public static char[] GetCommonChars(this IList<string> lines, char[] ignores = null)
        {
            var maxLen = lines.Max(l => l?.Length ?? 0);
            var common = new char[maxLen];
            var first = true;
            foreach (var line in lines)
            {
                if (line == null)
                    continue;
                for (var i = 0; i < line.Length; i++)
                {
                    if (first)
                    {
                        common[i] = line[i];
                        continue;
                    }
                    if (common[i] == line[i])
                        continue;
                    if (ignores != null && ignores.Contains(common[i]))
                        continue;
                    common[i] = '_';
                }
                if (first) first = false;
            }
            return common;
        }
    }
}