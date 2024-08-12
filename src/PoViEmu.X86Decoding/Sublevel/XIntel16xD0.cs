﻿// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;
using C = PoViEmu.Core.Machine.Decoding.Constants;

namespace PoViEmu.Expert
{
    internal static class Intel16xD0
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Plus(R.SI), C.One)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Plus(R.DI), C.One)], [second]);
                case 0x02:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BP.Plus(R.SI), C.One)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.SI.Box()), C.One], [second]);
                case 0x05:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.DI.Box()), C.One], [second]);
                case 0x06:
                    return new(pos, first, 4, O.rol, [M.@byte.On(s.NextShort(buff).Box(), C.One)], [second]);
                case 0x07:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Box()), C.One], [second]);
                case 0x09:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BX.Plus(R.DI), C.One)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BP.Plus(R.SI), C.One)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BP.Plus(R.DI), C.One)], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.SI.Box()), C.One], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.DI.Box()), C.One], [second]);
                case 0x0E:
                    return new(pos, first, 4, O.ror, [M.@byte.On(s.NextShort(buff).Box(), C.One)], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BX.Box()), C.One], [second]);
                case 0x10:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BX.Plus(R.SI), C.One)], [second]);
                case 0x11:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BX.Plus(R.DI), C.One)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BP.Plus(R.SI), C.One)], [second]);
                case 0x13:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BP.Plus(R.DI), C.One)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.SI.Box()), C.One], [second]);
                case 0x15:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.DI.Box()), C.One], [second]);
                case 0x16:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(s.NextShort(buff).Box(), C.One)], [second]);
                case 0x18:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Plus(R.SI), C.One)], [second]);
                case 0x19:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Plus(R.DI), C.One)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BP.Plus(R.SI), C.One)], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BP.Plus(R.DI), C.One)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.SI.Box()), C.One], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.DI.Box()), C.One], [second]);
                case 0x1E:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(s.NextShort(buff).Box(), C.One)], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Box()), C.One], [second]);
                case 0x20:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BX.Plus(R.SI), C.One)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BX.Plus(R.DI), C.One)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BP.Plus(R.SI), C.One)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.SI.Box()), C.One], [second]);
                case 0x25:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.DI.Box()), C.One], [second]);
                case 0x26:
                    return new(pos, first, 4, O.shl, [M.@byte.On(s.NextShort(buff).Box(), C.One)], [second]);
                case 0x28:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Plus(R.SI), C.One)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Plus(R.DI), C.One)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BP.Plus(R.SI), C.One)], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BP.Plus(R.DI), C.One)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.SI.Box()), C.One], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.DI.Box()), C.One], [second]);
                case 0x2E:
                    return new(pos, first, 4, O.shr, [M.@byte.On(s.NextShort(buff).Box(), C.One)], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Box()), C.One], [second]);
                case 0x38:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Plus(R.SI), C.One)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Plus(R.DI), C.One)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BP.Plus(R.SI), C.One)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BP.Plus(R.DI), C.One)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.SI.Box()), C.One], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.DI.Box()), C.One], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.sar, [M.@byte.On(s.NextShort(buff).Box(), C.One)], [second]);
                case 0x3F:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Box()), C.One], [second]);
                case 0x40:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x42:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x43:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x45:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.DI.Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x46:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x47:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x48:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x49:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x4A:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x4B:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x4C:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.SI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x4D:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.DI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x4E:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x4F:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x51:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x52:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x53:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x54:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.SI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x55:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.DI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x56:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x57:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BX.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x58:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x59:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x5A:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x5B:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x5C:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.SI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x5D:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.DI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x5E:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BP.Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x60:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x61:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x62:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x63:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x64:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.SI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x65:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.DI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x66:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x67:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x68:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x69:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x6A:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x6B:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x6C:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.SI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x6D:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.DI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x6E:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BP.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x6F:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Minus(s.NextByte(buff)), C.One)], [second]);
                case 0x79:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x7A:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x7B:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x7C:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.SI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x7D:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.DI.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x7E:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Signed(s.NextByte(buff)), C.One)], [second]);
                case 0x80:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x82:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x83:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x84:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x85:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x86:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x87:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x88:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x89:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x8A:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x8B:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x8C:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x8F:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x90:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x91:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x92:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x93:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x94:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x95:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x96:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x97:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x98:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x99:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x9A:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x9B:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0x9C:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x9D:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0x9F:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xA1:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xA2:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xA3:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xA4:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xA5:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xA6:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xA7:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xA8:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xA9:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xAA:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xAB:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xAC:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xAD:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xAF:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xB8:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xB9:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xBA:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xBB:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One], [second]);
                case 0xBC:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xBD:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xBE:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xBF:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), C.One)], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.rol, [R.AL, C.One], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.rol, [R.CL, C.One], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.rol, [R.DL, C.One], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.rol, [R.BL, C.One], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.rol, [R.CH, C.One], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.rol, [R.DH, C.One], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.rol, [R.BH, C.One], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.ror, [R.AL, C.One], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.ror, [R.CL, C.One], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.ror, [R.DL, C.One], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.ror, [R.BL, C.One], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.ror, [R.AH, C.One], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.ror, [R.CH, C.One], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.ror, [R.BH, C.One], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.rcl, [R.CL, C.One], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.rcl, [R.DL, C.One], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.rcl, [R.BL, C.One], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.rcl, [R.CH, C.One], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.rcl, [R.DH, C.One], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.rcl, [R.BH, C.One], [second]);
                case 0xD8:
                    return new(pos, first, 2, O.rcr, [R.AL, C.One], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.rcr, [R.CL, C.One], [second]);
                case 0xDA:
                    return new(pos, first, 2, O.rcr, [R.DL, C.One], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.rcr, [R.CH, C.One], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.rcr, [R.DH, C.One], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.rcr, [R.BH, C.One], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.shl, [R.DL, C.One], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.shl, [R.BL, C.One], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.shl, [R.AH, C.One], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.shl, [R.CH, C.One], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.shl, [R.DH, C.One], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.shr, [R.AL, C.One], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.shr, [R.CL, C.One], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.shr, [R.DL, C.One], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.shr, [R.BL, C.One], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.shr, [R.AH, C.One], [second]);
                case 0xED:
                    return new(pos, first, 2, O.shr, [R.CH, C.One], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.shr, [R.BH, C.One], [second]);
                case 0xF8:
                    return new(pos, first, 2, O.sar, [R.AL, C.One], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.sar, [R.CL, C.One], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.sar, [R.DL, C.One], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.sar, [R.BL, C.One], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.sar, [R.AH, C.One], [second]);
                case 0xFD:
                    return new(pos, first, 2, O.sar, [R.CH, C.One], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.sar, [R.DH, C.One], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.sar, [R.BH, C.One], [second]);
            }
            return null;
        }
    }
}