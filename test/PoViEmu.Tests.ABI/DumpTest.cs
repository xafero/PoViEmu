using System.IO;
using Xunit;
using static PoViEmu.Tests.ABI.DumpCheck;

namespace PoViEmu.Tests.ABI
{
    public class DumpTest
    {
        [Theory]
        [InlineData("Extract1")]
        [InlineData("Extract2")]
        [InlineData("Extract3")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Dumps");
            DoShouldRead(dir, fileName);
        }
    }
}