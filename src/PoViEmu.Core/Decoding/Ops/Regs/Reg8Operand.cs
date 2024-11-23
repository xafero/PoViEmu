using B8 = PoViEmu.Core.Hardware.AckNow.B8Register;

namespace PoViEmu.Core.Decoding.Ops.Regs
{
    public record Reg8Operand(B8 Reg) : RegOperand<B8>;
}