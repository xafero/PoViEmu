using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum Opa1C : byte
    {
        pop = 0b11000000,
    }
}