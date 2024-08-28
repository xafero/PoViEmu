using System;
using System.IO;
using System.Linq;

namespace PoViEmu.Common
{
    public static class PathHelper
    {
        public static string GetShortName(string root, string file)
            => file.Replace(root, string.Empty).TrimStart(Path.DirectorySeparatorChar);

        public static string CurrentDir
            => Environment.CurrentDirectory;

        public static string GetChild(this string root, string sub)
        {
            return Path.Combine(root, sub);
        }

        public static string MakeDirFor(this string root, string file, params string[] dirs)
        {
            var target = Path.Combine(root, Path.Combine(dirs));
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            return Path.Combine(target, file);
        }

        public static string GetLast(string url)
        {
            return url.Split('/').Last();
        }
    }
}