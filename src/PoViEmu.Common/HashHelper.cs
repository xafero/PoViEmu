using System;
using System.Security.Cryptography;

namespace PoViEmu.Common
{
    public static class HashHelper
    {
        public static string GetSha(byte[] bytes)
        {
            var hash = SHA1.HashData(bytes);
            return Convert.ToHexString(hash);
        }
    }
}