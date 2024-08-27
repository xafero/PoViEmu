using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoViEmu.Core.Inventory
{
    public class RepoEntry
    {
        [JsonProperty("A")]
        public SortedDictionary<string, IDictionary<string,
            IDictionary<string, IDictionary<string, List<AddInEntry>>>>> AddIn { get; set; }

        [JsonProperty("S")] public SortedDictionary<string, object> System { get; set; }

        [JsonProperty("B")] public SortedDictionary<string, object> Bios { get; set; }
    }
}