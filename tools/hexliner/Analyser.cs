using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static HexLiner.Tools;

namespace HexLiner
{
    internal static class Analyser
    {
        public static void DoMain(string root)
        {
            Console.WriteLine($"Root = {root}");

            string[] names = ["addins_hex.txt", "bios_hex.txt", "addins-vs-bios_hex.txt"];
            var files = names.Select(n => Path.Combine(root, n));
            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file, Encoding.UTF8);

                var chars = new SortedDictionary<int, SortedSet<byte>>();
                var size = int.MinValue;

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    var parts = line.Split("=>", 2);
                    var hexStr = parts[0].Trim();

                    var bytes = Convert.FromHexString(hexStr);
                    size = Math.Max(bytes.Length, size);

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        var oneByte = bytes[i];
                        if (chars.TryGetValue(i, out var set))
                            set.Add(oneByte);
                        else
                            chars[i] = [oneByte];
                    }
                }

                var allLine = new StringBuilder();
                for (var i = 0; i < chars.Count; i++)
                {
                    var hex = chars[i];
                    if (hex.Count == 1)
                    {
                        allLine.Append($"{hex.First():x2}");
                        continue;
                    }
                    allLine.Append($"__");
                }

                var mscLines = new List<string>();
                for (var i = 0; i < chars.Count; i++)
                {
                    var hex = chars[i];
                    if (hex.Count == 1)
                        continue;
                    mscLines.Add($" [{i:D3}] {ToHexLine(hex)} | {ToTxtLine(hex)}");
                }

                var tgtFile = $"{file}.p.txt";
                var myLines = new List<string> { string.Empty, allLine.ToString(), string.Empty };
                myLines.AddRange(mscLines);
                File.WriteAllLines(tgtFile, myLines, Encoding.UTF8);

                var text = $" * {file}";
                Console.WriteLine(text);
            }

            Console.WriteLine("Done.");
        }
    }
}