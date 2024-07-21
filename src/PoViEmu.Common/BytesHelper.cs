using System;

namespace PoViEmu.Common
{
    public static class BytesHelper
    {
        public static string ToHex(this byte[] bytes, bool prependSize = true)
        {
            var hex = Convert.ToHexString(bytes);
            return prependSize ? $"({bytes.Length}) {hex}" : hex;
        }
    }
}