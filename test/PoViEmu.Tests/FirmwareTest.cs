using System.IO;
using PoViEmu.Core;
using Xunit;

namespace PoViEmu.Tests
{
    public class FirmwareTest
    {
        [Theory]
        [InlineData("Sample1")]
        [InlineData("Sample2")]
        [InlineData("Sample3")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Firmware");
            DoShouldRead(dir, fileName);
        }

        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.hex");
            using var stream = File.OpenRead(file);
            var binary = IntelHexReader.Extract(stream);

            var actual = binary;
            var bFile = Path.Combine(dir, $"{fileName}.bin");
            File.WriteAllBytes($"{bFile}.bin", actual);
            var expected = File.ReadAllBytes(bFile);
            TestTool.Equal(expected, actual);
        }
    }
}