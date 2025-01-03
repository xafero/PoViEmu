using System;

namespace PoViEmu.I186.ISA
{
    /// <summary>
    /// Intel 80186 flags
    /// ( ____ODITSZ_A_P_C )
    /// ( 5432109876543210 )
    /// </summary>
    [Flags]
    public enum Flagged : ushort
    {
        Carry = 1 << 0,
        Parity = 1 << 2,
        Auxiliary = 1 << 4,
        Zero = 1 << 6,
        Sign = 1 << 7,
        Trap = 1 << 8,
        Interrupt = 1 << 9,
        Direction = 1 << 10,
        Overflow = 1 << 11
    }
}