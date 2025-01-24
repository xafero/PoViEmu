using System;
using System.IO;
using PoViEmu.Base;
using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Core;
using PoViEmu.SH3.ISA.Decoding;
using PoViEmu.SH3.ISA.Ops.Mems;
using PoViEmu.SH3.ISA.Ops.Places;
using PoViEmu.Tests.ISA.Util;
using Xunit;

namespace PoViEmu.Tests.ISA
{
    public class AlignTest
    {
        [Theory]
        [InlineData("bf")]
        [InlineData("bfs")]
        [InlineData("bt")]
        [InlineData("bts")]
        [InlineData("bra")]
        [InlineData("bsr")]
        [InlineData("mova")]
        [InlineData("movl")]
        [InlineData("movw")]
        public void TestAlignment(string fileName)
        {
            var dir = Path.Combine("Resources", "Aligns");
            var comFile = Path.Combine(dir, $"{fileName}.com");
            var txtFile = Path.Combine(dir, $"{fileName}.txt");

            var outFile = Path.Combine(dir, $"{fileName}.o.txt");
            using var writer = File.CreateText(outFile);

            var comBytes = File.ReadAllBytes(comFile);
            const uint start = 0x10000;
            var offset = start;
            var st = new NonState();
            Array.Copy(comBytes, 0, st.Bytes, start, comBytes.Length);

            for (var i = 0; i <= (comBytes.Length - 2); i += 2)
            {
                var bytes = comBytes[i..(i + 2)];
                var rHex = Convert.ToHexString(bytes).ToLowerInvariant();
                var hex = $"{rHex[..2]} {rHex[2..4]}";
                var reader = new ArrayReader(bytes);
                var instr = Parser.Parse(reader);
                instr.IP32 = st[ShRegister.PC] = (uint)offset;
                var suffix = string.Empty;
                if (instr.Args[0] is AddressOperand ao)
                {
                    var newOff = ao.CalcAddr(st);
                    instr.Args[0] = new AddressOperand(ao.Base, (int)newOff, ao.Align);
                }
                else if (instr.Args[0] is Mu32Operand m32)
                {
                    var newOff = m32.OffA(st);
                    var nm32 = new Mu32Operand(m32.Mode, m32.Base, m32.Idx, (int)newOff);
                    instr.Args[0] = nm32;
                    suffix = $"\t! {m32[st]:x8}";
                }
                else if (instr.Args[0] is Mu16Operand m16)
                {
                    var newOff = m16.OffA(st);
                    var nm16 = new Mu16Operand(m16.Mode, m16.Base, m16.Idx, (int)newOff);
                    instr.Args[0] = nm16;
                    suffix = $"\t! {m16[st]:x4}";
                }
                if (suffix.EndsWith("! 00000000"))
                    suffix = string.Empty;
                else if (suffix.EndsWith("! 0000"))
                    suffix = string.Empty;
                var iTxt = instr.ToString().Replace("   0x000", "\t0x");
                writer.WriteLine($"   {offset:x5}:\t{hex}       \t{iTxt}{suffix}");
                offset = (uint)(offset + bytes.Length);
            }

            writer.Flush();
            writer.Close();

            var peLines = TextHelper.ReadUtf8Lines(txtFile);
            var paLines = TextHelper.ReadUtf8Lines(outFile);
            Assert.Equal(peLines, paLines);
        }
    }
}