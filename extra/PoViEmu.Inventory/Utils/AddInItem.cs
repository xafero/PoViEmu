using PoViEmu.Inventory.Upper;

namespace PoViEmu.Inventory.Utils
{
    public record AddInItem(string Model, string Hash, AddInEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";
    }
}