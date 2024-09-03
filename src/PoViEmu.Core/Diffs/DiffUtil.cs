using System;
using System.Collections.Generic;
using System.Linq;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using PoViEmu.Common;

namespace PoViEmu.Core.Diffs
{
    public record DiffValue(string Key, string? Old, string? New);

    public record DiffLine(char Mod, string Key, string Val);

    public static class DiffUtil
    {
        public static IEnumerable<DiffValue> Check(string before, string after)
        {
            var lines = CheckLine(before, after).GroupBy(x => x.Key);
            foreach (var line in lines)
            {
                var key = line.Key;
                var sub = line.ToArray();
                var iOld = sub.FirstOrDefault(s => s.Mod == '-');
                var iNew = sub.FirstOrDefault(s => s.Mod == '+');
                if (sub.Length == 1)
                    yield return new DiffValue(key, iOld?.Val, iNew?.Val);
                else if (sub.Length == 2)
                    yield return new DiffValue(key, iOld?.Val, iNew?.Val);
            }
        }

        public static IEnumerable<DiffLine> CheckLine(string before, string after)
        {
            var diff = InlineDiffBuilder.Diff(before, after);
            foreach (var line in diff.Lines)
            {
                char mod;
                switch (line.Type)
                {
                    case ChangeType.Deleted:
                        mod = '-';
                        break;
                    case ChangeType.Inserted:
                        mod = '+';
                        break;
                    case ChangeType.Imaginary:
                        mod = '°';
                        break;
                    case ChangeType.Modified:
                        mod = '~';
                        break;
                    case ChangeType.Unchanged:
                    default:
                        continue;
                }
                var text = line.Text;
                string[] parts;
                string key;
                string val;
                if (text is [_, _, ':', ..])
                {
                    parts = text.Split("   ", 2);
                    key = parts[0];
                    val = parts[1];
                }
                else if (text is [_, _, _, _, ':', ..])
                {
                    parts = text.Split("   ", 2);
                    key = parts[0];
                    val = parts[1];
                }
                else
                {
                    parts = text.Split('=', 2);
                    key = parts[0];
                    val = parts[1];
                }
                yield return new DiffLine(mod, key, val);
            }
        }
    }
}