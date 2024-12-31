using O = PoViEmu.Core.Risc.Mnemonic;
using R = PoViEmu.Core.Risc.ShRegister;
using T = PoViEmu.Core.Risc.InstTool;

namespace PoViEmu.Core.Risc
{
    public sealed class Parser
    {
        public static Instruction? Parse(IReader reader)
        {
            var first = reader.ReadNextByte();
            byte second = default;
            var hadSec = false;
            byte imm;
            byte dis;
            ushort dsp;
            byte dst;
            byte src;
            switch (first)
            {
                case 0b00000000:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    switch (second)
                    {
                        case 0b00000000:
                            return null;

                        case 0b00001000:
                            // Clear T Bit
                            return T.Create(first, second, O.Clrt);
                        case 0b00001001:
                            // No Operation
                            return T.Create(first, second, O.Nop);
                        case 0b00001011:
                            // Return from Subroutine
                            return T.Create(first, second, O.Rts);
                        case 0b00011000:
                            // Set T Bit
                            return T.Create(first, second, O.Sett);
                        case 0b00011001:
                            // Divide Step 0 as Unsigned
                            return T.Create(first, second, O.Div0u);
                        case 0b00011011:
                            // Sleep
                            return T.Create(first, second, O.Sleep);
                        case 0b00101000:
                            // Clear MAC Register
                            return T.Create(first, second, O.Clrmac);
                        case 0b00101011:
                            // Return from Exception
                            return T.Create(first, second, O.Rte);
                        case 0b00111000:
                            // Load PTEH/PTEL to TLB
                            return T.Create(first, second, O.Ldtlb);
                        case 0b01001000:
                            // Clear S Bit
                            return T.Create(first, second, O.Clrs);
                        case 0b01011000:
                            // Set S Bit
                            return T.Create(first, second, O.Sets);
                    }
                    break;
                case 0b10000000:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var (movN1, movD1) = T.SplitByte(second);
                    return T.Create(first, second, O.MovB, a: [T.N(0), T.M(movN1, movD1)]);
                case 0b10000001:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var (movN2, movD2) = T.SplitByte(second);
                    var movD2d = (ushort)(movD2 * 2);
                    return T.Create(first, second, O.MovW, a: [T.N(0), T.M(movN2, movD2d)]);
                case 0b10000100:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var (movM1, movD3) = T.SplitByte(second);
                    return T.Create(first, second, O.MovB, a: [T.M(movM1, movD3), T.N(0)]);
                case 0b10000101:
                    // Move data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var (movM2, movD4) = T.SplitByte(second);
                    var movD4d = (ushort)(movD4 * 2);
                    return T.Create(first, second, O.MovW, a: [T.M(movM2, movD4d), T.N(0)]);
                case 0b10001000:
                    // Compare Conditionally
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.CmpEq, i: imm, n: 0);
                case 0b10001001:
                    // Branch if True
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var btDis = (uint)((sbyte)second * 2 + 4);
                    return T.Create(first, second, O.Bt, a: [T.D(btDis)]);
                case 0b10001011:
                    // Branch if False
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var bfDis = (uint)((sbyte)second * 2 + 4);
                    return T.Create(first, second, O.Bf, a: [T.D(bfDis)]);
                case 0b10001101:
                    // Branch if True with Delay Slot
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var btsDis = (uint)((sbyte)second * 2 + 4);
                    return T.Create(first, second, O.BtS, a: [T.D(btsDis)]);
                case 0b10001111:
                    // Branch if False with Delay Slot
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var bfsDis = (uint)((sbyte)second * 2 + 4);
                    return T.Create(first, second, O.BfS, a: [T.D(bfsDis)]);
                case 0b11000000:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    dis = second;
                    return T.Create(first, second, O.MovB, a: [T.N(0), T.M(R.GBR, dis)]);
                case 0b11000001:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var psDis = (ushort)(second * 2);
                    return T.Create(first, second, O.MovW, a: [T.R0, T.M(R.GBR, psDis)]);
                case 0b11000010:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var ppDis = (ushort)(second * 4);
                    return T.Create(first, second, O.MovL, a: [T.R0, T.M(R.GBR, ppDis)]);
                case 0b11000011:
                    // Trap Always
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.Trapa, ui: imm);
                case 0b11000100:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    return T.Create(first, second, O.MovB, a: [T.M(R.GBR, second), T.R0]);
                case 0b11000101:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var mDis = (ushort)(second * 2);
                    return T.Create(first, second, O.MovW, a: [T.M(R.GBR, mDis), T.R0]);
                case 0b11000110:
                    // Move Peripheral Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var pDis = (ushort)(second * 4);
                    return T.Create(first, second, O.MovL, a: [T.M(R.GBR, pDis), T.N(0)]);
                case 0b11000111:
                    // Move Effective Address
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var maDis = (ushort)(second * 4 + 4);
                    return T.Create(first, second, O.Mova, a: [T.D(maDis), T.N(0)]);
                case 0b11001000:
                    // Test Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.Tst, ui: imm, m: 0);
                case 0b11001001:
                    // AND Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.And, ui: imm, n: 0);
                case 0b11001010:
                    // Exclusive OR Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.Xor, ui: imm, n: 0);
                case 0b11001011:
                    // OR Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.Or, ui: imm, n: 0);
                case 0b11001100:
                    // Test Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.TstB, ui: imm, a: T.R0Gbr);
                case 0b11001101:
                    // AND Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.AndB, ui: imm, a: T.R0Gbr);
                case 0b11001110:
                    // XOR
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.XorB, ui: imm, a: T.R0Gbr);
                case 0b11001111:
                    // OR Logical
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    imm = second;
                    return T.Create(first, second, O.OrB, ui: imm, a: T.R0Gbr);
            }

            var (high, low) = T.SplitByte(first);
            switch (high)
            {
                case 0b0000:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    switch (second)
                    {
                        case 0b00000010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.Sr, T.N(low)]);
                        case 0b00000011:
                            // Branch to Subroutine Far 
                            return T.Create(first, second, O.Bsrf, n: low);
                        case 0b00001010:
                            // Store System Register
                            return T.Create(first, second, O.Sts, a: [T.Mach, T.N(low)]);
                        case 0b00010010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.Gbr, T.N(low)]);
                        case 0b00011010:
                            // Store System Register
                            return T.Create(first, second, O.Sts, a: [T.Macl, T.N(low)]);
                        case 0b00100010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.Vbr, T.N(low)]);
                        case 0b00100011:
                            // Branch Far
                            return T.Create(first, second, O.Braf, n: low);
                        case 0b00101001:
                            // Move T Bit
                            return T.Create(first, second, O.Movt, n: low);
                        case 0b00101010:
                            // Store System Register
                            return T.Create(first, second, O.Sts, a: [T.Pr, T.N(low)]);
                        case 0b00110010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.Ssr, T.N(low)]);
                        case 0b01000010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.Spc, T.N(low)]);
                        case 0b01010010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R5Bank, T.N(low)]);
                        case 0b01100010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R6Bank, T.N(low)]);
                        case 0b01101010:
                            return null;
                        case 0b01110010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R7Bank, T.N(low)]);
                        case 0b01111010:
                            return null;
                        case 0b10000010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R0Bank, T.N(low)]);
                        case 0b10000011:
                            // Prefetch Data to the Cache
                            return T.Create(first, second, O.Pref, n: low, nIsRef: true);
                        case 0b10001010:
                            return null;
                        case 0b10010010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R1Bank, T.N(low)]);
                        case 0b10011010:
                            return null;
                        case 0b10100010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R2Bank, T.N(low)]);
                        case 0b10101010:
                            return null;
                        case 0b10110010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R3Bank, T.N(low)]);
                        case 0b10111010:
                            return null;
                        case 0b11000010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R4Bank, T.N(low)]);
                        case 0b11010010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R5Bank, T.N(low)]);
                        case 0b11100010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R6Bank, T.N(low)]);
                        case 0b11110010:
                            // Store Control Register
                            return T.Create(first, second, O.Stc, a: [T.R7Bank, T.N(low)]);
                    }

                    var (secH, secL) = T.SplitByte(second);
                    switch (secL)
                    {
                        case 0b0100:
                            // Move Data
                            return T.Create(first, second, O.MovB, a: [T.N(secH), T.M(low, R.R0)]);
                        case 0b0101:
                            // Move Data
                            return T.Create(first, second, O.MovW, a: [T.N(secH), T.M(low, R.R0)]);
                        case 0b0110:
                            // Move Data
                            return T.Create(first, second, O.MovL, a: [T.N(secH), T.M(low, R.R0)]);
                        case 0b0111:
                            // Multiply Long
                            return T.Create(first, second, O.MulL, n: low, m: secH);
                        case 0b1100:
                            // Move Data
                            return T.Create(first, second, O.MovB, a: [T.M(secH, R.R0), T.M(low)]);
                        case 0b1101:
                            // Move Data
                            return T.Create(first, second, O.MovW, a: [T.M(secH, R.R0), T.M(low)]);
                        case 0b1110:
                            // Move Data
                            return T.Create(first, second, O.MovL, a: [T.M(secH, R.R0), T.M(low)]);
                        case 0b1111:
                            // Multiply and Accumulate Long
                            return T.Create(first, second, O.MacL, n: low, m: secH, nIsRefP: true, mIsRefP: true);
                    }
                    break;
                case 0b0001:
                    // Move Structure Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    (src, dis) = T.SplitByte(second);
                    var msDis = (ushort)(dis * 4);
                    return T.Create(first, second, O.MovL, a: [T.N(src), T.M(low, msDis)]);
                case 0b0010:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);

                    var (secH2, secL2) = T.SplitByte(second);
                    switch (secL2)
                    {
                        case 0b0000:
                            // Move Data
                            return T.Create(first, second, O.MovB, m: secH2, n: low, nIsRef: true);
                        case 0b0001:
                            // Move Data
                            return T.Create(first, second, O.MovW, n: low, m: secH2, nIsRef: true);
                        case 0b0010:
                            // Move Data
                            return T.Create(first, second, O.MovL, n: low, m: secH2, nIsRef: true);
                        case 0b0100:
                            // Move Data
                            return T.Create(first, second, O.MovB, a: [T.N(secH2), T.Nm(low)]);
                        case 0b0101:
                            // Move Data
                            return T.Create(first, second, O.MovW, a: [T.N(secH2), T.Nm(low)]);
                        case 0b0110:
                            // Move Data
                            return T.Create(first, second, O.MovL, a: [T.N(secH2), T.Nm(low)]);
                        case 0b0111:
                            // Divide Step 0 as Signed
                            return T.Create(first, second, O.Div0s, n: low, m: secH2);
                        case 0b1000:
                            // Test Logical
                            return T.Create(first, second, O.Tst, n: low, m: secH2);
                        case 0b1001:
                            // AND Logical
                            return T.Create(first, second, O.And, n: low, m: secH2);
                        case 0b1010:
                            // Exclusive OR Logical
                            return T.Create(first, second, O.Xor, n: low, m: secH2);
                        case 0b1011:
                            // OR Logical
                            return T.Create(first, second, O.Or, n: low, m: secH2);
                        case 0b1100:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpStr, n: low, m: secH2);
                        case 0b1101:
                            // Extract
                            return T.Create(first, second, O.Xtrct, n: low, m: secH2);
                        case 0b1110:
                            // Multiply as Unsigned Word
                            return T.Create(first, second, O.MuluW, n: low, m: secH2);
                        case 0b1111:
                            // Multiply as Signed Word
                            return T.Create(first, second, O.MulsW, n: low, m: secH2);
                    }
                    break;
                case 0b0011:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);

                    var (secH3, secL3) = T.SplitByte(second);
                    switch (secL3)
                    {
                        case 0b0000:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpEq, n: low, m: secH3);
                        case 0b0010:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpHs, n: low, m: secH3);
                        case 0b0011:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpGe, n: low, m: secH3);
                        case 0b0100:
                            // Divide Step 1
                            return T.Create(first, second, O.Div1, n: low, m: secH3);
                        case 0b0101:
                            // Double-Length Multiply as Unsigned
                            return T.Create(first, second, O.DmuluL, n: low, m: secH3);
                        case 0b0110:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpHi, n: low, m: secH3);
                        case 0b0111:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpGt, n: low, m: secH3);
                        case 0b1000:
                            // Subtract Binary
                            return T.Create(first, second, O.Sub, n: low, m: secH3);
                        case 0b1010:
                            // Subtract with Carry
                            return T.Create(first, second, O.Subc, n: low, m: secH3);
                        case 0b1011:
                            // Subtract with V Flag Underflow Check
                            return T.Create(first, second, O.Subv, n: low, m: secH3);
                        case 0b1100:
                            // Add Binary
                            return T.Create(first, second, O.Add, n: low, m: secH3);
                        case 0b1101:
                            // Double-Length Multiply as Signed
                            return T.Create(first, second, O.DmulsL, n: low, m: secH3);
                        case 0b1110:
                            // Add with Carry
                            return T.Create(first, second, O.Addc, n: low, m: secH3);
                        case 0b1111:
                            // Add with V Flag Overflow Check
                            return T.Create(first, second, O.Addv, n: low, m: secH3);
                    }
                    break;
                case 0b0100:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    switch (second)
                    {
                        case 0b00000110:
                            // Load to System Register
                            return T.Create(first, second, O.LdsL, m: low, a: T.Mach, mIsRefP: true);
                        case 0b00000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.Sr, mIsRefP: true);
                        case 0b00001010:
                            // Load to System Register
                            return T.Create(first, second, O.Lds, m: low, a: T.Mach);
                        case 0b00001110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.Sr);
                        case 0b00010110:
                            // Load to System Register
                            return T.Create(first, second, O.LdsL, m: low, a: T.Macl, mIsRefP: true);
                        case 0b00010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.Gbr, mIsRefP: true);
                        case 0b00011010:
                            // Load to System Register
                            return T.Create(first, second, O.Lds, m: low, a: T.Macl);
                        case 0b00011110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.Gbr);
                        case 0b00100110:
                            // Load to System Register
                            return T.Create(first, second, O.LdsL, m: low, a: T.Pr, mIsRefP: true);
                        case 0b00100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.Vbr, mIsRefP: true);
                        case 0b00101010:
                            // Load to System Register
                            return T.Create(first, second, O.Lds, m: low, a: T.Pr);
                        case 0b00101110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.Vbr);
                        case 0b00110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.Ssr, mIsRefP: true);
                        case 0b00111110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.Ssr);
                        case 0b01000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.Spc, mIsRefP: true);
                        case 0b01001110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.Spc);
                        case 0b01010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R5Bank, mIsRefP: true);
                        case 0b01011110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R5Bank);
                        case 0b01100110:
                            return null;
                        case 0b01100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R6Bank, mIsRefP: true);
                        case 0b01101010:
                            return null;
                        case 0b01101110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R6Bank);
                        case 0b01110110:
                            return null;
                        case 0b01110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R7Bank, mIsRefP: true);
                        case 0b01111010:
                            return null;
                        case 0b01111110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R7Bank);
                        case 0b10000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R0Bank, mIsRefP: true);
                        case 0b10001010:
                            return null;
                        case 0b10001110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R0Bank);
                        case 0b10010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R1Bank, mIsRefP: true);
                        case 0b10011010:
                            return null;
                        case 0b10011110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R1Bank);
                        case 0b10100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R2Bank, mIsRefP: true);
                        case 0b10101010:
                            return null;
                        case 0b10101110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R2Bank);
                        case 0b10110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R3Bank, mIsRefP: true);
                        case 0b10111010:
                            return null;
                        case 0b10111110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R3Bank);
                        case 0b11000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R4Bank, mIsRefP: true);
                        case 0b11001110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R4Bank);
                        case 0b11010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R5Bank, mIsRefP: true);
                        case 0b11011110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R5Bank);
                        case 0b11100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R6Bank, mIsRefP: true);
                        case 0b11101110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R6Bank);
                        case 0b11110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LdcL, m: low, a: T.R7Bank, mIsRefP: true);
                        case 0b11111110:
                            // Load to Control Register
                            return T.Create(first, second, O.Ldc, m: low, a: T.R7Bank);
                        case 0b00000000:
                            // Shift Logical Left
                            return T.Create(first, second, O.Shll, n: low);
                        case 0b00000001:
                            // Shift Logical Right
                            return T.Create(first, second, O.Shlr, n: low);
                        case 0b00000010:
                            // Store System Register
                            return T.Create(first, second, O.StsL, a: [T.Mach, T.Nm(low)]);
                        case 0b00000011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.Sr, T.Nm(low)]);
                        case 0b00000100:
                            // Rotate Left
                            return T.Create(first, second, O.Rotl, n: low);
                        case 0b00000101:
                            // Rotate Right
                            return T.Create(first, second, O.Rotr, n: low);
                        case 0b00001000:
                            // Shift Logical Left n Bits
                            return T.Create(first, second, O.Shll2, n: low);
                        case 0b00001001:
                            // Shift Logical Right n Bits
                            return T.Create(first, second, O.Shlr2, n: low);
                        case 0b00001011:
                            // Jump to Subroutine
                            return T.Create(first, second, O.Jsr, n: low, nIsRef: true);
                        case 0b00010000:
                            // Decrement and Test
                            return T.Create(first, second, O.Dt, n: low);
                        case 0b00010001:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpPz, n: low);
                        case 0b00010010:
                            // Store System Register
                            return T.Create(first, second, O.StsL, a: [T.Macl, T.Nm(low)]);
                        case 0b00010011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.Gbr, T.Nm(low)]);
                        case 0b00010101:
                            // Compare Conditionally
                            return T.Create(first, second, O.CmpPl, n: low);
                        case 0b00011000:
                            // Shift Logical Left n Bits
                            return T.Create(first, second, O.Shll8, n: low);
                        case 0b00011001:
                            // Shift Logical Right n Bits
                            return T.Create(first, second, O.Shlr8, n: low);
                        case 0b00011011:
                            // Test and Set
                            return T.Create(first, second, O.TasB, n: low, nIsRef: true);
                        case 0b00100000:
                            // Shift Arithmetic Left
                            return T.Create(first, second, O.Shal, n: low);
                        case 0b00100001:
                            // Shift Arithmetic Right
                            return T.Create(first, second, O.Shar, n: low);
                        case 0b00100010:
                            // Store System Register
                            return T.Create(first, second, O.StsL, a: [T.Pr, T.Nm(low)]);
                        case 0b00100011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.Vbr, T.Nm(low)]);
                        case 0b00100100:
                            // Rotate with Carry Left
                            return T.Create(first, second, O.Rotcl, n: low);
                        case 0b00100101:
                            // Rotate with Carry Right
                            return T.Create(first, second, O.Rotcr, n: low);
                        case 0b00101000:
                            // Shift Logical Left n Bits
                            return T.Create(first, second, O.Shll16, n: low);
                        case 0b00101001:
                            // Shift Logical Right n Bits
                            return T.Create(first, second, O.Shlr16, n: low);
                        case 0b00101011:
                            // Jump
                            return T.Create(first, second, O.Jmp, n: low, nIsRef: true);
                        case 0b00110011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.Ssr, T.Nm(low)]);
                        case 0b01000011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.Spc, T.Nm(low)]);
                        case 0b01010011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R5Bank, T.Nm(low)]);
                        case 0b01100010:
                            return null;
                        case 0b01100011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R6Bank, T.Nm(low)]);
                        case 0b01110011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R7Bank, T.Nm(low)]);
                        case 0b10000010:
                            return null;
                        case 0b10000011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R0Bank, T.Nm(low)]);
                        case 0b10000110:
                            return null;
                        case 0b10010010:
                            return null;
                        case 0b10010011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R1Bank, T.Nm(low)]);
                        case 0b10010110:
                            return null;
                        case 0b10100010:
                            return null;
                        case 0b10100011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R2Bank, T.Nm(low)]);
                        case 0b10100110:
                            return null;
                        case 0b10110010:
                            return null;
                        case 0b10110011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R3Bank, T.Nm(low)]);
                        case 0b10110110:
                            return null;
                        case 0b11000011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R4Bank, T.Nm(low)]);
                        case 0b11010011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R5Bank, T.Nm(low)]);
                        case 0b11100011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R6Bank, T.Nm(low)]);
                        case 0b11110011:
                            // Store Control Register
                            return T.Create(first, second, O.StcL, a: [T.R7Bank, T.Nm(low)]);
                    }

                    var (secH4, secL4) = T.SplitByte(second);
                    switch (secL4)
                    {
                        case 0b1100:
                            // Shift Arithmetic Dynamically
                            return T.Create(first, second, O.Shad, n: low, m: secH4);
                        case 0b1101:
                            // Shift Logical Dynamically
                            return T.Create(first, second, O.Shld, n: low, m: secH4);
                        case 0b1111:
                            // Multiply and Accumulate
                            return T.Create(first, second, O.MacW, n: low, m: secH4, nIsRefP: true, mIsRefP: true);
                    }
                    break;
                case 0b0101:
                    // Move Structure Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    var (secH5, secL5) = T.SplitByte(second);
                    var secL5d = (ushort)(secL5 * 4);
                    return T.Create(first, second, O.MovL, a: [T.M(secH5, secL5d), T.N(low)]);
                case 0b0110:
                    reader.LoadSecIfNeeded(ref second, ref hadSec);

                    var (secH6, secL6) = T.SplitByte(second);
                    switch (secL6)
                    {
                        case 0b0000:
                            // Move Data
                            return T.Create(first, second, O.MovB, n: low, m: secH6, mIsRef: true);
                        case 0b0001:
                            // Move Data
                            return T.Create(first, second, O.MovW, n: low, m: secH6, mIsRef: true);
                        case 0b0010:
                            // Move Data
                            return T.Create(first, second, O.MovL, n: low, m: secH6, mIsRef: true);
                        case 0b0011:
                            // Move Data
                            return T.Create(first, second, O.Mov, n: low, m: secH6);
                        case 0b0100:
                            // Move Data
                            return T.Create(first, second, O.MovB, n: low, m: secH6, mIsRefP: true);
                        case 0b0101:
                            // Move Data
                            return T.Create(first, second, O.MovW, n: low, m: secH6, mIsRefP: true);
                        case 0b0110:
                            // Move Data
                            return T.Create(first, second, O.MovL, n: low, m: secH6, mIsRefP: true);
                        case 0b0111:
                            // NOT Logical Complement
                            return T.Create(first, second, O.Not, n: low, m: secH6);
                        case 0b1000:
                            // Swap Register Halves
                            return T.Create(first, second, O.SwapB, n: low, m: secH6);
                        case 0b1001:
                            // Swap Register Halves
                            return T.Create(first, second, O.SwapW, n: low, m: secH6);
                        case 0b1010:
                            // Negate with Carry
                            return T.Create(first, second, O.Negc, n: low, m: secH6);
                        case 0b1011:
                            // Negate
                            return T.Create(first, second, O.Neg, n: low, m: secH6);
                        case 0b1100:
                            // Extend as Unsigned
                            return T.Create(first, second, O.ExtuB, n: low, m: secH6);
                        case 0b1101:
                            // Extend as Unsigned
                            return T.Create(first, second, O.ExtuW, n: low, m: secH6);
                        case 0b1110:
                            // Extend as Signed
                            return T.Create(first, second, O.ExtsB, n: low, m: secH6);
                        case 0b1111:
                            // Extend as Signed
                            return T.Create(first, second, O.ExtsW, n: low, m: secH6);
                    }
                    break;
                case 0b0111:
                    // Add Binary
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    dst = low;
                    imm = second;
                    return T.Create(first, second, O.Add, n: dst, i: imm);
                case 0b1001:
                    // Move Immediate Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    dst = low;
                    var bDis = (ushort)(second * 2 + 4);
                    return T.Create(first, second, O.MovW, a: [T.D(bDis), T.N(dst)]);
                case 0b1010:
                    // Branch
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    dsp = T.CombineBytes(low, second);
                    var braDis = (uint)((short)dsp * 2 + 4);
                    return T.Create(first, second, O.Bra, a: [T.D(braDis)]);
                case 0b1011:
                    // Branch to Subroutine
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    dsp = T.CombineBytes(low, second);
                    var brasDis = (uint)((short)dsp * 2 + 4);
                    return T.Create(first, second, O.Bsr, a: [T.D(brasDis)]);
                case 0b1101:
                    // Move Immediate Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    dst = low;
                    var mDis = (ushort)(second * 4 + 4);
                    return T.Create(first, second, O.MovL, a: [T.D(mDis), T.N(dst)]);
                case 0b1110:
                    // Move Immediate Data
                    reader.LoadSecIfNeeded(ref second, ref hadSec);
                    dst = low;
                    imm = second;
                    return T.Create(first, second, O.Mov, n: dst, i: imm);
            }

            return null;
        }
    }
}