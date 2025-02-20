using System;

namespace PoViEmu.Inventory.Config
{
    public class GlobalConfig
    {
        public required DateTime Created { get; set; }

        public required string InstanceDir { get; set; }
    }
}