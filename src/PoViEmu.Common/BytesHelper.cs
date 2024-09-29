using System.IO;
using System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
    }
}