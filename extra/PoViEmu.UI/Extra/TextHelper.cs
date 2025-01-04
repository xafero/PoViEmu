using System;
using System.IO;
using System.Linq;

namespace PoViEmu.Common
{
    public static class PathHelper
    {
        public static string GetChild(this string root, string sub)
        {
            return Path.Combine(root, sub);
        }
    }
    
    public static class TextHelper
    {
        public static string[] SplitOn(string l)
        {
            var opt = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
            return l.Split(' ', 3, opt);
        }
        
        public static string TryAsText(this byte[] bytes)
        {
            var chars = bytes.Select(b =>
            {
                var letter = (char)b;
                return char.IsLetterOrDigit(letter) && letter <= 175 ? letter : '.';
            });
            var txt = new string(chars.ToArray());
            return txt;
        }
    }
}