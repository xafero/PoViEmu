using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Machine;
using PoViEmu.CpuFuzzer.Core;
using SortedOps = System.Collections.Generic.SortedDictionary<PoViEmu.Core.Machine.Ops.OpCode,
    System.Collections.Generic.SortedDictionary<string, 
        System.Collections.Generic.HashSet<PoViEmu.CpuFuzzer.Core.NasmLine>>>;

namespace PoViEmu.CpuFuzzer.App
{
    public static class CodeFuzz
    {
        public static void Start()
        {
            var enc = Encoding.UTF8;
            var outDir = Directory.CreateDirectory("out").FullName;

            var allFile = Path.Combine(outDir, "all.json");
            var allDict = JsonHelper.FromJson<SortedOps>(File.ReadAllText(allFile, enc));
            var allLines = GenUtil.Iter(allDict).ToArray();

            foreach (var line in allLines.OrderBy(l => l.H.ToString())
                         .ThenBy(l => l.B.Length).ThenBy(a => a.B))
            {
                Console.WriteLine($" * {line}");
            }

            Console.WriteLine($"Processed {allLines.Length} lines.");
        }
    }
}