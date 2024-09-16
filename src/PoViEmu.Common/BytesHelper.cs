using System;
using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Common
{
    public static class BytesHelper
    {
        public static string ToHex(this byte[] bytes, bool prependSize = true, bool withSpace = false)
        {
            var txt = Convert.ToHexString(bytes);
            var hex = withSpace ? string.Join(' ', txt.SplitEvery(2)) : txt;
            return prependSize ? $"({bytes.Length}) {hex}" : hex;
        }

        public static string ToBinary(this byte value)
        {
            var bits = Convert.ToString(value, 2).PadLeft(8, '0');
            return bits;
        }

        public static string ToBinary(this byte[] values, string sep = "", int skip = 0)
        {
            var bits = string.Join(sep, values.Skip(skip).Select(v => v.ToBinary()));
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

        public static byte[] Allocate(int mb)
        {
            var data = new byte[mb * 1024 * 1024];
            Array.Clear(data, 0, data.Length);
            return data;
        }

        public static IEnumerable<byte[]> SplitEvery(this byte[] text, int count)
        {
            var bld = new List<byte>();
            for (var i = 0; i < text.Length; i += count)
            {
                bld.Clear();
                for (var j = 0; j < count; j++)
                {
                    var idx = i + j;
                    if (idx >= text.Length)
                        break;
                    bld.Add(text[idx]);
                }
                yield return bld.ToArray();
            }
        }

        public static void Write(this byte[] array, int offset, int length, byte val)
        {
            for (var i = 0; i < length; i++)
            {
                array[offset + i] = val;
            }
        }
    }
}