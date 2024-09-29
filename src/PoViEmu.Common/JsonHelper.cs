using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PoViEmu.Common
{
    public static class JsonHelper
    {
        private static JsonSerializerSettings GetConfig(bool format)
        {
            var config = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = format ? Formatting.Indented : Formatting.None
            };
            return config;
        }

        public static string ToJson(object obj, bool format = true)
        {
            var config = GetConfig(format);
            var json = JsonConvert.SerializeObject(obj, config);
            return json;
        }
        
        public static T FromJson<T>(string json, bool format = true)
        {
            var config = GetConfig(format);
            var obj = JsonConvert.DeserializeObject<T>(json, config);
            return obj;
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