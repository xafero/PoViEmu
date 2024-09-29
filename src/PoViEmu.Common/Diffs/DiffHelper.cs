using System.Collections.Generic;
using System.Linq;
using System;

namespace PoViEmu.Common.Diffs
{
    public static class DiffHelper
    {
        public static IEnumerable<Changed> Compare(this IList<string> alpha, IList<string> beta)
        {
            var modes = Enum.GetValues<Mode>();
            var firstD = ToDict(alpha);
            var secondD = ToDict(beta);
            var keys = firstD.Keys.Concat(secondD.Keys).Distinct();
            foreach (var oKey in keys)
            {
                var first = firstD.TryGetValue(oKey, out var f) ? f : string.Empty;
                var second = secondD.TryGetValue(oKey, out var s) ? s : string.Empty;
                if (first.Equals(second))
                    continue;
                var keyPts = oKey.Split('_', 2);
                var mode = modes.SingleOrDefault(e => e.ToString()[0] == keyPts[0].Single());
                if (mode == default)
                    throw new InvalidOperationException(oKey);
                var key = keyPts[1];
                var item = new Changed(mode, key, first, second);
                yield return item;
            }
        }

        private static string GetNormed(string text) => text.Replace('*', ' ');

        public static Dictionary<string, string> ToDict(this IEnumerable<string> lines)
        {
            var dict = new Dictionary<string, string>();
            foreach (var rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;
                var line = GetNormed(rawLine);
                var len = line.Length;
                if (len is 4 or 7 or 8 && line.Split('=') is { Length: 2 } eq)
                {
                    var eqKey = eq[0].RemoveSpaces();
                    var eqVal = eq[1].RemoveSpaces();
                    dict[$"R_{eqKey}"] = eqVal;
                    continue;
                }
                if (len is 14 && line.Split("   ") is { Length: 2 } st)
                {
                    var stKey = st[0].RemoveSpaces();
                    var stVal = st[1].RemoveSpaces();
                    dict[$"S_{stKey}"] = stVal;
                    continue;
                }
                if (len is >= 40 and <= 56 && line.Split("   ", 2) is { Length: 2 } cd)
                {
                    var cdKey = cd[0].RemoveSpaces();
                    var cdVal = cd[1].RemoveSpaces();
                    dict[$"C_{cdKey}"] = cdVal;
                    continue;
                }
                if (len == 78 && line.Split("   ", 2) is { Length: 2 } md)
                {
                    var mdKey = md[0].RemoveSpaces();
                    var mdVal = md[1].RemoveSpaces();
                    dict[$"M_{mdKey}"] = mdVal;
                    continue;
                }
                throw new InvalidOperationException($"{line.Length} / {line}");
            }
            return dict;
        }
    }
}