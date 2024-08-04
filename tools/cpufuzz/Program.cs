using System;
using System.IO;

namespace CpuFuzzer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var root = Environment.CurrentDirectory;
            root = Path.GetFullPath(root);
            Console.WriteLine($"Root = {root}");

            Console.WriteLine("Done.");
        }
    }
}