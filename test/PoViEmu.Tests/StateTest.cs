using System;
using PoViEmu.Core.Hardware;
using Xunit;
using static PoViEmu.Core.Compat.StateTool;

namespace PoViEmu.Tests
{
    public class StateTest
    {
        [Fact]
        public void TestStackOperation()
        {
            MachineState t = default;
            Action<MachineState> init = m =>
            {
                m.SS = 0x1050;
                m.SP = 0x0008;
                m.CS = 0x1049;
                m.IP = 0x0004;
                m[m.SS, 0x0000] =
                [
                    0xCD, 0xEF, 0x89, 0xAB, 0x45, 0x67, 0x01, 0x23, 0xAA, 0xBB,
                    0x88, 0x99, 0x66, 0x77, 0x44, 0x55, 0x22, 0x33, 0x00, 0x11
                ];
                m[m.CS, 0x0004] =
                [
                    0xB8, 0x34, 0x12, // MOV AX, 0x1234 
                    0x50, // PUSH AX 
                    0x58, // POP AX
                    0x5B, // POP BX
                    0xF4 // HALT
                ];

                t = m;
                Assert.Equal("0x2301", $"{t.U16[t.SS, 0x0006].Format()}");
            };

            var (@out, ret, diff) = Execute([], act: init);
            var actual = diff.ToChangeLines(ignoreIP: false);

            TestTool.Equal([
                "AX = 0x0000 --> 0x1234",
                "IP = 0x0004 --> 0x0007",
                "SP = 0x0008 --> 0x0006",
                "U16|1050:0006 = 0x2301 --> 0x1234",
                "IP = 0x0007 --> 0x0008",
                "SP = 0x0006 --> 0x0008",
                "IP = 0x0008 --> 0x0009",
                "SP = 0x0008 --> 0x000A",
                "BX = 0x0000 --> 0xBBAA",
                "IP = 0x0009 --> 0x000A",
                "IP = 0x000A --> 0x000B"
            ], actual);

            Assert.Equal("", $"{@out}");
            Assert.Equal("", $"{ret}");
            Assert.Equal("0x1234", $"{t.U16[t.SS, 0x0006].Format()}");
        }
    }
}