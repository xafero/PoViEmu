using System.Linq;

namespace PoViEmu.Inventory.Upper
{
    public sealed class SeedRepo
    {
        public static SeedRepo Instance { get; } = new();

        private SeedRepo()
        {
        }

        public SeedItem Wrap(DeviceMeta dm, TemplEntry entry)
        {
            var urls = dm.Seeds?.Select(s => $"{s}/{entry.Label}.json").ToArray();
            return new SeedItem(urls, entry.Label);
        }
    }
}