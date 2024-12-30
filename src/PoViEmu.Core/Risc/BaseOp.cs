// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Risc
{
    public abstract record BaseOperand();

    /// <summary>
    /// dddd: Displacement
    /// </summary>
    public sealed record DisplOperand(ushort Val) : BaseOperand
    {
        public override string ToString()
        {
            return $"0x{Val:x}";
        }
    }

    /// <summary>
    /// iiii: Immediate data
    /// </summary>
    public sealed record ImmedUOperand(byte Val) : BaseOperand
    {
        public override string ToString()
        {
            return $"#{Val}";
        }
    }

    /// <summary>
    /// iiii: Immediate data
    /// </summary>
    public sealed record ImmedOperand(sbyte Val) : BaseOperand
    {
        public override string ToString()
        {
            return $"#{Val}";
        }
    }

    public sealed record MemoryOperand(ShRegister Off, ShRegister? Base = null, bool isPlus = false)
        : BaseOperand
    {
        public override string ToString()
        {
            var @base = Base?.Name();
            var off = Off.Name();
            var suffix = isPlus ? "+" : "";
            if (@base == null)
                return $"@{off}{suffix}";

            return $"@({off},{@base})";
        }
    }

    public abstract record RegOperand : BaseOperand
    {
        public abstract ShRegister Reg { get; init; }

        public sealed override string ToString()
        {
            var name = Reg.Name();
            return name;
        }
    }

    /// <summary>
    /// nnnn: Destination register
    /// </summary>
    public sealed record DestReg(ShRegister Reg) : RegOperand;

    /// <summary>
    /// mmmm: Source register
    /// </summary>
    public sealed record SourceReg(ShRegister Reg) : RegOperand;

    public enum ShRegister
    {
        Unknown = 0,

        R0,
        R1,
        R2,
        R3,
        R4,
        R5,
        R6,
        R7,
        R8,
        R9,
        R10,
        R11,
        R12,
        R13,
        R14,
        R15,

        R0_Bank,
        R1_Bank,
        R2_Bank,
        R3_Bank,
        R4_Bank,
        R5_Bank,
        R6_Bank,
        R7_Bank,

        MACH,
        
        MACL,

        /// <summary>
        /// Global Base Register
        /// </summary>
        GBR,
        
        PR,

        /// <summary>
        /// Saved Program Counter
        /// </summary>
        SPC,

        SR,

        SSR,

        VBR
    }
}