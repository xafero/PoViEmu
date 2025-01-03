using System;

namespace PoViEmu.I186.ISA.Ops.Regs
{
    public abstract record RegOperand : BaseOperand;

    public abstract record RegOperand<T> : RegOperand
        where T : Enum
    {
        public abstract T Reg { get; init; }
    }
}