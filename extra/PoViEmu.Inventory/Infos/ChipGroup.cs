using System.Collections.Generic;

namespace PoViEmu.Core.Infos
{
    public class ChipGroup
    {
        public Dictionary<char, Chip> Chips { get; set; } = new();
    }
}