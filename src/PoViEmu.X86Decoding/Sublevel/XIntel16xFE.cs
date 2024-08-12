// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;

namespace PoViEmu.Expert
{
    internal static class Intel16xFE
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x01:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x02:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x03:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x05:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.DI.Box())], [second]);
                case 0x06:
                    return new(pos, first, 4, O.inc, [M.@byte.On(s.NextShort(buff).Box())], [second]);
                case 0x07:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Box())], [second]);
                case 0x08:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x09:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.SI.Box())], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.DI.Box())], [second]);
                case 0x0E:
                    return new(pos, first, 4, O.dec, [M.@byte.On(s.NextShort(buff).Box())], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Box())], [second]);
                case 0x40:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))], [second]);
                case 0x41:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)))], [second]);
                case 0x42:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)))], [second]);
                case 0x43:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)))], [second]);
                case 0x44:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.SI.Signed(s.NextByte(buff)))], [second]);
                case 0x45:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.DI.Signed(s.NextByte(buff)))], [second]);
                case 0x46:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.BP.Signed(s.NextByte(buff)))], [second]);
                case 0x47:
                    return new(pos, first, 3, O.inc, [M.@byte.On(R.BX.Signed(s.NextByte(buff)))], [second]);
                case 0x48:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))], [second]);
                case 0x49:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)))], [second]);
                case 0x4A:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)))], [second]);
                case 0x4B:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)))], [second]);
                case 0x4C:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.SI.Signed(s.NextByte(buff)))], [second]);
                case 0x4D:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.DI.Signed(s.NextByte(buff)))], [second]);
                case 0x4E:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.BP.Signed(s.NextByte(buff)))], [second]);
                case 0x4F:
                    return new(pos, first, 3, O.dec, [M.@byte.On(R.BX.Minus(s.NextByte(buff)))], [second]);
                case 0x80:
                    return new(pos, first, 4, O.inc, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [second]);
                case 0x81:
                    return new(pos, first, 4, O.inc, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))], [second]);
                case 0x82:
                    return new(pos, first, 4, O.inc, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [second]);
                case 0x84:
                    return new(pos, first, 4, O.inc, [M.@byte.On(R.SI.Signed(s.NextShort(buff)))], [second]);
                case 0x85:
                    return new(pos, first, 4, O.inc, [M.@byte.On(R.DI.Signed(s.NextShort(buff)))], [second]);
                case 0x86:
                    return new(pos, first, 4, O.inc, [M.@byte.On(R.BP.Signed(s.NextShort(buff)))], [second]);
                case 0x87:
                    return new(pos, first, 4, O.inc, [M.@byte.On(R.BX.Signed(s.NextShort(buff)))], [second]);
                case 0x88:
                    return new(pos, first, 4, O.dec, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [second]);
                case 0x8A:
                    return new(pos, first, 4, O.dec, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [second]);
                case 0x8B:
                    return new(pos, first, 4, O.dec, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))], [second]);
                case 0x8C:
                    return new(pos, first, 4, O.dec, [M.@byte.On(R.SI.Signed(s.NextShort(buff)))], [second]);
                case 0x8D:
                    return new(pos, first, 4, O.dec, [M.@byte.On(R.DI.Signed(s.NextShort(buff)))], [second]);
                case 0x8E:
                    return new(pos, first, 4, O.dec, [M.@byte.On(R.BP.Signed(s.NextShort(buff)))], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.inc, [R.AL], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.inc, [R.CL], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.inc, [R.DL], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.inc, [R.AH], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.inc, [R.CH], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.inc, [R.DH], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.inc, [R.BH], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.dec, [R.AL], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.dec, [R.CL], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.dec, [R.DL], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.dec, [R.AH], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.dec, [R.CH], [second]);
                case 0xCE:
                    return new(pos, first, 2, O.dec, [R.DH], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.dec, [R.BH], [second]);
            }
            return null;
        }
    }
}
