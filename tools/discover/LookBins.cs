using System;
using System.IO;
using PoViEmu.Base;
using PoViEmu.Base.ABI.Twf;
using PoViEmu.I186.ABI;
using PoViEmu.I186.ABI.Dumps;
using PoViEmu.Inventory.Mime;
using PoViEmu.SH3.ABI;
using MT = PoViEmu.Inventory.Mime.MimeType;

namespace Discover
{
    internal static class LookBins
    {
        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);
            Console.WriteLine($"Root = {folder}");

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            foreach (var file in files)
            {
                if (ReadBins.IsIgnoredFolder(file))
                    continue;

                var ext = Path.GetExtension(file);
                if (ReadBins.IsIgnoredFile(ext))
                    continue;

                var label = file.Replace(folder, string.Empty).Trim('/');
                Console.Write($" * {label}");

                var bytes = File.ReadAllBytes(file);
                var mime = bytes.GetMimeType();
                if (mime == null)
                {
                    Console.WriteLine($" --> ?");
                    continue;
                }

                var jFile = file.Replace(Path.GetExtension(file), ".json");
                Console.WriteLine($" --> {mime}");

                var obj = mime.Value.Load(bytes);
                if (obj is AddInInfo aii)
                {
                    JsonHelper.TrySaveToFile(aii, jFile, out _);
                    continue;
                }
                if (obj is DumpInfo dii)
                {
                    JsonHelper.TrySaveToFile(dii, jFile, out _);
                    continue;
                }
                if (obj is PvaInfo pii)
                {
                    JsonHelper.TrySaveToFile(pii, jFile, out _);
                    continue;
                }
                if (obj is TwfInfo mii)
                {
                    mii.SetFile(file);
                    JsonHelper.TrySaveToFile(mii, jFile, out _);
                    continue;
                }
                if (mime is MT.V30PvChip or MT.SH3PvDump)
                    continue;
                throw new InvalidOperationException($"{mime} | {obj} | {obj?.GetType()}");
            }
        }
    }
}