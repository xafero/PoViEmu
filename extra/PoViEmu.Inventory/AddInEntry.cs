using System;
using ByteSizeLib;
using Newtonsoft.Json;

namespace PoViEmu.Inventory
{
    public class AddInEntry
    {
        [JsonProperty("P")] public string Path { get; set; }

        [JsonProperty("N")] public string Name { get; set; }

        [JsonProperty("V")] public Version Version { get; set; }

        [JsonProperty("C")] public DateTime Compiled { get; set; }

        [JsonProperty("S")] public int Size { get; set; }

        [JsonProperty("M")] public ImageObj MenuIcon { get; set; }

        [JsonProperty("L")] public ImageObj ListIcon { get; set; }

        [JsonIgnore] public ByteSize HumanSize => ByteSize.FromBytes(Size);
    }
}