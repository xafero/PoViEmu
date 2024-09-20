using System.Collections.Generic;

namespace PoViEmu.Core.Infos
{
    public class ModelTree
    {
        public string Name { get; set; }
        public Clock Clock { get; set; }
        public int CSysRam { get; set; }
        public Display Display { get; set; }
        public Dictionary<char, ChipGroup> Groups { get; set; } = new();
    }
}