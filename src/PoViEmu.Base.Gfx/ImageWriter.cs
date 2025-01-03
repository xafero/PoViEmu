using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PoViEmu.Base.Gfx
{
    public static class ImageWriter
    {
        private static byte GetByteFrom(bool[] array)
        {
            var res = default(byte);
            for (var i = 0; i < Math.Min(array.Length, 8); i++)
                if (array[i])
                    res |= (byte)(1 << (7 - i));
            return res;
        }

        private static IEnumerable<byte> GetBytesFrom(bool[] array)
        {
            var resCount = (int)Math.Ceiling(array.Length / 8.0);
            for (var i = 0; i < resCount; i++)
                yield return GetByteFrom(array[(i * 8)..]);
        }

        private static IEnumerable<bool> GetRowBits(Image<Rgba32> image, int row)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var pixel = image[x, row];
                var pixelInt = (pixel.B << 0) | (pixel.G << 8) | (pixel.R << 16);
                var isZero = pixelInt <= 3026477 ? true : pixelInt == 16777215 ? false : default(bool?);
                if (isZero == null)
                    throw new InvalidOperationException($"{pixel} => {pixelInt}");
                yield return isZero.Value;
            }
        }

        public static void FromBmpToPv(byte[] bytes, Stream stream)
        {
            using var image = Image.Load(bytes);

            stream.Write(BitConverter.GetBytes((short)image.Width));
            stream.Write(BitConverter.GetBytes((short)image.Height));

            Image<Rgba32> img32;
            if (image is Image<Rgb24> img24)
                img32 = img24.CloneAs<Rgba32>();
            else if (image is Image<L8> img8)
                img32 = img8.CloneAs<Rgba32>();
            else
                img32 = (Image<Rgba32>)image;

            for (var y = 0; y < image.Height; y++)
                foreach (var bit in GetBytesFrom(GetRowBits(img32, y).ToArray()))
                    stream.WriteByte(bit);
            stream.Flush();
        }
    }
}