using PoViEmu.Base.ISA;

namespace PoViEmu.I186.ISA
{
    public interface IMachineState
    {
        MemAccess<byte> U8 { get; }
        MemAccess<ushort> U16 { get; }

        ushort this[B16Register reg] { get; }
    }
}