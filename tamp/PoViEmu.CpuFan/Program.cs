using System;
using System.IO;
using System.Linq;
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
                "..", "..", "..", "SimuHacks", "Projs", "Raw"));
            Console.WriteLine($"Root = {folder}");

            foreach (var (file, bytes) in FindLoadFiles(folder, ".com")
                         .OrderBy(j => j.bytes.Length)
                         .Where(j => j.file
                             .Contains("check2", StringComparison.InvariantCultureIgnoreCase)))
            {
                var name = Path.GetFileName(file);
                Console.WriteLine($" * {name} --> {bytes.Length} bytes");

                var c = new NC3022c();
                var m = new MachineState();
                m.InitForCom();
                m.WriteMemory(m.CS, m.IP, bytes);

                var reader = new StateCodeReader(m);
                var count = 0;
                while (!c.Halted)
                {
                    var current = reader.NextInstruction();
                    Console.WriteLine(current);

                    c.Execute(current, m);
                    if (count++ >= 50) break;
                }

                var dos = (DOSInterrupts)c.InterruptTable[0x21];
                Console.WriteLine($" '{dos.StdOut}' => {dos.ReturnCode}");

                break;
            }
        }
    }
}