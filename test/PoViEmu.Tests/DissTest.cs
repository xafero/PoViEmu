using System.IO;
using PoViEmu.Common;
using PoViEmu.Core.Machine;
using Xunit;

#pragma warning disable xUnit1013

namespace PoViEmu.Tests
{
    public class DissTest
    {
        [Theory]
        [InlineData("Ejemplo1")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Codes");
            DoShouldRead(dir, fileName);
        }

        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.bin");
            using var stream = File.OpenRead(file);
            var codes = stream.Disassemble();

            var actual = codes.ToText();
            var dFile = Path.Combine(dir, $"{fileName}.dtx");
            var expected = TextHelper.ToText(dFile);
            TestTool.Equal(expected, actual);
        }
    }
}