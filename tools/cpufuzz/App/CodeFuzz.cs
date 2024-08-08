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
using SortedOps = System.Collections.Generic.SortedDictionary<string,
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

            foreach (var line in allLines.Where(l => l.H != nameof(OpCode.Unknown)))
            {
                var lineKey = line.X;
                var bytes = Convert.FromHexString(line.X);
                var cmd = line.H.ToNotKeyword();
                var arg = line.A.ParseArg();
                var size = bytes.Length;
                var gen = $"yield return new(pos, first, {size}, O.{cmd}, [{arg}]);";
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

            var xIntelBase = "XIntel16";
            var bld = GenerateCode(treeDict, out var afterX);
            var dstFile = Path.Combine(dstDir, $"{xIntelBase}.cs");
            File.WriteAllLines(dstFile, bld, TextHelper.Utf8);
            foreach (var one in afterX)
            {
                var dssFile = Path.Combine(dstDir, $"{xIntelBase}x{one.Key}.cs");
                File.WriteAllLines(dssFile, one.Value, TextHelper.Utf8);
            }

            Console.WriteLine($"Processed {allLines.Length} lines.");
        }

        private static List<string> CreateHeader()
        {
            var bld = new List<string>();
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
            return bld;
        }

        private static List<string> GenerateCode(SD treeDict, out Dictionary<string, List<string>> after)
        {
            after = new Dictionary<string, List<string>>();
            var bld = new List<string>();
            var sp = TextHelper.Space(4);
            bld.AddRange(CreateHeader());
            bld.Add("");
            bld.Add("namespace PoViEmu.Expert");
            bld.Add("{");
            bld.Add($"{sp}public static class XIntel16");
            bld.Add($"{sp}{{");
            bld.Add($"{sp}{sp}public static IEnumerable<Instruction> Disassemble(Stream s, byte[] buff)");
            bld.Add($"{sp}{sp}{{");
            bld.Add($"{sp}{sp}{sp}while (s.ReadBytesPos(buff) is {{ }} pos)");
            bld.Add($"{sp}{sp}{sp}{{");
            bld.Add($"{sp}{sp}{sp}{sp}var first = buff[0];");
            bld.Add($"{sp}{sp}{sp}{sp}switch (first)");
            bld.Add($"{sp}{sp}{sp}{sp}{{");
            foreach (var it in treeDict)
            {
                var fKey = it.Key;
                bld.Add($"{sp}{sp}{sp}{sp}{sp}case 0x{fKey}:");
                if (it.Value is SD s1)
                {
                    if (s1.Count == 1 && s1.TryGetValue("_", out var s2) && s2 is string s3)
                    {
                        bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}{s3}");
                        bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}continue;");
                        continue;
                    }

                    var fKeyV = $"x{fKey}";
                    bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}if (Intel16x{fKey}.Parse(s, buff, pos, first) is {{ }} {fKeyV})");
                    bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}{{");
                    bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}{sp}yield return {fKeyV};");
                    bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}{sp}continue;");
                    bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}}}");
                    bld.Add($"{sp}{sp}{sp}{sp}{sp}{sp}break;");

                    if (!after.TryGetValue(fKey, out var aftex))
                        aftex = after[fKey] = new List<string>();

                    aftex.AddRange(CreateHeader());
                    aftex.Add("");
                    aftex.Add($"namespace PoViEmu.Expert");
                    aftex.Add("{");
                    aftex.Add($"{sp}internal static class Intel16x{fKey}");
                    aftex.Add($"{sp}{{");
                    aftex.Add($"{sp}{sp}internal static Instruction? Parse(Stream s, " +
                              $"byte[] buff, long pos, byte first)");
                    aftex.Add($"{sp}{sp}{{");
                    aftex.Add($"{sp}{sp}{sp}var second = s.NextByte();");
                    aftex.Add($"{sp}{sp}{sp}switch (second)");
                    aftex.Add($"{sp}{sp}{sp}{{");
                    foreach (var a1 in s1)
                    {
                        aftex.Add($"{sp}{sp}{sp}{sp}case 0x{a1.Key}:");
                        if (a1.Value is SD b1)
                        {
                            if (b1.Count == 1 && b1.TryGetValue("_", out var b2) && b2 is string b3)
                            {
                                var b3Txt = b3.Split(' ', 2).Last();
                                aftex.Add($"{sp}{sp}{sp}{sp}{sp}{b3Txt}");
                                continue;
                            }
                        }
                        aftex.Add($"{sp}{sp}{sp}{sp}{sp}break;");
                    }
                    aftex.Add($"{sp}{sp}{sp}}}");
                    aftex.Add($"{sp}{sp}{sp}return null;");
                    aftex.Add($"{sp}{sp}}}");
                    aftex.Add($"{sp}}}");
                    aftex.Add($"}}");
                }
            }
            bld.Add($"{sp}{sp}{sp}{sp}}}");
            bld.Add($"{sp}{sp}{sp}{sp}throw new InstructionError(pos, first);");
            bld.Add($"{sp}{sp}{sp}}}");
            bld.Add($"{sp}{sp}}}");
            bld.Add($"{sp}}}");
            bld.Add($"}}");
            return bld;
        }
    }
}