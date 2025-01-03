namespace PoViEmu.I186.CPU
{
    public interface IPorts
    {
        byte this[byte nr] { get; }

        byte this[ushort nr] { get; }
    }
}