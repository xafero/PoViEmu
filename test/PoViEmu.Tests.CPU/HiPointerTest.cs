using System.Linq;
using Iced.Intel;
using PoViEmu.Base;
using PoViEmu.SH3.CPU;
using PoViEmu.SH3.ISA.Core;
using PoViEmu.SH3.ISA.Decoding;
using PoViEmu.SH3.ISA.Ops.Mems;
using Xunit;
using Decoder = PoViEmu.SH3.ISA.Decoding.Decoder;

namespace PoViEmu.Tests.CPU
{
    public class HíPointerTest
    {
        [Theory]
        [InlineData("mov.l   r0,@r15", "@r15", 0x2F02, "00000000")]
        [InlineData("mac.l   @r11+,@r15+", "@r11+", 0x0FBF, "00000000")]
        [InlineData("mov.l   r0,@-r15", "@-r15", 0x2F06, "00000000")]
        [InlineData("mov.b   r0,@(13,r15)", "@(13,r15)", 0x80FD, "0000000D")]
        [InlineData("or.b   #104,@(r0,gbr)", "@(r0,gbr)", 0xCF68, "00000000")]
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

        private static MachineState GenerateState(ushort bp = 0x1000,
            ushort bx = 0x2000, ushort si = 0x0120, ushort di = 0x0005)
        {
            var state = new MachineState
            {
                R1 = bp, R2 = bx, R3 = si, R4 = di
            };
            return state;
        }
    }
}