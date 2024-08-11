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

        public static byte HaveComplement(this byte value, out bool isNeg)
        {
            if ((value & 0x80) == 0)
            {
                isNeg = false;
                return value;
            }
            isNeg = true;
            var res = ~value;
            res += 1;
            return (byte)res;
        }

        public static short HaveComplement(this short value, out bool isNeg)
        {
            if ((value & 0x8000) == 0)
            {
                isNeg = false;
                return value;
            }
            isNeg = true;
            var res = ~value;
            res += 1;
            return (short)res;
        }
    }
}