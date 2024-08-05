using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;
using PoViEmu.Tests;
using PoViEmu.CpuFuzzer.Core;
using static PoViEmu.CpuFuzzer.Core.OptUtil;
using SortedOps = System.Collections.Generic.SortedDictionary<PoViEmu.Core.Machine.Ops.OpCode, 
    System.Collections.Generic.SortedDictionary<string, 
        System.Collections.Generic.HashSet<PoViEmu.CpuFuzzer.Core.NasmLine>>>;

namespace PoViEmu.CpuFuzzer.App
{
    public static class FillFuzz
    {
        public static void Start()
        {
            const int byteCount = 9;
            const int tryCount = 24;

            var bytes = new byte[byteCount];
            var rnd = Random.Shared;
            var nl = Environment.NewLine;

            var first = new SortedOps();
            var outDir = Directory.CreateDirectory("out").FullName;
            var allFile = Path.Combine(outDir, "all.json");
            if (File.Exists(allFile))
                first = JsonHelper.FromJson<SortedOps>(File.ReadAllText(allFile, Encoding.UTF8));

            for (var i = 0; i < tryCount; i++)
            {
                rnd.NextBytes(bytes);

                var expected = NasmTool.DisassembleNasm(bytes);
                Console.Write($" [{i}]");

                if (IsIgnored(expected))
                {
                    i -= 1;
                    continue;
                }
                Console.WriteLine();

                var genLines = expected.Split(nl).Take(1);
                foreach (var genLine in genLines)
                {
                    var objLine = NasmLine.ParseLine(genLine);
                    if (!first.TryGetValue(objLine.H, out var second))
                        second = first[objLine.H] = new SortedDictionary<string, HashSet<NasmLine>>();
                    if (!second.TryGetValue(objLine.B, out var third))
                        third = second[objLine.B] = new HashSet<NasmLine>();
                    third.Add(objLine);
                }
                Console.WriteLine();
            }

            var json = JsonHelper.ToJson(first, format: true);
            File.WriteAllText(allFile, json, Encoding.UTF8);

            foreach (var (a, b) in first)
            {
                json = JsonHelper.ToJson(b, format: true);
                File.WriteAllText(Path.Combine(outDir, $"{a}.json"), json, Encoding.UTF8);
            }
            var allOpCodes = Enum.GetValues<OpCode>();
            Console.WriteLine($"Generated {first.Count} from {allOpCodes.Length} opcodes.");
        }
    }
}