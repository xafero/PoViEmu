using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.CodeGen.Core;
using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;
using SD = System.Collections.Generic.SortedDictionary<string, object>;
using SortedOps = System.Collections.Generic.SortedDictionary<string,
    System.Collections.Generic.SortedDictionary<string,
        System.Collections.Generic.HashSet<PoViEmu.CodeGen.Core.NasmLine>>>;

// ReSharper disable ForCanBeConvertedToForeach

namespace PoViEmu.CodeGen.App
{
    public static class LineFuzz
    {
        public static void Start()
        {
            var outDir = Directory.CreateDirectory("out").FullName;

            var allFile = Path.Combine(outDir, "all.json");
            var allDict = JsonHelper.LoadFromFile<SortedOps>(allFile);
            var allLines = allDict.Iter().ToArray();

            foreach (var line in allLines
                         .Where(l => l.H != nameof(OpCode.Unknown))
                         .OrderBy(l => l.X.Length / 2)
                         .GroupBy(l => l.X.Length / 2))
            {
                var key = line.Key;
                var val = line.OrderBy(y => y.H)
                    .ThenBy(y => y.A ?? string.Empty)
                    .ToArray();
                var valDict = CodeFuzz.ParseLineWithArgs(val);

                var listFile = Path.Combine(outDir, $"sublist_{key:D2}.json");
                var json = JsonHelper.ToJson(valDict);
                File.WriteAllText(listFile, json, Encoding.UTF8);
            }

            Console.WriteLine($"Processed {allLines.Length} lines.");
        }
    }
}