using System.Collections.Generic;

// ReSharper disable ClassNeverInstantiated.Global

namespace PoViEmu.Inventory.Upper
{
    public class DeviceMeta
    {
        public string[]? Seeds { get; set; }

        public IList<TemplEntry>? Devices { get; set; }
    }
}