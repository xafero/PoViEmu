using System.IO;
using PoViEmu.Binary;
using PoViEmu.Common;
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

            var file = Path.Combine(dir, $"{fileName}.hex");
            using var stream = File.OpenRead(file);
            var binary = IntelHexReader.Extract(stream);

            var actual = binary;
            var bFile = Path.Combine(dir, $"{fileName}.bin");
            File.WriteAllBytes($"{bFile}.bin", actual);
            var expected = File.ReadAllBytes(bFile);
            Assert.Equal(expected.ToHex(), actual.ToHex());
        }
    }
}