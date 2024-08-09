using System.Collections.Generic;
using System.Linq;
using PoViEmu.CodeGen.Core;

namespace PoViEmu.CpuFuzzer.Core
{
    internal static class OptUtil
    {
        internal static bool IsIgnored(string expected)
        {
            return expected.Contains("  db ") || expected.Contains("  64") || expected.Contains("  67")
                   || expected.Contains("  26") || expected.Contains("  36") || expected.Contains("  F3")
                   || expected.Contains("  65") || expected.Contains("  F2") || expected.Contains("  0F")
                   || expected.Contains("  2E") || expected.Contains("  9B") || expected.Contains("  F0")
                   || expected.Contains("  3E") || expected.Contains("  66");
        }

        internal static IDictionary<string, List<string>> ToOpDict(this NasmLine[] allLines)
        {
            var opDict = new SortedDictionary<string, List<string>>();
            foreach (var item in allLines.GroupBy(g => g.B.Split(' ', 2)[0]))
            {
                var opNames = item.Select(t => t.H).OrderBy(x => x).Distinct();
                foreach (var opName in opNames)
                {
                    var opKey = opName.ToString();
                    if (!opDict.TryGetValue(opKey, out var opVal))
                        opVal = opDict[opKey] = new List<string>();
                    opVal.Add(item.Key);
                }
            }
            return opDict;
        }

        internal static IDictionary<string, List<string>> OptimizeOpDict(this IDictionary<string, List<string>> opRaw)
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

        internal static char[] GetCommonChars(this IList<string> lines, char[] ignores = null)
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