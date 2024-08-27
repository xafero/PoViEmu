using System.IO;
using PoViEmu.Core.Addins;
using SixLabors.ImageSharp;

namespace PoViEmu.Core.Images
{
    public static class ImageTools
    {
        public static Stream LoadImage(byte[] rawBytes, uint offset)
        {
            var bytes = rawBytes[(int)offset..];
            var stream = new MemoryStream();
            ImageReader.FromPvToBmp(bytes, stream);
            stream.Position = 0L;
            return stream;
        }

        public static AddInInfoPlus<Image> WithImages(this AddInInfo info, byte[] bytes)
        {
            var plus = new AddInInfoPlus<Image>(info, bytes, Image.Load);
            return plus;
        }
    }
}