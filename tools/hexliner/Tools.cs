using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexLiner
{
    internal static class Tools
    {
        internal static string ToTxtLine(IEnumerable<byte> hex)
        {
            return string.Join(" ", hex.Select(h => $"{Encoding.ASCII.GetString([h]).Trim()}"));
        }

        internal static string ToHexLine(IEnumerable<byte> hex)
        {
            return string.Join(" ", hex.Select(h => $"{h:X2}"));
        }
    }
}