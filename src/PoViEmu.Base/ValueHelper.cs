using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PoViEmu.Base
{
    public static class ValueHelper
    {
        public static readonly CultureInfo Ignore = CultureInfo.InvariantCulture;

        public static T?[]? AsEnumArray<T>(string[]? args) where T : struct, Enum
        {
            return args?.Select(txt => Enum.TryParse<T>(txt, ignoreCase: true, out var value)
                ? value
                : default(T?)).ToArray();
        }

        public static double?[]? AsDoubleArray(IEnumerable<string>? args)
        {
            return args?.Select(txt => double.TryParse(txt, Ignore, out var value)
                ? value
                : default(double?)).ToArray();
        }

        public static string[]? AsStringArray(object? raw, string sep = ";")
        {
            return raw switch
            {
                IEnumerable<string> es => es.ToArray(),
                string txt => txt.Split(sep),
                _ => null
            };
        }
    }
}