using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoViEmu.Common;

namespace PoViEmu.Core.Inventory
{
    public sealed class AppRepo
    {
        public static AppRepo Instance { get; } = new();

        private readonly string _baseUrl;

        public AppRepo(string baseUrl = ThisAssembly.Constants.Defaults.Repo.Base)
        {
            _baseUrl = baseUrl;
        }

        private RepoEntry _repo;

        public async Task Load()
        {
            var root = PathHelper.CurrentDir;
            var repoJsonUrl = $"{_baseUrl}/repo.json";
            var repoJsonFile = root.MakeDirFor("index.json", "cache", "repo");
            var text = await WebHelper.GetCachedText(repoJsonUrl, repoJsonFile);
            _repo = JsonHelper.FromJson<RepoEntry>(text);
        }

        public async Task<CachedItem<T>> GetCached<T>(T item, string file) where T : IRelUrl
        {
            var itemUrl = item.BuildUrl(_baseUrl);
            var bytes = await WebHelper.GetCachedBytes(itemUrl, file);
            return new CachedItem<T>(item, bytes);
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

        public IEnumerable<AddInItem> SearchAddIn(string text)
            => AllAddInEntries().Where(a => a.Entry.Name.Contains(text, TextHelper.Ignore));

        public IEnumerable<SystemItem> AllSystemEntries()
        {
            foreach (var item1 in _repo.System)
            foreach (var item2 in item1.Value)
            foreach (var item3 in item2.Value)
                yield return new(item1.Key, item2.Key, item3);
        }

        public IEnumerable<SystemItem> SearchSystem(string model)
            => AllSystemEntries().Where(a => a.Model.Contains(model, TextHelper.Ignore));

        public IEnumerable<BiosItem> AllBiosEntries()
        {
            foreach (var item1 in _repo.Bios)
            foreach (var item2 in item1.Value)
            foreach (var item3 in item2.Value)
                yield return new(item1.Key, item2.Key, item3);
        }

        public IEnumerable<BiosItem> SearchBios(string model)
            => AllBiosEntries().Where(a => a.Model.Contains(model, TextHelper.Ignore));
    }

    public record CachedItem<T>(T Item, byte[] Bytes) where T : IRelUrl;

    public record AddInItem(string Model, string Hash, AddInEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";
    }

    public record SystemItem(string Model, string Hash, SystemEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";
    }

    public record BiosItem(string Model, string Hash, BiosEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";
    }

    public interface IRelUrl
    {
        string BuildUrl(string @base);
    }
}