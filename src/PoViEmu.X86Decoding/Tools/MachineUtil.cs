using System;
using System.IO;

namespace PoViEmu.Core.Machine.Core
{
    public static class MachineUtil
    {
        public static long? ReadBytesPos(this Stream s, byte[] buff, long? skip = null, int count = 1, int off = 0)
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

        public static short? NextShort(this Stream s, byte[] buff, int count = 2, int off = 2)
        {
            return s.Read(buff, off, count) == count ? BitConverter.ToInt16(buff, off) : null;
        }
    }
}