using System;
using System.IO;
using System.Linq;
using PoViEmu.CpuFuzzer.App;

namespace PoViEmu.CpuFuzzer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var root = Environment.CurrentDirectory;
            root = Path.GetFullPath(root);
            Console.WriteLine($"Root = {root}");
            
            var arg0 = args.FirstOrDefault();
            switch (arg0)
            {
                case "cpu":
                    InstrFuzz.Start();
                    break;
            }

            Console.WriteLine("Done.");
        }
    }
}