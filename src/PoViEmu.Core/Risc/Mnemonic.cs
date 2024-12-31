// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace PoViEmu.Core.Risc
{
    public enum Mnemonic
    {
        Invalid = 0,

        /// <summary>
        /// Add Binary (Arithmetic Instruction) [8.2.1]
        /// </summary>
        Add,

        /// <summary>
        /// Add with Carry (Arithmetic Instruction) [8.2.2]
        /// </summary>
        Addc,

        /// <summary>
        /// Add with V Flag Overflow Check (Arithmetic Instruction) [8.2.3]
        /// </summary>
        Addv,

        /// <summary>
        /// AND Logical (Logic Operation Instruction) [8.2.4]
        /// </summary>
        And,

        /// <summary>
        /// AND Logical (Logic Operation Instruction) [8.2.4]
        /// </summary>
        AndB,

        /// <summary>
        /// Branch if False (Branch Instruction) [8.2.5]
        /// </summary>
        Bf,

        /// <summary>
        /// Branch if False with Delay Slot (Branch Instruction) [8.2.6]
        /// </summary>
        BfS,

        /// <summary>
        /// Branch (Branch Instruction) [8.2.7]
        /// </summary>
        Bra,

        /// <summary>
        /// Branch Far (Branch Instruction) [8.2.8]
        /// </summary>
        Braf,

        /// <summary>
        /// Branch to Subroutine (Branch Instruction) [8.2.9]
        /// </summary>
        Bsr,

        /// <summary>
        /// Branch to Subroutine Far (Branch Instruction) [8.2.10]
        /// </summary>
        Bsrf,

        /// <summary>
        /// Branch if True (Branch Instruction) [8.2.11]
        /// </summary>
        Bt,

        /// <summary>
        /// Branch if True with Delay Slot (Branch Instruction) [8.2.12]
        /// </summary>
        BtS,

        /// <summary>
        /// Clear MAC Register (System Control Instruction) [8.2.13]
        /// </summary>
        Clrmac,

        /// <summary>
        /// Clear S Bit (System Control Instruction) [8.2.14]
        /// </summary>
        Clrs,

        /// <summary>
        /// Clear T Bit (System Control Instruction) [8.2.15]
        /// </summary>
        Clrt,

        /// <summary>
        /// Compare Equal (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpEq,

        /// <summary>
        /// Compare Greater Equal (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpGe,

        /// <summary>
        /// Compare Greater (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpGt,

        /// <summary>
        /// Compare Higher (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpHi,

        /// <summary>
        /// Compare Higher or Same (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpHs,

        /// <summary>
        /// Compare Plus (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpPl,

        /// <summary>
        /// Compare Plus Zero (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpPz,

        /// <summary>
        /// Compare Strings (Arithmetic Instruction) [8.2.16]
        /// </summary>
        CmpStr,

        /// <summary>
        /// Divide Step 0 as Signed (Arithmetic Instruction) [8.2.17]
        /// </summary>
        Div0s,

        /// <summary>
        /// Divide Step 0 as Unsigned (Arithmetic Instruction) [8.2.18]
        /// </summary>
        Div0u,

        /// <summary>
        /// Divide Step 1 (Arithmetic Instruction) [8.2.19]
        /// </summary>
        Div1,

        /// <summary>
        /// Double-Length Multiply as Signed (Arithmetic Instruction) [8.2.20]
        /// </summary>
        DmulsL,

        /// <summary>
        /// Double-Length Multiply as Unsigned (Arithmetic Instruction) [8.2.21]
        /// </summary>
        DmuluL,

        /// <summary>
        /// Decrement and Test (Arithmetic Instruction) [8.2.22]
        /// </summary>
        Dt,

        /// <summary>
        /// Extend as Signed (Arithmetic Instruction) [8.2.23]
        /// </summary>
        ExtsB,

        /// <summary>
        /// Extend as Signed (Arithmetic Instruction) [8.2.23]
        /// </summary>
        ExtsW,

        /// <summary>
        /// Extend as Unsigned (Arithmetic Instruction) [8.2.24]
        /// </summary>
        ExtuB,

        /// <summary>
        /// Extend as Unsigned (Arithmetic Instruction) [8.2.24]
        /// </summary>
        ExtuW,

        /// <summary>
        /// Jump (Branch Instruction) [8.2.25]
        /// </summary>
        Jmp,

        /// <summary>
        /// Jump to Subroutine (Branch Instruction) [8.2.26]
        /// </summary>
        Jsr,

        /// <summary>
        /// Load to Control Register (System Control Instruction) [8.2.27]
        /// </summary>
        Ldc,

        /// <summary>
        /// Load to Control Register (System Control Instruction) [8.2.27]
        /// </summary>
        LdcL,

        /// <summary>
        /// Load to System Register (System Control Instruction) [8.2.30]
        /// </summary>
        Lds,

        /// <summary>
        /// Load to System Register (System Control Instruction) [8.2.30]
        /// </summary>
        LdsL,

        /// <summary>
        /// Load PTEH/PTEL to TLB (System Control Instruction) [8.2.31]
        /// </summary>
        Ldtlb,

        /// <summary>
        /// Multiply and Accumulate Long (Arithmetic Instruction) [8.2.32]
        /// </summary>
        MacL,

        /// <summary>
        /// Multiply and Accumulate (Arithmetic Instruction) [8.2.33]
        /// </summary>
        MacW,

        /// <summary>
        /// Move Data (Data Transfer Instruction) [8.2.34]
        /// </summary>
        Mov,

        /// <summary>
        /// Move Data (Data Transfer Instruction) [8.2.34]
        /// </summary>
        MovB,

        /// <summary>
        /// Move Data (Data Transfer Instruction) [8.2.34]
        /// </summary>
        MovL,

        /// <summary>
        /// Move Data (Data Transfer Instruction) [8.2.34]
        /// </summary>
        MovW,

        /// <summary>
        /// Move Effective Address (Data Transfer Instruction) [8.2.38]
        /// </summary>
        Mova,

        /// <summary>
        /// Move T Bit (Data Transfer Instruction) [8.2.39]
        /// </summary>
        Movt,

        /// <summary>
        /// Multiply Long (Arithmetic Instruction) [8.2.40]
        /// </summary>
        MulL,

        /// <summary>
        /// Multiply as Signed Word (Arithmetic Instruction) [8.2.41]
        /// </summary>
        MulsW,

        /// <summary>
        /// Multiply as Unsigned Word (Arithmetic Instruction) [8.2.42]
        /// </summary>
        MuluW,

        /// <summary>
        /// Negate (Arithmetic Instruction) [8.2.43]
        /// </summary>
        Neg,

        /// <summary>
        /// Negate with Carry (Arithmetic Instruction) [8.2.44]
        /// </summary>
        Negc,

        /// <summary>
        /// No Operation (System Control Instruction) [8.2.45]
        /// </summary>
        Nop,

        /// <summary>
        /// NOT Logical Complement (Logic Operation Instruction) [8.2.46]
        /// </summary>
        Not,

        /// <summary>
        /// OR Logical (Logic Operation Instruction) [8.2.47]
        /// </summary>
        Or,

        /// <summary>
        /// OR Logical (Logic Operation Instruction) [8.2.47]
        /// </summary>
        OrB,

        /// <summary>
        /// Prefetch Data to the Cache (System Control Instruction) [8.2.48]
        /// </summary>
        Pref,

        /// <summary>
        /// Rotate with Carry Left (Shift Instruction) [8.2.49]
        /// </summary>
        Rotcl,

        /// <summary>
        /// Rotate with Carry Right (Shift Instruction) [8.2.50]
        /// </summary>
        Rotcr,

        /// <summary>
        /// Rotate Left (Shift Instruction) [8.2.51]
        /// </summary>
        Rotl,

        /// <summary>
        /// Rotate Right (Shift Instruction) [8.2.52]
        /// </summary>
        Rotr,

        /// <summary>
        /// Return from Exception (System Control Instruction) [8.2.53]
        /// </summary>
        Rte,

        /// <summary>
        /// Return from Subroutine (Branch Instruction) [8.2.54]
        /// </summary>
        Rts,

        /// <summary>
        /// Set S Bit (System Control Instruction) [8.2.56]
        /// </summary>
        Sets,

        /// <summary>
        /// Set T Bit (System Control Instruction) [8.2.57]
        /// </summary>
        Sett,

        /// <summary>
        /// Shift Arithmetic Dynamically (Shift Instruction) [8.2.58]
        /// </summary>
        Shad,

        /// <summary>
        /// Shift Arithmetic Left (Shift Instruction) [8.2.59]
        /// </summary>
        Shal,

        /// <summary>
        /// Shift Arithmetic Right (Shift Instruction) [8.2.60]
        /// </summary>
        Shar,

        /// <summary>
        /// Shift Logical Dynamically (Shift Instruction) [8.2.61]
        /// </summary>
        Shld,

        /// <summary>
        /// Shift Logical Left (Shift Instruction) [8.2.62]
        /// </summary>
        Shll,

        /// <summary>
        /// Shift Logical Left n Bits (Shift Instruction) [8.2.63]
        /// </summary>
        Shll16,

        /// <summary>
        /// Shift Logical Left n Bits (Shift Instruction) [8.2.63]
        /// </summary>
        Shll2,

        /// <summary>
        /// Shift Logical Left n Bits (Shift Instruction) [8.2.63]
        /// </summary>
        Shll8,

        /// <summary>
        /// Shift Logical Right (Shift Instruction) [8.2.64]
        /// </summary>
        Shlr,

        /// <summary>
        /// Shift Logical Right n Bits (Shift Instruction) [8.2.65]
        /// </summary>
        Shlr16,

        /// <summary>
        /// Shift Logical Right n Bits (Shift Instruction) [8.2.65]
        /// </summary>
        Shlr2,

        /// <summary>
        /// Shift Logical Right n Bits (Shift Instruction) [8.2.65]
        /// </summary>
        Shlr8,

        /// <summary>
        /// Sleep (System Control Instruction) [8.2.66]
        /// </summary>
        Sleep,

        /// <summary>
        /// Store Control Register (System Control Instruction) [8.2.67]
        /// </summary>
        Stc,

        /// <summary>
        /// Store Control Register (System Control Instruction) [8.2.67]
        /// </summary>
        StcL,

        /// <summary>
        /// Store System Register (System Control Instruction) [8.2.68]
        /// </summary>
        Sts,

        /// <summary>
        /// Store System Register (System Control Instruction) [8.2.68]
        /// </summary>
        StsL,

        /// <summary>
        /// Subtract Binary (Arithmetic Instruction) [8.2.69]
        /// </summary>
        Sub,

        /// <summary>
        /// Subtract with Carry (Arithmetic Instruction) [8.2.70]
        /// </summary>
        Subc,

        /// <summary>
        /// Subtract with V Flag Underflow Check (Arithmetic Instruction) [8.2.71]
        /// </summary>
        Subv,

        /// <summary>
        /// Swap Register Halves (Data Transfer Instruction) [8.2.72]
        /// </summary>
        SwapB,

        /// <summary>
        /// Swap Register Halves (Data Transfer Instruction) [8.2.72]
        /// </summary>
        SwapW,

        /// <summary>
        /// Test and Set (Logic Operation Instruction) [8.2.73]
        /// </summary>
        TasB,

        /// <summary>
        /// Trap Always (System Control Instruction) [8.2.74]
        /// </summary>
        Trapa,

        /// <summary>
        /// Test Logical (Logic Operation Instruction) [8.2.75]
        /// </summary>
        Tst,

        /// <summary>
        /// Test Logical (Logic Operation Instruction) [8.2.75]
        /// </summary>
        TstB,

        /// <summary>
        /// Exclusive OR Logical (Logic Operation Instruction) [8.2.76]
        /// </summary>
        Xor,

        /// <summary>
        /// Exclusive OR Logical (Logic Operation Instruction) [8.2.76]
        /// </summary>
        XorB,

        /// <summary>
        /// Extract (Data Transfer Instruction) [8.2.77]
        /// </summary>
        Xtrct
    }
}