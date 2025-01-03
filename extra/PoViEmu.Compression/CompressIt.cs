using System;
using System.IO;
using System.IO.Compression;

namespace PoViEmu.Common
{
    public static class CompressIt
    {
        private static Stream GetCompressor(CompressAlgo algo, Stream output)
        {
            switch (algo)
            {
                case CompressAlgo.Brotli: return new BrotliStream(output, CompressionLevel.Optimal);
                case CompressAlgo.GZip: return new GZipStream(output, CompressionLevel.Optimal);
                case CompressAlgo.Deflate: return new DeflateStream(output, CompressionLevel.Optimal);
                case CompressAlgo.ZLib: return new ZLibStream(output, CompressionLevel.Optimal);
            }
            throw new ArgumentOutOfRangeException(nameof(algo), algo, null);
        }

        private static Stream GetDecompressor(CompressAlgo algo, Stream input)
        {
            switch (algo)
            {
                case CompressAlgo.Brotli: return new BrotliStream(input, CompressionMode.Decompress);
                case CompressAlgo.GZip: return new GZipStream(input, CompressionMode.Decompress);
                case CompressAlgo.Deflate: return new DeflateStream(input, CompressionMode.Decompress);
                case CompressAlgo.ZLib: return new ZLibStream(input, CompressionMode.Decompress);
            }
            throw new ArgumentOutOfRangeException(nameof(algo), algo, null);
        }

        public static void Compress(this Stream input, CompressAlgo algo, Stream output)
        {
            using var zip = GetCompressor(algo, output);
            input.CopyTo(zip);
        }

        public static Compressed Compress(this CompressAlgo algo, byte[] data)
        {
            using var input = new MemoryStream(data);
            using var output = new MemoryStream();
            input.Compress(algo, output);
            var bytes = output.ToArray();
            return new Compressed(algo, bytes);
        }

        public static void Decompress(this Stream input, CompressAlgo algo, Stream output)
        {
            using var zip = GetDecompressor(algo, input);
            zip.CopyTo(output);
        }

        public static byte[] Decompress(this Compressed compressed)
        {
            var (algo, data) = compressed;
            using var input = new MemoryStream(data);
            using var output = new MemoryStream();
            input.Decompress(algo, output);
            return output.ToArray();
        }
    }
}