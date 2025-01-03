using B16 = PoViEmu.I186.ISA.B16Register;

namespace PoViEmu.I186.ISA.Ops.Regs
{
    public record Reg16Operand(B16 Reg) : RegOperand<B16Register>;
}