using System.Collections.Generic;

namespace PoViEmu.SamCon
{
    internal static class ParseTool
    {
        public static IEnumerable<(string key, string val)> Parse(string text)
        {
            foreach (var item in text.Trim().Split(' '))
            {
                var parts = item.Split('=', 2);
                var key = parts[0].Trim();
                var val = parts[1].Trim();
                yield return (key, val);
            }
        }
    }
}