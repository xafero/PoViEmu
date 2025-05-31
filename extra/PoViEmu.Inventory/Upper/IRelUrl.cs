namespace PoViEmu.Inventory.Upper
{
    public interface IRelUrl
    {
        string UName { get; }

        string BuildUrl(string @base);
    }
}