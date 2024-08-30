using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PoViEmu.Common
{
    public static class TextHelper
    {
        public static string Title(this string text)
        {
            return text[..1].ToUpperInvariant() + text[1..];
        }

        public static string Space(int count, char c = ' ')
        {
            return new string(Enumerable.Repeat(c, count).ToArray());
        }

        public static string ToText(string file)
        {
            var txt = File.ReadAllText(file, Encoding.UTF8);
            return txt.Trim();
        }

        public static string ToMaxText(string file, int max)
        {
            var sep = "\n";
            var lines = ToText(file).Split(sep);
            if (max >= 1)
                lines = lines.Take(max).ToArray();
            var txt = string.Join(sep, lines);
            return txt.Trim();
        }

        public static T ToEnum<T>(ushort value, T defaultVal) where T : struct
        {
            return ToEnum($"{value}", defaultVal);
        }

        public static T ToEnum<T>(string text, T defaultVal) where T : struct
        {
            return Enum.TryParse<T>(text, ignoreCase: true, out var result) ? result : defaultVal;
        }

        public static string CleanUp(string text)
        {
            return text
                .Replace((char)0, ' ')
                .Replace((char)65533, ' ')
                .Replace((char)3, ' ')
                .Replace('ÿ', ' ')
                .Replace('º', ' ')
                .Replace('“', ' ')
                .Replace('¡', ' ')
                .Replace('\u00a2', ' ')
                .Replace('\u00ad', ' ')
                .Replace('ª', ' ')
                .Replace('\u00b8', ' ')
                .Replace('\u00bd', ' ')
                .Trim();
        }

        public static Version ToVersion(string text)
        {
            var major = int.Parse($"{text[0]}{text[1]}");
            var minor = int.Parse($"{text[2]}{text[3]}");
            return new Version(major, minor);
        }

        public static DateTime ToDate(string date, string time)
        {
            var format = time == null ? "yyyyMMdd" : "yyyyMMddHHmm";
            var text = date + time;
            var dt = DateTime.ParseExact(text, format, null);
            return dt;
        }

        public static string? ToHex<T>(T enumVal, ushort enumNum) where T : struct, Enum
        {
            return Enum.IsDefined(enumVal) ? null : $"0x{enumNum:X2}";
        }

        public static string? TrimNull(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }

        public static string FixSpaces(this string text)
        {
            return text.Replace((char)0, ' ')
                .Replace((char)4, ' ')
                .Replace((char)5, ' ')
                .Replace((char)14, ' ')
                .Replace((char)16, ' ')
                .Replace((char)63, ' ')
                .Replace((char)64, ' ')
                .Trim();
        }

        public static string RemoveSpaces(this string rawText)
        {
            var text = rawText
                .Replace('\r', ' ')
                .Replace('\n', ' ');
            return Regex.Replace(text, @"\s+", " ");
        }

        internal static string ToTxtLine(IEnumerable<byte> hex)
        {
            return string.Join(" ", hex.Select(h => $"{Encoding.ASCII.GetString([h]).Trim()}"));
        }

        internal static string ToHexLine(IEnumerable<byte> hex)
        {
            return string.Join(" ", hex.Select(h => $"{h:X2}"));
        }

        public static readonly Encoding Utf8 = Encoding.UTF8;

        public static IEnumerable<string> SplitEvery(this string text, int count)
        {
            var bld = new StringBuilder();
            for (var i = 0; i < text.Length; i += count)
            {
                bld.Clear();
                for (var j = 0; j < count; j++)
                    bld.Append(text[i + j]);
                yield return bld.ToString();
            }
        }

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

        public const StringComparison Ignore = StringComparison.InvariantCultureIgnoreCase;

        public static string AddSpaceTo(this string rawText, int size)
        {
            var text = rawText.Trim();
            return text + Space(size - text.Length);
        }
    }
}