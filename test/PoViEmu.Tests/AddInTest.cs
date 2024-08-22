using System.IO;
using Xunit;
using static PoViEmu.Tests.AddInCheck;

namespace PoViEmu.Tests
{
    public class AddInTest
    {
        [Theory]
        [InlineData("Small")]
        [InlineData("Hello")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Addins");
            DoShouldRead(dir, fileName);
        }
    }
}