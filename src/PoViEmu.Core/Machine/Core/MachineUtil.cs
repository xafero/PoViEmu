using System;
using System.IO;

namespace PoViEmu.Core.Machine.Core
{
    public static class MachineUtil
    {
        public static long? ReadBytesWithPos(this Stream s, byte[] buff, long? skip = null, int count = 1, int off = 0)
        {
            var pos = s.Position;
            if (skip is { } startOff)
                pos -= startOff;
            return s.Read(buff, off, count) == count ? pos : null;
        }

        public static byte? NextByte(this Stream s, byte[] buff, int count = 1, int off = 1)
        {
            return s.Read(buff, off, count) == count ? buff[off] : null;
        }
    }
}