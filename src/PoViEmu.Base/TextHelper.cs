using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PoViEmu.Base
{
    public static class TextHelper
    {
        public static readonly Encoding Utf8 = Encoding.UTF8;
        
        public static string ToText(string file)
        {
            var txt = File.ReadAllText(file, Encoding.UTF8);
            return txt.Trim();
        }
        
        public static string? TrimNull(this string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }
        
        public static string RemoveSpaces(this string rawText)
        {
            var text = rawText
                .Replace('\r', ' ')
                .Replace('\n', ' ');
            return Regex.Replace(text, @"\s+", " ").Trim();
        }
        
        private static IEnumerable<string> FilterLines(this IEnumerable<string> lines,
            bool noEmpty, bool noSpaces)
        {
            if (noEmpty)
            {
                lines = lines.Where(l => !string.IsNullOrWhiteSpace(l));
            }
            if (noSpaces)
            {
                lines = lines.Select(l => l.Trim());
            }
            return lines;
        }
        
        public static string[] ReadUtf8Lines(string file,
            bool noEmpty = true, bool noSpaces = true)
        {
            var lines = File.ReadLines(file, Utf8);
            return FilterLines(lines, noEmpty, noSpaces).ToArray();
        }
        
        public static string Space(int count, char c = ' ')
        {
            return new string(Enumerable.Repeat(c, count).ToArray());
        }

        public static string AddSpaceTo(this string rawText, int size, char c = ' ')
        {
            var text = rawText.Trim();
            return text + Space(size - text.Length, c);
        }

        public static string FormatRle(IEnumerable<string> texts)
        {
            var count = 1;
            var last = string.Empty;
            return string.Join("", texts.Concat([string.Empty]).Select(text =>
            {
                var res = string.Empty;
                if (text.Equals(last))
                {
                    count++;
                }
                else if (last.Length != 0)
                {
                    res = $"{count}°{last.Replace("0x", "")} ";
                    count = 1;
                }
                last = text;
                return res;
            })).Trim();
        }
        
        public static string[] ToLines(this string text,
            bool noEmpty = true, bool noSpaces = true)
        {
            var lines = text.Split('\n');
            return FilterLines(lines, noEmpty, noSpaces).ToArray();
        }
    }
}