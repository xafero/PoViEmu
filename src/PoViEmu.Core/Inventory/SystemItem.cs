namespace PoViEmu.Core.Inventory
{
    public record SystemItem(string Model, string Hash, SystemEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";
    }
}