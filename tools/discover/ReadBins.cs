using System;
using System.IO;

namespace Discover
{
    internal static class ReadBins
    {
        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            // TODO
            Console.WriteLine(folder);
        }
    }
}