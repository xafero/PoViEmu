// ReSharper disable InconsistentNaming

namespace PoViEmu.I186.ISA.Ops.Jumps
{
    public record FarOperand(ushort Seg, ushort Off)
        : JumpOperand
    {
    }
}