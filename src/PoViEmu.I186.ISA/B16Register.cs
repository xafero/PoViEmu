// ReSharper disable InconsistentNaming

namespace PoViEmu.I186.ISA
{
    /// <summary>
    /// 16-bit registers in the x86 architecture
    /// </summary>
    public enum B16Register
    {
        None = 0,

        /// <summary>
        /// Accumulator register
        /// (typically used for arithmetic and logic operations)
        /// </summary>
        AX,

        /// <summary>
        /// Base register
        /// (often used for address calculations)
        /// </summary>
        BX,

        /// <summary>
        /// Count register
        /// (used for loop counters and shift/rotate operations)
        /// </summary>
        CX,

        /// <summary>
        /// Data register
        /// (often used for I/O operations)
        /// </summary>
        DX,

        /// <summary>
        /// Base or frame pointer
        /// (commonly used to point to the base of the stack frame)
        /// </summary>
        BP,

        /// <summary>
        /// Instruction pointer
        /// (holds the address of the next instruction to execute)
        /// </summary>
        IP,

        /// <summary>
        /// Stack pointer
        /// (points to the top of the stack)
        /// </summary>
        SP,

        /// <summary>
        /// Destination index
        /// (used for string and array operations)
        /// </summary>
        DI,

        /// <summary>
        /// Source index
        /// (used for string and array operations)
        /// </summary>
        SI,

        /// <summary>
        /// Code segment register
        /// (points to the segment containing executable instructions)
        /// </summary>
        CS,

        /// <summary>
        /// Data segment register
        /// (points to the segment containing global and static data)
        /// </summary>
        DS,

        /// <summary>
        /// Extra segment register
        /// (provides additional segment for memory addressing)
        /// </summary>
        ES,

        /// <summary>
        /// Stack segment register
        /// (points to the segment containing the stack)
        /// </summary>
        SS
    }
}