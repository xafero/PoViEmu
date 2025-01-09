using PoViEmu.SH3.ISA.Ops.Regs;

namespace PoViEmu.SH3.ISA.Ops
{
    public abstract record BaseOperand()
    {
        public static implicit operator BaseOperand(ShRegister reg) => new Reg32Operand(reg);
    }
}