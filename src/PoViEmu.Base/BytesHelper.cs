using System;
using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Base
{
    public static class BytesHelper
    {
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

        public static byte[] ToArray(long value, int skip = 0, bool noZero = true)
        {
            var array = BitConverter.GetBytes(value);
            var items = array.Reverse();
            if (noZero)
                items = items.SkipWhile(b => b == 0);
            if (skip >= 1)
                items = items.Skip(skip);
            return items.ToArray();
        }

        public static string ToHex(this byte[] bytes, bool prependSize = true, bool withSpace = false)
        {
            var txt = Convert.ToHexString(bytes);
            var hex = withSpace ? string.Join(' ', txt.SplitEvery(2)) : txt;
            return prependSize ? $"({bytes.Length}) {hex}" : hex;
        }

        public static int FindArray(this byte[] outer, byte[] inner)
        {
            var len = inner.Length;
            var limit = outer.Length - len;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                    if (inner[k] != outer[i + k])
                        break;
                if (k == len)
                    return i;
            }
            return -1;
        }

        public static byte[] FromBinaryStr(string text)
        {
            var array = Enumerable.Range(0, int.MaxValue / 8)
                .Select(i => i * 8)
                .TakeWhile(i => i < text.Length)
                .Select(i => text.Substring(i, 8))
                .Select(s => Convert.ToByte(s, 2))
                .ToArray();
            return array;
        }

        public static string ToBinaryStr(byte[] array)
        {
            var text = string.Concat(array.Select(b => Convert.ToString(b, 2)
                .PadLeft(8, '0')));
            return text;
        }
    }
}