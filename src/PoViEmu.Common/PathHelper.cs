using System;
using System.IO;

namespace PoViEmu.Common
{
    public static class PathHelper
    {
        public static string GetShortName(string root, string file)
            => file.Replace(root, string.Empty).TrimStart(Path.DirectorySeparatorChar);

        public static string CurrentDir
            => Environment.CurrentDirectory;
    }
}