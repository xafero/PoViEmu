using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoViEmu.Inventory.Utils;

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

        public async Task<IList<SeedEntry>> FetchAndFill(SeedItem wrap)
        {
            var list = new List<SeedEntry>();
            await FetchAndFill(wrap, list);
            return list;
        }

        private async Task FetchAndFill(SeedItem wrap, List<SeedEntry> list)
        {
            var res = await FileRepo.GetCached<SeedItem, SeedEntry>(wrap);
            var item = res.Content;
            list.Insert(0, item);

            if (string.IsNullOrWhiteSpace(item.Base))
                return;
            var baseUri = GetUrl(res, item);
            if (string.IsNullOrWhiteSpace(baseUri))
                return;
            var baseName = GetLastPart(baseUri);
            if (string.IsNullOrWhiteSpace(baseName))
                return;
            var subItem = new SeedItem([baseUri], baseName);
            await FetchAndFill(subItem, list);
        }

        private static string? GetLastPart(string? url)
        {
            var start = url?.LastIndexOf('/');
            if (url == null || start == null)
                return null;
            var name = url[(int)(start + 1)..];
            name = name.Replace(".json", "");
            return name;
        }

        private static string? GetUrl(CachedItem<SeedItem, SeedEntry> res, SeedEntry item)
        {
            if (!Uri.TryCreate(new Uri(res.Url), item.Base, out var uri))
                return null;
            var baseUri = $"{uri}.json";
            return baseUri;
        }

        public SeedEntry Merge(IEnumerable<SeedEntry> multiple)
        {
            var entry = new SeedEntry();
            foreach (var one in multiple)
            {
                entry.Comments = one.Comments;
                if (one.Files is { } oneFiles)
                    entry.Files = (entry.Files ?? []).Concat(oneFiles).ToArray();
                if (one.Bunks is { } oneBunks)
                    entry.Bunks = (entry.Bunks ?? []).Concat(oneBunks).ToArray();
                if (one.Settings is { } oneSettings)
                {
                    if (oneSettings.Erased is { } oneErased)
                        (entry.Settings ??= new()).Erased = oneErased;
                }
            }
            return entry;
        }
    }
}