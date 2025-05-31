using PoViEmu.Inventory.Upper;

namespace PoViEmu.Inventory.Utils
{
    public record CachedItem<T>(T Item, byte[] Bytes)
        where T : IRelUrl;

    public record CachedItem<TI, TR>(TI Item, byte[] Bytes, TR Content)
        : CachedItem<TI>(Item, Bytes) where TI : IRelUrl;
}