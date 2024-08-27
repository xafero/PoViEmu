using System;
using System.IO;
using System.Security.Cryptography;
using PoViEmu.Common;
using PoViEmu.Core;
using PoViEmu.Core.Dumps;
using PoViEmu.Core.Images;
using SixLabors.ImageSharp;

namespace Discover
{
    internal static class ReadBins
    {
        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            foreach (var file in files)
            {
                if (IsIgnoredFolder(file))
                    continue;

                var ext = Path.GetExtension(file);
                if (IsIgnoredFile(ext))
                    continue;

                try
                {
                    var aInfo = new { File = file, Obj = ReadAddIn(file, out var aHex, out var aLen) };

                    Console.WriteLine(JsonHelper.ToJson(aInfo) + " / " + aHex + " / " + aLen);

                    continue;
                }
                catch (Exception)
                {
                    // ignored
                }

                try
                {
                    var dInfo = new { File = file, Obj = ReadDump(file, out var dHex, out var dLen) };

                    Console.WriteLine(JsonHelper.ToJson(dInfo) + " / " + dHex + " / " + dLen);

                    continue;
                }
                catch (Exception)
                {
                    // ignored
                }

                Console.Error.WriteLine($" * Could not read '{file}'!");
            }
        }

        private static bool IsIgnoredFile(string ext)
        {
            return ext == ".json" || ext == ".hex" || ext == ".ttf" || ext == ".png" || ext == ".cs" ||
                   ext == ".axaml" || ext == ".props" || ext == ".cache" || ext == ".targets" ||
                   ext == ".csproj" || ext == ".py" || ext == ".sh" || ext == ".gitignore" ||
                   ext == ".sln" || ext == ".md" || ext == ".yml" || ext == ".manifest" ||
                   ext == ".ico" || ext == ".dmp" || ext == ".txt" || ext == "";
        }

        private static bool IsIgnoredFolder(string file)
        {
            return file.Contains("/bin/Debug/") || file.Contains("/obj/Debug/") ||
                   file.Contains("/.git/") || file.Contains("/parent/");
        }

        private static AddInInfoPlus<Image> ReadAddIn(string file, out string hex, out int len)
        {
            var bytes = File.ReadAllBytes(file);
            hex = HashThis(bytes);
            len = bytes.Length;
            var info = AddInReader.Read(bytes);
            var plus = info.WithImages(bytes);
            return plus;
        }

        private static DumpInfo ReadDump(string file, out string hex, out int len)
        {
            var bytes = File.ReadAllBytes(file);
            hex = HashThis(bytes);
            len = bytes.Length;
            var info = DumpReader.Read(bytes);
            var mem = new MemoryStream(bytes);
            info.LoadOsAddIns(mem);
            return info;
        }

        private static string HashThis(byte[] bytes)
        {
            var hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash);
        }
    }
}