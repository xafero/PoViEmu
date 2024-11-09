using System.IO;
using Xunit;
using static PoViEmu.Tests.CodeCheck;

namespace PoViEmu.Tests
{
    public class CodeTest
    {
        // TODO
        // [Theory(Skip = "Some invalid parser stuff?!")]
        [InlineData("Ast1")]
        [InlineData("Ast2")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Codes");
            DoShouldRead(dir, fileName);
        }
    }
}