using PoViEmu.Inventory.Upper;

namespace PoViEmu.Inventory.Utils
{
    public record CachedItem<T>(T Item, byte[] Bytes) where T : IRelUrl;
}