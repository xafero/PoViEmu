using System.Linq;

namespace PoViEmu.Core.Data
{
    public static class TwfDecoder
    {
        public static void Decode(byte[] p, int len)
        {
            for (var i = 0; i < len; i++)
            {
                var b = p[i];
                var p1 = b / 32 + 1;
                p1 = 256 - p1 * 32;
                var p2 = b % 32 / 8;
                p1 += p2 * 8;
                p[i] = (byte)(p1 + 8 - b % 8 - 1);
            }
        }

        private static bool IsLegit(char c)
            => char.IsLetterOrDigit(c) ||
               char.IsPunctuation(c) ||
               char.IsSymbol(c) ||
               char.IsSeparator(c);

        public static string ToText(byte[] bytes, string? replace = null)
            => string.Join("", values: bytes
                .Select(c => (char)c)
                .Select(c => IsLegit(c)
                    ? $"{c}"
                    : replace ?? $"({(int)c})"));
    }
}