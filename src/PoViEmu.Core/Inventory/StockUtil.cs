using System.IO;
using SixLabors.ImageSharp;

namespace PoViEmu.Core.Inventory
{
    public static class StockUtil
    {
        public static ImageObj ToImageObj(this Image image)
        {
            using var mem = new MemoryStream();
            image.SaveAsPng(mem);
            mem.Flush();
            return new ImageObj
            {
                Width = image.Width,
                Height = image.Height,
                Png = mem.ToArray()
            };
        }
    }
}