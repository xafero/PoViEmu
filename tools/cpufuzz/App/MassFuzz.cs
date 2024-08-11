using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.CodeGen.Core;
using PoViEmu.Common;
using PoViEmu.Core.Machine;
using PoViEmu.CpuFuzzer.Core;
using PoViEmu.Expert;
using SortedOps = System.Collections.Generic.SortedDictionary<PoViEmu.Core.Machine.Ops.OpCode,
    System.Collections.Generic.SortedDictionary<string,
        System.Collections.Generic.HashSet<PoViEmu.CodeGen.Core.NasmLine>>>;

namespace PoViEmu.CpuFuzzer.App
{
    public static class MassFuzz
    {
        public static void Start()
        {
            var enc = Encoding.UTF8;
            var outDir = Directory.CreateDirectory("out").FullName;

            var allFile = Path.Combine(outDir, "all.json");
            var allDict = JsonHelper.FromJson<SortedOps>(File.ReadAllText(allFile, enc));

            var allLines = allDict.SelectMany(f => f.Value
                .SelectMany(x => x.Value.Select(y => y))).ToArray();

            var useLines = new List<NasmLine>();
            foreach (var line in allLines.OrderBy(l => l.B.Length).ThenBy(a => a.B))
            {
                if (line.H == default)
                    continue;
                if (!((line.A ?? "").Length <= 5))
                    continue;
                useLines.Add(line);
            }

            var opDict = allLines.ToOpDict();
            var json = JsonHelper.ToJson(opDict);
            File.WriteAllText(Path.Combine(outDir, "opDict.json"), json, enc);

            opDict = opDict.OptimizeOpDict();
            json = JsonHelper.ToJson(opDict);
            File.WriteAllText(Path.Combine(outDir, "opDixt.json"), json, enc);

            var srcParser = new List<string>();
            var dstParser = new List<string>();
            foreach (var line in useLines)
            {
                var bytes = Convert.FromHexString(line.X);
                var buffer = new byte[1];
                var mem = new MemoryStream(bytes);
                var instr = XIntel16.Disassemble(mem, buffer).ToArray().FirstOrDefault();
                if (instr != default)
                {
                    var debug = new[] { instr }.ToText();
                    var instrPts = debug.Split("  ", 3,
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    var instrPtx = instrPts[2].Split(' ', 2);
                    var argo = instrPtx.Length >= 2 ? $", A = {instrPtx[1]}" : ", A = ";
                    var copy = $"NasmLine {{ B = {bytes.ToBinary()}, X = {bytes.ToHex(false)}" +
                               $", H = {instrPtx[0]}{argo} }}";
                    srcParser.Add($" // {copy}");
                }
                else
                {
                    srcParser.Add($" // ???");
                }
                dstParser.Add($" // {line}");

                Console.WriteLine($"{allLines.Length} lines read.");
                File.WriteAllLines(Path.Combine(outDir, "OpParser1.cs"), srcParser, enc);
                File.WriteAllLines(Path.Combine(outDir, "OpParser2.cs"), dstParser, enc);
            }
        }
    }
}