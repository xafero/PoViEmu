// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;
using A = PoViEmu.Core.Machine.Ops.OpArg;
using C = PoViEmu.Core.Machine.Decoding.Constants;

namespace PoViEmu.Expert
{
    internal static class Intel16xD2
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Plus(R.SI), R.CL)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Plus(R.DI), R.CL)], [second]);
                case 0x02:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BP.Plus(R.SI), R.CL)], [second]);
                case 0x03:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BP.Plus(R.DI), R.CL)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.SI.Box(), R.CL)], [second]);
                case 0x06:
                    return new(pos, first, 4, O.rol, [M.@byte.On(s.NextShort(buff).Box(), R.CL)]);
                case 0x07:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Box(), R.CL)], [second]);
                case 0x08:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BX.Plus(R.SI), R.CL)], [second]);
                case 0x09:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BX.Plus(R.DI), R.CL)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BP.Plus(R.SI), R.CL)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BP.Plus(R.DI), R.CL)], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.SI.Box(), R.CL)], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.DI.Box(), R.CL)], [second]);
                case 0x0E:
                    return new(pos, first, 4, O.ror, [M.@byte.On(s.NextShort(buff).Box(), R.CL)]);
                case 0x0F:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BX.Box(), R.CL)], [second]);
                case 0x10:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BX.Plus(R.SI), R.CL)], [second]);
                case 0x11:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BX.Plus(R.DI), R.CL)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BP.Plus(R.SI), R.CL)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.SI.Box(), R.CL)], [second]);
                case 0x15:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.DI.Box(), R.CL)], [second]);
                case 0x16:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(s.NextShort(buff).Box(), R.CL)]);
                case 0x17:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BX.Box(), R.CL)], [second]);
                case 0x18:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Plus(R.SI), R.CL)], [second]);
                case 0x19:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Plus(R.DI), R.CL)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BP.Plus(R.SI), R.CL)], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BP.Plus(R.DI), R.CL)], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.DI.Box(), R.CL)], [second]);
                case 0x1E:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(s.NextShort(buff).Box(), R.CL)]);
                case 0x1F:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Box(), R.CL)], [second]);
                case 0x20:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BX.Plus(R.SI), R.CL)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BX.Plus(R.DI), R.CL)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BP.Plus(R.SI), R.CL)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BP.Plus(R.DI), R.CL)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.SI.Box(), R.CL)], [second]);
                case 0x25:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.DI.Box(), R.CL)], [second]);
                case 0x26:
                    return new(pos, first, 4, O.shl, [M.@byte.On(s.NextShort(buff).Box(), R.CL)]);
                case 0x27:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BX.Box(), R.CL)], [second]);
                case 0x28:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Plus(R.SI), R.CL)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Plus(R.DI), R.CL)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BP.Plus(R.SI), R.CL)], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BP.Plus(R.DI), R.CL)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.SI.Box(), R.CL)], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.DI.Box(), R.CL)], [second]);
                case 0x2E:
                    return new(pos, first, 4, O.shr, [M.@byte.On(s.NextShort(buff).Box(), R.CL)]);
                case 0x39:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Plus(R.DI), R.CL)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BP.Plus(R.SI), R.CL)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BP.Plus(R.DI), R.CL)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.SI.Box(), R.CL)], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.DI.Box(), R.CL)], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.sar, [M.@byte.On(s.NextShort(buff).Box(), R.CL)]);
                case 0x3F:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Box(), R.CL)], [second]);
                case 0x40:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), R.CL]);
                case 0x41:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x42:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), R.CL]);
                case 0x43:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff))), R.CL]);
                case 0x44:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.SI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x45:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.DI.Minus(s.NextByte(buff)), R.CL)]);
                case 0x46:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Minus(s.NextByte(buff)), R.CL)]);
                case 0x47:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), R.CL)]);
                case 0x48:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), R.CL]);
                case 0x49:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff))), R.CL]);
                case 0x4A:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), R.CL]);
                case 0x4B:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x4C:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.SI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x4D:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.DI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x4E:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Minus(s.NextByte(buff)), R.CL)]);
                case 0x4F:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), R.CL)]);
                case 0x50:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), R.CL]);
                case 0x51:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff))), R.CL]);
                case 0x52:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff))), R.CL]);
                case 0x53:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x54:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.SI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x55:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.DI.Minus(s.NextByte(buff)), R.CL)]);
                case 0x56:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Plus(s.NextByte(buff)), R.CL)]);
                case 0x57:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), R.CL)]);
                case 0x58:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.CL)]);
                case 0x59:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x5A:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff))), R.CL]);
                case 0x5B:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)), R.CL)]);
                case 0x5C:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.SI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x5D:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.DI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x5E:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BP.Plus(s.NextByte(buff)), R.CL)]);
                case 0x5F:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), R.CL)]);
                case 0x60:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff))), R.CL]);
                case 0x61:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x62:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), R.CL)]);
                case 0x63:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x64:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.SI.Minus(s.NextByte(buff)), R.CL)]);
                case 0x65:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.DI.Minus(s.NextByte(buff)), R.CL)]);
                case 0x66:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Minus(s.NextByte(buff)), R.CL)]);
                case 0x67:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), R.CL)]);
                case 0x68:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.CL)]);
                case 0x69:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x6C:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.SI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x6D:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.DI.Minus(s.NextByte(buff)), R.CL)]);
                case 0x6E:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BP.Minus(s.NextByte(buff)), R.CL)]);
                case 0x6F:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), R.CL)]);
                case 0x79:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), R.CL]);
                case 0x7A:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), R.CL)]);
                case 0x7B:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff))), R.CL]);
                case 0x7C:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.SI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x7D:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.DI.Plus(s.NextByte(buff)), R.CL)]);
                case 0x7E:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Plus(s.NextByte(buff)), R.CL)]);
                case 0x7F:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), R.CL)]);
                case 0x80:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0x81:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0x82:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0x85:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), R.CL)]);
                case 0x86:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), R.CL)]);
                case 0x89:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0x8B:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0x8C:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), R.CL)]);
                case 0x8D:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), R.CL)]);
                case 0x8E:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), R.CL)]);
                case 0x8F:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), R.CL)]);
                case 0x90:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0x91:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0x92:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0x93:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0x94:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), R.CL)]);
                case 0x95:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), R.CL)]);
                case 0x96:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), R.CL)]);
                case 0x97:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), R.CL)]);
                case 0x98:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0x99:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0x9A:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0x9B:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0x9C:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), R.CL)]);
                case 0x9D:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), R.CL)]);
                case 0x9E:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), R.CL)]);
                case 0x9F:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), R.CL)]);
                case 0xA0:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0xA1:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0xA2:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0xA4:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), R.CL)]);
                case 0xA5:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), R.CL)]);
                case 0xA6:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), R.CL)]);
                case 0xA7:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), R.CL)]);
                case 0xA8:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0xAA:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0xAB:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0xAE:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Signed(s.NextShort(buff)), R.CL)]);
                case 0xAF:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), R.CL)]);
                case 0xB8:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0xB9:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0xBA:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), R.CL]);
                case 0xBB:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), R.CL]);
                case 0xBC:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.SI.Signed(s.NextShort(buff)), R.CL)]);
                case 0xBD:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.DI.Signed(s.NextShort(buff)), R.CL)]);
                case 0xBF:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Signed(s.NextShort(buff)), R.CL)]);
                case 0xC0:
                    return new(pos, first, 2, O.rol, [R.AL, R.CL], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.rol, [R.CL, R.CL], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.rol, [R.DL, R.CL], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.rol, [R.BL, R.CL], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.rol, [R.AH, R.CL], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.rol, [R.CH, R.CL], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.rol, [R.DH, R.CL], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.rol, [R.BH, R.CL], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.ror, [R.AL, R.CL], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.ror, [R.CL, R.CL], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.ror, [R.DL, R.CL], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.ror, [R.BL, R.CL], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.ror, [R.AH, R.CL], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.ror, [R.CH, R.CL], [second]);
                case 0xCE:
                    return new(pos, first, 2, O.ror, [R.DH, R.CL], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.ror, [R.BH, R.CL], [second]);
                case 0xD0:
                    return new(pos, first, 2, O.rcl, [R.AL, R.CL], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.rcl, [R.CL, R.CL], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.rcl, [R.DL, R.CL], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.rcl, [R.BL, R.CL], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.rcl, [R.CH, R.CL], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.rcl, [R.DH, R.CL], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.rcl, [R.BH, R.CL], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.rcr, [R.CL, R.CL], [second]);
                case 0xDB:
                    return new(pos, first, 2, O.rcr, [R.BL, R.CL], [second]);
                case 0xDC:
                    return new(pos, first, 2, O.rcr, [R.AH, R.CL], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.rcr, [R.CH, R.CL], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.rcr, [R.DH, R.CL], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.rcr, [R.BH, R.CL], [second]);
                case 0xE0:
                    return new(pos, first, 2, O.shl, [R.AL, R.CL], [second]);
                case 0xE1:
                    return new(pos, first, 2, O.shl, [R.CL, R.CL], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.shl, [R.DL, R.CL], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.shl, [R.BL, R.CL], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.shl, [R.AH, R.CL], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.shl, [R.CH, R.CL], [second]);
                case 0xE7:
                    return new(pos, first, 2, O.shl, [R.BH, R.CL], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.shr, [R.AL, R.CL], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.shr, [R.CL, R.CL], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.shr, [R.DL, R.CL], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.shr, [R.BL, R.CL], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.shr, [R.AH, R.CL], [second]);
                case 0xED:
                    return new(pos, first, 2, O.shr, [R.CH, R.CL], [second]);
                case 0xEE:
                    return new(pos, first, 2, O.shr, [R.DH, R.CL], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.shr, [R.BH, R.CL], [second]);
                case 0xF8:
                    return new(pos, first, 2, O.sar, [R.AL, R.CL], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.sar, [R.CL, R.CL], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.sar, [R.DL, R.CL], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.sar, [R.BL, R.CL], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.sar, [R.AH, R.CL], [second]);
                case 0xFD:
                    return new(pos, first, 2, O.sar, [R.CH, R.CL], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.sar, [R.DH, R.CL], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.sar, [R.BH, R.CL], [second]);
            }
            return null;
        }
    }
}
