// ReSharper disable InconsistentNaming

using System;

namespace PoViEmu.SH3.ISA
{
    /// <summary>
    /// Hitachi SH-3 flags
    /// ( _ORB__________________MQIIII__ST )
    /// ( 10987654321098765432109876543210 )
    /// </summary>
    [Flags]
    public enum Flagged : uint
    {
        T_bit = 1 << 0,
        S_bit = 1 << 1,
        I0 = 1 << 4,
        I1 = 1 << 5,
        I2 = 1 << 6,
        I3 = 1 << 7,
        Q_bit = 1 << 8,
        M_bit = 1 << 9,
        Block_bit = 1 << 28,
        Bank_bit = 1 << 29,
        Mode_bit = 1 << 30
    }
}