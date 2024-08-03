using System;
using System.Linq;

namespace PoViEmu.Common
{
    public static class BytesHelper
    {
        public static string ToHex(this byte[] bytes, bool prependSize = true)
        {
            var hex = Convert.ToHexString(bytes);
            return prependSize ? $"({bytes.Length}) {hex}" : hex;
        }

        public static string ToBinary(this byte value)
        {
            var bits = Convert.ToString(value, 2).PadLeft(8, '0');
            return bits;
        }

        public static string ToBinary(this byte[] values)
        {
            var bits = string.Join(" ", values.Select(v => v.ToBinary()));
            return bits;
        }
    }
}