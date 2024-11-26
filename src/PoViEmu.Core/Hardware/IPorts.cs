namespace PoViEmu.Core.Hardware
{
    public interface IPorts
    {
        byte this[byte nr] { get; }

        byte this[ushort nr] { get; }
    }
}