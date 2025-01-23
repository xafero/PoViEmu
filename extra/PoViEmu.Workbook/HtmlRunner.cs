using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using PoViEmu.Base;
using PoViEmu.Base.CPU;
using PoViEmu.Base.ISA;
using DefI = PoViEmu.I186.CPU.Impl.Defaults;
using DefS = PoViEmu.SH3.CPU.Impl.Defaults;
using static PoViEmu.Base.FileHelper;
using static PoViEmu.Workbook.SoftUtil;

// ReSharper disable CoVariantArrayConversion

namespace PoViEmu.Workbook
{
    internal static class HtmlRunner
    {
        public static void Start(string folder)
        {
            var outDir = "output".GetOrCreateDir();
            Console.WriteLine($"Dest = {outDir}");

            var files = FindLoadFiles(folder, ".com")
                .ToImmutableSortedDictionary(k => Path.GetFileNameWithoutExtension(k.file), v => v);
            foreach (var (key, (name, bytes)) in files)
            {
                var label = Path.GetFileNameWithoutExtension(name);
                var pkIdx = label.LastIndexOf('_');
                var pkLbl = pkIdx == -1 ? "x86" : label[(pkIdx + 1)..];
                Console.WriteLine($" * Executing {label} with {bytes.Length} bytes on {{{pkLbl}}}...");

                var is86 = pkLbl == "x86";
                var title = $"Report ({DateTime.Now:u})";
                var doc = HtmlHelper.CreateDoc(title);

                var head = (XElement)doc.Root!.FirstNode!;
                head.Add(new XElement("style", HtmlHelper.ToText(
                    "body { font-family: Arial, sans-serif; margin: 20px; }",
                    "table { width: 100%; border-collapse: collapse; margin-top: 20px; }",
                    "th, td { border: 1px solid #ccc; padding: 10px; text-align: left; }",
                    "th { background-color: #f4f4f4; }")));

                var table = HtmlHelper.CreateTable();
                var fmt = is86 ? DefI.ValFormatter : DefS.ValFormatter;
                var adrHdr = fmt.GetAdrTitle();

                var thead = (XElement)table.FirstNode!;
                thead.Add(new XElement("tr",
                    HtmlHelper.Repeat("th", adrHdr, "Hex", "Instruction", "Changes"))
                );

                ICpu cpu;
                INotifyPropertyChanged m;
                Func<string, object> fetchProp;
                Action<IInstruction> execThis;
                Func<IInstruction> readNext;
                if (is86)
                {
                    var cpuFi = DefI.CpuFactory;
                    var cpuI = cpuFi.CreateCpu(bytes, out var m1);
                    var cpuRi = cpuFi.CreateReader(m1);
                    fetchProp = nn => m1[nn];
                    execThis = ni => cpuI.Execute((I186.ISA.Decoding.XInstruction)ni, m1);
                    readNext = () => cpuRi.NextInstruction();
                    cpu = cpuI;
                    m = m1;
                }
                else
                {
                    var cpuFs = DefS.CpuFactory;
                    var cpuS = cpuFs.CreateCpu(bytes, out var m2);
                    var cpuRs = cpuFs.CreateReader(m2);
                    fetchProp = nn => m2[nn];
                    execThis = ni => cpuS.Execute((SH3.ISA.Decoding.XInstruction)ni, m2);
                    readNext = () => cpuRs.NextInstruction();
                    cpu = cpuS;
                    m = m2;
                }

                var changes = new List<string>();
                m.PropertyChanged += (_, eventArgs) =>
                {
                    var propName = eventArgs.PropertyName!;
                    var propValue = fetchProp(propName);
                    changes.Add($" {propName}={fmt.Format(propValue)}");
                };

                var tbody = (XElement)table.LastNode!;
                var count = 0;

                var stateBeg = m.ToString()!;
                tbody.Add(new XElement("tr", HtmlHelper.ColSpan(4, stateBeg)));
                const int maxInstrCount = 950;

                while (!cpu.Halted)
                {
                    changes.Clear();

                    IInstruction current;
                    try {
                        current = readNext();
                    } catch (Exception ex) {
                        cpu.Halted = true;
                        current = new FailedInstr(ex);
                    }

                    var pre = fmt.GetFull(current, (IState)m);
                    var o = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
                    var tmp = pre.Split("   ", 3, o);
                    var it1 = tmp[0];
                    var it2 = tmp[1];
                    var it3 = tmp[2];

                    try
                    {
                        if (current is not FailedInstr)
                            execThis(current);

                        if (GetDosOut(cpu, out var stdOut, out var retNum))
                        {
                            if (stdOut != null) changes.Add($"--> '{stdOut}'");
                            if (retNum != null) changes.Add($"--> {retNum}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($" {ex.GetType().Name}: {ex.Message}");
                        cpu.Halted = true;
                    }

                    var it4 = string.Join(" ", changes);
                    tbody.Add(new XElement("tr",
                        HtmlHelper.Repeat("td", it1, it2, it3, it4))
                    );
                    if (count++ >= maxInstrCount) break;
                }

                var stateEnd = m.ToString()!;
                tbody.Add(new XElement("tr", HtmlHelper.ColSpan(4, stateEnd)));

                var body = (XElement)doc.Root!.LastNode!;
                body.Add(table);

                var xml = HtmlHelper.AsBytes(doc);
                var xFile = Path.Combine(outDir, $"{key}.html");
                File.WriteAllBytes(xFile, xml);
            }

            Console.WriteLine("Done.");
        }
    }
}