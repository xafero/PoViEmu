using System.IO;
using System.Reflection;

namespace PoViEmu.Common
{
    public static class SysInfo
    {
        public static string GetEntryDir(Assembly? assembly = null)
        {
            var ass = assembly ?? Assembly.GetEntryAssembly();
            var full = Path.GetFullPath(ass?.Location ?? string.Empty);
            full = Path.GetDirectoryName(full) ?? string.Empty;
            return full;
        }
    }
}