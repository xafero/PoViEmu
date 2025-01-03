using System;
using System.IO;
using PoViEmu.Base;

namespace PoViEmu.Workbook.CPU
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var root = DirHelper.GetCurrentDirectory();
            var folder = DirHelper.GetFullPath(root, "..", "..",
                "test", "PoViEmu.Tests.CPU", "Resources", "Chimes");
            if (args.Length == 1)
                folder = Path.GetFullPath(args[0]);
            Console.WriteLine($"Root = {folder}");

            HtmlRunner.Start(folder);
        }
    }
}