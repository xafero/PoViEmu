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
        public static string Space(int count)
        {
            return new string(Enumerable.Repeat(' ', count).ToArray());
        }

        public static string ToText(string file)
        {
            var txt = File.ReadAllText(file, Encoding.UTF8);
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

        public static string ToHex<T>(T enumVal, ushort enumNum) where T : struct, Enum
        {
            return Enum.IsDefined(enumVal) ? null : $"0x{enumNum:X2}";
        }

        public static string TrimNull(this string text)
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
    }
}