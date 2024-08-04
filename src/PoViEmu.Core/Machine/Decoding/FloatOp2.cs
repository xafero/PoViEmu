using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum FloatOp2 : byte
    {
        fld = 0b11000000,

        fxch = 0b11001000
    }
}