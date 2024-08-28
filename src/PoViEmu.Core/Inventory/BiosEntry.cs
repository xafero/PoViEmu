using ByteSizeLib;
using Newtonsoft.Json;

namespace PoViEmu.Core.Inventory
{
    public class BiosEntry
    {
        [JsonProperty("P")] public string Path { get; set; }

        [JsonProperty("N")] public string Name { get; set; }

        [JsonProperty("S")] public int Size { get; set; }

        [JsonIgnore] public ByteSize HumanSize => ByteSize.FromBytes(Size);
    }
}