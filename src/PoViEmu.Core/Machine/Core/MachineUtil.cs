using System;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Core
{
    public static class MachineUtil
    {
        public static long? ReadBytesPos(this Stream stream, byte[] buffer, int count = 1)
        {
            var pos = stream.Position;
            return stream.Read(buffer, 0, count) == count ? pos : null;
        }

        public static byte[] ReadBytesMany(this Stream stream, byte[] buffer = null, int count = 1)
        {
            var myBuffer = buffer ?? new byte[count];
            return stream.Read(myBuffer, 0, count) == count ? myBuffer : null;
        }

        public static ShortArg NextShortC(this Stream stream)
        {
            var res = stream.ReadBytesMany(count: 2);
            var val = BitConverter.ToInt16(res);
            return new ShortArg(val);
        }

        public static ConstantArg NextByteC(this Stream stream, bool isSkip = false)
        {
            var res = stream.ReadBytesMany()[0];
            return isSkip ? new SkipArg(res) : new ConstantArg(res);
        }

        public static RegByteArg NextRegC(this Stream stream)
        {
            var res = stream.ReadBytesMany()[0];
            var reg = res switch
            {
                0xC4 => Register.ah,
                _ => default
            };
            return new RegByteArg(reg, res);
        }
    }
}