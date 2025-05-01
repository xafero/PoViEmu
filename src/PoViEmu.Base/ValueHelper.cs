using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PoViEmu.Base
{
    public static class ValueHelper
    {
        public static readonly CultureInfo Ignore = CultureInfo.InvariantCulture;

        public static T?[]? AsFuncArray<T>(string[]? args, Func<string, T[], bool> parse) where T : struct
        {
            var res = new T[1];
            return args?.Select(txt => parse(txt, res)
                ? res[0]
                : default(T?)).ToArray();
        }

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
        
        public static int?[]? AsIntArray(IEnumerable<string>? args)
        {
            return args?.Select(txt => int.TryParse(txt, Ignore, out var value)
                ? value
                : default(int?)).ToArray();
        }
        
        public static bool?[]? AsBoolArray(IEnumerable<string>? args)
        {
            return args?.Select(txt => bool.TryParse(txt, out var value)
                ? value
                : default(bool?)).ToArray();
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