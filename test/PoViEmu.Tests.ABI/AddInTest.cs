using System.IO;
using Xunit;
using static PoViEmu.Tests.ABI.AddInCheck;

namespace PoViEmu.Tests.ABI
{
    public class AddInTest
    {
        [Theory]
        [InlineData("Small")]
        [InlineData("Hello")]
        [InlineData("Ast1")]
        [InlineData("Ast2")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Addins");
            DoShouldRead(dir, fileName);
        }
    }
}