// ReSharper disable IdentifierTypo

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public interface IAddressOperand
    {
        AddressingMode Mode { get; }

        ShRegister Base { get; }

        byte ByteSize { get; }

        int? Disp { get; }

        ShRegister? Idx { get; }
    }
}