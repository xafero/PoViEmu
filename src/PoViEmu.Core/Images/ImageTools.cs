using System.IO;

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
    }
}