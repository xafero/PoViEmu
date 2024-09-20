using System.Collections.Generic;

namespace PoViEmu.Core.Infos
{
    public class ModelTree
    {
        public string Name { get; set; }
        public Dictionary<char, ChipGroup> Groups { get; set; } = new();
    }
}