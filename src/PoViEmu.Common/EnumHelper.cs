using System;
using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Common
{
    public static class EnumHelper<T> where T : struct, Enum
    {
        private static readonly SortedDictionary<byte, T> Mapping;

        static EnumHelper()
        {
            Mapping = new SortedDictionary<byte, T>(new DescendingComparer<byte>());
            var keys = Enum.GetValuesAsUnderlyingType<T>().Cast<byte>().ToArray();
            var vals = Enum.GetValues<T>();
            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                var val = vals[i];
                Mapping[key] = val;
            }
        }

        public static T? FindFlag(byte rawVal, int? shift = null)
        {
            var value = (byte)(shift == null ? rawVal : rawVal << shift);
            foreach (var (key, val) in Mapping)
                if ((key & value) == key)
                    return val;
            return null;
        }
    }
}