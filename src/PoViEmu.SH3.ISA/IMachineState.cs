using PoViEmu.Base.ISA;

namespace PoViEmu.SH3.ISA
{
    public interface IMachineState
    {
        FlatMemAccess<byte> U8 { get; }
        FlatMemAccess<ushort> U16 { get; }
        FlatMemAccess<uint> U32 { get; }

        uint this[ShRegister reg] { get; set; }
    }
}