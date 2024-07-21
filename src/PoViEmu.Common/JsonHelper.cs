using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PoViEmu.Common
{
    public static class JsonHelper
    {
        public static string ToJson(object obj, bool format = true)
        {
            var config = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = format ? Formatting.Indented : Formatting.None
            };
            var json = JsonConvert.SerializeObject(obj, config);
            return json;
        }
    }
}