using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.CodeGen.Core;
using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;
using PoViEmu.X86Instructions;
using SD = System.Collections.Generic.SortedDictionary<string, object>;
using SortedOps = System.Collections.Generic.SortedDictionary<string,
    System.Collections.Generic.SortedDictionary<string,
        System.Collections.Generic.HashSet<PoViEmu.CodeGen.Core.NasmLine>>>;

// ReSharper disable ForCanBeConvertedToForeach

namespace PoViEmu.CodeGen.App
{
    public static class TestFuzz
    {
        public static void Start()
        {
            var outDir = Directory.CreateDirectory("out").FullName;

            var allFile = Path.Combine(outDir, "all.json");
            var allDict = JsonHelper.LoadFromFile<SortedOps>(allFile);
            var allLines = allDict.Iter().ToArray();

            var listDict = new SD();
            foreach (var line in allLines.Where(l => l.H != nameof(OpCode.Unknown)))
            {
                var lineKey = line.X;
                var bytes = Convert.FromHexString(line.X);
                var cmd = line.H.ToNotKeyword();
                var arg = line.A.ParseArg();
                var size = bytes.Length;
                var suffix = string.Empty;
                if (size == 2) suffix = ", [second]";
                var gen = $"yield return new(pos, first, {size}, O.{cmd}, [{arg}]{suffix});";
                if (bytes.Length != 2)
                    continue;
                listDict[lineKey] = gen;
            }

            var grouped = GroupByValue(listDict);

            var json = JsonHelper.ToJson(grouped);
            File.WriteAllText("lines.json", json, Encoding.UTF8);

            var superGen = new SortedSet<string>();
            var rnd = Random.Shared;

            foreach (var it in grouped)
            {
                var parts = it.Key.Split('|');
                var idx = rnd.Next(0, parts.Length);
                var one = parts[idx].Trim();
                var ot = string.Join(", ", one.SplitEvery(2).Select(i => $"0x{i}"));
                superGen.Add(ot);
            }

            Console.WriteLine(superGen.Count);
            var res = new List<string>();

            var perLine = 8;
            while (superGen.Count >= 1)
            {
                var line = superGen.Take(perLine).ToArray();
                var txt = $"[InlineData({string.Join(", ", line)})]";
                res.Add(txt);
                Console.WriteLine(txt);
                Array.ForEach(line, l => superGen.Remove(l));
            }

            Console.WriteLine($"Generated {res.Count} test lines.");
            Console.WriteLine($"Processed {allLines.Length} lines.");
        }

        internal static IDictionary<string, string> GroupByValue(SD listDict)
        {
            var raw = listDict.GroupBy(l => (string)l.Value)
                .Select(l => (string.Join(" | ", l.Select(x => x.Key)), l.Key))
                .ToDictionary(k => k.Item1, v => v.Item2);
            return new SortedDictionary<string, string>(raw);
        }
    }
}