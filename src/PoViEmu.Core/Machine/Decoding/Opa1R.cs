using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Opa1R : byte
    {
        rol = 0b11000000,

        ror = 0b11001000,

        rcl = 0b11010000,

        rcr = 0b11011000,

        shl = 0b11100000,

        shr = 0b11101000,

        sar = 0b11111000
    }
}