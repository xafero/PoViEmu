using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HexLiner
{
    internal static class Liner
    {
        public static void DoMain(string root)
        {
            Console.WriteLine($"Root = {root}");

            var hexes = new SortedSet<string>();
            var humms = new SortedSet<string>();
            const int size = 170;

            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                using var stream = File.OpenRead(file);
                var buffer = new byte[size];
                var gotSize = stream.Read(buffer, 0, buffer.Length);
                if (gotSize != buffer.Length)
                    continue;

                var hex = Convert.ToHexString(buffer);
                var hum = Encoding.ASCII.GetString(buffer)
                    .Replace('\n', ' ').Replace('\r', ' ').Trim();
                var text = $"{hex} => {file}";
                Console.WriteLine(text);
                hexes.Add(text);
                var txxt = $"{hum} => {file}";
                humms.Add(txxt);
            }

            File.WriteAllLines("hexdumps.txt", hexes, Encoding.UTF8);
            File.WriteAllLines("humdumps.txt", humms, Encoding.UTF8);

            Console.WriteLine("Done.");
        }
    }
}