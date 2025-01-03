using System.Linq;

namespace PoViEmu.Base
{
    public static class PropHelper
    {
        public static (string? key, string? arg) SplitPropName(string? name)
        {
            var parts = name?.Split('|') ?? [];
            var key = parts.FirstOrDefault();
            var arg = parts.Length >= 2 ? parts.Skip(1).First() : null;
            return (key, arg);
        }
    }
}