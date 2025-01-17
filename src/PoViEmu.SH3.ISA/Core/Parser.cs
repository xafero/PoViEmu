using PoViEmu.SH3.ISA.Decoding;
using static PoViEmu.SH3.ISA.Core.ArgTool;
using static PoViEmu.SH3.ISA.ShRegister;
using O = PoViEmu.SH3.ISA.Mnemonic;
using X = PoViEmu.SH3.ISA.Core.InstTool;

namespace PoViEmu.SH3.ISA.Core
{
    public static class Parser
    {
        public static Instruction Parse(IByteReader reader)
        {
            var first = reader.ReadByte();
            byte second = 0;
            var hadSec = false;
            switch (first)
            {
                case 0b00000000:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    switch (second)
                    {
                        case 0b00000000:
                            return DoNull();

                        case 0b00001000:
                            // Clear T Bit
                            return X.Create(first, second, O.Clrt);
                        case 0b00001001:
                            // No Operation
                            return X.Create(first, second, O.Nop);
                        case 0b00001011:
                            // Return from Subroutine
                            return X.Create(first, second, O.Rts);
                        case 0b00011000:
                            // Set T Bit
                            return X.Create(first, second, O.Sett);
                        case 0b00011001:
                            // Divide Step 0 as Unsigned
                            return X.Create(first, second, O.Div0u);
                        case 0b00011011:
                            // Sleep
                            return X.Create(first, second, O.Sleep);
                        case 0b00101000:
                            // Clear MAC Register
                            return X.Create(first, second, O.Clrmac);
                        case 0b00101011:
                            // Return from Exception
                            return X.Create(first, second, O.Rte);
                        case 0b00111000:
                            // Load PTEH/PTEL to TLB
                            return X.Create(first, second, O.Ldtlb);
                        case 0b01001000:
                            // Clear S Bit
                            return X.Create(first, second, O.Clrs);
                        case 0b01011000:
                            // Set S Bit
                            return X.Create(first, second, O.Sets);
                    }
                    break;
                case 0b10000000:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovB, a: [R0, B(X.SplitByte(second))]);
                case 0b10000001:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovW, a: [R0, W(X.SplitByte(second))]);
                case 0b10000100:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovB, a: [B(X.SplitByte(second)), R0]);
                case 0b10000101:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovW, a: [W(X.SplitByte(second)), R0]);
                case 0b10001000:
                    // Compare Conditionally
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.CmpEq, a: [I(second), R0]);
                case 0b10001001:
                    // Branch if True
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Bt, a: [Ac(PC, second)]);
                case 0b10001011:
                    // Branch if False
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Bf, a: [Ac(PC, second)]);
                case 0b10001101:
                    // Branch if True with Delay Slot
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.BtS, a: [Ac(PC, second)]);
                case 0b10001111:
                    // Branch if False with Delay Slot
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.BfS, a: [Ac(PC, second)]);
                case 0b11000000:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovB, a: [R0, B(GBR, second)]);
                case 0b11000001:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovW, a: [R0, W(GBR, second)]);
                case 0b11000010:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovL, a: [R0, L(GBR, second)]);
                case 0b11000011:
                    // Trap Always
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Trapa, a: [U(second)]);
                case 0b11000100:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovB, a: [B(GBR, second), R0]);
                case 0b11000101:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovW, a: [W(GBR, second), R0]);
                case 0b11000110:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovL, a: [L(GBR, second), R0]);
                case 0b11000111:
                    // Move Effective Address
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Mova, a: [Am(PC, second), R0]);
                case 0b11001000:
                    // Test Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Tst, a: [U(second), R0]);
                case 0b11001001:
                    // AND Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.And, a: [U(second), R0]);
                case 0b11001010:
                    // Exclusive OR Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Xor, a: [U(second), R0]);
                case 0b11001011:
                    // OR Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Or, a: [U(second), R0]);
                case 0b11001100:
                    // Test Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.TstB, a: [U(second), B(GBR, R0)]);
                case 0b11001101:
                    // AND Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.AndB, a: [U(second), B(GBR, R0)]);
                case 0b11001110:
                    // XOR
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.XorB, a: [U(second), B(GBR, R0)]);
                case 0b11001111:
                    // OR Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.OrB, a: [U(second), B(GBR, R0)]);
            }

            var (high, low) = X.SplitByte(first);
            byte secH, secL;
            switch (high)
            {
                case 0b0000:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    switch (second)
                    {
                        case 0b00000010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [SR, R(low)]);
                        case 0b00000011:
                            // Branch to Subroutine Far
                            return X.Create(first, second, O.Bsrf, a: [R(low)]);
                        case 0b00001010:
                            // Store System Register
                            return X.Create(first, second, O.Sts, a: [MACH, R(low)]);
                        case 0b00010010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [GBR, R(low)]);
                        case 0b00011010:
                            // Store System Register
                            return X.Create(first, second, O.Sts, a: [MACL, R(low)]);
                        case 0b00100010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [VBR, R(low)]);
                        case 0b00100011:
                            // Branch Far
                            return X.Create(first, second, O.Braf, a: [R(low)]);
                        case 0b00101001:
                            // Move T Bit
                            return X.Create(first, second, O.Movt, a: [R(low)]);
                        case 0b00101010:
                            // Store System Register
                            return X.Create(first, second, O.Sts, a: [PR, R(low)]);
                        case 0b00110010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [SSR, R(low)]);
                        case 0b01000010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [SPC, R(low)]);
                        case 0b01010010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R5_Bank, R(low)]);
                        case 0b01100010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R6_Bank, R(low)]);
                        case 0b01101010:
                            return DoNull();
                        case 0b01110010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R7_Bank, R(low)]);
                        case 0b01111010:
                            return DoNull();
                        case 0b10000010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R0_Bank, R(low)]);
                        case 0b10000011:
                            // Prefetch Data to the Cache
                            return X.Create(first, second, O.Pref, a: [RrL(low)]);
                        case 0b10001010:
                            return DoNull();
                        case 0b10010010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R1_Bank, R(low)]);
                        case 0b10011010:
                            return DoNull();
                        case 0b10100010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R2_Bank, R(low)]);
                        case 0b10101010:
                            return DoNull();
                        case 0b10110010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R3_Bank, R(low)]);
                        case 0b10111010:
                            return DoNull();
                        case 0b11000010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R4_Bank, R(low)]);
                        case 0b11010010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R5_Bank, R(low)]);
                        case 0b11100010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R6_Bank, R(low)]);
                        case 0b11110010:
                            // Store Control Register
                            return X.Create(first, second, O.Stc, a: [R7_Bank, R(low)]);
                    }

                    (secH, secL) = X.SplitByte(second);
                    switch (secL)
                    {
                        case 0b0100:
                            // Move Data
                            return X.Create(first, second, O.MovB, a: [R(secH), B(low, R0)]);
                        case 0b0101:
                            // Move Data
                            return X.Create(first, second, O.MovW, a: [R(secH), W(low, R0)]);
                        case 0b0110:
                            // Move Data
                            return X.Create(first, second, O.MovL, a: [R(secH), L(low, R0)]);
                        case 0b0111:
                            // Multiply Long
                            return X.Create(first, second, O.MulL, a: [R(secH), R(low)]);
                        case 0b1100:
                            // Move Data
                            return X.Create(first, second, O.MovB, a: [B(secH, R0), R(low)]);
                        case 0b1101:
                            // Move Data
                            return X.Create(first, second, O.MovW, a: [W(secH, R0), R(low)]);
                        case 0b1110:
                            // Move Data
                            return X.Create(first, second, O.MovL, a: [L(secH, R0), R(low)]);
                        case 0b1111:
                            // Multiply and Accumulate Long
                            return X.Create(first, second, O.MacL, a: [RpL(secH), RpL(low)]);
                    }
                    break;
                case 0b0001:
                    // Move Structure Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var (src, dis) = X.SplitByte(second);
                    return X.Create(first, second, O.MovL, a: [R(src), L((low, dis))]);
                case 0b0010:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);

                    (secH, secL) = X.SplitByte(second);
                    switch (secL)
                    {
                        case 0b0000:
                            // Move Data
                            return X.Create(first, second, O.MovB, a: [R(secH), RrB(low)]);
                        case 0b0001:
                            // Move Data
                            return X.Create(first, second, O.MovW, a: [R(secH), RrW(low)]);
                        case 0b0010:
                            // Move Data
                            return X.Create(first, second, O.MovL, a: [R(secH), RrL(low)]);
                        case 0b0100:
                            // Move Data
                            return X.Create(first, second, O.MovB, a: [R(secH), RmB(low)]);
                        case 0b0101:
                            // Move Data
                            return X.Create(first, second, O.MovW, a: [R(secH), RmW(low)]);
                        case 0b0110:
                            // Move Data
                            return X.Create(first, second, O.MovL, a: [R(secH), RmL(low)]);
                        case 0b0111:
                            // Divide Step 0 as Signed
                            return X.Create(first, second, O.Div0s, a: [R(secH), R(low)]);
                        case 0b1000:
                            // Test Logical
                            return X.Create(first, second, O.Tst, a: [R(secH), R(low)]);
                        case 0b1001:
                            // AND Logical
                            return X.Create(first, second, O.And, a: [R(secH), R(low)]);
                        case 0b1010:
                            // Exclusive OR Logical
                            return X.Create(first, second, O.Xor, a: [R(secH), R(low)]);
                        case 0b1011:
                            // OR Logical
                            return X.Create(first, second, O.Or, a: [R(secH), R(low)]);
                        case 0b1100:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpStr, a: [R(secH), R(low)]);
                        case 0b1101:
                            // Extract
                            return X.Create(first, second, O.Xtrct, a: [R(secH), R(low)]);
                        case 0b1110:
                            // Multiply as Unsigned Word
                            return X.Create(first, second, O.MuluW, a: [R(secH), R(low)]);
                        case 0b1111:
                            // Multiply as Signed Word
                            return X.Create(first, second, O.MulsW, a: [R(secH), R(low)]);
                    }
                    break;
                case 0b0011:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);

                    (secH, secL) = X.SplitByte(second);
                    switch (secL)
                    {
                        case 0b0000:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpEq, a: [R(secH), R(low)]);
                        case 0b0010:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpHs, a: [R(secH), R(low)]);
                        case 0b0011:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpGe, a: [R(secH), R(low)]);
                        case 0b0100:
                            // Divide Step 1
                            return X.Create(first, second, O.Div1, a: [R(secH), R(low)]);
                        case 0b0101:
                            // Double-Length Multiply as Unsigned
                            return X.Create(first, second, O.DmuluL, a: [R(secH), R(low)]);
                        case 0b0110:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpHi, a: [R(secH), R(low)]);
                        case 0b0111:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpGt, a: [R(secH), R(low)]);
                        case 0b1000:
                            // Subtract Binary
                            return X.Create(first, second, O.Sub, a: [R(secH), R(low)]);
                        case 0b1010:
                            // Subtract with Carry
                            return X.Create(first, second, O.Subc, a: [R(secH), R(low)]);
                        case 0b1011:
                            // Subtract with V Flag Underflow Check
                            return X.Create(first, second, O.Subv, a: [R(secH), R(low)]);
                        case 0b1100:
                            // Add Binary
                            return X.Create(first, second, O.Add, a: [R(secH), R(low)]);
                        case 0b1101:
                            // Double-Length Multiply as Signed
                            return X.Create(first, second, O.DmulsL, a: [R(secH), R(low)]);
                        case 0b1110:
                            // Add with Carry
                            return X.Create(first, second, O.Addc, a: [R(secH), R(low)]);
                        case 0b1111:
                            // Add with V Flag Overflow Check
                            return X.Create(first, second, O.Addv, a: [R(secH), R(low)]);
                    }
                    break;
                case 0b0100:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    switch (second)
                    {
                        case 0b00000110:
                            // Load to System Register
                            return X.Create(first, second, O.LdsL, a: [RpL(low), MACH]);
                        case 0b00000111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), SR]);
                        case 0b00001010:
                            // Load to System Register
                            return X.Create(first, second, O.Lds, a: [R(low), MACH]);
                        case 0b00001110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), SR]);
                        case 0b00010110:
                            // Load to System Register
                            return X.Create(first, second, O.LdsL, a: [RpL(low), MACL]);
                        case 0b00010111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), GBR]);
                        case 0b00011010:
                            // Load to System Register
                            return X.Create(first, second, O.Lds, a: [R(low), MACL]);
                        case 0b00011110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), GBR]);
                        case 0b00100110:
                            // Load to System Register
                            return X.Create(first, second, O.LdsL, a: [RpL(low), PR]);
                        case 0b00100111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), VBR]);
                        case 0b00101010:
                            // Load to System Register
                            return X.Create(first, second, O.Lds, a: [R(low), PR]);
                        case 0b00101110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), VBR]);
                        case 0b00110111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), SSR]);
                        case 0b00111110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), SSR]);
                        case 0b01000111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), SPC]);
                        case 0b01001110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), SPC]);
                        case 0b01010111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R5_Bank]);
                        case 0b01011110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R5_Bank]);
                        case 0b01100110:
                            return DoNull();
                        case 0b01100111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R6_Bank]);
                        case 0b01101010:
                            return DoNull();
                        case 0b01101110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R6_Bank]);
                        case 0b01110110:
                            return DoNull();
                        case 0b01110111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R7_Bank]);
                        case 0b01111010:
                            return DoNull();
                        case 0b01111110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R7_Bank]);
                        case 0b10000111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R0_Bank]);
                        case 0b10001010:
                            return DoNull();
                        case 0b10001110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R0_Bank]);
                        case 0b10010111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R1_Bank]);
                        case 0b10011010:
                            return DoNull();
                        case 0b10011110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R1_Bank]);
                        case 0b10100111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R2_Bank]);
                        case 0b10101010:
                            return DoNull();
                        case 0b10101110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R2_Bank]);
                        case 0b10110111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R3_Bank]);
                        case 0b10111010:
                            return DoNull();
                        case 0b10111110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R3_Bank]);
                        case 0b11000111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R4_Bank]);
                        case 0b11001110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R4_Bank]);
                        case 0b11010111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R5_Bank]);
                        case 0b11011110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R5_Bank]);
                        case 0b11100111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R6_Bank]);
                        case 0b11101110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R6_Bank]);
                        case 0b11110111:
                            // Load to Control Register
                            return X.Create(first, second, O.LdcL, a: [RpL(low), R7_Bank]);
                        case 0b11111110:
                            // Load to Control Register
                            return X.Create(first, second, O.Ldc, a: [R(low), R7_Bank]);
                        case 0b00000000:
                            // Shift Logical Left
                            return X.Create(first, second, O.Shll, a: [R(low)]);
                        case 0b00000001:
                            // Shift Logical Right
                            return X.Create(first, second, O.Shlr, a: [R(low)]);
                        case 0b00000010:
                            // Store System Register
                            return X.Create(first, second, O.StsL, a: [MACH, RmL(low)]);
                        case 0b00000011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [SR, RmL(low)]);
                        case 0b00000100:
                            // Rotate Left
                            return X.Create(first, second, O.Rotl, a: [R(low)]);
                        case 0b00000101:
                            // Rotate Right
                            return X.Create(first, second, O.Rotr, a: [R(low)]);
                        case 0b00001000:
                            // Shift Logical Left n Bits
                            return X.Create(first, second, O.Shll2, a: [R(low)]);
                        case 0b00001001:
                            // Shift Logical Right n Bits
                            return X.Create(first, second, O.Shlr2, a: [R(low)]);
                        case 0b00001011:
                            // Jump to Subroutine
                            return X.Create(first, second, O.Jsr, a: [RrL(low)]);
                        case 0b00010000:
                            // Decrement and Test
                            return X.Create(first, second, O.Dt, a: [R(low)]);
                        case 0b00010001:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpPz, a: [R(low)]);
                        case 0b00010010:
                            // Store System Register
                            return X.Create(first, second, O.StsL, a: [MACL, RmL(low)]);
                        case 0b00010011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [GBR, RmL(low)]);
                        case 0b00010101:
                            // Compare Conditionally
                            return X.Create(first, second, O.CmpPl, a: [R(low)]);
                        case 0b00011000:
                            // Shift Logical Left n Bits
                            return X.Create(first, second, O.Shll8, a: [R(low)]);
                        case 0b00011001:
                            // Shift Logical Right n Bits
                            return X.Create(first, second, O.Shlr8, a: [R(low)]);
                        case 0b00011011:
                            // Test and Set
                            return X.Create(first, second, O.TasB, a: [RrB(low)]);
                        case 0b00100000:
                            // Shift Arithmetic Left
                            return X.Create(first, second, O.Shal, a: [R(low)]);
                        case 0b00100001:
                            // Shift Arithmetic Right
                            return X.Create(first, second, O.Shar, a: [R(low)]);
                        case 0b00100010:
                            // Store System Register
                            return X.Create(first, second, O.StsL, a: [PR, RmL(low)]);
                        case 0b00100011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [VBR, RmL(low)]);
                        case 0b00100100:
                            // Rotate with Carry Left
                            return X.Create(first, second, O.Rotcl, a: [R(low)]);
                        case 0b00100101:
                            // Rotate with Carry Right
                            return X.Create(first, second, O.Rotcr, a: [R(low)]);
                        case 0b00101000:
                            // Shift Logical Left n Bits
                            return X.Create(first, second, O.Shll16, a: [R(low)]);
                        case 0b00101001:
                            // Shift Logical Right n Bits
                            return X.Create(first, second, O.Shlr16, a: [R(low)]);
                        case 0b00101011:
                            // Jump
                            return X.Create(first, second, O.Jmp, a: [RrL(low)]);
                        case 0b00110011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [SSR, RmL(low)]);
                        case 0b01000011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [SPC, RmL(low)]);
                        case 0b01010011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R5_Bank, RmL(low)]);
                        case 0b01100010:
                            return DoNull();
                        case 0b01100011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R6_Bank, RmL(low)]);
                        case 0b01110011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R7_Bank, RmL(low)]);
                        case 0b10000010:
                            return DoNull();
                        case 0b10000011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R0_Bank, RmL(low)]);
                        case 0b10000110:
                            return DoNull();
                        case 0b10010010:
                            return DoNull();
                        case 0b10010011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R1_Bank, RmL(low)]);
                        case 0b10010110:
                            return DoNull();
                        case 0b10100010:
                            return DoNull();
                        case 0b10100011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R2_Bank, RmL(low)]);
                        case 0b10100110:
                            return DoNull();
                        case 0b10110010:
                            return DoNull();
                        case 0b10110011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R3_Bank, RmL(low)]);
                        case 0b10110110:
                            return DoNull();
                        case 0b11000011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R4_Bank, RmL(low)]);
                        case 0b11010011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R5_Bank, RmL(low)]);
                        case 0b11100011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R6_Bank, RmL(low)]);
                        case 0b11110011:
                            // Store Control Register
                            return X.Create(first, second, O.StcL, a: [R7_Bank, RmL(low)]);
                    }

                    (secH, secL) = X.SplitByte(second);
                    switch (secL)
                    {
                        case 0b1100:
                            // Shift Arithmetic Dynamically
                            return X.Create(first, second, O.Shad, a: [R(secH), R(low)]);
                        case 0b1101:
                            // Shift Logical Dynamically
                            return X.Create(first, second, O.Shld, a: [R(secH), R(low)]);
                        case 0b1111:
                            // Multiply and Accumulate
                            return X.Create(first, second, O.MacW, a: [RpW(secH), RpW(low)]);
                    }
                    break;
                case 0b0101:
                    // Move Structure Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovL, a: [L(X.SplitByte(second)), R(low)]);
                case 0b0110:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);

                    (secH, secL) = X.SplitByte(second);
                    switch (secL)
                    {
                        case 0b0000:
                            // Move Data
                            return X.Create(first, second, O.MovB, a: [RrB(secH), R(low)]);
                        case 0b0001:
                            // Move Data
                            return X.Create(first, second, O.MovW, a: [RrW(secH), R(low)]);
                        case 0b0010:
                            // Move Data
                            return X.Create(first, second, O.MovL, a: [RrL(secH), R(low)]);
                        case 0b0011:
                            // Move Data
                            return X.Create(first, second, O.Mov, a: [R(secH), R(low)]);
                        case 0b0100:
                            // Move Data
                            return X.Create(first, second, O.MovB, a: [RpB(secH), R(low)]);
                        case 0b0101:
                            // Move Data
                            return X.Create(first, second, O.MovW, a: [RpW(secH), R(low)]);
                        case 0b0110:
                            // Move Data
                            return X.Create(first, second, O.MovL, a: [RpL(secH), R(low)]);
                        case 0b0111:
                            // NOT Logical Complement
                            return X.Create(first, second, O.Not, a: [R(secH), R(low)]);
                        case 0b1000:
                            // Swap Register Halves
                            return X.Create(first, second, O.SwapB, a: [R(secH), R(low)]);
                        case 0b1001:
                            // Swap Register Halves
                            return X.Create(first, second, O.SwapW, a: [R(secH), R(low)]);
                        case 0b1010:
                            // Negate with Carry
                            return X.Create(first, second, O.Negc, a: [R(secH), R(low)]);
                        case 0b1011:
                            // Negate
                            return X.Create(first, second, O.Neg, a: [R(secH), R(low)]);
                        case 0b1100:
                            // Extend as Unsigned
                            return X.Create(first, second, O.ExtuB, a: [R(secH), R(low)]);
                        case 0b1101:
                            // Extend as Unsigned
                            return X.Create(first, second, O.ExtuW, a: [R(secH), R(low)]);
                        case 0b1110:
                            // Extend as Signed
                            return X.Create(first, second, O.ExtsB, a: [R(secH), R(low)]);
                        case 0b1111:
                            // Extend as Signed
                            return X.Create(first, second, O.ExtsW, a: [R(secH), R(low)]);
                    }
                    break;
                case 0b0111:
                    // Add Binary
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Add, a: [I(second), R(low)]);
                case 0b1001:
                    // Move Immediate Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovW, a: [Wa(PC, second), R(low)]);
                case 0b1010:
                    // Branch
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Bra, a: [Au(PC, X.CombineBytes(low, second))]);
                case 0b1011:
                    // Branch to Subroutine
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Bsr, a: [Au(PC, X.CombineBytes(low, second))]);
                case 0b1101:
                    // Move Immediate Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.MovL, a: [La(PC, second), R(low)]);
                case 0b1110:
                    // Move Immediate Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return X.Create(first, second, O.Mov, a: [I(second), R(low)]);
            }

            return DoNull();
        }

        private static Instruction DoNull() => new([], default) { IsInvalid = true };
    }
}