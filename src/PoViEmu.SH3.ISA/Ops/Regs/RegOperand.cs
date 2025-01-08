using System;
using B32 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Regs
{
    public abstract record RegOperand : BaseOperand;

    public abstract record RegOperand<T> : RegOperand
        where T : Enum
    {
        public abstract T Reg { get; init; }
    }

    public record Reg32Operand(B32 Reg) : RegOperand<B32>
    {
    }
}