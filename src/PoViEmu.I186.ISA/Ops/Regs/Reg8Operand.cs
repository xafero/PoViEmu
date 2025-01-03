using B8 = PoViEmu.I186.ISA.B8Register;

namespace PoViEmu.I186.ISA.Ops.Regs
{
    public record Reg8Operand(B8 Reg) : RegOperand<B8Register>;
}