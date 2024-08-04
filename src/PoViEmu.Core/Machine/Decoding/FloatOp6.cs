using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum FloatOp6 : byte
    {
        faddp = 0b11000000,

        fmulp = 0b11001000,

        fcompp = 0b11011001,

        fsubrp = 0b11100000,

        fsubp = 0b11101000,

        fdivrp = 0b11110000,

        fdivp = 0b11111000
    }
}