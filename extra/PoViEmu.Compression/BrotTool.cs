using System.IO;
using System.IO.Compression;

namespace PoViEmu.Compression
{
    public static class BrotTool
    {
        public static byte[] Compress(byte[] bytes)
        {
            using var output = new MemoryStream();
            using var input = new MemoryStream(bytes);
            using (var zip = new BrotliStream(output, CompressionLevel.SmallestSize))
            {
                input.CopyTo(zip);
                zip.Flush();
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] bytes)
        {
            using var output = new MemoryStream();
            using var input = new MemoryStream(bytes);
            using (var zip = new BrotliStream(input, CompressionMode.Decompress))
            {
                zip.CopyTo(output);
                output.Flush();
            }
            return output.ToArray();
        }
    }
}