using System;
using PoViEmu.Common;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Xml.Linq;
using PoViEmu.Core.Compat;
using PoViEmu.Core.Hardware;
using static PoViEmu.Common.FileHelper;

// ReSharper disable CoVariantArrayConversion

namespace PoViEmu.Workbook
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var root = DirHelper.GetCurrentDirectory();
            var folder = DirHelper.GetFullPath(root, "..", "..",
                "test", "PoViEmu.Tests", "Resources", "Compat");
            Console.WriteLine($"Root = {folder}");

            var outDir = "output".GetOrCreateDir();
            Console.WriteLine($"Dest = {outDir}");

            var files = FindLoadFiles(folder, ".com")
                .ToImmutableSortedDictionary(k => Path.GetFileNameWithoutExtension(k.file), v => v);
            foreach (var (key, (name, bytes)) in files)
            {
                var label = Path.GetFileNameWithoutExtension(name);
                Console.WriteLine($" * Executing {label} with {bytes.Length} bytes...");

                var title = $"Report ({DateTime.Now:u})";
                var doc = HtmlHelper.CreateDoc(title);

                var head = (XElement)doc.Root!.FirstNode!;
                head.Add(new XElement("style", HtmlHelper.ToText(
                    "body { font-family: Arial, sans-serif; margin: 20px; }",
                    "table { width: 100%; border-collapse: collapse; margin-top: 20px; }",
                    "th, td { border: 1px solid #ccc; padding: 10px; text-align: left; }",
                    "th { background-color: #f4f4f4; }")));

                var table = HtmlHelper.CreateTable();

                var thead = (XElement)table.FirstNode!;
                thead.Add(new XElement("tr",
                    HtmlHelper.Repeat("th", "Seg:Off", "Hex", "Instruction", "Changes"))
                );

                var c = new NC3022();
                var m = new MachineState();
                m.InitForCom(loadSeg: 0x0750, cxInit: 0x002C, axInit: 0xFFFF);
                m.WriteMemory(m.CS, m.IP, bytes);

                var changes = new List<string>();
                m.PropertyChanged += (_, eventArgs) =>
                {
                    var propName = eventArgs.PropertyName;
                    var propValue = m[propName];
                    changes.Add($" {propName}={propValue.Format()}");
                };

                var tbody = (XElement)table.LastNode!;
                var reader = new StateCodeReader(m);
                var count = 0;

                var stateBeg = m.ToString();
                tbody.Add(new XElement("tr", HtmlHelper.ColSpan(4, stateBeg)));

                while (!c.Halted)
                {
                    changes.Clear();
                    var current = reader.NextInstruction();

                    var pre = $"{m.CS:X4}:{current}";
                    var o = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
                    var tmp = pre.Split("   ", 3, o);
                    var it1 = tmp[0];
                    var it2 = tmp[1];
                    var it3 = tmp[2];
                    try
                    {
                        c.Execute(current, m);

                        if (GetDosOut(c, out var stdOut, out var retNum))
                        {
                            if (stdOut != null) changes.Add($"--> '{stdOut}'");
                            if (retNum != null) changes.Add($"--> {retNum}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($" {ex.GetType().Name}: {ex.Message}");
                        c.Halted = true;
                    }

                    var it4 = string.Join(" ", changes);
                    tbody.Add(new XElement("tr",
                        HtmlHelper.Repeat("td", it1, it2, it3, it4))
                    );
                    if (count++ >= 250) break;
                }

                var stateEnd = m.ToString();
                tbody.Add(new XElement("tr", HtmlHelper.ColSpan(4, stateEnd)));

                var body = (XElement)doc.Root!.LastNode!;
                body.Add(table);

                var xml = HtmlHelper.AsBytes(doc);
                var xFile = Path.Combine(outDir, $"{key}.html");
                File.WriteAllBytes(xFile, xml);
            }

            Console.WriteLine("Done.");
        }

        private static bool GetDosOut(NC3022 c, out string stdTxt, out string retNum)
        {
            stdTxt = retNum = null;
            var dos = c.GetDOS();
            var stdOut = dos.StdOut;
            var stdBld = stdOut.GetStringBuilder();
            if (stdBld.Length >= 1)
            {
                stdTxt = $"{stdOut}";
                stdBld.Clear();
            }
            if (dos.ReturnCode is { } retCode)
            {
                retNum = $"{retCode:X4}";
            }
            return stdTxt != null || retNum != null;
        }
    }
}