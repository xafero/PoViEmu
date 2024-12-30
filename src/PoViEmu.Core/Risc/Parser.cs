using O = PoViEmu.Core.Risc.OpCodes;
using T = PoViEmu.Core.Risc.InstTool;

namespace PoViEmu.Core.Risc
{
    public sealed class Parser
    {
        public static Instruction? Parse(IReader reader)
        {
            var first = reader.ReadNextByte();
            byte second;
            byte imm;
            byte dis;
            ushort dsp;
            byte dst;
            byte src;
            switch (first)
            {
                case 0b00000000:
                    second = reader.ReadNextByte();
                    switch (second)
                    {
                        case 0b00000000:
                            return null;
                        
                        case 0b00001000:
                            // Clear T Bit
                            return T.Create(first, second, O.CLRT);
                        case 0b00001001:
                            // No Operation
                            return T.Create(first, second, O.NOP);
                        case 0b00001011:
                            // Return from Subroutine
                            return T.Create(first, second, O.RTS);
                        case 0b00011000:
                            // Set T Bit
                            return T.Create(first, second, O.SETT);
                        case 0b00011001:
                            // Divide Step 0 as Unsigned
                            return T.Create(first, second, O.DIV0U);
                        case 0b00011011:
                            // Sleep
                            return T.Create(first, second, O.SLEEP);
                        case 0b00101000:
                            // Clear MAC Register
                            return T.Create(first, second, O.CLRMAC);
                        case 0b00101011:
                            // Return from Exception
                            return T.Create(first, second, O.RTE);
                        case 0b00111000:
                            // Load PTEH/PTEL to TLB
                            return T.Create(first, second, O.LDTLB);
                        case 0b01001000:
                            // Clear S Bit
                            return T.Create(first, second, O.CLRS);
                        case 0b01011000:
                            // Set S Bit
                            return T.Create(first, second, O.SETS);
                    }
                    break;
                case 0b10000000:
                    // Move data
                    second = reader.ReadNextByte();
                    var (movN1, movD1) = T.SplitByte(second);
                    return T.Create(first, second, O.MOV, n: movN1, d: movD1);
                case 0b10000001:
                    // Move data
                    second = reader.ReadNextByte();
                    var (movN2, movD2) = T.SplitByte(second);
                    return T.Create(first, second, O.MOV, n: movN2, d: movD2);
                case 0b10000010:
                    // Set Repeat Count to RC
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.SETRC, i: imm);
                case 0b10000100:
                    // Move data
                    second = reader.ReadNextByte();
                    var (movM1, movD3) = T.SplitByte(second);
                    return T.Create(first, second, O.MOV, m: movM1, d: movD3);
                case 0b10000101:
                    // Move data
                    second = reader.ReadNextByte();
                    var (movM2, movD4) = T.SplitByte(second);
                    return T.Create(first, second, O.MOV, m: movM2, d: movD4);
                case 0b10001000:
                    // Compare Conditionally
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.CMP, i: imm);
                case 0b10001001:
                    // Branch if True
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BT, d: dis);
                case 0b10001011:
                    // Branch if False
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BF, d: dis);
                case 0b10001101:
                    // Branch if True with Delay Slot
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BT, d: dis);
                case 0b10001111:
                    // Branch if False with Delay Slot
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BF, d: dis);
                case 0b11000000:
                    // Move Peripheral Data
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000001:
                    // Move Peripheral Data
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000010:
                    // Move Peripheral Data
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000011:
                    // Trap Always
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.TRAPA, i: imm);
                case 0b11000100:
                    // Move Peripheral Data
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000101:
                    // Move Peripheral Data
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000110:
                    // Move Peripheral Data
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000111:
                    // Move Effective Address
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOVA, d: dis);
                case 0b11001000:
                    // Test Logical
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.TST, i: imm);
                case 0b11001001:
                    // AND Logical
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.AND, i: imm);
                case 0b11001010:
                    // Exclusive OR Logical
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.XOR, i: imm);
                case 0b11001011:
                    // OR Logical
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.OR, i: imm);
                case 0b11001100:
                    // Test Logical
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.TST, i: imm);
                case 0b11001101:
                    // AND Logical
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.AND, i: imm);
                case 0b11001111:
                    // OR Logical
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.OR, i: imm);
            }

            var (high, low) = T.SplitByte(first);
            switch (high)
            {
                case 0b0000:
                    second = reader.ReadNextByte();
                    switch (second)
                    {
                        case 0b00000010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b00000011:
                            // Branch to Subroutine Far 
                            return T.Create(first, second, O.BSRF, n: low);
                        case 0b00001010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b00010010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b00011010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b00100010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b00100011:
                            // Branch Far
                            return T.Create(first, second, O.BRAF, n: low);
                        case 0b00101001:
                            // Move T Bit
                            return T.Create(first, second, O.MOVT, n: low);
                        case 0b00101010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b00110010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01000010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01010010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01100010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01101010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b01110010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01111010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10000010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10000011:
                            // Prefetch Data to the Cache
                            return T.Create(first, second, O.PREF, n: low);
                        case 0b10001010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10010010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10011010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10100010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10101010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10110010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10111010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b11000010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b11010010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b11100010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b11110010:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                    }

                    var (secH, secL) = T.SplitByte(second);
                    switch (secL)
                    {
                        case 0b0100:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH);
                        case 0b0101:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH);
                        case 0b0110:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH);
                        case 0b0111:
                            // Multiply Long
                            return T.Create(first, second, O.MUL, n: low, m: secH);
                        case 0b1100:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH);
                        case 0b1101:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH);
                        case 0b1110:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH);
                        case 0b1111:
                            // Multiply and Accumulate Long
                            return T.Create(first, second, O.MAC, n: low, m: secH);
                    }
                    break;
                case 0b0001:
                    // Move Structure Data
                    second = reader.ReadNextByte();
                    (src, dis) = T.SplitByte(first);
                    return T.Create(first, second, O.MOV, n: low, m: src, d: dis);
                case 0b0010:
                    second = reader.ReadNextByte();

                    var (secH2, secL2) = T.SplitByte(second);
                    switch (secL2)
                    {
                        case 0b0000:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH2);
                        case 0b0001:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH2);
                        case 0b0010:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH2);
                        case 0b0100:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH2);
                        case 0b0101:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH2);
                        case 0b0110:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH2);
                        case 0b0111:
                            // Divide Step 0 as Signed
                            return T.Create(first, second, O.DIV0S, n: low, m: secH2);
                        case 0b1000:
                            // Test Logical
                            return T.Create(first, second, O.TST, n: low, m: secH2);
                        case 0b1001:
                            // AND Logical
                            return T.Create(first, second, O.AND, n: low, m: secH2);
                        case 0b1010:
                            // Exclusive OR Logical
                            return T.Create(first, second, O.XOR, n: low, m: secH2);
                        case 0b1011:
                            // OR Logical
                            return T.Create(first, second, O.OR, n: low, m: secH2);
                        case 0b1100:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low, m: secH2);
                        case 0b1101:
                            // Extract
                            return T.Create(first, second, O.XTRCT, n: low, m: secH2);
                        case 0b1110:
                            // Multiply as Unsigned Word
                            return T.Create(first, second, O.MULU, n: low, m: secH2);
                        case 0b1111:
                            // Multiply as Signed Word
                            return T.Create(first, second, O.MULS, n: low, m: secH2);
                    }
                    break;
                case 0b0011:
                    second = reader.ReadNextByte();

                    var (secH3, secL3) = T.SplitByte(second);
                    switch (secL3)
                    {
                        case 0b0000:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low, m: secH3);
                        case 0b0010:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low, m: secH3);
                        case 0b0011:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low, m: secH3);
                        case 0b0100:
                            // Divide Step 1
                            return T.Create(first, second, O.DIV1, n: low, m: secH3);
                        case 0b0101:
                            // Double-Length Multiply as Unsigned
                            return T.Create(first, second, O.DMULU, n: low, m: secH3);
                        case 0b0110:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low, m: secH3);
                        case 0b0111:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low, m: secH3);
                        case 0b1000:
                            // Subtract Binary
                            return T.Create(first, second, O.SUB, n: low, m: secH3);
                        case 0b1010:
                            // Subtract with Carry
                            return T.Create(first, second, O.SUBC, n: low, m: secH3);
                        case 0b1011:
                            // Subtract with V Flag Underflow Check
                            return T.Create(first, second, O.SUBV, n: low, m: secH3);
                        case 0b1100:
                            // Add Binary
                            return T.Create(first, second, O.ADD, n: low, m: secH3);
                        case 0b1101:
                            // Double-Length Multiply as Signed
                            return T.Create(first, second, O.DMULS, n: low, m: secH3);
                        case 0b1110:
                            // Add with Carry
                            return T.Create(first, second, O.ADDC, n: low, m: secH3);
                        case 0b1111:
                            // Add with V Flag Overflow Check
                            return T.Create(first, second, O.ADDV, n: low, m: secH3);
                    }
                    break;
                case 0b0100:
                    second = reader.ReadNextByte();
                    switch (second)
                    {
                        case 0b00000110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b00000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00001010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b00001110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00010100:
                            // Set Repeat Count to RC
                            return T.Create(first, second, O.SETRC, m: low);
                        case 0b00010110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b00010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00011010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b00011110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00100110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b00100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00101010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b00101110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00111110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01001110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01011110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01100110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b01100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01101010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b01101110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01110110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b01110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b01111010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b01111110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10001010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b10001110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10011010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b10011110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10101010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b10101110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b10111010:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, m: low);
                        case 0b10111110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11000111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11001110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11010111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11011110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11100111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11101110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11110111:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b11111110:
                            // Load to Control Register
                            return T.Create(first, second, O.LDC, m: low);
                        case 0b00000000:
                            // Shift Logical Left
                            return T.Create(first, second, O.SHLL, n: low);
                        case 0b00000001:
                            // Shift Logical Right
                            return T.Create(first, second, O.SHLR, n: low);
                        case 0b00000010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b00000011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b00000100:
                            // Rotate Left
                            return T.Create(first, second, O.ROTL, n: low);
                        case 0b00000101:
                            // Rotate Right
                            return T.Create(first, second, O.ROTR, n: low);
                        case 0b00001000:
                            // Shift Logical Left n Bits
                            return T.Create(first, second, O.SHLL, n: low);
                        case 0b00001001:
                            // Shift Logical Right n Bits
                            return T.Create(first, second, O.SHLR, n: low);
                        case 0b00001011:
                            // Jump to Subroutine
                            return T.Create(first, second, O.JSR, n: low);
                        case 0b00010000:
                            // Decrement and Test
                            return T.Create(first, second, O.DT, n: low);
                        case 0b00010001:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low);
                        case 0b00010010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b00010011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b00010101:
                            // Compare Conditionally
                            return T.Create(first, second, O.CMP, n: low);
                        case 0b00011000:
                            // Shift Logical Left n Bits
                            return T.Create(first, second, O.SHLL, n: low);
                        case 0b00011001:
                            // Shift Logical Right n Bits
                            return T.Create(first, second, O.SHLR, n: low);
                        case 0b00011011:
                            // Test and Set
                            return T.Create(first, second, O.TAS, n: low);
                        case 0b00100000:
                            // Shift Arithmetic Left
                            return T.Create(first, second, O.SHAL, n: low);
                        case 0b00100001:
                            // Shift Arithmetic Right
                            return T.Create(first, second, O.SHAR, n: low);
                        case 0b00100010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b00100011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b00100100:
                            // Rotate with Carry Left
                            return T.Create(first, second, O.ROTCL, n: low);
                        case 0b00100101:
                            // Rotate with Carry Right
                            return T.Create(first, second, O.ROTCR, n: low);
                        case 0b00101000:
                            // Shift Logical Left n Bits
                            return T.Create(first, second, O.SHLL, n: low);
                        case 0b00101001:
                            // Shift Logical Right n Bits
                            return T.Create(first, second, O.SHLR, n: low);
                        case 0b00101011:
                            // Jump
                            return T.Create(first, second, O.JMP, n: low);
                        case 0b00110011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01000011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01010011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01100010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b01100011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b01110011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10000010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10000011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10000110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, n: low);
                        case 0b10010010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10010011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10010110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, n: low);
                        case 0b10100010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10100011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10100110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, n: low);
                        case 0b10110010:
                            // Store System Register
                            return T.Create(first, second, O.STS, n: low);
                        case 0b10110011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b10110110:
                            // Load to System Register
                            return T.Create(first, second, O.LDS, n: low);
                        case 0b11000011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b11010011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b11100011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                        case 0b11110011:
                            // Store Control Register
                            return T.Create(first, second, O.STC, n: low);
                    }

                    var (secH4, secL4) = T.SplitByte(second);
                    switch (secL4)
                    {
                        case 0b1100:
                            // Shift Arithmetic Dynamically
                            return T.Create(first, second, O.SHAD, n: low, m: secH4);
                        case 0b1101:
                            // Shift Logical Dynamically
                            return T.Create(first, second, O.SHLD, n: low, m: secH4);
                        case 0b1111:
                            // Multiply and Accumulate
                            return T.Create(first, second, O.MAC, n: low, m: secH4);
                    }
                    break;
                case 0b0101:
                    // Move Structure Data
                    second = reader.ReadNextByte();
                    var (secH5, secL5) = T.SplitByte(second);
                    return T.Create(first, second, O.MOV, n: low, m: secH5, d: secL5);
                case 0b0110:
                    second = reader.ReadNextByte();

                    var (secH6, secL6) = T.SplitByte(second);
                    switch (secL6)
                    {
                        case 0b0000:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH6);
                        case 0b0001:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH6);
                        case 0b0010:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH6);
                        case 0b0011:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH6);
                        case 0b0100:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH6);
                        case 0b0101:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH6);
                        case 0b0110:
                            // Move Data
                            return T.Create(first, second, O.MOV, n: low, m: secH6);
                        case 0b0111:
                            // NOT Logical Complement
                            return T.Create(first, second, O.NOT, n: low, m: secH6);
                        case 0b1000:
                            // Swap Register Halves
                            return T.Create(first, second, O.SWAP, n: low, m: secH6);
                        case 0b1001:
                            // Swap Register Halves
                            return T.Create(first, second, O.SWAP, n: low, m: secH6);
                        case 0b1010:
                            // Negate with Carry
                            return T.Create(first, second, O.NEGC, n: low, m: secH6);
                        case 0b1011:
                            // Negate
                            return T.Create(first, second, O.NEG, n: low, m: secH6);
                        case 0b1100:
                            // Extend as Unsigned
                            return T.Create(first, second, O.EXTU, n: low, m: secH6);
                        case 0b1101:
                            // Extend as Unsigned
                            return T.Create(first, second, O.EXTU, n: low, m: secH6);
                        case 0b1110:
                            // Extend as Signed
                            return T.Create(first, second, O.EXTS, n: low, m: secH6);
                        case 0b1111:
                            // Extend as Signed
                            return T.Create(first, second, O.EXTS, n: low, m: secH6);
                    }
                    break;
                case 0b0111:
                    // Add Binary
                    second = reader.ReadNextByte();
                    dst = low;
                    imm = second;
                    return T.Create(first, second, O.ADD, n: dst, i: imm);
                case 0b1001:
                    // Move Immediate Data
                    second = reader.ReadNextByte();
                    dst = low;
                    dis = second;
                    return T.Create(first, second, O.MOV, n: dst, d: dis);
                case 0b1010:
                    // Branch
                    second = reader.ReadNextByte();
                    dsp = T.CombineBytes(low, second);
                    return T.Create(first, second, O.BRA, d: dsp);
                case 0b1011:
                    // Branch to Subroutine
                    second = reader.ReadNextByte();
                    dsp = T.CombineBytes(low, second);
                    return T.Create(first, second, O.BSR, d: dsp);
                case 0b1101:
                    // Move Immediate Data
                    second = reader.ReadNextByte();
                    dst = low;
                    dis = second;
                    return T.Create(first, second, O.MOV, n: dst, d: dis);
                case 0b1110:
                    // Move Immediate Data
                    second = reader.ReadNextByte();
                    dst = low;
                    imm = second;
                    return T.Create(first, second, O.MOV, n: dst, i: imm);
            }

            return null;
        }
    }
}