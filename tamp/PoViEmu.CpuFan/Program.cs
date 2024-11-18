using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Core.Compat;
using PoViEmu.Core.Hardware;
using static PoViEmu.Common.FileHelper;

namespace PoViEmu.CpuFan
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var root = Environment.CurrentDirectory;
            root = root.Replace(Path.Combine("bin", "Debug", "net8.0"), "");
            var folder = Path.GetFullPath(Path.Combine(root,
                "..", "..", "test", "PoViEmu.Tests", "Resources", "Compat"));
            Console.WriteLine($"Root = {folder}");

            var ic = StringComparer.InvariantCultureIgnoreCase;
            foreach (var (file, bytes) in FindLoadFiles(folder, ".com")
                         .OrderBy(j => j.bytes.Length))
            {
                if (new[]
                    {
                        "Op_outsb", "Op_scasb", "Op_outsw", "Op_scasw", "Op_pushf",
                        "Op_pusha", "Op_cbw", "Op_cmc", "Op_das", "Op_aaa", "Op_std",
                        "Op_aad", "Op_aas", "Op_enter", "Op_stc", "Op_movsb", "Op_out",
                        "Op_in", "Op_movsw", "Op_lopne", "Op_loope", "Op_lopnz",
                        "Op_sti", "Op_aam", "Op_cwd", "Op_into"
                    }
                    .Contains(Path.GetFileNameWithoutExtension(file), ic))
                    continue;

                var name = Path.GetFileName(file);
                var fileTxt = file.Replace(".com", ".txt");
                var comEx = File.ReadAllText(fileTxt, Encoding.UTF8);

                var c = new NC3022();
                var m = new MachineState();
                m.InitForCom();
                m.WriteMemory(m.CS, m.IP, bytes);

                var lines = new List<string>();
                var reader = new StateCodeReader(m);
                var count = 0;
                while (!c.Halted)
                {
                    var current = reader.NextInstruction();
                    lines.Add($"{current}");
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

                PrintOut(name, bytes, lines, comEx, c);
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
            Console.WriteLine($" '{stdOut}' => {dos.ReturnCode} --> '{comEx}'");
        }
    }
}