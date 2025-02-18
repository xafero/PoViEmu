using System;
using Avalonia;
using Avalonia.Media;
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

        public static WriteableBitmap CreateBitmap(int width = 160, int height = 160, int dpi = 96)
        {
            var size = new PixelSize(width, height);
            var res = new Vector(dpi, dpi);
            return new WriteableBitmap(size, res, PixelFormat.Bgra8888, AlphaFormat.Premul);
        }

        public static void CopyFrom(this WriteableBitmap bitmap, byte[] pixelData)
        {
            using var framebuffer = bitmap.Lock();
            unsafe
            {
                fixed (byte* srcPtr = pixelData)
                {
                    Buffer.MemoryCopy(srcPtr,
                        framebuffer.Address.ToPointer(),
                        pixelData.Length,
                        pixelData.Length);
                }
            }
        }

        public static void RenderIn(this DrawingContext context, byte[] pixelData, int width, int height)
        {
            using var bitmap = CreateBitmap(width, height);
            bitmap.CopyFrom(pixelData);
            context.DrawImage(bitmap, new Rect(0, 0, width, height));
        }

        public static byte[] CreatePixels(int width = 160, int height = 160)
        {
            var pixelData = new byte[width * height * 4];
            return pixelData;
        }
    }
}