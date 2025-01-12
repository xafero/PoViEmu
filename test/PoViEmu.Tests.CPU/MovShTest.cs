using System;
using Xunit;
using static PoViEmu.Tests.CPU.SnippetsCheck;

namespace PoViEmu.Tests.CPU
{
    public class MovShTest
    {
        [Theory]
// 1001nnnndddddddd
[InlineData(0b1001110111000110, "mov.w 0x00000190,r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 1101nnnndddddddd
[InlineData(0b1101110101100011, "mov.l 0x00000190,r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 1110nnnniiiiiiii
[InlineData(0b1110000001101011, "mov #107,r0", new[] { "R0", "0x12345678" }, new[] { "" })]
// 1110nnnniiiiiiii
[InlineData(0b1110000010010101, "mov #-107,r0", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0011
[InlineData(0b0110100111010011, "mov r13,r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0011
[InlineData(0b0110110110010011, "mov r9,r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0010nnnnmmmm0100
[InlineData(0b0010100111010100, "mov.b r13,@-r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0010nnnnmmmm0101
[InlineData(0b0010100111010101, "mov.w r13,@-r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0010nnnnmmmm0110
[InlineData(0b0010100111010110, "mov.l r13,@-r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0100
[InlineData(0b0110100111010100, "mov.b @r13+,r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0101
[InlineData(0b0110100111010101, "mov.w @r13+,r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0110
[InlineData(0b0110100111010110, "mov.l @r13+,r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0100nnnnmmmm1111
[InlineData(0b0100100111011111, "mac.w @r13+,@r9+", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0100nnnnmmmm1111
[InlineData(0b0100110110011111, "mac.w @r9+,@r13+", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm1111
[InlineData(0b0000100111011111, "mac.l @r13+,@r9+", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm1111
[InlineData(0b0000110110011111, "mac.l @r9+,@r13+", new[] { "R0", "0x12345678" }, new[] { "" })]
// 11000100dddddddd
[InlineData(0b1100010000100100, "mov.b @(36,gbr),r0", new[] { "R0", "0x12345678" }, new[] { "" })]
// 11000000dddddddd
[InlineData(0b1100000000100100, "mov.b r0,@(36,gbr)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 11000101dddddddd
[InlineData(0b1100010100010010, "mov.w @(36,gbr),r0", new[] { "R0", "0x12345678" }, new[] { "" })]
// 11000001dddddddd
[InlineData(0b1100000100010010, "mov.w r0,@(36,gbr)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 11000110dddddddd
[InlineData(0b1100011000001001, "mov.l @(36,gbr),r0", new[] { "R0", "0x12345678" }, new[] { "" })]
// 11000010dddddddd
[InlineData(0b1100001000001001, "mov.l r0,@(36,gbr)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0010nnnnmmmm0000
[InlineData(0b0010100111010000, "mov.b r13,@r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0000
[InlineData(0b0110110110010000, "mov.b @r9,r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0010nnnnmmmm0001
[InlineData(0b0010100111010001, "mov.w r13,@r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0001
[InlineData(0b0110110110010001, "mov.w @r9,r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0010nnnnmmmm0010
[InlineData(0b0010100111010010, "mov.l r13,@r9", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0110nnnnmmmm0010
[InlineData(0b0110110110010010, "mov.l @r9,r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm0100
[InlineData(0b0000100111010100, "mov.b r13,@(r0,r9)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm1100
[InlineData(0b0000110110011100, "mov.b @(r0,r9),r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm0101
[InlineData(0b0000100111010101, "mov.w r13,@(r0,r9)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm1101
[InlineData(0b0000110110011101, "mov.w @(r0,r9),r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm0110
[InlineData(0b0000100111010110, "mov.l r13,@(r0,r9)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0000nnnnmmmm1110
[InlineData(0b0000110110011110, "mov.l @(r0,r9),r13", new[] { "R0", "0x12345678" }, new[] { "" })]
// 10000000nnnndddd
[InlineData(0b1000000010011000, "mov.b r0,@(8,r9)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 10000100mmmmdddd
[InlineData(0b1000010010011000, "mov.b @(8,r9),r0", new[] { "R0", "0x12345678" }, new[] { "" })]
// 10000001nnnndddd
[InlineData(0b1000000110010100, "mov.w r0,@(8,r9)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 10000101mmmmdddd
[InlineData(0b1000010110010100, "mov.w @(8,r9),r0", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0001nnnnmmmmdddd
[InlineData(0b0001100100000010, "mov.l r0,@(8,r9)", new[] { "R0", "0x12345678" }, new[] { "" })]
// 0101nnnnmmmmdddd
[InlineData(0b0101000010010010, "mov.l @(8,r9),r0", new[] { "R0", "0x12345678" }, new[] { "" })]
        public void ShouldCheck(ushort bin, string code, string[] input, string[] checks)
        {
            var bytes = BitConverter.GetBytes(bin);
            (bytes[0], bytes[1]) = (bytes[1], bytes[0]);

            var (changes, ret, actual) = DoShouldExec(bytes, code, input);

            // Assert.Equal(checks, changes);
            Assert.Null(ret);
            Assert.Empty(actual);
        }
    }
}