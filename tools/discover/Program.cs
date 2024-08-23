using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace discover
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var folder = args.FirstOrDefault()!;

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            var dict = new Dictionary<string, string>();

            foreach (var file in files)
            {
                var bytes = File.ReadAllBytes(file);
                var hex = Convert.ToHexString(bytes);
                dict[file] = hex;
            }

            foreach (var hex in dict.Values.OrderBy(x => x.Length))
            {
                Console.WriteLine($" * Searching {hex.Length / 2} hex: {string.Join("", hex.Take(10))}");
                var it = dict.Where(d => d.Value.Contains(hex)).ToArray();
                if (it.Length < 2)
                    continue;

                foreach (var item in it)
                {
                    var idx = item.Value.IndexOf(hex, StringComparison.InvariantCultureIgnoreCase);
                    var end = idx + (hex.Length / 2);
                    Console.WriteLine("   # " + item.Key + ", at " + idx + " until " + end);
                }
            }
        }
    }
}