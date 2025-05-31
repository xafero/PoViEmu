using System.Threading.Tasks;
using PoViEmu.Base;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.Inventory.Upper
{
    public sealed class DeviceRepo
    {
        public static DeviceRepo Instance { get; } = new();

        private DeviceRepo()
        {
        }

        public async Task Load()
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;
            var baseUrl = inst.BaseUrl;

            var repoUrl = $"{baseUrl}/repoD.json";
            var repoFile = root.MakeDirFor("indexD.json", "cache", "repo");
            DeviceCatalog = await WebHelper.GetCachedJson<DeviceMeta>(repoUrl, repoFile);
        }

        public DeviceMeta? DeviceCatalog { get; private set; }
    }
}