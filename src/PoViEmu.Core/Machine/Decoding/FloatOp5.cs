using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum FloatOp5 : byte
    {
        fcmovnb = 0b11000000,

        fcmovne = 0b11001000,

        fcmovnbe = 0b11010000,

        fcmovnu = 0b11011000,

        fucomi = 0b11101000,

        fcomi = 0b11110000
    }
}