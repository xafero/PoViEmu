using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoViEmu.Base;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.Inventory.Upper
{
    public sealed class AppRepo
    {
        public static AppRepo Instance { get; } = new();

        private RepoEntry _repo;

        public async Task Load()
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;
            var baseUrl = inst.BaseUrl;

            var repoUrl = $"{baseUrl}/repo.json";
            var repoFile = root.MakeDirFor("index.json", "cache", "repo");
            _repo = await WebHelper.GetCachedJson<RepoEntry>(repoUrl, repoFile);
        }

        public IEnumerable<AddInItem> AllAddInEntries()
        {
            foreach (var item1 in _repo.AddIn)
            foreach (var item2 in item1.Value)
            foreach (var item3 in item2.Value)
            foreach (var item4 in item3.Value)
            foreach (var item5 in item4.Value)
                yield return new(item1.Key, item4.Key, item5);
        }

        public IEnumerable<AddInItem> SearchAddIn(string? text)
            => AllAddInEntries()
                .Where(a => text == null || a.Entry.Name.Contains(text, TextHelper.Ignore));

        public IEnumerable<SystemItem> AllSystemEntries()
        {
            foreach (var item1 in _repo.System)
            foreach (var item2 in item1.Value)
            foreach (var item3 in item2.Value)
                yield return new(item1.Key, item2.Key, item3);
        }

        public IEnumerable<SystemItem> SearchSystem(string? text)
            => AllSystemEntries()
                .Where(a => text == null || a.Model.Contains(text, TextHelper.Ignore));

        public IEnumerable<BiosItem> AllBiosEntries()
        {
            foreach (var item1 in _repo.Bios)
            foreach (var item2 in item1.Value)
            foreach (var item3 in item2.Value)
                yield return new(item1.Key, item2.Key, item3);
        }

        public IEnumerable<BiosItem> SearchBios(string? text)
            => AllBiosEntries()
                .Where(a => text == null || a.Model.Contains(text, TextHelper.Ignore));
    }
}