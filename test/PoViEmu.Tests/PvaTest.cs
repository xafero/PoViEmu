using System.IO;
using Xunit;
using static PoViEmu.Tests.PvaCheck;

namespace PoViEmu.Tests
{
    public class PvaTest
    {
        [Theory]
        [InlineData("Editor")]
        [InlineData("Sample")]
        [InlineData("Scan")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Pvas");
            DoShouldRead(dir, fileName);
        }
    }
}