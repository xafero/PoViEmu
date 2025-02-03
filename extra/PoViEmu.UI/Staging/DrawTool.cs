using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace PoViEmu.UI.Staging
{
    public static class DrawTool
    {
        public static void UpdatePixels(byte[] pixelData)
        {
            var rand = new Random();
            for (var i = 0; i < pixelData.Length; i += 4)
            {
                var blue = (byte)rand.Next(256);
                var green = (byte)rand.Next(256);
                var red = (byte)rand.Next(256);
                const int alpha = 255;
                pixelData[i] = blue;
                pixelData[i + 1] = green;
                pixelData[i + 2] = red;
                pixelData[i + 3] = alpha;
            }
        }

        public static WriteableBitmap CreateBitmap(int width = 160, int height = 160)
        {
            return new WriteableBitmap(new PixelSize(width, height),
                new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Premul);
        }
    }
}