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

        public static string AddSpaceTo(this string rawText, int size, char c = ' ')
        {
            var text = rawText.Trim();
            return text + Space(size - text.Length, c);
        }

        public static string DecodeChars(this byte[] line)
        {
            return string.Join("", line.Select(b =>
                Array.IndexOf(ValidChars, b) >= 0 ? (char)b : '.'));
        }

        private static readonly byte[] ValidChars =
        [
            0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e,
            0x2f, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c,
            0x3d, 0x3e, 0x3f, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4a,
            0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58,
            0x59, 0x5a, 0x5b, 0x5c, 0x5d, 0x5e, 0x5f, 0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66,
            0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 0x6e, 0x6f, 0x70, 0x71, 0x72, 0x73, 0x74,
            0x75, 0x76, 0x77, 0x78, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e
        ];

        public static string ReplaceFirst(this string text, string term, string newTxt)
        {
            var index = text.IndexOf(term, StringComparison.Ordinal);
            return index < 1
                ? text
                : text.Remove(index, term.Length).Insert(index, newTxt);
        }
    }
}