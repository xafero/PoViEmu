using System;
using System.IO;
using System.Linq;

namespace PoViEmu.Base
{
    public static class PathHelper
    {
        public static string CurrentDir
            => Environment.CurrentDirectory;
        
        public static string GetLast(string url)
        {
            return url.Split('/').Last();
        }
        
        public static string GetDirFor(this string root, string file, params string[] dirs)
        {
            return Path.Combine(Path.Combine(root, Path.Combine(dirs)), file);
        }
        
        public static string MakeDirFor(this string root, string file, params string[] dirs)
        {
            var target = Path.Combine(root, Path.Combine(dirs));
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            return Path.Combine(target, file);
        }
        
        public static string? Normalize(string path)
        {
            return path.Replace('\\', Path.DirectorySeparatorChar).TrimNull();
        }
        
        public static string? TryGetDep(string first, string secondRaw)
        {
            var second = Normalize(secondRaw);
            if (second == null || !File.Exists(first))
                return null;
            var dir = Path.GetDirectoryName(first);
            if (dir == null)
                return null;
            var inv = StringComparison.InvariantCultureIgnoreCase;
            var o = SearchOption.AllDirectories;
            var current = Path.Combine(dir, second);
            while (!File.Exists(current) && dir.Length > 3)
            {
                dir = Path.GetFullPath(Path.Combine(dir, ".."));
                current = Path.Combine(dir, second);
                if (File.Exists(current))
                    continue;
                var foundFile = Directory.EnumerateFiles(dir, "*.*", o)
                    .Where(f => f.Length == current.Length)
                    .FirstOrDefault(f => f.Equals(current, inv));
                if (foundFile != null)
                    current = foundFile;
            }
            return current;
        }
    }
}