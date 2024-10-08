using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops.Regs
{
    public record Reg16Operand(B16Register Reg) : RegOperand<B16Register>;
}