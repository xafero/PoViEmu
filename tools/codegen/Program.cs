using System;
using System.IO;
using System.Linq;
using PoViEmu.CodeGen.App;

namespace PoViEmu.CodeGen
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
                case "test":
                    TestFuzz.Start();
                    break;
                default:
                    CodeFuzz.Start();
                    break;
            }

            Console.WriteLine("Done.");
        }
    }
}