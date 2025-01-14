using PoViEmu.SH3.ISA.Core;

namespace PoViEmu.SH3.ISA.Ops.Regs
{
    public record Reg32Operand(ShRegister Reg) : RegOperand<ShRegister>
    {
        public override string ToString()
        {
            var val = Reg.Name();
            return $"{val}";
        }
    }
}