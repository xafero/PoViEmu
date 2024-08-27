using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoViEmu.Core.Inventory
{
    public class RepoEntry
    {
        [JsonProperty("A")]
        public SortedDictionary<string, IDictionary<string,
            IDictionary<string, IDictionary<string, List<AddInEntry>>>>> AddIn { get; set; }

        [JsonProperty("S")] public SortedDictionary<string, IDictionary<string, List<SystemEntry>>> System { get; set; }

        [JsonProperty("B")] public SortedDictionary<string, IDictionary<string, List<BiosEntry>>> Bios { get; set; }
    }
}