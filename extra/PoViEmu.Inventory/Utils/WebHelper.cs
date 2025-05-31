using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoViEmu.Inventory.Utils
{
    public static class WebHelper
    {
        private static readonly HttpClient Client = new();

        public static async Task<T> GetCachedJson<T>(string url, string file, bool reload = false)
            => await CacheHelper.GetCachedJson<T>(() => LoadString(url), file, reload);

        private static async Task<string> GetCachedText(string url, string file, bool reload = false)
            => await CacheHelper.GetCachedText(() => LoadString(url), file, reload);

        public static async Task<byte[]> GetCachedBytes(string url, string file, bool reload = false)
            => await CacheHelper.GetCachedBytes(() => LoadBytes(url), file, reload);

        private static async Task<string> LoadString(string url)
        {
            using var response = await Client.GetAsync(url);
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw new InvalidOperationException(url, e);
            }
        }

        private static async Task<byte[]> LoadBytes(string url)
        {
            using var response = await Client.GetAsync(url);
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException e)
            {
                throw new InvalidOperationException(url, e);
            }
        }
    }
}