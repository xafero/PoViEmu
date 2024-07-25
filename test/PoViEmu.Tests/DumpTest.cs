using System.IO;
using PoViEmu.Core;
using PoViEmu.Common;
using PoViEmu.Core.Dumps;
using Xunit;

namespace PoViEmu.Tests
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

        public static void DoShouldRead(string dir, string fileName, string ext = ".bin")
        {
            var file = Path.Combine(dir, $"{fileName}{ext}");
            using var stream = File.OpenRead(file);
            var dump = DumpReader.Read(stream);
            dump.LoadOsAddIns(stream);

            var actual = JsonHelper.ToJson(dump);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            TestTool.Equal(expected, actual);
        }
    }
}