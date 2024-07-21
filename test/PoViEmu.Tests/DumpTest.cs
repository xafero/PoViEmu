using System.IO;
using PoViEmu.Core;
using PoViEmu.Common;
using Xunit;

namespace PoViEmu.Tests
{
    public class DumpTest
    {
        [Theory]
        [InlineData("Small")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Dumps");

            var file = Path.Combine(dir, $"{fileName}.bin");
            using var stream = File.OpenRead(file);
            var dump = DumpReader.Read(stream);

            var actual = JsonHelper.ToJson(dump);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            Assert.Equal(expected, actual);
        }
    }
}