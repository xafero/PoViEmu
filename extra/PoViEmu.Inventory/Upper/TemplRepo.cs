using System.Collections.Generic;
using System.Threading.Tasks;
using PoViEmu.Base;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.Inventory.Upper
{
    public sealed class TemplRepo
    {
        public static TemplRepo Instance { get; } = new();

        public async Task Load()
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;
            var baseUrl = inst.BaseUrl;

            var repoUrl = $"{baseUrl}/repoM.json";
            var repoFile = root.MakeDirFor("indexM.json", "cache", "repo");
            AllTemplates = await WebHelper.GetCached<TemplEntry[]>(repoUrl, repoFile);
        }

        public IList<TemplEntry>? AllTemplates { get; private set; }
    }
}