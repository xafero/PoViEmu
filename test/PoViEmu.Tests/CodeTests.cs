using PoViEmu.Common;
using PoViEmu.Core.Hardware;
using Xunit;
using System.IO;
using System.Text;
using PoViEmu.Core.Hardware;
using Xunit;
using M = PoViEmu.Common.Diffs.Mode;
using static PoViEmu.Tests.TestHelper;

namespace PoViEmu.Tests
{
    public class CodeTests
    {
        [Theory(Skip = "Don't check for now!")] // TODO
        [InlineData("Ast1")]
        [InlineData("Ast2")]
        public void TestCmdRunning(string name)
        {
            var bytes = BytesHelper.ReadFile(["Resources", "Codes"], name, "raw");

            var m = new MachineState { SS = 0x1050, SP = 0x0000, CS = 0x1060, IP = 0x0000 };
            m[m.CS, 0x0000] = bytes;

            var res = m.ExecAndCollect(2444);

            Check(res, [
            ]);
        }
    }
}