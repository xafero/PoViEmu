using System.IO;
using Xunit;
using static PoViEmu.Tests.ABI.TwfCheck;

namespace PoViEmu.Tests.ABI
{
    public class TwfTest
    {
        [Theory]
        [InlineData("Block")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Twfs");
            DoShouldRead(dir, fileName);
        }
    }
}