using System;

namespace PoViEmu.Base
{
    public static class EnumHelper
    {
        public static T Parse<T>(object? parameter) where T : struct, Enum
        {
            if (parameter is T pT)
                return pT;
            if (parameter is string pS && Enum.TryParse<T>(pS, ignoreCase: true, out var resS))
                return resS;
            if (parameter?.ToString() is { } pO && Enum.TryParse<T>(pO, out var resO))
                return resO;

            // No match at all!
            return default;
        }
    }

    public static class CollHelper
    {
        public static bool IsContained(this byte[] first, byte[] second)
        {
            for (var i = 0; i < first.Length && i < second.Length; i++)
            {
                var one = first[i];
                var two = second[i];
                if (one != two)
                    return false;
            }
            return true;
        }
    }
}