using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops.Regs
{
    public record Reg8Operand(B8Register Reg) : RegOperand<B8Register>;
}