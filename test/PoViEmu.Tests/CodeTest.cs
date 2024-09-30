using System.IO;
using Xunit;
using static PoViEmu.Tests.CodeCheck;

namespace PoViEmu.Tests
{
    public class CodeTest
    {
        [Theory]
        [InlineData("Ast1")]
        [InlineData("Ast2")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Codes");
            DoShouldRead(dir, fileName);
        }
    }
}