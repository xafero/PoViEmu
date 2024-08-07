using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Machine;
using PoViEmu.Core.Machine.Ops;
using PoViEmu.CpuFuzzer.Core;
using SD = System.Collections.Generic.SortedDictionary<string, object>;
using SortedOps = System.Collections.Generic.SortedDictionary<PoViEmu.Core.Machine.Ops.OpCode,
    System.Collections.Generic.SortedDictionary<string,
        System.Collections.Generic.HashSet<PoViEmu.CpuFuzzer.Core.NasmLine>>>;

// ReSharper disable ForCanBeConvertedToForeach

namespace PoViEmu.CpuFuzzer.App
{
    public static class CodeFuzz
    {
        public static void Start()
        {
            var outDir = Directory.CreateDirectory("out").FullName;

            var allFile = Path.Combine(outDir, "all.json");
            var allDict = JsonHelper.LoadFromFile<SortedOps>(allFile);
            var allLines = allDict.Iter().ToArray();

            var treeFile = Path.Combine(outDir, "tree.json");
            var treeDict = new SD();

            foreach (var line in allLines.Where(l => l.H != OpCode.Unknown)
                         .OrderBy(l => l.X.Length).Take(240))
            {
                var bytes = Convert.FromHexString(line.X);
                var current = treeDict;

                for (var i = 0; i < bytes.Length; i++)
                {
                    var hex = $"{bytes[i]:X2}";
                    if (!current.TryGetValue(hex, out var next))
                        next = current[hex] = new SD();
                    current = (SD)next;
                }

                var cmd = line.H.ToNotKeyword();
                var arg = line.A.ParseArg();
                var size = bytes.Length;
                var gen = $"yield return new(pos, first, {size}, O.{cmd}, args: [{arg}]); continue;";

                current["_"] = gen;
            }

            Console.WriteLine($"Generated {treeDict.Count} keys.");
            JsonHelper.SaveToFile(treeDict, treeFile);

            Console.WriteLine($"Processed {allLines.Length} lines.");
        }
    }
}