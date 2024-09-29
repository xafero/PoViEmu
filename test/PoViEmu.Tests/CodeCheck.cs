using PoViEmu.Common;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Tests
{
    public static class CodeCheck
    {
        public static void DoShouldRead(string dir, string name)
        {
            var bytes = BytesHelper.ReadFile([dir], name, "raw");

            var m = new MachineState { SS = 0x1050, SP = 0x0000, CS = 0x1060, IP = 0x0000 };
            m[m.CS, 0x0000] = bytes;

            var res = m.ExecAndCollect(2444);

            TestHelper.Check(res, [
            ]);
        }
    }
}