using PoViEmu.Core.Hardware;
using Xunit;

namespace PoViEmu.Tests
{
    public class BytesTest
    {
        [Theory]
        [InlineData(0xABCD, 0xCD, 0xAB, 0x3412, 0x12, 0x34)]
        [InlineData(0x8220, 0x20, 0x82, 0x8B7C, 0x7C, 0x8B)]
        [InlineData(0x1210, 0x10, 0x12, 0x174D, 0x4D, 0x17)]
        [InlineData(0x3741, 0x41, 0x37, 0x3A65, 0x65, 0x3A)]
        public void ShouldOverwrite(ushort r1, byte l1, byte h1, ushort r2, byte l2, byte h2)
        {
            TestHelper.EqualHex(MachTool.GetLow(r1), l1, 2);
            TestHelper.EqualHex(MachTool.GetHigh(r1), h1, 2);

            r1 = MachTool.SetLow(r1, l2);
            r1 = MachTool.SetHigh(r1, h2);

            TestHelper.EqualHex(r2, r1, 4);
        }
    }
}