using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum FloatOp4 : byte
    {
        fcmovb = 0b11000000,

        fcmove = 0b11001000,

        fcmovbe = 0b11010000,

        fcmovu = 0b11011000
    }
}