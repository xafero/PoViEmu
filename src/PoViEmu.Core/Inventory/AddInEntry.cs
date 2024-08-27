using System;

namespace PoViEmu.Core.Inventory
{
    public class AddInEntry
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }
        public DateTime Compiled { get; set; }
        public int Size { get; set; }
        public ImageObj MenuIcon { get; set; }
        public ImageObj ListIcon { get; set; }
    }
}