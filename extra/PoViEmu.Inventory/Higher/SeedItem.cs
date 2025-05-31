using System.Linq;

namespace PoViEmu.Inventory.Upper
{
    public class SeedItem : IRelUrl
    {
        public SeedItem(string[]? urls, string label)
        {
            Urls = urls;
            Label = label;
        }

        public string Label { get; }
        public string[]? Urls { get; }

        public string BuildUrl(string @base)
        {
            var firstUrl = Urls?.FirstOrDefault() ?? "";
            firstUrl = firstUrl.Replace(@base, string.Empty).TrimStart('/');
            var seedUrl = $"{@base}/{firstUrl}";
            return seedUrl;
        }

        public string UName => "init";
    }
}