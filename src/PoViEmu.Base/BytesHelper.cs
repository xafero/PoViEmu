using System;
using System.Linq;

namespace PoViEmu.Base
{
    public static class BytesHelper
    {
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
    }
}