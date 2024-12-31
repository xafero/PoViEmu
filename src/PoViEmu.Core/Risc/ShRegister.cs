// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Risc
{
    /// <summary>
    /// Hitachi SH-3 registers
    /// </summary>
    public enum ShRegister
    {
        Unknown = 0,

        #region General-purpose registers

        /// <summary>
        /// General-purpose register R0
        /// </summary>
        R0,

        /// <summary>
        /// General-purpose register R1
        /// </summary>
        R1,

        /// <summary>
        /// General-purpose register R2
        /// </summary>
        R2,

        /// <summary>
        /// General-purpose register R3
        /// </summary>
        R3,

        /// <summary>
        /// General-purpose register R4
        /// </summary>
        R4,

        /// <summary>
        /// General-purpose register R5
        /// </summary>
        R5,

        /// <summary>
        /// General-purpose register R6
        /// </summary>
        R6,

        /// <summary>
        /// General-purpose register R7
        /// </summary>
        R7,

        /// <summary>
        /// General-purpose register R8
        /// </summary>
        R8,

        /// <summary>
        /// General-purpose register R9
        /// </summary>
        R9,

        /// <summary>
        /// General-purpose register R10
        /// </summary>
        R10,

        /// <summary>
        /// General-purpose register R11
        /// </summary>
        R11,

        /// <summary>
        /// General-purpose register R12
        /// </summary>
        R12,

        /// <summary>
        /// General-purpose register R13
        /// </summary>
        R13,

        /// <summary>
        /// General-purpose register R14
        /// (often used as a link register for subroutine calls)
        /// </summary>
        R14,

        /// <summary>
        /// Stack pointer register
        /// (serves as the stack pointer)
        /// </summary>
        R15,

        #endregion

        #region Banked general-purpose registers

        /// <summary>
        /// Banked version of R0
        /// </summary>
        R0_Bank,

        /// <summary>
        /// Banked version of R1
        /// </summary>
        R1_Bank,

        /// <summary>
        /// Banked version of R2
        /// </summary>
        R2_Bank,

        /// <summary>
        /// Banked version of R3
        /// </summary>
        R3_Bank,

        /// <summary>
        /// Banked version of R4
        /// </summary>
        R4_Bank,

        /// <summary>
        /// Banked version of R5
        /// </summary>
        R5_Bank,

        /// <summary>
        /// Banked version of R6
        /// </summary>
        R6_Bank,

        /// <summary>
        /// Banked version of R7
        /// </summary>
        R7_Bank,

        #endregion

        #region Multiply-accumulate registers

        /// <summary>
        /// High 32 bits of the MAC (Multiply-Accumulate) result
        /// </summary>
        MACH,

        /// <summary>
        /// Low 32 bits of the MAC (Multiply-Accumulate) result
        /// </summary>
        MACL,

        #endregion

        #region Special-purpose registers

        /// <summary>
        /// Global Base Register
        /// (used for addressing global memory)
        /// </summary>
        GBR,

        /// <summary>
        /// Vector Base Register
        /// (points to the exception vector table)
        /// </summary>
        VBR,

        /// <summary>
        /// Procedure Register
        /// (stores the return address for subroutine calls)
        /// </summary>
        PR,

        /// <summary>
        /// Status Register
        /// (contains processor state flags)
        /// </summary>
        SR,

        /// <summary>
        /// Saved Status Register
        /// (used during exceptions)
        /// </summary>
        SSR,

        /// <summary>
        /// Program Counter
        /// (points to the current instruction)
        /// </summary>
        PC,

        /// <summary>
        /// Saved Program Counter
        /// (used during exceptions)
        /// </summary>
        SPC

        #endregion
    }
}