using System.IO;
using Xunit;
using static PoViEmu.Tests.ImageCheck;

namespace PoViEmu.Tests
{
    public class ImageTest
    {
        [Theory]
        [InlineData("clown1.bmp")]
        [InlineData("clown1.png")]
        [InlineData("clown2.bmp")]
        [InlineData("clown2.png")]
        [InlineData("clown3.bmp")]
        [InlineData("clown3.png")]
        [InlineData("clown4.bmp")]
        [InlineData("clown4.png")]
        public void ShouldCreateBin(string fileName)
        {
            var dir = Path.Combine("Resources", "Images");
            DoShouldReadImage(dir, fileName);
        }

        [Theory]
        [InlineData("clown1.bin")]
        [InlineData("clown2.bin")]
        [InlineData("clown3.bin")]
        [InlineData("clown4.bin")]
        public void ShouldCreateBmp(string fileName)
        {
            var dir = Path.Combine("Resources", "Images");
            DoShouldReadBinary(dir, fileName);
        }
    }
}