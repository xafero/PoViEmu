using System;
using System.IO;

namespace PoViEmu.Core.Machine.Core
{
    public static class MachineUtil
    {
        public static short? NextShort(this Stream stream, byte[]? buff = null)
        {
            var res = NextBytes(stream, buff, 2);
            if (res == null)
                return default;
            var num = BitConverter.ToInt16(res, 0);
            return num;
        }

        public static byte? NextByte(this Stream stream, byte[]? buff = null)
        {
            var res = NextBytes(stream, buff);
            if (res == null)
                return default;
            var num = res?[0];
            return num;
        }

        public static byte[]? NextBytes(this Stream stream, byte[]? buffer = null, int count = 1)
        {
            var myBuffer = buffer ?? new byte[count];
            return stream.Read(myBuffer, 0, count) == count ? myBuffer : null;
        }

        public static long? ReadBytesPos(this Stream stream, byte[] buffer, int count = 1, long? off = null)
        {
            var pos = stream.Position;
            if (off is { } startOff)
                pos -= startOff;
            return stream.Read(buffer, 0, count) == count ? pos : null;
        }
    }
}