using System;
using PoViEmu.Common;
using PoViEmu.Core.Addins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Compat;
using PoViEmu.Core.Hardware;
using static PoViEmu.Common.FileHelper;

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

            var files = FindLoadFiles(folder, ".com").OrderBy(j => j.bytes.Length);
            foreach (var (file, bytes) in files)
            {
                Console.WriteLine(" ? " + file + " " + bytes.Length);

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
            }
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