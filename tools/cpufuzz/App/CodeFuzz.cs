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

            var bld = new List<string>();
            var space = TextHelper.Space(4);
            bld.Add("using System;");
            bld.Add("");
            bld.Add("namespace PoViEmu.Expert");
            bld.Add("{");
            bld.Add($"{space}public static class XIntel16");
            bld.Add($"{space}{{");
            bld.Add($"{space}}}");
            bld.Add("}");

            var dstFile = Path.Combine(dstDir, "XIntel16.cs");
            File.WriteAllLines(dstFile, bld, TextHelper.Utf8);

            Console.WriteLine($"Processed {allLines.Length} lines.");
        }
    }
}