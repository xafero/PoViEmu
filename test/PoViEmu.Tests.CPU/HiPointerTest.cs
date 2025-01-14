using System.Linq;
using PoViEmu.Base;
using PoViEmu.SH3.CPU;
using PoViEmu.SH3.ISA.Decoding;
using PoViEmu.SH3.ISA.Ops.Mems;
using Xunit;
using Decoder = PoViEmu.SH3.ISA.Decoding.Decoder;

namespace PoViEmu.Tests.CPU
{
    public class HiPointerTest
    {
        [Theory]
        [InlineData("mov.l   r0,@r15", "@r15", 0x2F02, "00002150")]
        [InlineData("mac.l   @r11+,@r15+", "@r11+", 0x0FBF, "00002110")]
        [InlineData("mov.l   r0,@-r15", "@-r15", 0x2F06, "0000214C")]
        [InlineData("mov.b   r0,@(13,r15)", "@(13,r15)", 0x80FD, "0000215D")]
        [InlineData("or.b   #104,@(r0,gbr)", "@(r0,gbr)", 0xCF68, "00004160")]
        public void ShouldCalcPointer(string code, string txt, long raw, string dst, int? bx = null)
        {
            var b = BytesHelper.ToArray(raw);
            var d = Decoder.Create(16, new ArrayReader(b), 0x100, default);
            d.Decode(out var i);
            var o = i.GetOps().OfType<MemOperand>().First();

            var expected = $"{txt}";
            var actual = $"{o}";
            Assert.Equal(expected, actual);
            Assert.Equal(code, i.ToString());

            var m = GenerateState();
            m.R2 = bx is { } bxv ? (ushort)bxv : m.R2;
            var target = $"{o.OffA(m):X8}";
            Assert.Equal(dst, target);
        }

        private static MachineState GenerateState()
        {
            var state = new MachineState
            {
                R0 = 0x2000, R1 = 0x2010, R2 = 0x2020, R3 = 0x2030, R4 = 0x2040, R5 = 0x2050,
                R6 = 0x2060, R7 = 0x2070, R8 = 0x2080, R9 = 0x2090, R10 = 0x2100, R11 = 0x2110,
                R12 = 0x2120, R13 = 0x2130, R14 = 0x2140, R15 = 0x2150, GBR = 0x2160
            };
            return state;
        }
    }
}