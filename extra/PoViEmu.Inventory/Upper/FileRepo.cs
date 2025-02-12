using System;
using System.Threading.Tasks;
using PoViEmu.Base;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.Inventory.Upper
{
    public static class FileRepo
    {
        public static async Task<CachedItem<T>> GetCached<T>(T item)
            where T : IRelUrl
        {
            var (itemUrl, itemFile) = GetFilePath(item, createDir: true);
            var bytes = await WebHelper.GetCachedBytes(itemUrl, itemFile);
            return new CachedItem<T>(item, bytes);
        }

        public static (string url, string file) GetFilePath<T>(T item, bool createDir = false)
            where T : IRelUrl
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;
            var baseUrl = inst.BaseUrl;

            string[] dirs;
            switch (item)
            {
                case AddInItem ai:
                    dirs = ["cache", "addins", ai.Model];
                    break;
                case SystemItem si:
                    dirs = ["cache", "system", si.Model];
                    break;
                case BiosItem bi:
                    dirs = ["cache", "bios", bi.Model];
                    break;
                case TemplEntry te:
                    dirs = ["cache", "templ", $"{te.Internal}"];
                    break;
                default:
                    throw new InvalidOperationException(typeof(T).FullName);
            }
            var itemUrl = item.BuildUrl(baseUrl);
            var itemName = PathHelper.GetLast(itemUrl);
            var itemFile = createDir
                ? root.MakeDirFor(itemName, dirs)
                : root.GetDirFor(itemName, dirs);
            return (itemUrl, itemFile);
        }
    }
}