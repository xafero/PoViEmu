namespace PoViEmu.Core.Inventory
{
    public record CachedItem<T>(T Item, byte[] Bytes) where T : IRelUrl;
}