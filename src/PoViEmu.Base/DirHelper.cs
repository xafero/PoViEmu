using System;
using System.IO;
using System.Linq;

namespace PoViEmu.Base
{
    public static class DirHelper
    {
        public static string GetCurrentDirectory()
        {
            var root = Environment.CurrentDirectory;
            root = root.Replace(Path.Combine("bin", "Debug", "net8.0"), "");
            return root;
        }
        
        public static string GetFullPath(string root, params string[] subs)
        {
            var args = new[] { root }.Concat(subs).ToArray();
            var dir = Path.Combine(args);
            return Path.GetFullPath(dir);
        }
        
        public static string GetOrCreateDir(this string root, params string[] dirs)
        {
            var target = Path.GetFullPath(Path.Combine(root, Path.Combine(dirs)));
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            return target;
        }
    }
}