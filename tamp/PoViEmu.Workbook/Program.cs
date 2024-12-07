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
                    HtmlHelper.Repeat("th", "Product", "Quantity", "Price", "Total"))
                );

                var tbody = (XElement)table.LastNode!;
                tbody.Add(new XElement("tr",
                    HtmlHelper.Repeat("td", "item1", "item2", "item3", "item4"))
                );

                var body = (XElement)doc.Root!.LastNode!;
                body.Add(table);

                var xml = HtmlHelper.AsBytes(doc);
                var xFile = Path.Combine(outDir, $"{key}.html");
                File.WriteAllBytes(xFile, xml);

                /*
                var c = new NC3022();
                var m = new MachineState();
                m.InitForCom(loadSeg: 0x0750, cxInit: 0x002C, axInit: 0xFFFF);
                m.WriteMemory(m.CS, m.IP, bytes);

                var reader = new StateCodeReader(m);
                var lines = new List<string>();
                var count = 0;

                while (!c.Halted)
                {
                    var current = reader.NextInstruction();
                    lines.Add("");
                    lines.AddRange(m.ToRegDebugLin());
                    lines.Add($"{m.CS:X4}:{current}");
                    try
                    {
                        c.Execute(current, m);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($" {ex.GetType().Name}: {ex.Message}");
                        c.Halted = true;
                    }
                    if (count++ >= 150) break;
                }
                */
            }

            Console.WriteLine("Done.");
        }

        private static void PrintOut(string name, byte[] bytes,
            List<string> lines, string comEx, NC3022 c)
        {
            var dos = c.GetDOS();
            var stdOut = $"{dos.StdOut}";
            if (stdOut == comEx)
                return;
            Console.WriteLine($" * {name} --> {bytes.Length} bytes");
            foreach (var line in lines)
                Console.WriteLine(line);
            Console.WriteLine($" '{stdOut}' => {dos.ReturnCode:X4} --> '{comEx}'");
        }
    }
}