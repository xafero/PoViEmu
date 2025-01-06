// ReSharper disable InconsistentNaming

namespace PoViEmu.I186.ISA
{
    /// <summary>
    /// 8-bit registers in the x86 architecture
    /// </summary>
    public enum B8Register
    {
        None = 0,

        /// <summary>
        /// High byte of the AX register
        /// (typically used for arithmetic and logic operations)
        /// </summary>
        AH,

        /// <summary>
        /// Low byte of the AX register
        /// (typically used for arithmetic and logic operations)
        /// </summary>
        AL,

        /// <summary>
        /// High byte of the BX register
        /// (often used for address calculations)
        /// </summary>
        BH,

        /// <summary>
        /// Low byte of the BX register
        /// (often used for address calculations)
        /// </summary>
        BL,

        /// <summary>
        /// High byte of the CX register
        /// (used for loop counters and shift/rotate operations)
        /// </summary>
        CH,

        /// <summary>
        /// Low byte of the CX register
        /// (used for loop counters and shift/rotate operations)
        /// </summary>
        CL,

        /// <summary>
        /// High byte of the DX register
        /// (often used for I/O operations)
        /// </summary>
        DH,

        /// <summary>
        /// Low byte of the DX register
        /// (often used for I/O operations)
        /// </summary>
        DL
    }
}