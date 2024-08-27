using System.IO;
using PoViEmu.Common;
using PoViEmu.Core.Dumps;
using PoViEmu.Core.Images;
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

        public static AddInInfoPlus<Image> ReadAddIn(string file, out string hex, out int len)
        {
            var bytes = File.ReadAllBytes(file);
            hex = HashHelper.GetSha(bytes);
            len = bytes.Length;
            var info = AddInReader.Read(bytes);
            var plus = info.WithImages(bytes);
            return plus;
        }

        public static DumpInfo ReadDump(string file, out string hex, out int len)
        {
            var bytes = File.ReadAllBytes(file);
            hex = HashHelper.GetSha(bytes);
            len = bytes.Length;
            var info = DumpReader.Read(bytes);
            var mem = new MemoryStream(bytes);
            info.LoadOsAddIns(mem);
            return info;
        }
    }
}