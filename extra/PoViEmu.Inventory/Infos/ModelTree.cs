using System.Collections.Generic;

namespace PoViEmu.Inventory.Infos
{
    public class ModelTree
    {
        public string? Changed { get; set; }
        public ModelKind Kind { get; set; }
        public string? Name { get; set; }
        public Clock? Clock { get; set; }
        public int? CSysRam { get; set; }
        public Display? Display { get; set; }
        public Dictionary<char, ChipGroup> Groups { get; set; } = new();
    }
}