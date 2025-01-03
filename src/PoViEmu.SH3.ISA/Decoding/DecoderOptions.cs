using System;

namespace PoViEmu.SH3.ISA.Decoding
{
    [Flags]
    public enum DecoderOptions : uint
    {
        None = 0x00000000,

        NoInvalidCheck = 0x00000001
    }
}