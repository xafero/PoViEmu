using System.IO;
using Xunit;
using static PoViEmu.Tests.Gfx.ImageCheck;

namespace PoViEmu.Tests.Gfx
{
    public class ImageTest
    {
        [Theory]
        [InlineData("clown1.bmp")]
        [InlineData("clown1.png")]
        [InlineData("clown2.bmp")]
        [InlineData("clown2.png")]
        public void ShouldCreateBin(string fileName)
        {
            var dir = Path.Combine("Resources", "Images");
            DoShouldReadImage(dir, fileName);
        }

        [Theory]
        [InlineData("clown1.bin")]
        [InlineData("clown2.bin")]
        public void ShouldCreateBmp(string fileName)
        {
            var dir = Path.Combine("Resources", "Images");
            DoShouldReadBinary(dir, fileName);
        }
    }
}