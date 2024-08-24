using System;
using System.IO;
using System.Linq;
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

        public static Image<Rgba32> FromPvToBmp(byte[] bytes)
        {
            var width = BitConverter.ToInt16(bytes[..2]);
            var height = BitConverter.ToInt16(bytes[2..4]);
            var image = new Image<Rgba32>(width, height);

            var bytesPerRow = (width + 7) / 8;
            var offset = 4;
            for (var i = offset; i < bytes.Length; i += bytesPerRow)
            {
                var line = bytes[i..(i + bytesPerRow)];
                var booleans = line.SelectMany(GetBoolFrom).ToArray();
                var y = (i - offset) / bytesPerRow;
                SetRowPixels(image, y, booleans);
            }

            return image;
        }

        public static void FromPvToBmp(byte[] bytes, Stream stream)
        {
            using var image = FromPvToBmp(bytes);
            image.SaveAsBmp(stream);
            stream.Flush();
        }
    }
}