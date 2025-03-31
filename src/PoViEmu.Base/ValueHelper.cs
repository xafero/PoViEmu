using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PoViEmu.Base
{
    public static class ValueHelper
    {
        public static readonly CultureInfo Ignore = CultureInfo.InvariantCulture;

        public static double?[]? AsDoubleArray(IEnumerable<string>? args)
        {
            return args?.Select(txt => double.TryParse((string?)txt, Ignore, out var value)
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