using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace PoViEmu.Common
{
    public static class JsonHelper
    {
        private static JsonSerializerSettings GetConfig(bool format, bool noDefaults)
        {
            var config = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = format ? Formatting.Indented : Formatting.None
            };
            if (noDefaults)
                config.DefaultValueHandling = DefaultValueHandling.Ignore;
            return config;
        }

        public static string ToJson(object obj, bool format = true, bool noDefaults = false)
        {
            var config = GetConfig(format, noDefaults);
            var json = JsonConvert.SerializeObject(obj, config);
            return json;
        }

        public static T FromJson<T>(string json, bool format = true, bool noDefaults = false)
        {
            var config = GetConfig(format, noDefaults);
            var obj = JsonConvert.DeserializeObject<T>(json, config);
            return obj;
        }

        public static T? LoadFromFile<T>(string file)
        {
            if (!File.Exists(file)) return default;
            var text = File.ReadAllText(file, TextHelper.Utf8);
            return FromJson<T>(text);
        }

        public static void SaveToFile<T>(T obj, string file)
        {
            var text = ToJson(obj);
            File.WriteAllText(file, text, TextHelper.Utf8);
        }

        public static bool TrySaveToFile<T>(T obj, string file, out Exception? e)
        {
            try
            {
                SaveToFile(obj, file);
                e = null;
                return true;
            }
            catch (JsonSerializationException jse)
            {
                e = jse.InnerException ?? jse;
                return false;
            }
        }
    }
}