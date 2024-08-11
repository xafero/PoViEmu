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
    internal static class Intel16xC0
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x01:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x02:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x03:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BP.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x04:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.SI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x05:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.DI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x06:
                    return new(pos, first, 5, O.rol, [M.@byte.On(s.NextShort(buff).Box()), M.@byte.On(s.NextByte(buff))]);
                case 0x07:
                    return new(pos, first, 3, O.rol, [M.@byte.On(R.BX.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x08:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x09:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x0A:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x0B:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BP.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x0D:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.DI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x0E:
                    return new(pos, first, 5, O.ror, [M.@byte.On(s.NextShort(buff).Box()), M.@byte.On(s.NextByte(buff))]);
                case 0x0F:
                    return new(pos, first, 3, O.ror, [M.@byte.On(R.BX.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x10:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BX.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x12:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x13:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BP.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x14:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.SI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x15:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.DI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x16:
                    return new(pos, first, 5, O.rcl, [M.@byte.On(s.NextShort(buff).Box()), M.@byte.On(s.NextByte(buff))]);
                case 0x17:
                    return new(pos, first, 3, O.rcl, [M.@byte.On(R.BX.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x18:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x19:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x1A:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BP.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x1C:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.SI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x1E:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(s.NextShort(buff).Box()), M.@byte.On(s.NextByte(buff))]);
                case 0x1F:
                    return new(pos, first, 3, O.rcr, [M.@byte.On(R.BX.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x21:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x22:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x23:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BP.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x24:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.SI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x25:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.DI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x26:
                    return new(pos, first, 5, O.shl, [M.@byte.On(s.NextShort(buff).Box()), M.@byte.On(s.NextByte(buff))]);
                case 0x27:
                    return new(pos, first, 3, O.shl, [M.@byte.On(R.BX.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x28:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x29:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x2A:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BP.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x2B:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BP.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x2C:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.SI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x2D:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.DI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x2E:
                    return new(pos, first, 5, O.shr, [M.@byte.On(s.NextShort(buff).Box()), M.@byte.On(s.NextByte(buff))]);
                case 0x2F:
                    return new(pos, first, 3, O.shr, [M.@byte.On(R.BX.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x38:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BX.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x39:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BX.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x3A:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Plus(R.SI)), M.@byte.On(s.NextByte(buff))]);
                case 0x3B:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BP.Plus(R.DI)), M.@byte.On(s.NextByte(buff))]);
                case 0x3D:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.DI.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x3E:
                    return new(pos, first, 5, O.sar, [M.@byte.On(s.NextShort(buff).Box()), M.@byte.On(s.NextByte(buff))]);
                case 0x3F:
                    return new(pos, first, 3, O.sar, [M.@byte.On(R.BX.Box(), M.@byte.On(s.NextByte(buff)))]);
                case 0x40:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x41:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x42:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x43:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x44:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.SI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x45:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.DI.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x46:
                    return new(pos, first, 4, O.rol, [M.@byte.On(R.BP.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x48:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x49:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x4A:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x4B:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x4C:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.SI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x4D:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.DI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x4E:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BP.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x4F:
                    return new(pos, first, 4, O.ror, [M.@byte.On(R.BX.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x50:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x51:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x52:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x53:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x54:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.SI.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x56:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BP.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x57:
                    return new(pos, first, 4, O.rcl, [M.@byte.On(R.BX.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x58:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x59:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x5A:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x5B:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x5C:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.SI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x5D:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.DI.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x5E:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BP.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x5F:
                    return new(pos, first, 4, O.rcr, [M.@byte.On(R.BX.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x60:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x61:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x62:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x63:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x64:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.SI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x65:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.DI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x66:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BP.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x67:
                    return new(pos, first, 4, O.shl, [M.@byte.On(R.BX.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x68:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x69:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x6A:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x6B:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x6C:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.SI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x6D:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.DI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x6E:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BP.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x6F:
                    return new(pos, first, 4, O.shr, [M.@byte.On(R.BX.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x78:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x79:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x7A:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x7B:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x7C:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.SI.Plus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x7D:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.DI.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x7E:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BP.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x7F:
                    return new(pos, first, 4, O.sar, [M.@byte.On(R.BX.Minus(s.NextByte(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x80:
                    return new(pos, first, 5, O.rol, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x81:
                    return new(pos, first, 5, O.rol, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x83:
                    return new(pos, first, 5, O.rol, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x84:
                    return new(pos, first, 5, O.rol, [M.@byte.On(R.SI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x85:
                    return new(pos, first, 5, O.rol, [M.@byte.On(R.DI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x86:
                    return new(pos, first, 5, O.rol, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x87:
                    return new(pos, first, 5, O.rol, [M.@byte.On(R.BX.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x89:
                    return new(pos, first, 5, O.ror, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x8A:
                    return new(pos, first, 5, O.ror, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x8B:
                    return new(pos, first, 5, O.ror, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x8C:
                    return new(pos, first, 5, O.ror, [M.@byte.On(R.SI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x8D:
                    return new(pos, first, 5, O.ror, [M.@byte.On(R.DI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x8E:
                    return new(pos, first, 5, O.ror, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x8F:
                    return new(pos, first, 5, O.ror, [M.@byte.On(R.BX.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x91:
                    return new(pos, first, 5, O.rcl, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x92:
                    return new(pos, first, 5, O.rcl, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x93:
                    return new(pos, first, 5, O.rcl, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x94:
                    return new(pos, first, 5, O.rcl, [M.@byte.On(R.SI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x96:
                    return new(pos, first, 5, O.rcl, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x97:
                    return new(pos, first, 5, O.rcl, [M.@byte.On(R.BX.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x98:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x99:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x9A:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x9B:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x9C:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.SI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x9D:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.DI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x9E:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0x9F:
                    return new(pos, first, 5, O.rcr, [M.@byte.On(R.BX.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA1:
                    return new(pos, first, 5, O.shl, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA2:
                    return new(pos, first, 5, O.shl, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA3:
                    return new(pos, first, 5, O.shl, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA4:
                    return new(pos, first, 5, O.shl, [M.@byte.On(R.SI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA5:
                    return new(pos, first, 5, O.shl, [M.@byte.On(R.DI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA6:
                    return new(pos, first, 5, O.shl, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA8:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xA9:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xAA:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xAB:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xAC:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.SI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xAD:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.DI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xAE:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xAF:
                    return new(pos, first, 5, O.shr, [M.@byte.On(R.BX.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xB8:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xB9:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xBA:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xBB:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xBC:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.SI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xBD:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.DI.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xBE:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.BP.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xBF:
                    return new(pos, first, 5, O.sar, [M.@byte.On(R.BX.Signed(s.NextShort(buff))), M.@byte.On(s.NextByte(buff))]);
                case 0xC1:
                    return new(pos, first, 3, O.rol, [R.CL, M.@byte.On(s.NextByte(buff))]);
                case 0xC2:
                    return new(pos, first, 3, O.rol, [R.DL, M.@byte.On(s.NextByte(buff))]);
                case 0xC3:
                    return new(pos, first, 3, O.rol, [R.BL, M.@byte.On(s.NextByte(buff))]);
                case 0xC4:
                    return new(pos, first, 3, O.rol, [R.AH, M.@byte.On(s.NextByte(buff))]);
                case 0xC5:
                    return new(pos, first, 3, O.rol, [R.CH, M.@byte.On(s.NextByte(buff))]);
                case 0xC6:
                    return new(pos, first, 3, O.rol, [R.DH, M.@byte.On(s.NextByte(buff))]);
                case 0xC7:
                    return new(pos, first, 3, O.rol, [R.BH, M.@byte.On(s.NextByte(buff))]);
                case 0xC8:
                    return new(pos, first, 3, O.ror, [R.AL, M.@byte.On(s.NextByte(buff))]);
                case 0xC9:
                    return new(pos, first, 3, O.ror, [R.CL, M.@byte.On(s.NextByte(buff))]);
                case 0xCA:
                    return new(pos, first, 3, O.ror, [R.DL, M.@byte.On(s.NextByte(buff))]);
                case 0xCB:
                    return new(pos, first, 3, O.ror, [R.BL, M.@byte.On(s.NextByte(buff))]);
                case 0xCC:
                    return new(pos, first, 3, O.ror, [R.AH, M.@byte.On(s.NextByte(buff))]);
                case 0xCD:
                    return new(pos, first, 3, O.ror, [R.CH, M.@byte.On(s.NextByte(buff))]);
                case 0xCE:
                    return new(pos, first, 3, O.ror, [R.DH, M.@byte.On(s.NextByte(buff))]);
                case 0xCF:
                    return new(pos, first, 3, O.ror, [R.BH, M.@byte.On(s.NextByte(buff))]);
                case 0xD0:
                    return new(pos, first, 3, O.rcl, [R.AL, M.@byte.On(s.NextByte(buff))]);
                case 0xD1:
                    return new(pos, first, 3, O.rcl, [R.CL, M.@byte.On(s.NextByte(buff))]);
                case 0xD2:
                    return new(pos, first, 3, O.rcl, [R.DL, M.@byte.On(s.NextByte(buff))]);
                case 0xD3:
                    return new(pos, first, 3, O.rcl, [R.BL, M.@byte.On(s.NextByte(buff))]);
                case 0xD4:
                    return new(pos, first, 3, O.rcl, [R.AH, M.@byte.On(s.NextByte(buff))]);
                case 0xD5:
                    return new(pos, first, 3, O.rcl, [R.CH, M.@byte.On(s.NextByte(buff))]);
                case 0xD6:
                    return new(pos, first, 3, O.rcl, [R.DH, M.@byte.On(s.NextByte(buff))]);
                case 0xD7:
                    return new(pos, first, 3, O.rcl, [R.BH, M.@byte.On(s.NextByte(buff))]);
                case 0xD8:
                    return new(pos, first, 3, O.rcr, [R.AL, M.@byte.On(s.NextByte(buff))]);
                case 0xD9:
                    return new(pos, first, 3, O.rcr, [R.CL, M.@byte.On(s.NextByte(buff))]);
                case 0xDA:
                    return new(pos, first, 3, O.rcr, [R.DL, M.@byte.On(s.NextByte(buff))]);
                case 0xDC:
                    return new(pos, first, 3, O.rcr, [R.AH, M.@byte.On(s.NextByte(buff))]);
                case 0xDD:
                    return new(pos, first, 3, O.rcr, [R.CH, M.@byte.On(s.NextByte(buff))]);
                case 0xDE:
                    return new(pos, first, 3, O.rcr, [R.DH, M.@byte.On(s.NextByte(buff))]);
                case 0xDF:
                    return new(pos, first, 3, O.rcr, [R.BH, M.@byte.On(s.NextByte(buff))]);
                case 0xE0:
                    return new(pos, first, 3, O.shl, [R.AL, M.@byte.On(s.NextByte(buff))]);
                case 0xE1:
                    return new(pos, first, 3, O.shl, [R.CL, M.@byte.On(s.NextByte(buff))]);
                case 0xE2:
                    return new(pos, first, 3, O.shl, [R.DL, M.@byte.On(s.NextByte(buff))]);
                case 0xE3:
                    return new(pos, first, 3, O.shl, [R.BL, M.@byte.On(s.NextByte(buff))]);
                case 0xE4:
                    return new(pos, first, 3, O.shl, [R.AH, M.@byte.On(s.NextByte(buff))]);
                case 0xE5:
                    return new(pos, first, 3, O.shl, [R.CH, M.@byte.On(s.NextByte(buff))]);
                case 0xE6:
                    return new(pos, first, 3, O.shl, [R.DH, M.@byte.On(s.NextByte(buff))]);
                case 0xE7:
                    return new(pos, first, 3, O.shl, [R.BH, M.@byte.On(s.NextByte(buff))]);
                case 0xE8:
                    return new(pos, first, 3, O.shr, [R.AL, M.@byte.On(s.NextByte(buff))]);
                case 0xE9:
                    return new(pos, first, 3, O.shr, [R.CL, M.@byte.On(s.NextByte(buff))]);
                case 0xEA:
                    return new(pos, first, 3, O.shr, [R.DL, M.@byte.On(s.NextByte(buff))]);
                case 0xEB:
                    return new(pos, first, 3, O.shr, [R.BL, M.@byte.On(s.NextByte(buff))]);
                case 0xEC:
                    return new(pos, first, 3, O.shr, [R.AH, M.@byte.On(s.NextByte(buff))]);
                case 0xED:
                    return new(pos, first, 3, O.shr, [R.CH, M.@byte.On(s.NextByte(buff))]);
                case 0xEE:
                    return new(pos, first, 3, O.shr, [R.DH, M.@byte.On(s.NextByte(buff))]);
                case 0xEF:
                    return new(pos, first, 3, O.shr, [R.BH, M.@byte.On(s.NextByte(buff))]);
                case 0xF8:
                    return new(pos, first, 3, O.sar, [R.AL, M.@byte.On(s.NextByte(buff))]);
                case 0xF9:
                    return new(pos, first, 3, O.sar, [R.CL, M.@byte.On(s.NextByte(buff))]);
                case 0xFA:
                    return new(pos, first, 3, O.sar, [R.DL, M.@byte.On(s.NextByte(buff))]);
                case 0xFB:
                    return new(pos, first, 3, O.sar, [R.BL, M.@byte.On(s.NextByte(buff))]);
                case 0xFC:
                    return new(pos, first, 3, O.sar, [R.AH, M.@byte.On(s.NextByte(buff))]);
                case 0xFD:
                    return new(pos, first, 3, O.sar, [R.CH, M.@byte.On(s.NextByte(buff))]);
                case 0xFE:
                    return new(pos, first, 3, O.sar, [R.DH, M.@byte.On(s.NextByte(buff))]);
                case 0xFF:
                    return new(pos, first, 3, O.sar, [R.BH, M.@byte.On(s.NextByte(buff))]);
            }
            return null;
        }
    }
}
