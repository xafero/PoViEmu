using System;
using System.Linq;

namespace PoViEmu.Base
{
    public static class DataHelper
    {
        public static string DecodeChars(this byte[] line)
        {
            return string.Join("", line.Select(b =>
                Array.IndexOf<byte>(ValidChars, b) >= 0 ? (char)b : '.'));
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
        
        public static T ToEnum<T>(string text, T defaultVal) where T : struct
        {
            return Enum.TryParse<T>(text, ignoreCase: true, out var result) ? result : defaultVal;
        }
        
        public static T ToEnum<T>(ushort value, T defaultVal) where T : struct
        {
            return ToEnum($"{value}", defaultVal);
        }
        
        public static DateTime ToDate(string date, string? time)
        {
            var format = time == null ? "yyyyMMdd" : "yyyyMMddHHmm";
            var text = date + time;
            var dt = DateTime.ParseExact(text, format, null);
            return dt;
        }
        
        public static Version ToHVersion(char[] t)
            => t.Length == 2
                ? ToVersion($"{(int)t[0]:D2}{(int)t[1]:D2}")
                : ToVersion($"{(int)t[0]}{(int)t[1]}{(int)t[2]}{(int)t[3]}");
        
        public static Version ToVersion(string text)
        {
            var major = int.Parse($"{text[0]}{text[1]}");
            var minor = int.Parse($"{text[2]}{text[3]}");
            return new Version(major, minor);
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
    }
}