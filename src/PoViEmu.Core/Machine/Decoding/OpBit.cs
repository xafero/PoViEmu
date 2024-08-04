using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum OpBit : byte
    {
        b08 = 0b00000000,

        b16 = 0b00000001
    }
}