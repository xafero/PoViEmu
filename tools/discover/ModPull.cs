using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IniFile;
using PoViEmu.Common;

namespace Discover
{
    internal static class ModPull
    {
        private static IEnumerable<(char c, string k, string v)> Read(Section chip)
        {
            foreach (var prop in chip)
            {
                var propKey = prop.Name;
                var propVal = prop.Value.ToString().TrimNull();
                var nameEnd = propKey.Last();
                if (char.IsDigit(nameEnd))
                {
                    var endKey = $"{nameEnd}";
                    var mainKey = propKey[..^endKey.Length].Replace("CHIP", string.Empty);
                    if (propVal is null)
                        continue;
                    yield return (endKey.Single(), mainKey, propVal);
                }
            }
        }

        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);

            var files = folder.FindFiles(".cpj");
            var outDir = "out".GetOrCreateDir();

            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine($" * {name}");

                var info = new FileInfo(file);
                var changed = info.LastWriteTime;
                var suffix = changed.ToString("u").Split(' ').First().Replace("-", "");
                var oFile = Path.Combine(outDir, $"{name}_{suffix}.json");
                Console.WriteLine($"    - {oFile}");

                var ini = IniTool.LoadIni(file);
                var dict = new ModelTree { Name = name };

                var general = ini["GENERAL"];
                var currentDir = general["CurrentDir"].ToString();
                var sourceDir = general["SourceDir"].ToString();
                Console.WriteLine($"       # {currentDir}, {sourceDir}");

                var inv = StringComparison.InvariantCultureIgnoreCase;
                var chips = ini.Where(s => s.Name.StartsWith("CsGroup", inv)
                                           || s.Name.Equals("_Inside", inv)).ToArray();
                var chipsNames = string.Join(", ", chips.Select(s => s.Name));
                Console.WriteLine($"       # {chipsNames}");

                foreach (var chip in chips)
                {
                    var cg = chip.Name.Replace("CSGROUP", string.Empty).Replace("_INSIDE", "I").Single();
                    var cGroup = new ChipGroup();

                    var cPos = Read(chip).GroupBy(i => i.c).ToArray();
                    foreach (var gPos in cPos)
                    {
                        var cDict = new Dictionary<string, string>();
                        foreach (var tuple in gPos)
                            cDict.Add(tuple.k, tuple.v);

                        var cItm = new Chip();
                        if (cDict.TryGetValue("NAME", out var xName))
                            cItm.Name = xName;
                        if (cDict.TryGetValue("BUS", out var xBus))
                            cItm.Bus = byte.Parse(xBus);
                        if (cDict.TryGetValue("CODE", out var xCode))
                            cItm.Code = xCode;
                        if (cDict.TryGetValue("OFFSET", out var xOffset))
                            cItm.Offset = xOffset;
                        if (cDict.TryGetValue("PROGAREA", out var xProgArea))
                            cItm.ProgArea = xProgArea;
                        if (cDict.TryGetValue("KIND", out var xKind))
                            cItm.Kind = xKind == "0" ? ChipKind.RAM :
                                xKind == "1" ? ChipKind.ROM :
                                xKind == "2" ? ChipKind.NorFlash :
                                xKind == "5" ? ChipKind.MemoryIO :
                                default;
                        if (cDict.TryGetValue("FILE", out var xFile))
                        {
                            var xFileReal = PathHelper.TryGetDep(file, xFile)!;
                            var xBytes = File.ReadAllBytes(xFileReal);
                            var xHash = HashHelper.GetSha(xBytes);
                            var xFName = Path.GetFileName(xFileReal);
                            cItm.File = new FileRef
                            {
                                Name = xFName, Size = xBytes.Length, Hash = xHash
                            };
                        }
                        cGroup.Chips[gPos.Key] = cItm;
                    }
                    dict.Groups[cg] = cGroup;
                }
                JsonHelper.SaveToFile(dict, oFile);
            }
        }
    }
}