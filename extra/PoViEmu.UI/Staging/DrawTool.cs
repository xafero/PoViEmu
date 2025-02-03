using System;

namespace PoViEmu.UI.Staging
{
    public static class DrawTool
    {
        public static void UpdatePixels(byte[] pixelData)
        {
            var rand = new Random();
            for (var i = 0; i < pixelData.Length; i += 4)
            {
                var color1 = (byte)rand.Next(256);
                var color2 = (byte)rand.Next(256);
                var color3 = (byte)rand.Next(256);
                pixelData[i] = color1;
                pixelData[i + 1] = color2;
                pixelData[i + 2] = color3;
                pixelData[i + 3] = 255;
            }
        }
    }
}