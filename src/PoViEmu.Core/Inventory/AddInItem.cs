namespace PoViEmu.Core.Inventory
{
    public record AddInItem(string Model, string Hash, AddInEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";
    }
}