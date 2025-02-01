using System.IO;

namespace PoViEmu.UI.Extra
{
    public static class PathHelper
    {
        public static string GetChild(this string root, string sub)
        {
            return Path.Combine(root, sub);
        }
    }
}