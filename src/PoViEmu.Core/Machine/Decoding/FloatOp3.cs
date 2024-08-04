using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum FloatOp3 : byte
    {
        fucomip = 0b11101000,

        fcomip = 0b11110000,

        ffreep = 0b11000000
    }
}