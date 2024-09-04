using System;
using System.IO;
using System.Linq;
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

        public static DumpInfo ReadDeviceDump(string file, out string hex, out int len)
        {
            var bytes = File.ReadAllBytes(file);
            hex = HashHelper.GetSha(bytes);
            len = bytes.Length;
            try
            {
                var info = DumpReader.Read(bytes);
                var mem = new MemoryStream(bytes);
                info.LoadOsAddIns(mem);
                return info;
            }
            catch (Exception)
            {
                if (ReadBundleDump(bytes, file) is { } bundleDump)
                    return bundleDump;
                throw;
            }
        }

        public static DumpInfo? ReadBundleDump(byte[] bytes, string file)
        {
            var len = bytes.Length;
            if (len is < 1044308 or > 1155100)
                return null;
            var info = new DumpInfo(new DumpHeader());
            var mem = new MemoryStream(bytes);
            info.LoadOsAddIns(mem);
            var ad = info.AddIns;
            if (ad.Count < 2 || ad.First().Key.Start != 0)
                return null;
            var sig = ad.Select(a => a.Value.Signature).Distinct().First();
            var model = ad.Select(a => a.Value.Model).Distinct().First();
            var ts = ad.Select(a => a.Value.Compiled).Max();
            var fp = file.Split('/').Reverse().Skip(1);
            var dev = fp.Select(f => TextHelper.ToEnum(f, default(DumpModel))).First();
            return new DumpInfo(new DumpHeader
            {
                Signature = sig.ToCharArray(), Model = model.ToString()
            })
            {
                AddIns = ad, DeviceModel = dev, TimeStamp = ts
            };
        }

        public static byte[] ReadOther(string file, out string hex, out int len)
        {
            var bytes = File.ReadAllBytes(file);
            hex = HashHelper.GetSha(bytes);
            len = bytes.Length;
            return bytes;
        }
    }
}