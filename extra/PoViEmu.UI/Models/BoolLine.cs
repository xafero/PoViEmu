namespace PoViEmu.UI.Models
{
    public record BoolLine(string Key, bool Value)
    {
        public string Hex => $"{(Value ? 1 : 0)}";
    }
}