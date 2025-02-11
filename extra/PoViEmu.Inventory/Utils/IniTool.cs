using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IniFile;

namespace PoViEmu.Inventory.Utils
{
    public static class IniTool
    {
        private static readonly Encoding Enc = Encoding.UTF8;

        public static Ini LoadIni(string file)
        {
            var text = File.ReadAllText(file, Enc);
            return LoadIniRaw(text);
        }

        private static Ini LoadIniRaw(string text)
        {
            text = text.TrimEnd((char)26).Trim();
            var settings = new IniLoadSettings
            {
                Encoding = Enc, CaseSensitive = false
            };
            return Ini.Load(text, settings);
        }

        public static Ini LoadDoubled(string file)
        {
            var enc = Encoding.UTF8;
            var lines = File.ReadAllLines(file, enc);
            var keys = new Dictionary<string, int>();
            string? lastLine = null;
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.StartsWith('['))
                {
                    var key = ToKey(line).Replace(':', '.');
                    if (lastLine != null && key.StartsWith('.'))
                        key = ToKey(lastLine) + key;
                    if (!keys.TryGetValue(key, out var found))
                        keys[key] = found = 0;
                    line = $"[{key}_{found}]";
                    line = line.Replace("_0]", "]");
                    lastLine = line;
                    keys[key] += 1;
                }
                lines[i] = line;
            }
            var text = string.Join(Environment.NewLine, lines);
            return LoadIniRaw(text);
        }

        private static string ToKey(string line)
        {
            return line.TrimStart('[').TrimEnd(']').ToLowerInvariant();
        }
    }
}