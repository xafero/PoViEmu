using System.IO;
using System.Text;
using PoViEmu.Core.Hardware;
using Xunit;
using M = PoViEmu.Common.Diffs.Mode;
using static PoViEmu.Tests.TestHelper;

namespace PoViEmu.Tests
{
    public class StateTests
    {
        [Fact]
        public void TestStackOperation()
        {
            var m = new MachineState { SS = 0x1050, SP = 0x0008, CS = 0x1049, IP = 0x0004 };
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
                0x5B // POP BX
            ];

            var res = m.ExecAndCollect(4);

            Check(res, [
                new(M.Register, "AX", "0000", "1234"),
                new(M.Register, "IP", "0004", "0007"),

                new(M.Register, "SP", "0008", "0006"),
                new(M.Register, "IP", "0007", "0008"),
                new(M.Stack, "SS:0006", "2301", "1234"),

                new(M.Register, "SP", "0006", "0008"),
                new(M.Register, "IP", "0008", "0009"),

                new(M.Register, "BX", "0000", "bbaa"),
                new(M.Register, "SP", "0008", "000a"),
                new(M.Register, "IP", "0009", "000a")
            ]);
        }
    }
}