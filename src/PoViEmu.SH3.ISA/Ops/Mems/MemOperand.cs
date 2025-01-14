using System;
using PoViEmu.SH3.ISA.Core;
using B32 = PoViEmu.SH3.ISA.ShRegister;
using AM = PoViEmu.SH3.ISA.Ops.Mems.AddressingMode;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public abstract record MemOperand<T>(AM Mode, B32 Base, B32? Idx = null, int? Disp = null)
        : MemOperand(Mode, Base, Idx, Disp)
    {
        public abstract T this[IMachineState m] { get; set; }

        public sealed override string ToString()
        {
            return Mode switch
            {
                AM.RegIndirect => $"@{Base.Name()}",
                AM.PostIncrement => $"@{Base.Name()}+",
                AM.PreDecrement => $"@-{Base.Name()}",
                AM.Displacement => $"@({Disp},{Base.Name()})",
                AM.Indexed => $"@({Idx?.Name()},{Base.Name()})",
                _ => throw new InvalidOperationException($"{Mode} ?!")
            };
        }
    }

    public abstract record MemOperand(AM Mode, B32 Base, B32? Idx = null, int? Disp = null)
        : BaseOperand
    {
        public abstract byte ByteSize { get; }
    }
}