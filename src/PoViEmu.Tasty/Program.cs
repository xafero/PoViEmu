using System;
using Newtonsoft.Json;

// ReSharper disable HeuristicUnreachableCode

namespace PoViEmu.Tasty
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var x = JsonConvert.SerializeObject(new { x = new VersionInfo() }, Formatting.Indented);
            Console.WriteLine(x);
        }
    }
}