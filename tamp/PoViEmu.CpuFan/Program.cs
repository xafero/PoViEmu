﻿using System;
using System.IO;
using System.Linq;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Hardware;
using static PoViEmu.Common.FileHelper;

namespace PoViEmu.CpuFan
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var root = Environment.CurrentDirectory;
            var folder = Path.GetFullPath(Path.Combine(root,
                "..", "..", "..", "SimuHacks", "Projs", "Raw"));
            Console.WriteLine($"Root = {folder}");

            foreach (var (file, bytes) in FindLoadFiles(folder, ".com")
                         .OrderBy(j => j.Item2.Length))
            {
                var name = Path.GetFileName(file);
                Console.WriteLine($" * {name} --> {bytes.Length} bytes");

                var m = new MachineState();
                m.InitForCom();

                var c = new StateCodeReader(m);

                // m.WriteMemory();
            }
        }
    }
}