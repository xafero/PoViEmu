using B16 = PoViEmu.Core.Hardware.AckNow.B16Register;

namespace PoViEmu.Core.Decoding.Ops.Regs
{
    public record Reg16Operand(B16 Reg) : RegOperand<B16>;
}