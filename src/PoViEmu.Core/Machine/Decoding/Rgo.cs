using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Rgo : byte
    {
        // al
        ax = 0b00_000_000,

        // bl
        bx = 0b00_011_000,

        // cl
        cx = 0b00_001_000,

        // dl
        dx = 0b00_010_000,

        // bh
        di = 0b00_111_000,

        // dh
        si = 0b00_110_000,

        // ch
        sp = 0b00_100_000,

        // ah
        bp = 0b00_101_000
    }
}