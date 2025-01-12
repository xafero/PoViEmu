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

            const int ss = 0x2000;
            const int cs = 0x1000;

            MachineState t = default;
            Action<MachineState> init = m =>
            {
                m.R15 = ss;
                m.PC = cs;

                m[ss] =
                [
                    0xEF, 0xCD, 0xAB, 0x89, 0x67, 0x45, 0x23, 0x01, 0xBB, 0xAA,
                    0x99, 0x88, 0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00
                ];
                m[cs] =
                [
                    0xE0, 0x65, // mov #101,r0
                    0xE1, 0x2E, // mov #46,r1
                    0x00, 0x17, // mul.l r1,r0
                    0x00, 0x1A, // sts macl,r0
                    0x70, 0x0E, // add #14,r0
                    0x7F, 0x08, // add     #8,r15
                    0x2f, 0x05, // mov.w   r0,@-r15 
                    0x60, 0xf5, // mov.w   @r15+,r0 
                    0x61, 0xf5, // mov.w   @r15+,r1  
                    0x00, 0x1b, // sleep  
                    0x00, 0x09 // nop
                ];

                t = m;
                Assert.Equal("0x2301", $"{fmt.Format(t.U16[ss + 0x0006])}");
            };

            var (@out, ret, diff) = Execute([], act: init);
            var actual = diff.ToChangeLines(fmt, ignoreIP: false);

            TestTool.Equal([
                "R0 = 0x00000000 --> 0x00000065",
                "PC = 0x00001000 --> 0x00001002",
                "R1 = 0x00000000 --> 0x0000002E",
                "PC = 0x00001002 --> 0x00001004",
                "MACL = 0x00000000 --> 0x00001226",
                "PC = 0x00001004 --> 0x00001006",
                "R0 = 0x00000065 --> 0x00001226",
                "PC = 0x00001006 --> 0x00001008",
                "R0 = 0x00001226 --> 0x00001234",
                "PC = 0x00001008 --> 0x0000100A",
                "R15 = 0x00002000 --> 0x00002008",
                "PC = 0x0000100A --> 0x0000100C",
                "R15 = 0x00002008 --> 0x00002006",
                "U16|00002006 = 0x2301 --> 0x1234",
                "PC = 0x0000100C --> 0x0000100E",
                "R15 = 0x00002006 --> 0x00002008",
                "PC = 0x0000100E --> 0x00001010",
                "R15 = 0x00002008 --> 0x0000200A",
                "R1 = 0x0000002E --> 0x0000BBAA",
                "PC = 0x00001010 --> 0x00001012"
            ], actual);

            Assert.Equal("", $"{@out}");
            Assert.Equal("", $"{ret}");
            Assert.Equal("0x1234", $"{fmt.Format(t.U16[ss + 0x0006])}");
        }
    }
}