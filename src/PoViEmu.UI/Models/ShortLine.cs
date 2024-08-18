namespace PoViEmu.UI.Models
{
    public record ShortLine(string Key, ushort Value)
    {
        public string Hex => $"{Value:X4}";
    }
}