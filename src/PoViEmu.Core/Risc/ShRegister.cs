namespace PoViEmu.Core.Risc
{
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