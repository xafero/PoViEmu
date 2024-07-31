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

        public static byte[]? ReadBytesMany(this Stream stream, byte[]? buffer = null, int count = 1)
        {
            var myBuffer = buffer ?? new byte[count];
            return stream.Read(myBuffer, 0, count) == count ? myBuffer : null;
        }

        public static ShortArg NextShortC(this Stream stream)
        {
            var res = stream.ReadBytesMany(count: 2);
            var val = res == null ? short.MaxValue : BitConverter.ToInt16(res);
            return new ShortArg(val);
        }

        public static BytePlusArg NextBytepC(this Stream stream)
        {
            var res = stream.ReadBytesMany()?[0] ?? 0xFE;
            return new BytePlusArg(res);
        }

        public static ConstantArg NextByteC(this Stream stream, bool isSkip = false)
        {
            var res = stream.ReadBytesMany()?[0] ?? 0xFE;
            return isSkip ? new SkipArg(res) : new ConstantArg(res);
        }

        public static ConstantArg NextBytekC(this Stream stream, short prefix)
        {
            var res = stream.ReadBytesMany()?[0] ?? 0xFE;
            return new ImplSkipArg(prefix, res);
        }

        public static RegByteArg NextRegC(this Stream stream)
        {
            var res = stream.ReadBytesMany()?[0] ?? 0xFE;
            var reg = res switch
            {
                0xC0 => Register.ax,
                0xC4 => Register.ah,
                0xC6 => Register.si,
                0xF6 => Register.si,
                0xC1 => Register.cx,
                0xD2 => Register.dx,
                0xC3 => Register.bx,
                _ => default
            };
            return reg.With(res);
        }

        public static RegByteArg With(this Register reg, int value)
        {
            return new RegByteArg(reg, (byte)value);
        }

        public static RegByteArg Plus(this Register reg, int value)
        {
            return new RegPlusArg(reg, (byte)value);
        }

        public static RegByteArg Plus(this Register reg, Register? add, int value)
        {
            return new RegPlusRegArg(reg, add, (byte)value);
        }
    }
}