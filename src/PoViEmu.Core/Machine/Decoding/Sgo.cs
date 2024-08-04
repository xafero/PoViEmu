using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Sgo : byte
    {
        es = 0b00_000_000,

        ds = 0b00_011_000,

        cs = 0b00_001_000,

        ss = 0b00_010_000,

        fs = 0b00_100_000,

        gs = 0b00_101_000
    }
}