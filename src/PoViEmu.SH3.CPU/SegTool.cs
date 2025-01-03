using System.Collections.Generic;

namespace PoViEmu.SH3.CPU
{
    public static class SegTool
    {
        public static void Write(byte[] mem, uint offset, IEnumerable<byte> bytes)
        {
            var physicalAddr = (offset);
            var i = 0;
            foreach (var bits in bytes)
                mem[physicalAddr + i++] = bits;
        }

        public static IEnumerable<byte> Read(byte[] mem, uint offset, int count)
        {
            var physicalAddr = (offset);
            for (var i = 0; i < count; i++)
                yield return mem[physicalAddr + i];
        }
    }
}