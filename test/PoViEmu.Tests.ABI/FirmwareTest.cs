using System.IO;
using Xunit;
using static PoViEmu.Tests.ABI.FirmwareCheck;

namespace PoViEmu.Tests.ABI
{
    public class FirmwareTest
    {
        [Theory]
        [InlineData("Sample1")]
        [InlineData("Sample2")]
        [InlineData("Sample3")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Firmware");
            DoShouldRead(dir, fileName);
        }
    }
}