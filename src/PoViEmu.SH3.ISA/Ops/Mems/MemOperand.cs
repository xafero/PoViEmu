using System;
using PoViEmu.SH3.ISA;
using B32 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public enum AddressingMode
    {
        Unknown = 0, RegisterIndirect, PostIncrement, PreDecrement, Displacement, Indexed
    }

    public abstract record MemOperand<T>(AddressingMode Mode, B32 Base, B32? Idx = null, int? Disp = null)
        : MemOperand(Mode, Base, Idx, Disp)
    {
        public abstract T this[IMachineState m] { get; set; }

        public sealed override string ToString()
        {
            return Mode switch
            {
                AddressingMode.RegisterIndirect => $"@{Base}",
                AddressingMode.PostIncrement => $"@{Base}+",
                AddressingMode.PreDecrement => $"@-{Base}",
                AddressingMode.Displacement => $"@({Disp},{Base})",
                AddressingMode.Indexed => $"@({Idx},{Base})",
                _ => throw new InvalidOperationException($"{Mode} ?!")
            };
        }
    }

    public abstract record MemOperand(AddressingMode Mode, B32 Base, B32? Idx = null, int? Disp = null)
        : BaseOperand;
}