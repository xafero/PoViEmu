using System.IO;
using PoViEmu.Core;
using PoViEmu.Common;
using PoViEmu.X86Instructions;
using Xunit;

#pragma warning disable xUnit1013

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

        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.bin");
            using var stream = File.OpenRead(file);
            var addIn = AddInReader.Read(stream);

            var actual = JsonHelper.ToJson(addIn);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            TestTool.Equal(expected, actual);
        }
    }
}