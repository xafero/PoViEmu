// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace PoViEmu.Core.Risc
{
    public enum OpCodes2
    {
        Invalid = 0,
        
        /// <summary>
        /// Add Binary (Arithmetic Instruction) [8.2.1]
        /// </summary>
        ADD,

        /// <summary>
        /// Add with Carry (Arithmetic Instruction) [8.2.2]
        /// </summary>
        ADDC,

        /// <summary>
        /// Add with V Flag Overflow Check (Arithmetic Instruction) [8.2.3]
        /// </summary>
        ADDV,

        /// <summary>
        /// AND Logical (Logic Operation Instruction) [8.2.4]
        /// </summary>
        AND,

        /// <summary>
        /// Branch if False (Branch Instruction) [8.2.5]
        /// </summary>
        BF,
        
        /// <summary>
        /// Branch if False with Delay Slot (Branch Instruction) [8.2.6]
        /// </summary>
        BFS,

        /// <summary>
        /// Branch (Branch Instruction) [8.2.7]
        /// </summary>
        BRA,

        /// <summary>
        /// Branch Far (Branch Instruction) [8.2.8]
        /// </summary>
        BRAF,

        /// <summary>
        /// Branch to Subroutine (Branch Instruction) [8.2.9]
        /// </summary>
        BSR,

        /// <summary>
        /// Branch to Subroutine Far (Branch Instruction) [8.2.10]
        /// </summary>
        BSRF,

        /// <summary>
        /// Branch if True (Branch Instruction) [8.2.11]
        /// </summary>
        BT,
        
        /// <summary>
        /// Branch if True with Delay Slot (Branch Instruction) [8.2.12]
        /// </summary>
        BTS,

        /// <summary>
        /// Clear MAC Register (System Control Instruction) [8.2.13]
        /// </summary>
        CLRMAC,

        /// <summary>
        /// Clear S Bit (System Control Instruction) [8.2.14]
        /// </summary>
        CLRS,

        /// <summary>
        /// Clear T Bit (System Control Instruction) [8.2.15]
        /// </summary>
        CLRT,

        /// <summary>
        /// Compare Conditionally (Arithmetic Instruction) [8.2.16]
        ///
        /// TODO: CMPEQ,CMPGE,CMPGT,CMPHI,CMPHS,CMPPL,CMPPZ,CMPSTR
        /// </summary>
        CMP,

        /// <summary>
        /// Divide Step 0 as Signed (Arithmetic Instruction) [8.2.17]
        /// </summary>
        DIV0S,

        /// <summary>
        /// Divide Step 0 as Unsigned (Arithmetic Instruction) [8.2.18]
        /// </summary>
        DIV0U,

        /// <summary>
        /// Divide Step 1 (Arithmetic Instruction) [8.2.19]
        /// </summary>
        DIV1,

        /// <summary>
        /// Double-Length Multiply as Signed (Arithmetic Instruction) [8.2.20]
        /// </summary>
        DMULS_L,

        /// <summary>
        /// Double-Length Multiply as Unsigned (Arithmetic Instruction) [8.2.21]
        /// </summary>
        DMULU_L,

        /// <summary>
        /// Decrement and Test (Arithmetic Instruction) [8.2.22]
        /// </summary>
        DT,

        /// <summary>
        /// Extend as Signed (Arithmetic Instruction) [8.2.23]
        /// </summary>
        EXTS,

        /// <summary>
        /// Extend as Unsigned (Arithmetic Instruction) [8.2.24]
        /// </summary>
        EXTU,

        /// <summary>
        /// Jump (Branch Instruction) [8.2.25]
        /// </summary>
        JMP,

        /// <summary>
        /// Jump to Subroutine (Branch Instruction) [8.2.26]
        /// </summary>
        JSR,

        /// <summary>
        /// Load to Control Register (System Control Instruction) [8.2.27]
        /// </summary>
        LDC,

        /// <summary>
        /// Load to System Register (System Control Instruction) [8.2.30]
        /// </summary>
        LDS,

        /// <summary>
        /// Load PTEH/PTEL to TLB (System Control Instruction) [8.2.31]
        /// </summary>
        LDTLB,
        
        /// <summary>
        /// Multiply and Accumulate Long (Arithmetic Instruction) [8.2.32]
        /// </summary>
        MACL,
        
        /// <summary>
        /// Multiply and Accumulate (Arithmetic Instruction) [8.2.33]
        /// </summary>
        MACW,

        /// <summary>
        /// Move Data (Data Transfer Instruction) [8.2.34]
        /// Move Immediate Data (Data Transfer Instruction) [8.2.35]
        /// Move Peripheral Data (Data Transfer Instruction) [8.2.36]
        /// Move Structure Data (Data Transfer Instruction) [8.2.37]
        ///
        /// TODO:     MOVB,MOVW,MOVL,MOVCAL
        /// </summary>
        MOV,

        /// <summary>
        /// Move Effective Address (Data Transfer Instruction) [8.2.38]
        /// </summary>
        MOVA,

        /// <summary>
        /// Move T Bit (Data Transfer Instruction) [8.2.39]
        /// </summary>
        MOVT,

        /// <summary>
        /// Multiply Long (Arithmetic Instruction) [8.2.40]
        /// </summary>
        MUL_L,

        /// <summary>
        /// Multiply as Signed Word (Arithmetic Instruction) [8.2.41]
        /// </summary>
        MULS_W,

        /// <summary>
        /// Multiply as Unsigned Word (Arithmetic Instruction) [8.2.42]
        /// </summary>
        MULU_W,

        /// <summary>
        /// Negate (Arithmetic Instruction) [8.2.43]
        /// </summary>
        NEG,

        /// <summary>
        /// Negate with Carry (Arithmetic Instruction) [8.2.44]
        /// </summary>
        NEGC,

        /// <summary>
        /// No Operation (System Control Instruction) [8.2.45]
        /// </summary>
        NOP,

        /// <summary>
        /// NOT Logical Complement (Logic Operation Instruction) [8.2.46]
        /// </summary>
        NOT,

        /// <summary>
        /// OR Logical (Logic Operation Instruction) [8.2.47]
        /// </summary>
        OR,

        /// <summary>
        /// Prefetch Data to the Cache () [8.2.48]
        /// </summary>
        PREF,

        /// <summary>
        /// Rotate with Carry Left (Shift Instruction) [8.2.49]
        /// </summary>
        ROTCL,

        /// <summary>
        /// Rotate with Carry Right (Shift Instruction) [8.2.50]
        /// </summary>
        ROTCR,

        /// <summary>
        /// Rotate Left (Shift Instruction) [8.2.51]
        /// </summary>
        ROTL,

        /// <summary>
        /// Rotate Right (Shift Instruction) [8.2.52]
        /// </summary>
        ROTR,

        /// <summary>
        /// Return from Exception (System Control Instruction) [8.2.53]
        /// </summary>
        RTE,

        /// <summary>
        /// Return from Subroutine (Branch Instruction) [8.2.54]
        /// </summary>
        RTS,

        /// <summary>
        /// Set Repeat Count to RC (System Control Instruction) [8.2.55]
        /// </summary>
        SETRC,

        /// <summary>
        /// Set S Bit (System Control Instruction) [8.2.56]
        /// </summary>
        SETS,

        /// <summary>
        /// Set T Bit (System Control Instruction) [8.2.57]
        /// </summary>
        SETT,

        /// <summary>
        /// Shift Arithmetic Dynamically (Shift Instruction) [8.2.58]
        /// </summary>
        SHAD,

        /// <summary>
        /// Shift Arithmetic Left (Shift Instruction) [8.2.59]
        /// </summary>
        SHAL,

        /// <summary>
        /// Shift Arithmetic Right (Shift Instruction) [8.2.60]
        /// </summary>
        SHAR,

        /// <summary>
        /// Shift Logical Dynamically (Shift Instruction) [8.2.61]
        /// </summary>
        SHLD,

        /// <summary>
        /// Shift Logical Left (Shift Instruction) [8.2.62]
        /// </summary>
        SHLL,

        /// <summary>
        /// Shift Logical Left n Bits (Shift Instruction) [8.2.63]
        /// </summary>
        SHLLn,

        /// <summary>
        /// Shift Logical Right (Shift Instruction) [8.2.64]
        /// </summary>
        SHLR,

        /// <summary>
        /// Shift Logical Right n Bits (Shift Instruction) [8.2.65]
        /// </summary>
        SHLRn,

        /// <summary>
        /// Sleep (System Control Instruction) [8.2.66]
        /// </summary>
        SLEEP,

        /// <summary>
        /// Store Control Register (System Control Instruction) [8.2.67]
        /// </summary>
        STC,

        /// <summary>
        /// Store System Register (System Control Instruction) [8.2.68]
        /// </summary>
        STS,

        /// <summary>
        /// Subtract Binary (Arithmetic Instruction) [8.2.69]
        /// </summary>
        SUB,

        /// <summary>
        /// Subtract with Carry (Arithmetic Instruction) [8.2.70]
        /// </summary>
        SUBC,

        /// <summary>
        /// Subtract with V Flag Underflow Check (Arithmetic Instruction) [8.2.71]
        /// </summary>
        SUBV,

        /// <summary>
        /// Swap Register Halves (Data Transfer Instruction) [8.2.72]
        /// </summary>
        SWAP,

        /// <summary>
        /// Test and Set (Logic Operation Instruction) [8.2.73]
        /// </summary>
        TAS,

        /// <summary>
        /// Trap Always (System Control Instruction) [8.2.74]
        /// </summary>
        TRAPA,

        /// <summary>
        /// Test Logical (Logic Operation Instruction) [8.2.75]
        /// </summary>
        TST,

        /// <summary>
        /// Exclusive OR Logical (Logic Operation Instruction) [8.2.76]
        /// </summary>
        XOR,

        /// <summary>
        /// Extract (Data Transfer Instruction) [8.2.77]
        /// </summary>
        XTRCT
    }
}