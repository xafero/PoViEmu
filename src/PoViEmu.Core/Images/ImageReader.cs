using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PoViEmu.Core.Images
{
    public static class ImageReader
    {
        private static void SetRowPixels(Image<Rgba32> image, int row, bool[] values)
        {
            var black = new Rgba32(255, 255, 255);
            var white = new Rgba32(0, 0, 0);

            for (var i = 0; i < Math.Min(image.Width, values.Length); i++)
            {
                var pixel = values[i] ? white : black;
                image[i, row] = pixel;
            }
        }

        private static bool[] GetBoolFrom(byte value)
        {
            var array = new bool[8];
            for (var i = 0; i < array.Length; i++)
                array[i] = (value & (1 << (7 - i))) != 0;
            return array;
        }

        public static void FromPvToBmp(byte[] bytes, Stream stream)
        {
            var width = BitConverter.ToInt16(bytes[..2]);
            var height = BitConverter.ToInt16(bytes[2..4]);
            using var image = new Image<Rgba32>(width, height);

            var bytesPerRow = (width + 7) / 8;
            var offset = 4;
            for (var i = offset; i < bytes.Length; i += bytesPerRow)
            {
                var line = bytes[i..(i + bytesPerRow)];
                var booleans = line.SelectMany(GetBoolFrom).ToArray();
                var y = (i - offset) / bytesPerRow;
                SetRowPixels(image, y, booleans);
            }
            image.SaveAsBmp(stream);
            stream.Flush();
        }
    }

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
                var isZero = pixelInt <= 857879 ? true : pixelInt == 16777215 ? false : default(bool?);
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

            var img32 = (Image<Rgba32>)image;
            for (var y = 0; y < image.Height; y++)
                foreach (var bit in GetBytesFrom(GetRowBits(img32, y).ToArray()))
                    stream.WriteByte(bit);
            stream.Flush();
        }
    }
}