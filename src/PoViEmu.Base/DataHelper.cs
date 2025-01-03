using System;

namespace PoViEmu.Base
{
    public static class DataHelper
    {
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