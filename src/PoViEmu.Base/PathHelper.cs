using System;
using System.IO;
using System.Linq;

namespace PoViEmu.Base
{
    public static class PathHelper
    {
        public static string CurrentDir
        {
            get
            {
                var dir = Environment.CurrentDirectory;
                if (dir == "/")
                {
                    // We could be running on Android here
                    dir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                }
                return dir;
            }
        }

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

        public static string? Normalize(string? path)
        {
            return path?.Replace('\\', Path.DirectorySeparatorChar).TrimNull();
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

        public static string? Combine(params string[] paths)
        {
            var parts = paths.Select(Normalize).ToArray();
            if (parts.Any(string.IsNullOrWhiteSpace))
                return null;
            var path = Path.Combine(parts!);
            return Path.GetFullPath(path);
        }

        public static string? Merge(string first, string second)
        {
            var firstPath = Normalize(first)!;
            var secondPath = Normalize(second)!;
            var sep = Path.DirectorySeparatorChar;
            var partsF = firstPath.Split(sep);
            var partsS = secondPath.Split(sep);
            var union = partsF.Intersect(partsS)
                .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
            if (union == null)
                return null;
            var tmp = sep + union + sep;
            var firstSt = firstPath.Split(tmp, 2);
            var secondSt = secondPath.Split(tmp, 2);
            var res = Path.Combine(firstSt[0], union, secondSt[1]);
            return res;
        }
    }
}