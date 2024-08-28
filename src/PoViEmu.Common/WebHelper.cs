using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoViEmu.Common
{
    public static class WebHelper
    {
        private static readonly HttpClient Client = new();

        public static async Task<string> GetOrDownloadText(string url, string file, bool reload = false)
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
    }
}