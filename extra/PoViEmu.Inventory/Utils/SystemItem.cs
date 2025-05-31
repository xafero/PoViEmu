using PoViEmu.Inventory.Upper;

namespace PoViEmu.Inventory.Utils
{
    public record SystemItem(string Model, string Hash, SystemEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";

        public string UName => "system";
    }
}