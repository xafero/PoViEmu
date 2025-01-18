using System.IO;
using Xunit;
using static PoViEmu.Tests.CPU.ChimesCheck;

namespace PoViEmu.Tests.CPU
{
    public class ChimesTest
    {
        [Theory]
        [InlineData("op", "x86")]
        [InlineData("op", "sh3")]
        public void ShouldRead(string fileName, string cpu)
        {
            var dir = Path.Combine("Resources", "Chimes");
            if (cpu == "x86")
                DoShouldRead_x86(dir, fileName);
            else
                DoShouldRead_sh3(dir, fileName);
        }
    }
}