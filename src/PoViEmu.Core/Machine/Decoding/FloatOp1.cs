using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum FloatOp1 : byte
    {
        fadd = 0b11000000,

        fmul = 0b11001000,

        fcom = 0b11010000,

        fcomp = 0b11011000,

        fsub = 0b11100000,

        fdivr = 0b11111000,

        fdiv = 0b11110000,

        fsubr = 0b11101000
    }
}