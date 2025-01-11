using System;
using PoViEmu.Base.CPU.Diff;
using PoViEmu.SH3.CPU;
using PoViEmu.SH3.CPU.Impl;
using PoViEmu.Tests.Base;
using Xunit;
using static PoViEmu.SH3.CPU.ExecTool;

namespace PoViEmu.Tests.CPU
{
    public class HíStateTest
    {
        [Fact]
        public void TestStackOperation()
        {
            var fmt = Defaults.ValFormatter;

            const uint SS = 0x2050;
            const uint CS = 0x1049;

            MachineState t = default;
            Action<MachineState> init = m =>
            {
                m.R15 = 0x0008;
                m.PC = 0x0004;
                m[SS + 0x0000] =
                [
                    0xEF, 0xCD, 0xAB, 0x89, 0x67, 0x45, 0x23, 0x01, 0xBB, 0xAA,
                    0x99, 0x88, 0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00
                ];
                m[CS + 0x0000] =
                [
                    0xe0, 0x81, // mov     #-127,r0
                    0x2f, 0x06, // mov.l   r0,@-r15
                    0x60, 0xf6, // mov.l   @r15+,r0
                    0x61, 0xf6, // mov.l   @r15+,r1
                    0xc3, 0x00, // trapa   #0
                    0x00, 0x0b, // rts
                    0x00, 0x09 // nop
                ];

                t = m;
                Assert.Equal("0x2301", $"{fmt.Format(t.U16[SS + 0x0006])}");
            };

            var (@out, ret, diff) = Execute([], act: init);
            var actual = diff.ToChangeLines(fmt, ignoreIP: false);

            TestTool.Equal([
                /*"R0 = 0x0000 --> 0x1234",
                "SP = 0x1000 --> 0x0FFC",
                "SP = 0x0FFC --> 0x1000",
                "PC = 0x0004 --> 0x0006",
                "PC = 0x0006 --> 0x0008",
                "PC = 0x0008 --> 0x000A"*/ // TODO
            ], actual);

            Assert.Equal("", $"{@out}");
            Assert.Equal("", $"{ret}");
            // TODO Assert.Equal("0x1234", $"{fmt.Format(t.U16[SS + 0x0006])}");
        }
    }
}