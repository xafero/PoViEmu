using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Fgo : byte
    {
        st0 = 0b00_000_000,

        st1 = 0b00_000_001,

        st2 = 0b00_000_010,

        st3 = 0b00_000_011,

        st4 = 0b00_000_100,

        st5 = 0b00_000_101,

        st6 = 0b00_000_110,

        st7 = 0b00_000_111
    }
}