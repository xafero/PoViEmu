namespace PoViEmu.Core.Inventory
{
    public record BiosItem(string Model, string Hash, BiosEntry Entry) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{Entry.Path}";
    }
}