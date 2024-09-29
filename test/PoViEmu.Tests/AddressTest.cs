using PoViEmu.Core.Hardware;
using Xunit;

namespace PoViEmu.Tests
{
    public class AddressTest
    {
        [Theory]
        [InlineData(0x8220, 0x95C0, 0x8b7c0)]
        [InlineData(0x1210, 0x53D1, 0x174d1)]
        [InlineData(0x3741, 0x3249, 0x3a659)]
        public void ShouldCalcPhysical(ushort segment, ushort offset, uint physical)
        {
            var pa = MachTool.ToPhysicalAddress(segment, offset);
            TestHelper.EqualHex(physical, pa, 5);

            var la = MachTool.ToLogicalAddress(pa, segment);
            Assert.Equal((segment, offset), la);
        }
    }
}