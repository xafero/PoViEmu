using System;

namespace PoViEmu.Base.CPU
{
    public static class MemTool
    {
        public static byte[] AllocateMemory(double megaBytes = 1, byte defaultVal = 0xFF)
        {
            var byteSize = (int)(megaBytes * 1024 * 1024);
            var array = new byte[byteSize];
            Array.Fill(array, defaultVal);
            return array;
        }
    }
}