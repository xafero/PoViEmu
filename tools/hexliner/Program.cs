using System;
using System.Linq;

namespace HexLiner
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var root = Environment.CurrentDirectory;
            var cmd = args == null || args.Length == 0 ? string.Empty : args.First();
            switch (cmd)
            {
                case "l":
                    Liner.DoMain(root);
                    break;
                case "a":
                    Analyser.DoMain(root);
                    break;
                default:
                    Console.WriteLine("No args given! Try 'a' or 'l'...");
                    break;
            }
        }
    }
}