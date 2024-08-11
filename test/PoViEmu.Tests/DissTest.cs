using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Machine;
using PoViEmu.Expert;
using Xunit;

#pragma warning disable xUnit1013

namespace PoViEmu.Tests
{
    public class DissTest
    {
        [Theory]
        [InlineData("Ejemplo1", 0, 7)]
        [InlineData("Ejemplo2", 0, 7)]
        [InlineData("SmallCom1", 12, 6)]
        [InlineData("SmallCom2", 0, 11)]
        public void ShouldRead(string fileName, int off, int max)
        {
            var dir = Path.Combine("Resources", "Codes");
            DoShouldRead(dir, fileName, off, max);
        }

        public static void DoShouldRead(string dir, string fileName, int off, int max)
        {
            var file = Path.Combine(dir, $"{fileName}.bin");
            using var stream = File.OpenRead(file);
            var codes = stream.Disassemble(off, err: false).ToMax(max).ToArray();

            var actual = codes.ToText();
            var dFile = Path.Combine(dir, $"{fileName}.dtx");
            File.WriteAllText($"{dFile}.txt", actual, Encoding.UTF8);

            var expected = TextHelper.ToMaxText(dFile, max);
            TestTool.Equal(expected, actual);
        }
    }
}