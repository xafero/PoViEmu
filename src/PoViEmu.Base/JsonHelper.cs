using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PoViEmu.Base
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
    }
}