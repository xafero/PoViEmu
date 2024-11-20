using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PoViEmu.Common
{
    public static class BytesHelper
    {
        public static byte[] ReadFile(string[] paths, string name, string ext)
        {
            var root = Path.Combine(paths);
            var file = Path.Combine(root, $"{name}.{ext}");
            return File.ReadAllBytes(file);
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

        public static string ToHex(this byte[] bytes, bool prependSize = true, bool withSpace = false)
        {
            var txt = Convert.ToHexString(bytes);
            var hex = withSpace ? string.Join(' ', txt.SplitEvery(2)) : txt;
            return prependSize ? $"({bytes.Length}) {hex}" : hex;
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

        public static byte[] ToArray(int value, int skip = 1)
        {
            var array = BitConverter.GetBytes(value);
            return array.Reverse().Skip(skip).ToArray();
        }
    }
}