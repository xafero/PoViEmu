using Newtonsoft.Json;

namespace PoViEmu.Core.Inventory
{
    public class SystemEntry
    {
        [JsonProperty("P")] public string Path { get; set; }

        [JsonProperty("N")] public string Name { get; set; }

        [JsonProperty("S")] public int Size { get; set; }

        [JsonProperty("A")] public string[] AddIns { get; set; }
    }
}