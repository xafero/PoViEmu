using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Opa1A : byte
    {
        inc = 0b11000000,

        dec = 0b11001000,

        call = 0b11010000,

        jmp = 0b11100000,

        push = 0b11110000
    }
}