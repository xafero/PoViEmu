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
            var allLines = allDict.Iter().ToArray();

            foreach (var g in allLines.OrderBy(l => l.X.Length)
                         .GroupBy(l => l.X.Length))
            {
                Console.WriteLine($" #{g.Key / 2}");
                foreach (var line in g.OrderBy(y => y.X))
                {
                    var bytes = Convert.FromHexString(line.X);
                    var cmd = line.H.ToNotKeyword();
                    var debug = line.ToString().Replace(nameof(NasmLine), string.Empty);
                    var arg = line.A.ParseArg();
                    if (bytes.Length == 1)
                    {
                        Console.WriteLine($"   case 0x{bytes[0]:X2}: yield return new(pos, first," +
                                          $" {bytes.Length}, O.{cmd}, args: [{arg}]); continue;");
                        continue;
                    }
                    Console.WriteLine($" * {cmd}, {bytes.Length}, {arg} --> {debug}");
                    continue;
                }
            }

            Console.WriteLine($"Processed {allLines.Length} lines.");
        }
    }
}