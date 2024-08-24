using Avalonia.Media.Imaging;
using PoViEmu.Core.Addins;
using PoViEmu.Core.Images;

namespace PoViEmu.UI.Tools
{
    public static class BitmapTools
    {
        public static AddInInfoPlus<Bitmap> LoadImages(this AddInInfo info, byte[] bytes)
        {
            var plus = new AddInInfoPlus<Bitmap>(info, bytes, s => new Bitmap(s));
            return plus;
        }
    }
}