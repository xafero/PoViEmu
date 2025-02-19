using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PoViEmu.Base;

namespace PoViEmu.Inventory.Utils
{
    public static class CacheHelper
    {
        public static Task<T> GetCachedJson<T>(Func<T> load, string file, bool reload = false)
            => GetCachedJson<T>(() => Task.FromResult(JsonHelper.ToJson(load()!)), file, reload);

        public static async Task<T> GetCachedJson<T>(Func<Task<string>> load, string file, bool reload = false)
            => JsonHelper.FromJson<T>(await GetCachedText(load, file, reload));

        public static async Task<string> GetCachedText(Func<Task<string>> load, string file, bool reload = false)
        {
            string text;
            if (!reload && File.Exists(file))
            {
                text = await File.ReadAllTextAsync(file, Encoding.UTF8);
                if (!string.IsNullOrWhiteSpace(text))
                    return text;
            }
            text = await load();
            await File.WriteAllTextAsync(file, text, Encoding.UTF8);
            return text;
        }

        public static async Task<byte[]> GetCachedBytes(Func<Task<byte[]>> load, string file, bool reload = false)
        {
            byte[] bytes;
            if (!reload && File.Exists(file))
            {
                bytes = await File.ReadAllBytesAsync(file);
                if (bytes.Length >= 1)
                    return bytes;
            }
            bytes = await load();
            await File.WriteAllBytesAsync(file, bytes);
            return bytes;
        }
    }
}