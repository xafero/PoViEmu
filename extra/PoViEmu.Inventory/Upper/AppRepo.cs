using System;
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
            var text = await WebHelper.GetCachedText(repoUrl, repoFile);
            _repo = JsonHelper.FromJson<RepoEntry>(text);
        }

        public async Task<CachedItem<T>> GetCached<T>(T item) where T : IRelUrl
        {
            var (itemUrl, itemFile) = GetFilePath(item, createDir: true);
            var bytes = await WebHelper.GetCachedBytes(itemUrl, itemFile);
            return new CachedItem<T>(item, bytes);
        }

        public (string url, string file) GetFilePath<T>(T item, bool createDir = false) where T : IRelUrl
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;
            var baseUrl = inst.BaseUrl;

            string[] dirs;
            switch (item)
            {
                case AddInItem ai:
                    dirs = ["cache", "addins", ai.Model];
                    break;
                case SystemItem si:
                    dirs = ["cache", "system", si.Model];
                    break;
                case BiosItem bi:
                    dirs = ["cache", "bios", bi.Model];
                    break;
                default:
                    throw new InvalidOperationException(typeof(T).FullName);
            }
            var itemUrl = item.BuildUrl(baseUrl);
            var itemName = PathHelper.GetLast(itemUrl);
            var itemFile = createDir ? root.MakeDirFor(itemName, dirs) : root.GetDirFor(itemName, dirs);
            return (itemUrl, itemFile);
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