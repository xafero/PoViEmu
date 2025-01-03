using Newtonsoft.Json;

namespace PoViEmu.Inventory
{
    public class ImageObj
    {
        [JsonProperty("W")] public int Width { get; set; }

        [JsonProperty("H")] public int Height { get; set; }

        [JsonProperty("P")] public byte[] Png { get; set; }
    }
}