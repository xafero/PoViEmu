using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IniFile;
using PoViEmu.Base;
using PoViEmu.Compression;
using PoViEmu.Inventory.Infos;
using PoViEmu.Inventory.Utils;

// ReSharper disable InconsistentNaming

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
            const string cpj = ".cpj";
            const string dlp = ".dlp";

            var files = folder.FindFiles(cpj, dlp);
            var outDir = "out".GetOrCreateDir();

            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                name = name.Replace("PV3S1600", "PVS1600");
                var ext = Path.GetExtension(file).ToLowerInvariant();
                var kind = ext switch { ".cpj" => ModelKind.X86, ".dlp" => ModelKind.SH3, _ => default };
                Console.WriteLine($" * {kind} => {name}");

                var info = new FileInfo(file);
                var changed = info.LastWriteTime.ToString("u");
                var suffix = changed.Split(' ').First().Replace("-", "");
                var oFile = Path.Combine(outDir, $"{suffix}_{name}.json");
                Console.WriteLine($"    - {oFile}");

                var changedT = changed.Split(' ', 2).First();
                var desc = name.Replace("PV", "PV-").Replace("Plus", " Plus");
                var dict = new ModelTree { Changed = changedT, Name = desc, Kind = kind };
                switch (kind)
                {
                    case ModelKind.X86: RunX86(file, dict); break;
                    case ModelKind.SH3: RunSH3(file, dict); break;
                }
                JsonHelper.SaveToFile(dict, oFile);
            }
        }

        private static void RunX86(string file, ModelTree dict)
        {
            var ini = IniTool.LoadIni(file);
            var dir = Path.GetDirectoryName(file)!;

            var general = ini["GENERAL"];
            var currentDir = PathHelper.Merge(dir, general["CurrentDir"]);
            var sourceDir = PathHelper.Merge(dir, general["SourceDir"]);
            if (sourceDir!.Contains("SAMPLE1"))
                sourceDir = sourceDir.Replace("SAMPLE1", "Sample1");
            else if (sourceDir!.Contains("SAMPLE"))
                sourceDir = sourceDir.Replace("SAMPLE", "Sample");
            Console.WriteLine($"       # {currentDir}, {sourceDir}");

            var cpuClock = int.Parse(general["CLOCK"]);
            var lcdClock = int.Parse(general["LCDC_FREQ"]);
            dict.Clock = new Clock(cpuClock, lcdClock);

            var cSysRam = int.Parse(general["C_SYSRAM"]);
            dict.CSysRam = cSysRam;

            var tablet = ini["TABLET_DISP"];
            var width = int.Parse(tablet["DispWidth"]);
            var height = int.Parse(tablet["DispHeight"]);
            dict.Display = new Display(width, height);

            var inv = StringComparison.InvariantCultureIgnoreCase;
            var chips = ini.Where(s => s.Name.StartsWith("CsGroup", inv)
                                       || s.Name.Equals("_Inside", inv)).ToArray();
            var chipsNames = string.Join(", ", chips.Select(s => s.Name));
            // Console.WriteLine($"       # {chipsNames}");

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
                        cItm.Caption = xName;
                    if (cDict.TryGetValue("BUS", out var xBus))
                        cItm.ConnectionBit = byte.Parse(xBus);
                    if (cDict.TryGetValue("CODE", out var xCode))
                        cItm.ProgChipName = xCode;
                    if (cDict.TryGetValue("OFFSET", out var xOffset))
                    {
                        var xOffsetInt = Convert.ToInt32(xOffset, 16);
                        cItm.LoadOffset = $"{xOffsetInt:X8}";
                    }
                    if (cDict.TryGetValue("PROGAREA", out var xProgArea))
                    {
                        var xProgAreaInt = Convert.ToInt32(xProgArea, 16);
                        cItm.ProgAreaSize = $"{xProgAreaInt:X8}";
                    }
                    if (cDict.TryGetValue("KIND", out var xKind))
                        cItm.ChipKind = xKind == "0" ? ChipKind.RAM :
                            xKind == "1" ? ChipKind.ROM :
                            xKind == "2" ? ChipKind.NorFlash :
                            xKind == "5" ? ChipKind.MemoryIO :
                            default;
                    if (cDict.TryGetValue("FILE", out var xFile))
                    {
                        var root = currentDir!;
                        var xFileReal = PathHelper.Combine(root, xFile)!;
                        LoadFileIn(xFileReal, cItm);
                    }
                    cGroup.Chips[gPos.Key] = cItm;
                }
                dict.Groups[cg] = cGroup;
            }
        }

        private static void LoadFileIn(string xFileReal, Chip cItm)
        {
            if (xFileReal.Contains("Sim"))
                xFileReal = xFileReal.Replace("Sim", "SIM");
            var xBytes = File.ReadAllBytes(xFileReal);
            var xHash = HashHelper.GetSha(xBytes);
            var xFName = Path.GetFileName(xFileReal);
            var xZip = CompressAlgo.Brotli.Compress(xBytes);
            cItm.File = new FileRef
            {
                Name = xFName, Size = xBytes.Length, Hash = xHash, Brotli = xZip.B
            };
            cItm.ChipSize = $"{xBytes.Length:X8}";
        }

        private static void RunSH3(string file, ModelTree dict)
        {
            var ini = IniTool.LoadIni(file);
            var dir = Path.GetDirectoryName(file)!;

            dict.Display = new Display(160, 160);
            dict.Clock = new Clock(25, 70);

            var general = ini["DLSimProject"];
            var sourcePath = PathHelper.Combine(dir, general["SourcePath"]);
            var memoryPath = PathHelper.Combine(dir, general["MemoryPath"]);
            if (sourcePath == null) return;
            Console.WriteLine($"       # {sourcePath}, {memoryPath}");

            var prog1 = ini["Program1"];
            var prog1F = PathHelper.Combine(dir, prog1["Program"]);
            var prog1A = prog1["LoadAddress"].ToString();

            var modelFile = PathHelper.Combine(dir, general["Model"])!;
            var modelIni = IniTool.LoadDoubled(modelFile);
            var modelDir = Path.GetDirectoryName(modelFile)!;

            var i = 0;
            foreach (var (prefix, kind) in new[]
                     {
                         ("_memoryrom", ChipKind.ROM),
                         ("_memoryram", ChipKind.RAM),
                         ("_memorynand", ChipKind.NorFlash),
                         ("_fileaccess", ChipKind.MemoryIO)
                     })
            {
                var mem = modelIni[$"{prefix}.mem"];
                var memAddr = mem["BaseAddress"].ToString();
                var memSave = mem["SaveFile"].ToString();
                var memInit = mem["DefaultFile"].ToString();
                string memSize = null;
                var memSubA = modelIni[$"{prefix}.mem.addr"];
                var memSubM = modelIni[$"{prefix}.mem.main"];
                if (memSubA != null) memSize = memSubA["Size"].ToString();
                if (memSubM != null) memSize = memSubM["Size"].ToString();

                var groupKey = $"{i++}"[0];
                var sub = new Dictionary<char, Chip>();
                if (memSave != null)
                {
                    var memSaveN = Path.GetFileNameWithoutExtension(memSave);
                    var maf = PathHelper.Combine(modelDir, memSave);
                    var ma = new Chip
                    {
                        ChipKind = kind, LoadOffset = memAddr, Caption = memSaveN, ChipSize = memSize
                    };
                    LoadFileIn(maf, ma);
                    sub['0'] = ma;
                }
                if (memInit != null)
                {
                    var memInitN = Path.GetFileNameWithoutExtension(memInit.Split('\\')[1]);
                    var mbf = PathHelper.Combine(modelDir, memInit);
                    var mb = new Chip
                    {
                        ChipKind = kind, LoadOffset = memAddr, Caption = memInitN, ChipSize = memSize
                    };
                    LoadFileIn(mbf, mb);
                    if (sub['0'].File?.Hash != mb.File?.Hash)
                        sub['1'] = mb;
                }
                if (kind == ChipKind.MemoryIO)
                {
                    var mc = new Chip
                    {
                        ChipKind = kind, LoadOffset = memAddr, ChipSize = memSize, Caption =
                            prefix.TrimStart('_').Replace('f', 'F').Replace('a', 'A')
                    };
                    sub['2'] = mc;
                }
                dict.Groups[groupKey] = new ChipGroup { Chips = sub };
            }

            if (prog1F != null)
            {
                var p1N = Path.GetFileNameWithoutExtension(prog1F).Title();
                var md = new Chip { ChipKind = ChipKind.ROM, LoadOffset = prog1A, Caption = p1N };
                LoadFileIn(prog1F, md);
                var sub = new Dictionary<char, Chip> { ['0'] = md };
                dict.Groups['5'] = new ChipGroup { Chips = sub };
            }
        }
    }
}