using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Opa1B : byte
    {
        not = 0b11010000,

        neg = 0b11011000,

        mul = 0b11100000,

        imul = 0b11101000,

        div = 0b11110000,

        idiv = 0b11111000
    }
}