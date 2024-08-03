using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
    }
}