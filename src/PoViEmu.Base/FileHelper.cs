using System;
using System.Collections.Generic;
using System.IO;

namespace PoViEmu.Base
{
    public static class FileHelper
    {
        private const StringComparison Inv = StringComparison.InvariantCultureIgnoreCase;

        public static IEnumerable<(string file, byte[] bytes)> FindLoadFiles(string root, string end)
        {
            const SearchOption o = SearchOption.AllDirectories;
            foreach (var file in Directory.EnumerateFiles(root, "*.*", o))
            {
                var ext = Path.GetExtension(file);
                if (!ext.Equals(end, Inv))
                    continue;
                var bytes = File.ReadAllBytes(file);
                yield return (file, bytes);
            }
        }
    }
}