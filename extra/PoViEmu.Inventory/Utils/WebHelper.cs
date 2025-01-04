using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoViEmu.Inventory.Utils
{
    public static class WebHelper
    {
        private static readonly HttpClient Client = new();

        public static async Task<string> GetCachedText(string url, string file, bool reload = false)
        {
            string text;
            if (!reload && File.Exists(file))
            {
                text = await File.ReadAllTextAsync(file, Encoding.UTF8);
                if (!string.IsNullOrWhiteSpace(text))
                    return text;
            }
            using var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            text = await response.Content.ReadAsStringAsync();
            await File.WriteAllTextAsync(file, text, Encoding.UTF8);
            return text;
        }

        public static async Task<byte[]> GetCachedBytes(string url, string file, bool reload = false)
        {
            byte[] bytes;
            if (!reload && File.Exists(file))
            {
                bytes = await File.ReadAllBytesAsync(file);
                if (bytes.Length >= 1)
                    return bytes;
            }
            using var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            bytes = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(file, bytes);
            return bytes;
        }
    }
}