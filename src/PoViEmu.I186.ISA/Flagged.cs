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
        /// <summary>
        /// C
        /// </summary>
        Carry = 1 << 0,

        /// <summary>
        /// P
        /// </summary>
        Parity = 1 << 2,

        /// <summary>
        /// A
        /// </summary>
        Auxiliary = 1 << 4,

        /// <summary>
        /// Z
        /// </summary>
        Zero = 1 << 6,

        /// <summary>
        /// S
        /// </summary>
        Sign = 1 << 7,

        /// <summary>
        /// T
        /// </summary>
        Trap = 1 << 8,

        /// <summary>
        /// I
        /// </summary>
        Interrupt = 1 << 9,

        /// <summary>
        /// D
        /// </summary>
        Direction = 1 << 10,

        /// <summary>
        /// O
        /// </summary>
        Overflow = 1 << 11
    }
}