using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Dumps;
using PoViEmu.Core.Inventory;
using static PoViEmu.Core.Inventory.StockUtil;

namespace Discover
{
    internal static class ReadBins
    {
        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);

            var container = new RepoEntry();
            var addIns = container.AddIn;
            var system = container.System;
            var bios = container.Bios;

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            foreach (var file in files)
            {
                if (IsIgnoredFolder(file))
                    continue;

                var ext = Path.GetExtension(file);
                if (IsIgnoredFile(ext))
                    continue;

                var localFile = file.Replace(folder, string.Empty).TrimStart('/');
                var localName = Path.GetFileNameWithoutExtension(file).Replace('_', ' ').Title();

                try
                {
                    var aInfo = new { File = file, Obj = ReadAddIn(file, out var aHex, out var aLen) };

                    var aModel = $"{aInfo.Obj.Info.Model}";
                    if (!addIns.TryGetValue(aModel, out var aDict1))
                        addIns[aModel] = aDict1 = new SortedDictionary<string,
                            IDictionary<string, IDictionary<string, List<AddInEntry>>>>();
                    
                    var aLName = aInfo.Obj.Info.Name.TrimNull() ?? localName;
                    var aName = aLName.ToUpperInvariant();
                    if (!aDict1.TryGetValue(aName, out var aDict2))
                        aDict1[aName] = aDict2 = new SortedDictionary<string, IDictionary<string, List<AddInEntry>>>();

                    var aVer = $"{aInfo.Obj.Info.Version}";
                    if (!aDict2.TryGetValue(aVer, out var aDict3))
                        aDict2[aVer] = aDict3 = new SortedDictionary<string, List<AddInEntry>>();

                    if (!aDict3.TryGetValue(aHex, out var aDict4))
                        aDict3[aHex] = aDict4 = new List<AddInEntry>();

                    _ = JsonHelper.ToJson(aInfo);

                    var aEntry = new AddInEntry
                    {
                        Path = localFile,
                        Name = aLName,
                        Version = aInfo.Obj.Info.Version,
                        Compiled = aInfo.Obj.Info.Compiled,
                        Size = aLen,
                        MenuIcon = aInfo.Obj.OffsetIcon.ToImageObj(),
                        ListIcon = aInfo.Obj.OffsetLIcon.ToImageObj()
                    };
                    aDict4.Add(aEntry);
                    continue;
                }
                catch (Exception)
                {
                    // ignored
                }

                try
                {
                    var dInfo = new { File = file, Obj = ReadDump(file, out var dHex, out var dLen) };
                    var dModel = $"{dInfo.Obj.Model}";

                    _ = JsonHelper.ToJson(dInfo);

                    if (dInfo.Obj.DeviceModel == DumpModel.Unknown && dInfo.Obj.AddIns.Count == 0)
                    {
                        if (!bios.TryGetValue(dModel, out var dictB))
                            bios[dModel] = dictB = new List<object>();

                        var bEntry = new BiosEntry
                        {
                            Path = localFile,
                            Name = localName,
                            Size = dLen
                        };

                        var x = (List<object>)dictB;
                        x.Add(bEntry);
                        continue;
                    }

                    if (!system.TryGetValue(dModel, out var dictS))
                        system[dModel] = dictS = new List<object>();

                    var dAdds = dInfo.Obj.AddIns.Select(a =>
                        a.Value.Name.TrimNull() ?? a.Value.Mode.ToString()).OrderBy(x => x).ToArray();
                    var sEntry = new SystemEntry
                    {
                        Path = localFile,
                        Name = localName,
                        Size = dLen,
                        AddIns = dAdds
                    };

                    var y = (List<object>)dictS;
                    y.Add(sEntry);
                    continue;
                }
                catch (Exception)
                {
                    // ignored
                }

                Console.Error.WriteLine($" * Could not read '{file}'!");
            }

            var json = JsonHelper.ToJson(new
            {
                AddIns = addIns, System = system, Bios = bios
            });
            File.WriteAllText("repo.json", json, Encoding.UTF8);
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
    }
}