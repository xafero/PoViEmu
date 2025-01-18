using System;
using Xunit;
using static PoViEmu.Tests.CPU.SnippetsCheck;

namespace PoViEmu.Tests.CPU
{
    public class MovShTest
    {
        [Theory]
// 1001nnnndddddddd
[InlineData(0b1001110111000110, "mov.w 0x00000190,r13", new[] { "R13", "0", "U16|28e", "A5A6" }, new[] { "R13 = 0x00000000 --> 0x0000A5A6" })]
// 1101nnnndddddddd
[InlineData(0b1101110101100011, "mov.l 0x00000190,r13", new[] { "R13", "0", "U32|28e", "A5A6A7A8" }, new[] { "R13 = 0x00000000 --> 0xA5A6A7A8" })]
// 1110nnnniiiiiiii
[InlineData(0b1110000001101011, "mov #107,r0", new[] { "R0", "0" }, new[] { "R0 = 0x00000000 --> 0x0000006B" })]
// 1110nnnniiiiiiii
[InlineData(0b1110000010010101, "mov #-107,r0", new[] { "R0", "0" }, new[] { "R0 = 0x00000000 --> 0xFFFFFF95" })]
// 0110nnnnmmmm0011
[InlineData(0b0110100111010011, "mov r13,r9", new[] { "R13", "0x12345678" }, new[] { "R9 = 0x00000000 --> 0x12345678" })]
// 0110nnnnmmmm0011
[InlineData(0b0110110110010011, "mov r9,r13", new[] { "R9", "0x12345678" }, new[] { "R13 = 0x00000000 --> 0x12345678" })]
// 0010nnnnmmmm0100
[InlineData(0b0010100111010100, "mov.b r13,@-r9", new[] { "R13","0xA5","R9","0x201","U8|200","11" }, new[] { "R9 = 0x00000201 --> 0x00000200","U8|00000200 = 0x11 --> 0xA5" })] 
// 0010nnnnmmmm0101
[InlineData(0b0010100111010101, "mov.w r13,@-r9", new[] { "R13","0xA5A6","R9","0x202","U16|200","1111" }, new[] { "R9 = 0x00000202 --> 0x00000200","U16|00000200 = 0x1111 --> 0xA5A6" })]
// 0010nnnnmmmm0110
[InlineData(0b0010100111010110, "mov.l r13,@-r9", new[] { "R13","0xA5A6A7A8","R9","0x204","U32|200","11111111" }, new[] { "R9 = 0x00000204 --> 0x00000200", "U32|00000200 = 0x11111111 --> 0xA5A6A7A8" })]
// 0110nnnnmmmm0100
[InlineData(0b0110100111010100, "mov.b @r13+,r9", new[] { "R13","0x200","U8|200","99" }, new[] { "R13 = 0x00000200 --> 0x00000201", "R9 = 0x00000000 --> 0x00000099" })]
// 0110nnnnmmmm0101
[InlineData(0b0110100111010101, "mov.w @r13+,r9", new[] { "R13","0x200","U16|200", "9988" }, new[] { "R13 = 0x00000200 --> 0x00000202", "R9 = 0x00000000 --> 0x00009988" })]
// 0110nnnnmmmm0110
[InlineData(0b0110100111010110, "mov.l @r13+,r9", new[] { "R13","0x200","U32|200", "99887766" }, new[] { "R13 = 0x00000200 --> 0x00000204", "R9 = 0x00000000 --> 0x99887766" })]
// 0100nnnnmmmm1111
[InlineData(0b0100100111011111, "mac.w @r13+,@r9+", new[] { "R0", "0x12345678" }, new [] { "R9 = 0x00000000 --> 0x00000002", "R13 = 0x00000000 --> 0x00000002", "MACL = 0x00000000 --> 0x00000001" })]
// 0100nnnnmmmm1111
[InlineData(0b0100110110011111, "mac.w @r9+,@r13+", new[] { "R0", "0x12345678" }, new [] { "R13 = 0x00000000 --> 0x00000002", "R9 = 0x00000000 --> 0x00000002", "MACL = 0x00000000 --> 0x00000001" })]
// 0000nnnnmmmm1111
[InlineData(0b0000100111011111, "mac.l @r13+,@r9+", new[] { "R0", "0x12345678" }, new[] { "R9 = 0x00000000 --> 0x00000004", "R13 = 0x00000000 --> 0x00000004", "MACL = 0x00000000 --> 0x00000001" })]
// 0000nnnnmmmm1111
[InlineData(0b0000110110011111, "mac.l @r9+,@r13+", new[] { "R0", "0x12345678" }, new [] { "R13 = 0x00000000 --> 0x00000004", "R9 = 0x00000000 --> 0x00000004", "MACL = 0x00000000 --> 0x00000001" })]
// 11000100dddddddd
[InlineData(0b1100010000100100, "mov.b @(36,gbr),r0", new[] { "GBR","0x200","U8|224","0x12" }, new[] { "R0 = 0x00000000 --> 0x00000012" })]
// 11000000dddddddd
[InlineData(0b1100000000100100, "mov.b r0,@(36,gbr)", new[] { "GBR","0x200","U8|224","0x11","R0","0xA5" }, new[] { "U8|00000224 = 0x11 --> 0xA5" })]
// 11000101dddddddd
[InlineData(0b1100010100010010, "mov.w @(36,gbr),r0", new[] { "GBR","0x200","U16|224","0x1234" }, new[] { "R0 = 0x00000000 --> 0x00001234" })]
// 11000001dddddddd
[InlineData(0b1100000100010010, "mov.w r0,@(36,gbr)", new[] { "GBR","0x200","U16|224","0x1234","R0","0xA5A6" }, new[] { "U16|00000224 = 0x1234 --> 0xA5A6" })]
// 11000110dddddddd
[InlineData(0b1100011000001001, "mov.l @(36,gbr),r0", new[] { "GBR","0x200","U32|224","0x12345678" }, new[] { "R0 = 0x00000000 --> 0x12345678" })]
// 11000010dddddddd
[InlineData(0b1100001000001001, "mov.l r0,@(36,gbr)", new[] { "GBR","0x200","U32|224","0x12345678","R0","0xA5A6A7A8" }, new[] { "U32|00000224 = 0x12345678 --> 0xA5A6A7A8" })]
// 0010nnnnmmmm0000
[InlineData(0b0010100111010000, "mov.b r13,@r9", new[] { "R13","0xA5","R9","0x200","U8|200","0x11" }, new[] { "U8|00000200 = 0x11 --> 0xA5" })]
// 0110nnnnmmmm0000
[InlineData(0b0110110110010000, "mov.b @r9,r13", new[] { "R9","0x200","U8|200","0x11" }, new[] { "R13 = 0x00000000 --> 0x00000011" })]
// 0010nnnnmmmm0001
[InlineData(0b0010100111010001, "mov.w r13,@r9", new[] { "R13","0xA5A6","R9","0x200","U16|200", "0x1111" }, new[] { "U16|00000200 = 0x1111 --> 0xA5A6" })]
// 0110nnnnmmmm0001
[InlineData(0b0110110110010001, "mov.w @r9,r13", new[] { "R9","0x200","U16|200","0x1111" }, new[] { "R13 = 0x00000000 --> 0x00001111" })]
// 0010nnnnmmmm0010
[InlineData(0b0010100111010010, "mov.l r13,@r9", new[] { "R13", "0xA5A6A7A8","R9","0x200","U32|200", "0x11111111" }, new[] { "U32|00000200 = 0x11111111 --> 0xA5A6A7A8" })]
// 0110nnnnmmmm0010
[InlineData(0b0110110110010010, "mov.l @r9,r13", new[] { "R9","0x200","U32|200", "0x11111111" }, new[] { "R13 = 0x00000000 --> 0x11111111" })]
// 0000nnnnmmmm0100
[InlineData(0b0000100111010100, "mov.b r13,@(r0,r9)", new[] { "R13","0xA5","R0","0x200","R9","0x10","U8|210","0x11" }, new[] { "U8|00000210 = 0x11 --> 0xA5" })]
// 0000nnnnmmmm1100
[InlineData(0b0000110110011100, "mov.b @(r0,r9),r13", new[] { "R0","0x200","R9","0x10","U8|210","0xA5" }, new[] { "R13 = 0x00000000 --> 0x000000A5" })]
// 0000nnnnmmmm0101
[InlineData(0b0000100111010101, "mov.w r13,@(r0,r9)", new[] { "R13","0xA5A6","R0","0x200","R9","0x10","U16|210","0x1111" }, new[] { "U16|00000210 = 0x1111 --> 0xA5A6" })]
// 0000nnnnmmmm1101
[InlineData(0b0000110110011101, "mov.w @(r0,r9),r13", new[] { "R0","0x200","R9","0x10","U16|210","0xA5A6" }, new[] { "R13 = 0x00000000 --> 0x0000A5A6" })]
// 0000nnnnmmmm0110
[InlineData(0b0000100111010110, "mov.l r13,@(r0,r9)", new[] { "R13","0xA5A6A7A8","R0","0x200","R9","0x10","U32|210","0x11111111" }, new[] { "U32|00000210 = 0x11111111 --> 0xA5A6A7A8" })]
// 0000nnnnmmmm1110
[InlineData(0b0000110110011110, "mov.l @(r0,r9),r13", new[] { "R0","0x200","R9","0x10","U32|210","0xA5A6A7A8" }, new[] { "R13 = 0x00000000 --> 0xA5A6A7A8" })]
// 10000000nnnndddd
[InlineData(0b1000000010011000, "mov.b r0,@(8,r9)", new[] { "R0","0xA5","R9","0x200","U8|208","0x11" }, new[] { "U8|00000208 = 0x11 --> 0xA5" })]
// 10000100mmmmdddd
[InlineData(0b1000010010011000, "mov.b @(8,r9),r0", new[] { "R9","0x200","U8|208","0xA5" }, new[] { "R0 = 0x00000000 --> 0x000000A5" })]
// 10000001nnnndddd
[InlineData(0b1000000110010100, "mov.w r0,@(8,r9)", new[] { "R0","0xA5A6","R9","0x200","U16|208","0x1111" }, new[] { "U16|00000208 = 0x1111 --> 0xA5A6" })]
// 10000101mmmmdddd
[InlineData(0b1000010110010100, "mov.w @(8,r9),r0", new[] { "R9","0x200","U16|208","0xA5A6" }, new[] { "R0 = 0x00000000 --> 0x0000A5A6" })]
// 0001nnnnmmmmdddd
[InlineData(0b0001100100000010, "mov.l r0,@(8,r9)", new[] { "R0","0xA5A6A7A8","R9","0x200","U32|208", "0x11111111" }, new[] { "U32|00000208 = 0x11111111 --> 0xA5A6A7A8" })]
// 0101nnnnmmmmdddd
[InlineData(0b0101000010010010, "mov.l @(8,r9),r0", new[] { "R9","0x200","U32|208","0xA5A6A7A8" }, new[] { "R0 = 0x00000000 --> 0xA5A6A7A8" })]
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