// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;

namespace PoViEmu.Expert
{
    internal static class Intel16xC6
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 3, O.mov, [M.@byte.On(R.BX.Plus(R.SI), s.NextByte(buff))], [second]);
                case 0x01:
                    return new(pos, first, 3, O.mov, [M.@byte.On(R.BX.Plus(R.DI), s.NextByte(buff))], [second]);
                case 0x02:
                    return new(pos, first, 3, O.mov, [M.@byte.On(R.BP.Plus(R.SI), s.NextByte(buff))], [second]);
                case 0x03:
                    return new(pos, first, 3, O.mov, [M.@byte.On(R.BP.Plus(R.DI), s.NextByte(buff))], [second]);
                case 0x04:
                    return new(pos, first, 3, O.mov, [M.@byte.On(R.SI.Box(), s.NextByte(buff))], [second]);
                case 0x05:
                    return new(pos, first, 3, O.mov, [M.@byte.On(R.DI.Box(), s.NextByte(buff))], [second]);
                case 0x06:
                    return new(pos, first, 5, O.mov, [M.@byte.On(s.NextShort(buff).Box(), s.NextByte(buff))], [second]);
                case 0x41:
                    return new(pos, first, 4, O.mov, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff))), s.NextByte(buff)], [second]);
                case 0x42:
                    return new(pos, first, 4, O.mov, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff))), s.NextByte(buff)], [second]);
                case 0x43:
                    return new(pos, first, 4, O.mov, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff))), s.NextByte(buff)], [second]);
                case 0x44:
                    return new(pos, first, 4, O.mov, [M.@byte.On(R.SI.Signed(s.NextByte(buff)), s.NextByte(buff))], [second]);
                case 0x45:
                    return new(pos, first, 4, O.mov, [M.@byte.On(R.DI.Signed(s.NextByte(buff)), s.NextByte(buff))], [second]);
                case 0x46:
                    return new(pos, first, 4, O.mov, [M.@byte.On(R.BP.Signed(s.NextByte(buff)), s.NextByte(buff))], [second]);
                case 0x47:
                    return new(pos, first, 4, O.mov, [M.@byte.On(R.BX.Signed(s.NextByte(buff)), s.NextByte(buff))], [second]);
                case 0x80:
                    return new(pos, first, 5, O.mov, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), s.NextByte(buff)], [second]);
                case 0x81:
                    return new(pos, first, 5, O.mov, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), s.NextByte(buff)], [second]);
                case 0x82:
                    return new(pos, first, 5, O.mov, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), s.NextByte(buff)], [second]);
                case 0x83:
                    return new(pos, first, 5, O.mov, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), s.NextByte(buff)], [second]);
                case 0x85:
                    return new(pos, first, 5, O.mov, [M.@byte.On(R.DI.Signed(s.NextShort(buff))), s.NextByte(buff)], [second]);
                case 0x86:
                    return new(pos, first, 5, O.mov, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), s.NextByte(buff)], [second]);
                case 0x87:
                    return new(pos, first, 5, O.mov, [M.@byte.On(R.BX.Signed(s.NextShort(buff))), s.NextByte(buff)], [second]);
                case 0xC0:
                    return new(pos, first, 3, O.mov, [R.AL, s.NextByte(buff)], [second]);
                case 0xC2:
                    return new(pos, first, 3, O.mov, [R.DL, s.NextByte(buff)], [second]);
                case 0xC3:
                    return new(pos, first, 3, O.mov, [R.BL, s.NextByte(buff)], [second]);
                case 0xC4:
                    return new(pos, first, 3, O.mov, [R.AH, s.NextByte(buff)], [second]);
                case 0xC5:
                    return new(pos, first, 3, O.mov, [R.CH, s.NextByte(buff)], [second]);
                case 0xC6:
                    return new(pos, first, 3, O.mov, [R.DH, s.NextByte(buff)], [second]);
                case 0xC7:
                    return new(pos, first, 3, O.mov, [R.BH, s.NextByte(buff)], [second]);
                case 0xF8:
                    return new(pos, first, 3, O.xabort, [s.NextByte(buff)], [second]);
            }
            return null;
        }
    }
}
