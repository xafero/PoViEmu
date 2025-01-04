using System.IO;
using Avalonia.Media.Imaging;
using PoViEmu.Base;
using PoViEmu.I186.ABI;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.UI.Tools
{
    public static class BitmapTools
    {
        public static AddInInfoPlus<Bitmap> LoadImages(this AddInInfo info, byte[] bytes)
        {
            var plus = new AddInInfoPlus<Bitmap>(info, bytes, s => new Bitmap(s));
            return plus;
        }

        public static Bitmap LoadImage(this byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            return new Bitmap(stream);
        }
    }
}