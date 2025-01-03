using System.Collections.Generic;

namespace PoViEmu.Inventory.Infos
{
    public class ChipGroup
    {
        public Dictionary<char, Chip> Chips { get; set; } = new();
    }
}