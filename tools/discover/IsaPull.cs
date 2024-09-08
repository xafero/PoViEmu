using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ByteSizeLib;
using Iced.Intel;
using PoViEmu.Common;
using PoViEmu.Core;
using PoViEmu.Core.Addins;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Dumps;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Images;

namespace Discover
{
    internal static class IsaPull
    {
        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            var instr = new SortedDictionary<string, SortedDictionary<string, SortedSet<string>>>();

            foreach (var file in files)
            {
                if (IsIgnoredExt(file) || IsIgnoredDir(file))
                    continue;
                var bytes = File.ReadAllBytes(file);
                if (bytes.GetMimeType() is not { } mt)
                    continue;
                var size = bytes.Length;
                var local = file.Replace(folder, string.Empty)[1..];
                Console.WriteLine($" * [{mt}] {local} ({ByteSize.FromBytes(size)})");

                var addIns = new List<AddInInfo>();
                var obj = mt.Load(bytes);

                if (obj is DumpInfo di)
                {
                    addIns.AddRange(di.AddIns.Values);
                }
                else if (obj is AddInInfo ai)
                {
                    addIns.Add(ai);
                }

                foreach (var adi in addIns)
                {
                    var ain = adi.GetName();
                    var aiv = adi.Version;
                    var aim = adi.Model;
                    Console.WriteLine($"    {ain} v{aiv} (compiled at {adi.Compiled:u} for {aim})");

                    var offIcon = (int)adi.OffsetIcon;
                    var offLIcon = (int)adi.OffsetLIcon;
                    if (offIcon == -1 || offLIcon == -1 || size <= 0xFF)
                        continue;

                    var iconLen = ImageReader.GetByteSize(bytes[offIcon..]);
                    var lIconLen = ImageReader.GetByteSize(bytes[offLIcon..]);
                    if (iconLen > 350_000 || lIconLen > 350_000)
                        continue;

                    byte empty = 0xFF;
                    var copy = bytes[..];
                    copy.Write(0, 0x100, empty);
                    copy.Write(offIcon, iconLen, empty);
                    copy.Write(offLIcon, lIconLen, empty);

                    var state = new MachineState
                    {
                        Memory =
                        {
                            [0] = new()
                            {
                                [0] = new MemList(copy)
                            }
                        }
                    };

                    foreach (var (_, (hex, i)) in state.ToInstructions())
                    {
                        if (i.IsInvalid)
                            continue;
                        if (i.Encoding != EncodingKind.Legacy)
                            continue;

                        var key = i.Code.ToString();
                        if (key.Contains("r32") || key.Contains("imm32") ||
                            key.Contains("rm32") || key.Contains("m32") ||
                            key.Contains("moffs32") || key.Contains("ptr1632") || 
                            key.Contains("rel32") || key.Contains("m1632") ||
                            key.Contains("rel8_32") || key.Contains("m128") ||
                            key.Contains("m64"))
                            continue;

                        if (!instr.TryGetValue(key, out var subD))
                            instr[key] = subD = new();

                        var sub = i.GetOpDebug(fake: true);
                        if (!subD.TryGetValue(sub, out var list))
                            subD[sub] = list = new();

                        var line = $"{hex} | {i}";
                        list.Clear();
                        list.Add(line);
                    }
                }
            }

            var json = JsonHelper.ToJson(instr);
            File.WriteAllText("found-isa.json", json, Encoding.UTF8);
        }

        private static readonly string[] Ext =
        [
            "", ".sh", ".png", ".md", ".7z", ".sample", ".cs", ".csproj", ".py", ".yml",
            ".txt", ".json", ".props", ".bat", ".dll", ".exe", ".cache", ".pyc", ".info",
            ".targets", ".axaml", ".xml", ".manifest", ".ico", ".pack", ".pdb", ".h",
            ".doc", ".fr", ".it", ".es", ".de", ".pdf", ".bmt", ".bmp", ".c", ".a86",
            ".map", ".hex", ".dmp", ".10", ".ps1", ".tml", ".dtx", ".fish", ".obj",
            ".csh", ".rev", ".idx", ".lib", ".1", ".2", ".editorconfig", ".copycomplete",
            ".pth", ".so", ".nupkg", ".gitignore", ".sln", ".zip", ".ttf", ".cmd",
            ".cfg", ".desktop", ".name", ".notice", ".license", ".about", ".rst",
            ".tmpl", ".pyi", ".apache", ".gz", ".dylib", ".nuspec", ".user", ".tests",
            ".pem", ".testcase", ".mo", ".po", ".js", ".bsd", ".typed"
        ];

        private static bool IsIgnoredExt(string file)
            => Ext.Contains(Path.GetExtension(file).ToLowerInvariant());

        private static bool IsIgnoredDir(string file)
        {
            var s = Path.DirectorySeparatorChar;
            return file.Contains($"{s}.venv{s}") ||
                   file.Contains($"{s}bin{s}Debug{s}") ||
                   file.Contains($"{s}bin{s}Release{s}");
        }
    }
}