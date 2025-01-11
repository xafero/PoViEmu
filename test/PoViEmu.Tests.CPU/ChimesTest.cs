using System.IO;
using Xunit;
using static PoViEmu.Tests.CPU.ChimesCheck;

namespace PoViEmu.Tests.CPU
{
    public class ChimesTest
    {
        [Theory(Skip = "Not usable atm!")]
        [InlineData("op")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Chimes");
            DoShouldRead(dir, fileName);
        }
    }
}