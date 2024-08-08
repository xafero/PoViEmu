using System;

namespace PoViEmu.Common
{
    public static class EnumTool
    {
        public static string ToSmallName<T>(this T value)
            where T : struct, Enum
        {
            return value.ToString()?.ToLowerInvariant();
        }
    }
}