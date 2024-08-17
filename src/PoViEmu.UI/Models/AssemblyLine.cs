namespace PoViEmu.UI.Models
{
    public record AssemblyLine(string Offset, string Hex, string Command);

    public record StackLine(string Prefix, ushort Offset, ushort Value)
    {
        public string Address => $"{Prefix}:{Offset:X4}";

        public string Hex => $"{Value:X4}";
    }
}
