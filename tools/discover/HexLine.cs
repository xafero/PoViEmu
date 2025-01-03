using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Base;

namespace Discover
{
    internal static class HexLine
    {
        public record HexedLine(string Name, string Hex, string Text) : IComparable
        {
            public int CompareTo(object obj)
                => string.Compare(Hex, ((HexedLine)obj)?.Hex, StringComparison.Ordinal);
        }

        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            var bDict = new SortedDictionary<string, SortedDictionary<string, SortedSet<string>>>();
            var byOutFile = "hexbytes.json";

            var items = new SortedSet<HexedLine>();
            var hlOutFile = "hexlines.json";

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file).ToLowerInvariant();
                if (!bDict.TryGetValue(ext, out var exist))
                    bDict[ext] = exist = new SortedDictionary<string, SortedSet<string>>();

                var label = file.Replace(folder, string.Empty).TrimStart('/');
                var bytes = File.ReadAllBytes(file);
                Console.WriteLine($" * {label} with {bytes.Length} bytes");

                var copy = bytes[..512];
                for (var i = 0; i < copy.Length; i++)
                {
                    var bKey = $"{i:X4}";
                    if (!exist.TryGetValue(bKey, out var sub))
                        exist[bKey] = sub = new SortedSet<string>();
                    var bit = copy[i];
                    var ascii = new[] { bit }.DecodeChars();
                    var num = (int)bit;
                    sub.Add($"{bit:X2} ({ascii}) ({num})");
                }
                
                var hex = Convert.ToHexString(bytes);
                items.Add(new HexedLine(label, hex, string.Empty));
            }

            var tmp = items.ToArray();
            var lines = new List<string> { "[" };
            for (var i = 0; i < items.Count; i++)
            {
                var item = tmp[i];
                var max = Math.Min(item.Hex.Length, 128 * 4);
                var capped = item.Hex[..max];
                var copy = item with
                {
                    Hex = capped, Text = Convert.FromHexString(capped).DecodeChars()
                };
                var end = i == items.Count - 1 ? "" : ",";
                lines.Add($"{JsonHelper.ToJson(copy, true)}{end}");
            }
            lines.Add("]");
            File.WriteAllLines(hlOutFile, lines, Encoding.UTF8);
            File.WriteAllText(byOutFile, JsonHelper.ToJson(ToEasy(bDict)), Encoding.UTF8);
        }

        private static SortedDictionary<string, string[]> ToEasy(
            IDictionary<string, SortedDictionary<string, SortedSet<string>>> i)
        {
            var d = new SortedDictionary<string, string[]>();
            foreach (var t in i)
            {
                d[t.Key] = t.Value.Select(x =>
                    $"{x.Key} : {string.Join(" | ", x.Value)}").ToArray();
            }
            return d;
        }
    }
}