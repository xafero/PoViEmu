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

            var listFile = Path.Combine(outDir, "list.json");
            var listDict = new SD();

            foreach (var line in allLines.Where(l => l.H != OpCode.Unknown))
            {
                var lineKey = line.X;
                var bytes = Convert.FromHexString(line.X);
                var cmd = line.H.ToNotKeyword();
                var arg = line.A.ParseArg();
                var size = bytes.Length;
                var gen = $"yield return new(pos, first, {size}, O.{cmd}, args: [{arg}]); continue;";
                listDict[lineKey] = gen;
            }

            Console.WriteLine($"Generated {listDict.Count} keys.");
            JsonHelper.SaveToFile(listDict, listFile);

            var treeFile = Path.Combine(outDir, "tree.json");
            var treeDict = new SD();

            foreach (var it in listDict)
            {
                var bytes = Convert.FromHexString(it.Key);
                var current = treeDict;
                for (var i = 0; i < bytes.Length; i++)
                {
                    var hex = $"{bytes[i]:X2}";
                    if (!current.TryGetValue(hex, out var next))
                        next = current[hex] = new SD();
                    current = (SD)next;
                }
                current["_"] = it.Value;
            }

            Console.WriteLine($"Generated {treeDict.Count} keys.");
            JsonHelper.SaveToFile(treeDict, treeFile);

            var pwd = Environment.CurrentDirectory;
            var dstDir = Path.Combine(pwd, "..", "..", "src", "PoViEmu.Expert");
            dstDir = Path.GetFullPath(dstDir);
            Console.WriteLine($"Dest = {dstDir}");

            var bld = GenerateCode(treeDict);
            var dstFile = Path.Combine(dstDir, "XIntel16.cs");
            File.WriteAllLines(dstFile, bld, TextHelper.Utf8);

            Console.WriteLine($"Processed {allLines.Length} lines.");
        }

        private static List<string> GenerateCode(SD treeDict)
        {
            var bld = new List<string>();
            var sp = TextHelper.Space(4);
            bld.Add("// ReSharper disable InconsistentNaming");
            bld.Add("");
            bld.Add("using System.Collections.Generic;");
            bld.Add("using System.IO;");
            bld.Add("using PoViEmu.Core.Machine.Args;");
            bld.Add("using PoViEmu.Core.Machine.Core;");
            bld.Add("using PoViEmu.Core.Machine.Ops;");
            bld.Add("using System;");
            bld.Add("using System.Collections.Generic;");
            bld.Add("using System.IO;");
            bld.Add("using System.Linq;");
            bld.Add("using PoViEmu.Core.Machine.Args;");
            bld.Add("using PoViEmu.Core.Machine.Core;");
            bld.Add("using PoViEmu.Core.Machine.Decoding;");
            bld.Add("using PoViEmu.Core.Machine.Ops;");
            bld.Add("using O = PoViEmu.Core.Machine.Ops.OpCode;");
            bld.Add("using R = PoViEmu.Core.Machine.Ops.Register;");
            bld.Add("using M = PoViEmu.Core.Machine.Ops.Modifier;");
            bld.Add("using A = PoViEmu.Core.Machine.Ops.OpArg;");
            bld.Add("");
            bld.Add("namespace PoViEmu.Expert");
            bld.Add("{");
            bld.Add($"{sp}public static class XIntel16");
            bld.Add($"{sp}{{");
            bld.Add($"{sp}{sp}public static IEnumerable<Instruction> Disassemble(Stream s, byte[] buffer)");
            bld.Add($"{sp}{sp}{{");
            bld.Add($"{sp}{sp}{sp}while (s.ReadBytesPos(buffer) is {{ }} pos)");
            bld.Add($"{sp}{sp}{sp}{{");
            bld.Add($"{sp}{sp}{sp}{sp}var first = buffer[0];");
            bld.Add($"{sp}{sp}{sp}{sp}switch (first)");
            bld.Add($"{sp}{sp}{sp}{sp}{{");
            foreach (var it in treeDict)
            {
                bld.Add($"{sp}{sp}{sp}{sp}{sp}case 0x{it.Key}: continue;");
            }
            bld.Add($"{sp}}}");
            bld.Add($"{sp}}}");
            bld.Add("}");
            return bld;
        }
    }
}