using System.Linq;

namespace PoViEmu.Inventory.Upper
{
    public sealed class SeedRepo
    {
        public static SeedRepo Instance { get; } = new();

        private SeedRepo()
        {
        }

        public static string[] BuildUrl(DeviceMeta dm, TemplEntry entry)
        {
            var inst = AppConst.Instance;
            var baseUrl = inst.BaseUrl;
            var urls = dm.Seeds?.Select(s => $"{baseUrl}/{s}/{entry.Label}.json").ToArray();
            return urls ?? [];
        }
    }
}