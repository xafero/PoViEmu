using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Rga : byte
    {
        R2M = 0b00_000_000,

        // TODO Displace8Bit = 0b01_000_000,

        // TODO Displace16Bit = 0b10_000_000,

        R2R = 0b11_000_000,
    }
}