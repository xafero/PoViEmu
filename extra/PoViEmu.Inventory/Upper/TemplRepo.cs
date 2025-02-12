using System.Collections.Generic;
using System.Threading.Tasks;
using PoViEmu.Base;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.Inventory.Upper
{
    public sealed class TemplRepo
    {
        public static TemplRepo Instance { get; } = new();

        private TemplEntry[] _repo;

        public async Task Load()
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;
            var baseUrl = inst.BaseUrl;

            var repoUrl = $"{baseUrl}/repoM.json";
            var repoFile = root.MakeDirFor("indexM.json", "cache", "repo");
            var text = await WebHelper.GetCachedText(repoUrl, repoFile);
            _repo = JsonHelper.FromJson<TemplEntry[]>(text);
        }

        public IList<TemplEntry> AllTemplates => _repo;
    }
}