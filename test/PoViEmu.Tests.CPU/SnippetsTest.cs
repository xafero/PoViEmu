using System;
using Xunit;
using static PoViEmu.Tests.CPU.SnippetsCheck;

namespace PoViEmu.Tests.CPU
{
    public class SnippetsTest
    {
        [Theory]
        // ADD
        [InlineData(0b0011000100001100, "add r0,r1",
            new[] { "R0", "0x7FFFFFFF", "R1", "0x00000001" },
            new[] { "R1 = 0x00000001 --> 0x80000000" })]
        [InlineData(0b0111001000000001, "add #1,r2",
            new[] { "R2", "0x00000000" },
            new[] { "R2 = 0x00000000 --> 0x00000001" })]
        [InlineData(0b0111001111111110, "add #-2,r3",
            new[] { "R3", "0x00000001" },
            new[] { "R3 = 0x00000001 --> 0xFFFFFFFF" })]
        // ADDC
        [InlineData(0b0011000100111110, "addc r3,r1",
            new[] { "T", "0", "R1", "0x00000001", "R3", "0xFFFFFFFF" },
            new[] { "R1 = 0x00000001 --> 0x00000000", "T = 0 --> 1", })]
        [InlineData(0b0011000000101110, "addc r2,r0",
            new[] { "T", "1", "R0", "0x00000000", "R2", "00000000" },
            new[] { "R0 = 0x00000000 --> 0x00000001", "T = 1 --> 0", })]
        // ADDV
        [InlineData(0b0011000100001111, "addv r0,r1",
            new[] { "R0", "0x00000001", "R1", "0x7FFFFFFE", "T", "0" },
            new[] { "R1 = 0x7FFFFFFE --> 0x7FFFFFFF" })]
        [InlineData(0b0011000100001111, "addv r0,r1",
            new[] { "R0", "0x00000002", "R1", "0x7FFFFFFE", "T", "0" },
            new[] { "R1 = 0x7FFFFFFE --> 0x80000000", "T = 0 --> 1" })]
        // AND
        [InlineData(0b0010000100001001, "and r0,r1",
            new[] { "R0", "0xAAAAAAAA", "R1", "0x55555555" },
            new[] { "R1 = 0x55555555 --> 0x00000000" })]
        [InlineData(0b1100100100001111, "and #15,r0",
            new[] { "R0", "0xFFFFFFFF" },
            new[] { "R0 = 0xFFFFFFFF --> 0x0000000F" })]
        [InlineData(0b1100110110000000, "and.b #128,@(r0,gbr)",
            new[] { "GBR", "0x1000", "R0", "0x0020", "U8|0x1020", "0xA5" },
            new[] { "U8|00001020 = 0xA5 --> 0x80" })]
        // CLRMAC  
        [InlineData(0b0000000000101000, "clrmac",
            new[] { "MACH", "1", "MACL", "2" },
            new[] { "MACH = 0x00000001 --> 0x00000000", "MACL = 0x00000002 --> 0x00000000" })]
        // CLRS
        [InlineData(0b0000000001001000, "clrs",
            new[] { "S", "1" },
            new[] { "S = 1 --> 0" })]
        // CLRT
        [InlineData(0b0000000000001000, "clrt",
            new[] { "T", "1" },
            new[] { "T = 1 --> 0" })]
        // CMP/GE
        [InlineData(0b0011000100000011, "cmp/ge r0,r1",
            new[] { "R0", "0x7FFFFFFF", "R1", "0x80000000", "T", "1" },
            new[] { "T = 1 --> 0" })]
        // CMP/HS
        [InlineData(0b0011000100000010, "cmp/hs r0,r1",
            new[] { "R0", "0x7FFFFFFF", "R1", "0x80000000" },
            new[] { "T = 0 --> 1" })]
        // CMP/STR
        [InlineData(0b0010001100101100, "cmp/str r2,r3",
            new[] { "R2", "0x41424344" /*"ABCD"*/, "R3", "0x5859435a" /*"XYCZ"*/ },
            new[] { "T = 0 --> 1" })]
        // DIV1
        [InlineData(0b0011000100000100, "div1 r0,r1",
            new[] { "R0", "2", "R1", "10" },
            new[] { "R1 = 0x00000010 --> 0x00000020", "R1 = 0x00000020 --> 0x0000001E", "T = 0 --> 1" })]
        [InlineData(0b0011000100000100, "div1 r0,r1",
            new[] { "R0", "10", "R1", "2" },
            new[] { "R1 = 0x00000002 --> 0x00000004", "R1 = 0x00000004 --> 0xFFFFFFF4", "Q = 0 --> 1" })]
        // DMULS
        [InlineData(0b0011000100001101, "dmuls.l r0,r1",
            new[] { "R0", "0xFFFFFFFE", "R1", "0x00005555" },
            new[] { "MACH = 0x00000000 --> 0xFFFFFFFF", "MACL = 0x00000000 --> 0xFFFF5556" })]
        // DMULU
        [InlineData(0b0011000100000101, "dmulu.l r0,r1",
            new[] { "R0", "0xFFFFFFFE", "R1", "0x00005555" },
            new[] { "MACH = 0x00000000 --> 0x00005554", "MACL = 0x00000000 --> 0xFFFF5556" })] // Maybe check?
        // EXTSB
        [InlineData(0b0110000100001110, "exts.b r0,r1",
            new[] { "R0", "0x00000080" },
            new[] { "R1 = 0x00000000 --> 0x00000080", "R1 = 0x00000080 --> 0xFFFFFF80" })]
        // EXTSW
        [InlineData(0b0110000100001111, "exts.w r0,r1",
            new[] { "R0", "0x00008000" },
            new[] { "R1 = 0x00000000 --> 0x00008000", "R1 = 0x00008000 --> 0xFFFF8000" })]
        // EXTUB
        [InlineData(0b0110000100001100, "extu.b r0,r1",
            new[] { "R0", "0xFFFFFF80" },
            new[] { "R1 = 0x00000000 --> 0xFFFFFF80", "R1 = 0xFFFFFF80 --> 0x00000080" })]
        // EXTUW
        [InlineData(0b0110000100001101, "extu.w r0,r1",
            new[] { "R0", "0xFFFF8000" },
            new[] { "R1 = 0x00000000 --> 0xFFFF8000", "R1 = 0xFFFF8000 --> 0x00008000" })]
        // LDC
        [InlineData(0b0100000000001110,"ldc r0,sr",
            new[] {"R0","0xFFFFFFFF", "SR","0"},
            new[] { "M = 0 --> 1", "Q = 0 --> 1", "S = 0 --> 1", "T = 0 --> 1", "I0 = 0 --> 1","I1 = 0 --> 1","I2 = 0 --> 1","I3 = 0 --> 1" })]
        [InlineData(0b0100000000011110,"ldc r0,gbr",
            new[] {"R0","0xFFFFFFFF", "GBR","0"},
            new[] {"GBR = 0x00000000 --> 0xFFFFFFFF"})]
        [InlineData(0b0100000000101110,"ldc r0,vbr",
            new[] {"R0","0xFFFFFFFF", "VBR","0"},
            new[] {"VBR = 0x00000000 --> 0xFFFFFFFF"})]
        [InlineData(0b0100000000111110,"ldc r0,ssr",
            new[] {"R0","0xFFFFFFFF", "SSR","0"},
            new[] {"SSR = 0x00000000 --> 0x700003F3"})]
        [InlineData(0b0100000001001110,"ldc r0,spc",
            new[] {"R0","0xFFFFFFFF", "SPC","0"},
            new[] {"SPC = 0x00000000 --> 0xFFFFFFFF"})]
        // LDCL
        [InlineData(0b0100111100010111,"ldc.l @r15+,gbr",
            new[] {"R15","0x200","U32|200","0xEDCBA987","GBR","0x11111111"},
            new[] {"R15 = 0x00000200 --> 0x00000204", "GBR = 0x11111111 --> 0xEDCBA987"})] 
        // LDS
        [InlineData(0b0100000000101010, "lds r0,pr",
            new[] { "R0", "0x12345678", "PR", "0x00000000" },
            new[] { "PR = 0x00000000 --> 0x12345678" })]
        // LDSL
        [InlineData(0b0100111100010110,"lds.l @r15+,macl",
            new[] {"R15","0x200","MACL","0x1111","U32|200","0xCDCDEF"},
            new[] { "R15 = 0x00000200 --> 0x00000204", "MACL = 0x00001111 --> 0x00CDCDEF" })] 
        // MOV
        [InlineData(0b0110000100000011, "mov r0,r1",
            new[] { "R0", "0xFFFFFFFF", "R1", "0x00000000" },
            new[] { "R1 = 0x00000000 --> 0xFFFFFFFF" })]
        // MOVB
        [InlineData(0b0110000100000000,"mov.b @r0,r1",
            new[] {"R0","0x200","U8|200","0x80"},
            new[] {"R1 = 0x00000000 --> 0x00000080"})]
        [InlineData(0b0000001000010100,"mov.b r1,@(r0,r2)",
            new[] {"R1","0x04","R0","0x200","R2","0x10","U8|210","0x11"},
            new[] {"U8|00000210 = 0x11 --> 0x04"})]
        // MOVW
        [InlineData(0b0010000100000001,"mov.w r0,@r1",
            new[] {"R0","0xFFFF7F80", "R1","0x200", "U16|200","0x1111"},
            new[] {"U16|00000200 = 0x1111 --> 0x7F80"})]
        [InlineData(0b0010000100000101,"mov.w r0,@-r1",
            new[] {"R0","0xAAAAAAAA","R1","0x202", "U16|200","0x1111"},
            new[] {"R1 = 0x00000202 --> 0x00000200", "U16|00000200 = 0x1111 --> 0xAAAA"})]
        [InlineData(0b0000000100101101,"mov.w @(r0,r2),r1",
            new[] {"R0","0x200","R2","0x10","U16|210","0xA5A6","R1","0x1111"},
            new[] {"R1 = 0x00001111 --> 0x0000A5A6"})]
        // MOVL
        [InlineData(0b0110000100000110,"mov.l @r0+,r1",
            new[] {"R0","0x200","U32|200","0xA5A6A7A8","R1","0x11"},
            new[] { "R0 = 0x00000200 --> 0x00000204", "R1 = 0x00000011 --> 0xA5A6A7A8" })]
        [InlineData(0b0001000100001111,"mov.l r0,@(60,r1)",
            new[] {"R0","0xFFFF7F80","R1","0x200","U32|23C","0x11"},
            new[] {"U32|0000023C = 0x00000011 --> 0xFFFF7F80"})] 
        // MULL
        [InlineData(0b0000000100000111, "mul.l r0,r1",
            new[] { "R0", "0xFFFFFFFE", "R1", "0x00005555" },
            new[] { "MACL = 0x00000000 --> 0xFFFF5556" })]
        // MULSW
        [InlineData(0b0010000100001111, "muls.w r0,r1",
            new[] { "R0", "0xFFFFFFFE", "R1", "0x00005555" },
            new[] { "MACL = 0x00000000 --> 0xFFFF5556" })]
        // MULUW
        [InlineData(0b0010000100001110, "mulu.w r0,r1",
            new[] { "R0", "0x00000002", "R1", "0xFFFFAAAA" },
            new[] { "MACL = 0x00000000 --> 0x00015554" })]
        // NEG
        [InlineData(0b0110000100001011, "neg r0,r1",
            new[] { "R0", "0x00000001" },
            new[] { "R1 = 0x00000000 --> 0xFFFFFFFF" })]
        // NEGC
        [InlineData(0b0110000100011010, "negc r1,r1",
            new[] { "R1", "0x00000001", "T", "0" },
            new[] { "R1 = 0x00000001 --> 0xFFFFFFFF", "T = 0 --> 1" })]
        [InlineData(0b0110000000001010, "negc r0,r0",
            new[] { "R0", "0x00000000", "T", "1" },
            new[] { "R0 = 0x00000000 --> 0xFFFFFFFF" })]
        // NOT
        [InlineData(0b0110000100000111, "not r0,r1",
            new[] { "R0", "0xAAAAAAAA" },
            new[] { "R1 = 0x00000000 --> 0x55555555" })]
        // OR
        [InlineData(0b0010000100001011, "or r0,r1",
            new[] { "R0", "0xAAAA5555", "R1", "0x55550000" },
            new[] { "R1 = 0x55550000 --> 0xFFFF5555" })]
        [InlineData(0b1100101111110000, "or #240,r0",
            new[] { "R0", "0x00000008" },
            new[] { "R0 = 0x00000008 --> 0x000000F8" })]
        [InlineData(0b1100111101010000, "or.b #80,@(r0,gbr)",
            new[] { "GBR", "0x1000", "R0", "0x0020", "U8|1020", "0xA5" },
            new[] { "U8|00001020 = 0xA5 --> 0xF5" })]
        // ROTCL
        [InlineData(0b0100000000100100, "rotcl r0",
            new[] { "R0", "0x80000000", "T", "0" },
            new[] { "R0 = 0x80000000 --> 0x00000000", "T = 0 --> 1" })]
        // ROTCR
        [InlineData(0b0100000000100101, "rotcr r0",
            new[] { "R0", "0x00000001", "T", "1" },
            new[] { "R0 = 0x00000001 --> 0x00000000", "R0 = 0x00000000 --> 0x80000000" })]
        // ROTL
        [InlineData(0b0100000000000100, "rotl r0",
            new[] { "R0", "0x80000000", "T", "0" },
            new[] { "T = 0 --> 1", "R0 = 0x80000000 --> 0x00000000", "R0 = 0x00000000 --> 0x00000001" })]
        // ROTR
        [InlineData(0b0100000000000101, "rotr r0",
            new[] { "R0", "0x00000001", "T", "0" },
            new[]
            {
                "T = 0 --> 1", "R0 = 0x00000001 --> 0x00000000",
                "R0 = 0x00000000 --> 0x80000000"
            })]
        // SETS
        [InlineData(0b0000000001011000, "sets",
            new[] { "S", "0" },
            new[] { "S = 0 --> 1" })]
        // SETT
        [InlineData(0b0000000000011000, "sett",
            new[] { "T", "0" },
            new[] { "T = 0 --> 1" })]
        // SHAD
        [InlineData(0b0100001000011100, "shad r1,r2",
            new[] { "R1", "0xFFFFFFEC", "R2", "0x80180000" },
            new[] { "R2 = 0x80180000 --> 0xFFFFF801" })]
        [InlineData(0b0100010000111100, "shad r3,r4",
            new[] { "R3", "0x00000014", "R4", "0xFFFFF801" },
            new[] { "R4 = 0xFFFFF801 --> 0x80100000" })]
        // SHAL
        [InlineData(0b0100000000100000, "shal r0",
            new[] { "R0", "0x80000001", "T", "0" },
            new[] { "T = 0 --> 1", "R0 = 0x80000001 --> 0x00000002" })]
        // SHAR
        [InlineData(0b0100000000100001, "shar r0",
            new[] { "R0", "0x80000001", "T", "0" },
            new[]
            {
                "T = 0 --> 1", "R0 = 0x80000001 --> 0x40000000",
                "R0 = 0x40000000 --> 0xC0000000"
            })]
        // SHLD
        [InlineData(0b0100001000011101, "shld r1,r2",
            new[] { "R1", "0xFFFFFFEC", "R2", "0x80180000" },
            new[] { "R2 = 0x80180000 --> 0x00000801" })]
        [InlineData(0b0100010000111101, "shld r3,r4",
            new[] { "R3", "0x00000014", "R4", "0xFFFFF801" },
            new[] { "R4 = 0xFFFFF801 --> 0x80100000" })]
        // SHLL
        [InlineData(0b0100000000000000, "shll r0",
            new[] { "R0", "0x80000001", "T", "0" },
            new[] { "T = 0 --> 1", "R0 = 0x80000001 --> 0x00000002" })]
        // SHLLn
        [InlineData(0b0100000000001000, "shll2 r0",
            new[] { "R0", "0x12345678" },
            new[] { "R0 = 0x12345678 --> 0x48D159E0" })]
        [InlineData(0b0100000000011000, "shll8 r0",
            new[] { "R0", "0x12345678" },
            new[] { "R0 = 0x12345678 --> 0x34567800" })]
        [InlineData(0b0100000000101000, "shll16 r0",
            new[] { "R0", "0x12345678" },
            new[] { "R0 = 0x12345678 --> 0x56780000" })]
        // SHLR
        [InlineData(0b0100000000000001, "shlr r0",
            new[] { "R0", "0x80000001", "T", "0" },
            new[] { "T = 0 --> 1", "R0 = 0x80000001 --> 0x40000000" })]
        // SHLRn
        [InlineData(0b0100000000001001, "shlr2 r0",
            new[] { "R0", "0x12345678" },
            new[] { "R0 = 0x12345678 --> 0x048D159E" })]
        [InlineData(0b0100000000011001, "shlr8 r0",
            new[] { "R0", "0x12345678" },
            new[] { "R0 = 0x12345678 --> 0x00123456" })]
        [InlineData(0b0100000000101001, "shlr16 r0",
            new[] { "R0", "0x12345678" },
            new[] { "R0 = 0x12345678 --> 0x00001234" })]
        // STC
        [InlineData(0b0000000000000010,"stc sr,r0",
            new[] {"R0","0x11", "SR","0xCDCE"},
            new[] {"R0 = 0x00000011 --> 0x000001C2"})]
        // STCL
        [InlineData(0b0100111100010011,"stc.l gbr,@-r15",
            new[] {"GBR","0xAEAD", "R15","0x204", "U32|200","0x1111" },
            new[] {"R15 = 0x00000204 --> 0x00000200", "U32|00000200 = 0x00001111 --> 0x0000AEAD"})]
        // STS
        [InlineData(0b0000000000001010, "sts mach,r0",
            new[] { "R0", "0xFFFFFFFF", "MACH", "0x00000000" },
            new[] { "R0 = 0xFFFFFFFF --> 0x00000000" })]
        // STSL
        [InlineData(0b0100111100100010,"sts.l pr,@-r15",
            new[] {"R15","0x204","U32|200","0x11111111","PR","0xA9"},
            new[] {"R15 = 0x00000204 --> 0x00000200","U32|00000200 = 0x11111111 --> 0x000000A9"})]
        // SUB
        [InlineData(0b0011000100001000, "sub r0,r1",
            new[] { "R0", "0x00000001", "R1", "0x80000000" },
            new[] { "R1 = 0x80000000 --> 0x7FFFFFFF" })]
        // SUBC
        [InlineData(0b0011000100111010, "subc r3,r1",
            new[] { "T", "0", "R1", "0x00000000", "R3", "0x00000001" },
            new[] { "R1 = 0x00000000 --> 0xFFFFFFFF", "T = 0 --> 1" })]
        [InlineData(0b0011000000101010, "subc r2,r0",
            new[] { "T", "1", "R0", "0x00000000", "R2", "0x00000000" },
            new[] { "R0 = 0x00000000 --> 0xFFFFFFFF" })]
        // SUBV
        [InlineData(0b0011000100001011, "subv r0,r1",
            new[] { "R0", "0x00000002", "R1", "0x80000001" },
            new[] { "R1 = 0x80000001 --> 0x7FFFFFFF", "T = 0 --> 1" })]
        [InlineData(0b0011001100101011, "subv r2,r3",
            new[] { "R2", "0xFFFFFFFE", "R3", "0x7FFFFFFE" },
            new[] { "R3 = 0x7FFFFFFE --> 0x80000000", "T = 0 --> 1" })]
        // SWAPB
        [InlineData(0b0110000100001000, "swap.b r0,r1",
            new[] { "R0", "0x12345678" },
            new[] { "R1 = 0x00000000 --> 0x00000056", "R1 = 0x00000056 --> 0x12347856" })]
        // SWAPW
        [InlineData(0b0110000100001001, "swap.w r0,r1",
            new[] { "R0", "0x12345678" },
            new[] { "R1 = 0x00000000 --> 0x56780000", "R1 = 0x56780000 --> 0x56781234" })]
        // TASB
        [InlineData(0b0100011100011011,"tas.b @r7",
            new[] {"R7","0x200", "U8|200","0x80"},
            new string[] { })]
        // TST
        [InlineData(0b0010000000001000, "tst r0,r0",
            new[] { "R0", "0x00000000" }, new[] { "T = 0 --> 1" })]
        [InlineData(0b1100100010000000, "tst #128,r0",
            new[] { "R0", "0xFFFFFF7F" }, new[] { "T = 0 --> 1" })]
        // TSTB
        [InlineData(0b1100110010100101,"tst.b #165,@(r0,gbr)",
            new[] { "R0","0x200", "GBR","0x10", "U8|210","0xA5", "T","1" },
            new[] {"T = 1 --> 0"})]
        // XOR
        [InlineData(0b0010000100001010, "xor r0,r1",
            new[] { "R0", "0xAAAAAAAA", "R1", "0x55555555" },
            new[] { "R1 = 0x55555555 --> 0xFFFFFFFF" })]
        [InlineData(0b1100101011110000, "xor #240,r0",
            new[] { "R0", "0xFFFFFFFF" },
            new[] { "R0 = 0xFFFFFFFF --> 0xFFFFFF0F" })]
        // XORB
        [InlineData(0b1100111010100101,"xor.b #165,@(r0,gbr)",
            new[] { "GBR","0x1000", "R0", "0x0020", "U8|1020","0xA5" },
            new[] {"U8|00001020 = 0xA5 --> 0x00"})]
        // XTRCT
        [InlineData(0b0010000100001101, "xtrct r0,r1",
            new[] { "R0", "0x01234567", "R1", "0x89ABCDEF" },
            new[] { "R1 = 0x89ABCDEF --> 0x000089AB", "R1 = 0x000089AB --> 0x456789AB" })]
        public void ShouldCheck(ushort bin, string code, string[] input, string[] checks)
        {
            var bytes = BitConverter.GetBytes(bin);
            (bytes[0], bytes[1]) = (bytes[1], bytes[0]);

            var (changes, ret, actual) = DoShouldExec(bytes, code, input);
            
            Assert.Equal(checks, changes);
            Assert.Null(ret);
            Assert.Empty(actual);
        }
    }
}