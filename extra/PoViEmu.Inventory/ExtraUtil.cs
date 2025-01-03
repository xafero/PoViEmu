using System.IO;
using PoViEmu.Base;
using PoViEmu.I186.ABI;
using PoViEmu.Inventory.Utils;
using SixLabors.ImageSharp;

namespace PoViEmu.Inventory
{
    public static class ExtraUtil
    {
        public static AddInInfoPlus<Image> ReadAddIn(string file, out string hex, out int len)
        {
            var bytes = File.ReadAllBytes(file);
            hex = HashHelper.GetSha(bytes);
            len = bytes.Length;
            var info = AddInReader.Read(bytes);
            var plus = info.WithImages(bytes);
            return plus;
        }
    }
}