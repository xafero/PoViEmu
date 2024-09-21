using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;

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

            var items = new SortedSet<HexedLine>();
            var outFile = "hexlines.json";

            foreach (var file in files)
            {
                var label = file.Replace(folder, string.Empty).TrimStart('/');
                var bytes = File.ReadAllBytes(file);
                Console.WriteLine($" * {label} with {bytes.Length} bytes");

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
                var end = i == (items.Count - 1) ? "" : ",";
                lines.Add($"{JsonHelper.ToJson(copy, true)}{end}");
            }
            lines.Add("]");
            File.WriteAllLines(outFile, lines, Encoding.UTF8);
        }
    }
}