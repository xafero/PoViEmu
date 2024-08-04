using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum FloatOp7 : byte
    {
        ffree = 0b11000000,

        fst = 0b11010000,

        fstp = 0b11011000,

        fucom = 0b11100000,

        fucomp = 0b11101000
    }
}