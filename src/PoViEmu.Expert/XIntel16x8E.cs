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
    internal static class Intel16x8E
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.mov, [R.ES, R.BX.Plus(R.SI)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.mov, [R.ES, R.BX.Plus(R.DI)], [second]);
                case 0x02:
                    return new(pos, first, 2, O.mov, [R.ES, R.BP.Plus(R.SI)], [second]);
                case 0x03:
                    return new(pos, first, 2, O.mov, [R.ES, R.BP.Plus(R.DI)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.mov, [R.ES, R.SI.Box()], [second]);
                case 0x05:
                    return new(pos, first, 2, O.mov, [R.ES, R.DI.Box()], [second]);
                case 0x06:
                    return new(pos, first, 4, O.mov, [R.ES, s.NextShort(buff).Box()]);
                case 0x07:
                    return new(pos, first, 2, O.mov, [R.ES, R.BX.Box()], [second]);
                case 0x08:
                    return new(pos, first, 2, O.mov, [R.CS, R.BX.Plus(R.SI)], [second]);
                case 0x09:
                    return new(pos, first, 2, O.mov, [R.CS, R.BX.Plus(R.DI)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.mov, [R.CS, R.BP.Plus(R.SI)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.mov, [R.CS, R.BP.Plus(R.DI)], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.mov, [R.CS, R.SI.Box()], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.mov, [R.CS, R.DI.Box()], [second]);
                case 0x0E:
                    return new(pos, first, 4, O.mov, [R.CS, s.NextShort(buff).Box()]);
                case 0x0F:
                    return new(pos, first, 2, O.mov, [R.CS, R.BX.Box()], [second]);
                case 0x10:
                    return new(pos, first, 2, O.mov, [R.SS, R.BX.Plus(R.SI)], [second]);
                case 0x11:
                    return new(pos, first, 2, O.mov, [R.SS, R.BX.Plus(R.DI)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.mov, [R.SS, R.BP.Plus(R.SI)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.mov, [R.SS, R.SI.Box()], [second]);
                case 0x15:
                    return new(pos, first, 2, O.mov, [R.SS, R.DI.Box()], [second]);
                case 0x16:
                    return new(pos, first, 4, O.mov, [R.SS, s.NextShort(buff).Box()]);
                case 0x18:
                    return new(pos, first, 2, O.mov, [R.DS, R.BX.Plus(R.SI)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.mov, [R.DS, R.BP.Plus(R.SI)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.mov, [R.DS, R.SI.Box()], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.mov, [R.DS, R.DI.Box()], [second]);
                case 0x1E:
                    return new(pos, first, 4, O.mov, [R.DS, s.NextShort(buff).Box()]);
                case 0x1F:
                    return new(pos, first, 2, O.mov, [R.DS, R.BX.Box()], [second]);
                case 0x20:
                    return new(pos, first, 2, O.mov, [R.FS, R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.mov, [R.FS, R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.mov, [R.FS, R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.mov, [R.FS, R.BP.Plus(R.DI)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.mov, [R.FS, R.SI.Box()], [second]);
                case 0x25:
                    return new(pos, first, 2, O.mov, [R.FS, R.DI.Box()], [second]);
                case 0x26:
                    return new(pos, first, 4, O.mov, [R.FS, s.NextShort(buff).Box()]);
                case 0x27:
                    return new(pos, first, 2, O.mov, [R.FS, R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.mov, [R.GS, R.BX.Plus(R.SI)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.mov, [R.GS, R.BP.Plus(R.SI)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.mov, [R.GS, R.SI.Box()], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.mov, [R.GS, R.DI.Box()], [second]);
                case 0x2E:
                    return new(pos, first, 4, O.mov, [R.GS, s.NextShort(buff).Box()]);
                case 0x2F:
                    return new(pos, first, 2, O.mov, [R.GS, R.BX.Box()], [second]);
                case 0x30:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BX.Plus(R.SI)], [second]);
                case 0x31:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BX.Plus(R.DI)], [second]);
                case 0x32:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BP.Plus(R.SI)], [second]);
                case 0x33:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BP.Plus(R.DI)], [second]);
                case 0x34:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.SI.Box()], [second]);
                case 0x35:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.DI.Box()], [second]);
                case 0x36:
                    return new(pos, first, 4, O.mov, [R.Segr6, s.NextShort(buff).Box()]);
                case 0x38:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BX.Plus(R.SI)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BX.Plus(R.DI)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BP.Plus(R.SI)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BP.Plus(R.DI)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.SI.Box()], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.DI.Box()], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.mov, [R.Segr7, s.NextShort(buff).Box()]);
                case 0x3F:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BX.Box()], [second]);
                case 0x40:
                    return new(pos, first, 3, O.mov, [R.ES, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x41:
                    return new(pos, first, 3, O.mov, [R.ES, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x42:
                    return new(pos, first, 3, O.mov, [R.ES, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x43:
                    return new(pos, first, 3, O.mov, [R.ES, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x44:
                    return new(pos, first, 3, O.mov, [R.ES, R.SI.Plus(s.NextByte(buff))]);
                case 0x45:
                    return new(pos, first, 3, O.mov, [R.ES, R.DI.Plus(s.NextByte(buff))]);
                case 0x46:
                    return new(pos, first, 3, O.mov, [R.ES, R.BP.Plus(s.NextByte(buff))]);
                case 0x47:
                    return new(pos, first, 3, O.mov, [R.ES, R.BX.Minus(s.NextByte(buff))]);
                case 0x48:
                    return new(pos, first, 3, O.mov, [R.CS, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x49:
                    return new(pos, first, 3, O.mov, [R.CS, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x4A:
                    return new(pos, first, 3, O.mov, [R.CS, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x4B:
                    return new(pos, first, 3, O.mov, [R.CS, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x4C:
                    return new(pos, first, 3, O.mov, [R.CS, R.SI.Plus(s.NextByte(buff))]);
                case 0x4D:
                    return new(pos, first, 3, O.mov, [R.CS, R.DI.Minus(s.NextByte(buff))]);
                case 0x4E:
                    return new(pos, first, 3, O.mov, [R.CS, R.BP.Plus(s.NextByte(buff))]);
                case 0x4F:
                    return new(pos, first, 3, O.mov, [R.CS, R.BX.Plus(s.NextByte(buff))]);
                case 0x50:
                    return new(pos, first, 3, O.mov, [R.SS, R.BX.Plus(R.SI).Minus(s.NextByte(buff))]);
                case 0x51:
                    return new(pos, first, 3, O.mov, [R.SS, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x52:
                    return new(pos, first, 3, O.mov, [R.SS, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x53:
                    return new(pos, first, 3, O.mov, [R.SS, R.BP.Plus(R.DI).Minus(s.NextByte(buff))]);
                case 0x54:
                    return new(pos, first, 3, O.mov, [R.SS, R.SI.Plus(s.NextByte(buff))]);
                case 0x55:
                    return new(pos, first, 3, O.mov, [R.SS, R.DI.Plus(s.NextByte(buff))]);
                case 0x56:
                    return new(pos, first, 3, O.mov, [R.SS, R.BP.Minus(s.NextByte(buff))]);
                case 0x57:
                    return new(pos, first, 3, O.mov, [R.SS, R.BX.Minus(s.NextByte(buff))]);
                case 0x58:
                    return new(pos, first, 3, O.mov, [R.DS, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x59:
                    return new(pos, first, 3, O.mov, [R.DS, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x5A:
                    return new(pos, first, 3, O.mov, [R.DS, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x5B:
                    return new(pos, first, 3, O.mov, [R.DS, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x5C:
                    return new(pos, first, 3, O.mov, [R.DS, R.SI.Plus(s.NextByte(buff))]);
                case 0x5D:
                    return new(pos, first, 3, O.mov, [R.DS, R.DI.Plus(s.NextByte(buff))]);
                case 0x5E:
                    return new(pos, first, 3, O.mov, [R.DS, R.BP.Plus(s.NextByte(buff))]);
                case 0x5F:
                    return new(pos, first, 3, O.mov, [R.DS, R.BX.Plus(s.NextByte(buff))]);
                case 0x61:
                    return new(pos, first, 3, O.mov, [R.FS, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x62:
                    return new(pos, first, 3, O.mov, [R.FS, R.BP.Plus(R.SI).Minus(s.NextByte(buff))]);
                case 0x63:
                    return new(pos, first, 3, O.mov, [R.FS, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x64:
                    return new(pos, first, 3, O.mov, [R.FS, R.SI.Plus(s.NextByte(buff))]);
                case 0x65:
                    return new(pos, first, 3, O.mov, [R.FS, R.DI.Plus(s.NextByte(buff))]);
                case 0x66:
                    return new(pos, first, 3, O.mov, [R.FS, R.BP.Plus(s.NextByte(buff))]);
                case 0x67:
                    return new(pos, first, 3, O.mov, [R.FS, R.BX.Plus(s.NextByte(buff))]);
                case 0x69:
                    return new(pos, first, 3, O.mov, [R.GS, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x6A:
                    return new(pos, first, 3, O.mov, [R.GS, R.BP.Plus(R.SI).Minus(s.NextByte(buff))]);
                case 0x6B:
                    return new(pos, first, 3, O.mov, [R.GS, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x6C:
                    return new(pos, first, 3, O.mov, [R.GS, R.SI.Plus(s.NextByte(buff))]);
                case 0x6D:
                    return new(pos, first, 3, O.mov, [R.GS, R.DI.Minus(s.NextByte(buff))]);
                case 0x6E:
                    return new(pos, first, 3, O.mov, [R.GS, R.BP.Plus(s.NextByte(buff))]);
                case 0x71:
                    return new(pos, first, 3, O.mov, [R.Segr6, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x73:
                    return new(pos, first, 3, O.mov, [R.Segr6, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x74:
                    return new(pos, first, 3, O.mov, [R.Segr6, R.SI.Plus(s.NextByte(buff))]);
                case 0x75:
                    return new(pos, first, 3, O.mov, [R.Segr6, R.DI.Plus(s.NextByte(buff))]);
                case 0x76:
                    return new(pos, first, 3, O.mov, [R.Segr6, R.BP.Plus(s.NextByte(buff))]);
                case 0x77:
                    return new(pos, first, 3, O.mov, [R.Segr6, R.BX.Plus(s.NextByte(buff))]);
                case 0x78:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x79:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x7A:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x7B:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.BP.Plus(R.DI).Minus(s.NextByte(buff))]);
                case 0x7C:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.SI.Minus(s.NextByte(buff))]);
                case 0x7D:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.DI.Plus(s.NextByte(buff))]);
                case 0x7E:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.BP.Minus(s.NextByte(buff))]);
                case 0x7F:
                    return new(pos, first, 3, O.mov, [R.Segr7, R.BX.Plus(s.NextByte(buff))]);
                case 0x80:
                    return new(pos, first, 4, O.mov, [R.ES, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x82:
                    return new(pos, first, 4, O.mov, [R.ES, R.BP.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x83:
                    return new(pos, first, 4, O.mov, [R.ES, R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x84:
                    return new(pos, first, 4, O.mov, [R.ES, R.SI.Plus(s.NextShort(buff))]);
                case 0x85:
                    return new(pos, first, 4, O.mov, [R.ES, R.DI.Minus(s.NextShort(buff))]);
                case 0x86:
                    return new(pos, first, 4, O.mov, [R.ES, R.BP.Plus(s.NextShort(buff))]);
                case 0x87:
                    return new(pos, first, 4, O.mov, [R.ES, R.BX.Minus(s.NextShort(buff))]);
                case 0x88:
                    return new(pos, first, 4, O.mov, [R.CS, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x89:
                    return new(pos, first, 4, O.mov, [R.CS, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x8A:
                    return new(pos, first, 4, O.mov, [R.CS, R.BP.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x8B:
                    return new(pos, first, 4, O.mov, [R.CS, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0x8C:
                    return new(pos, first, 4, O.mov, [R.CS, R.SI.Minus(s.NextShort(buff))]);
                case 0x8D:
                    return new(pos, first, 4, O.mov, [R.CS, R.DI.Plus(s.NextShort(buff))]);
                case 0x8E:
                    return new(pos, first, 4, O.mov, [R.CS, R.BP.Plus(s.NextShort(buff))]);
                case 0x8F:
                    return new(pos, first, 4, O.mov, [R.CS, R.BX.Plus(s.NextShort(buff))]);
                case 0x90:
                    return new(pos, first, 4, O.mov, [R.SS, R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x91:
                    return new(pos, first, 4, O.mov, [R.SS, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x92:
                    return new(pos, first, 4, O.mov, [R.SS, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x93:
                    return new(pos, first, 4, O.mov, [R.SS, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0x94:
                    return new(pos, first, 4, O.mov, [R.SS, R.SI.Plus(s.NextShort(buff))]);
                case 0x95:
                    return new(pos, first, 4, O.mov, [R.SS, R.DI.Plus(s.NextShort(buff))]);
                case 0x96:
                    return new(pos, first, 4, O.mov, [R.SS, R.BP.Plus(s.NextShort(buff))]);
                case 0x97:
                    return new(pos, first, 4, O.mov, [R.SS, R.BX.Minus(s.NextShort(buff))]);
                case 0x98:
                    return new(pos, first, 4, O.mov, [R.DS, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x99:
                    return new(pos, first, 4, O.mov, [R.DS, R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0x9A:
                    return new(pos, first, 4, O.mov, [R.DS, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x9B:
                    return new(pos, first, 4, O.mov, [R.DS, R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x9E:
                    return new(pos, first, 4, O.mov, [R.DS, R.BP.Minus(s.NextShort(buff))]);
                case 0x9F:
                    return new(pos, first, 4, O.mov, [R.DS, R.BX.Minus(s.NextShort(buff))]);
                case 0xA0:
                    return new(pos, first, 4, O.mov, [R.FS, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xA1:
                    return new(pos, first, 4, O.mov, [R.FS, R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xA2:
                    return new(pos, first, 4, O.mov, [R.FS, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xA3:
                    return new(pos, first, 4, O.mov, [R.FS, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xA4:
                    return new(pos, first, 4, O.mov, [R.FS, R.SI.Minus(s.NextShort(buff))]);
                case 0xA5:
                    return new(pos, first, 4, O.mov, [R.FS, R.DI.Plus(s.NextShort(buff))]);
                case 0xA6:
                    return new(pos, first, 4, O.mov, [R.FS, R.BP.Minus(s.NextShort(buff))]);
                case 0xA7:
                    return new(pos, first, 4, O.mov, [R.FS, R.BX.Plus(s.NextShort(buff))]);
                case 0xA8:
                    return new(pos, first, 4, O.mov, [R.GS, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xA9:
                    return new(pos, first, 4, O.mov, [R.GS, R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xAA:
                    return new(pos, first, 4, O.mov, [R.GS, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xAB:
                    return new(pos, first, 4, O.mov, [R.GS, R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xAC:
                    return new(pos, first, 4, O.mov, [R.GS, R.SI.Plus(s.NextShort(buff))]);
                case 0xAD:
                    return new(pos, first, 4, O.mov, [R.GS, R.DI.Minus(s.NextShort(buff))]);
                case 0xAE:
                    return new(pos, first, 4, O.mov, [R.GS, R.BP.Minus(s.NextShort(buff))]);
                case 0xAF:
                    return new(pos, first, 4, O.mov, [R.GS, R.BX.Minus(s.NextShort(buff))]);
                case 0xB0:
                    return new(pos, first, 4, O.mov, [R.Segr6, R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xB3:
                    return new(pos, first, 4, O.mov, [R.Segr6, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xB4:
                    return new(pos, first, 4, O.mov, [R.Segr6, R.SI.Plus(s.NextShort(buff))]);
                case 0xB5:
                    return new(pos, first, 4, O.mov, [R.Segr6, R.DI.Minus(s.NextShort(buff))]);
                case 0xB6:
                    return new(pos, first, 4, O.mov, [R.Segr6, R.BP.Minus(s.NextShort(buff))]);
                case 0xB7:
                    return new(pos, first, 4, O.mov, [R.Segr6, R.BX.Minus(s.NextShort(buff))]);
                case 0xB8:
                    return new(pos, first, 4, O.mov, [R.Segr7, R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xB9:
                    return new(pos, first, 4, O.mov, [R.Segr7, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xBA:
                    return new(pos, first, 4, O.mov, [R.Segr7, R.BP.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xBB:
                    return new(pos, first, 4, O.mov, [R.Segr7, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xBC:
                    return new(pos, first, 4, O.mov, [R.Segr7, R.SI.Plus(s.NextShort(buff))]);
                case 0xBD:
                    return new(pos, first, 4, O.mov, [R.Segr7, R.DI.Plus(s.NextShort(buff))]);
                case 0xBF:
                    return new(pos, first, 4, O.mov, [R.Segr7, R.BX.Minus(s.NextShort(buff))]);
                case 0x07:
                    return new(pos, first, 2, O.mov, [R.ES, R.BX.Box()], [second]);
                case 0x08:
                    return new(pos, first, 2, O.mov, [R.CS, R.BX.Plus(R.SI)], [second]);
                case 0x09:
                    return new(pos, first, 2, O.mov, [R.CS, R.BX.Plus(R.DI)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.mov, [R.CS, R.BP.Plus(R.SI)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.mov, [R.CS, R.BP.Plus(R.DI)], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.mov, [R.CS, R.SI.Box()], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.mov, [R.CS, R.DI.Box()], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.mov, [R.CS, R.BX.Box()], [second]);
                case 0x10:
                    return new(pos, first, 2, O.mov, [R.SS, R.BX.Plus(R.SI)], [second]);
                case 0x11:
                    return new(pos, first, 2, O.mov, [R.SS, R.BX.Plus(R.DI)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.mov, [R.SS, R.BP.Plus(R.SI)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.mov, [R.SS, R.SI.Box()], [second]);
                case 0x15:
                    return new(pos, first, 2, O.mov, [R.SS, R.DI.Box()], [second]);
                case 0x18:
                    return new(pos, first, 2, O.mov, [R.DS, R.BX.Plus(R.SI)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.mov, [R.DS, R.BP.Plus(R.SI)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.mov, [R.DS, R.SI.Box()], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.mov, [R.DS, R.DI.Box()], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.mov, [R.DS, R.BX.Box()], [second]);
                case 0x20:
                    return new(pos, first, 2, O.mov, [R.FS, R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.mov, [R.FS, R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.mov, [R.FS, R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.mov, [R.FS, R.BP.Plus(R.DI)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.mov, [R.FS, R.SI.Box()], [second]);
                case 0x25:
                    return new(pos, first, 2, O.mov, [R.FS, R.DI.Box()], [second]);
                case 0x27:
                    return new(pos, first, 2, O.mov, [R.FS, R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.mov, [R.GS, R.BX.Plus(R.SI)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.mov, [R.GS, R.BP.Plus(R.SI)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.mov, [R.GS, R.SI.Box()], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.mov, [R.GS, R.DI.Box()], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.mov, [R.GS, R.BX.Box()], [second]);
                case 0x30:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BX.Plus(R.SI)], [second]);
                case 0x31:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BX.Plus(R.DI)], [second]);
                case 0x32:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BP.Plus(R.SI)], [second]);
                case 0x33:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.BP.Plus(R.DI)], [second]);
                case 0x34:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.SI.Box()], [second]);
                case 0x35:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.DI.Box()], [second]);
                case 0x38:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BX.Plus(R.SI)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BX.Plus(R.DI)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BP.Plus(R.SI)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BP.Plus(R.DI)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.SI.Box()], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.DI.Box()], [second]);
                case 0x3F:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BX.Box()], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.mov, [R.ES, R.AX], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.mov, [R.ES, R.DX], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.mov, [R.ES, R.BX], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.mov, [R.ES, R.SP], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.mov, [R.ES, R.BP], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.mov, [R.ES, R.SI], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.mov, [R.ES, R.DI], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.mov, [R.CS, R.AX], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.mov, [R.CS, R.DX], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.mov, [R.CS, R.BX], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.mov, [R.CS, R.BP], [second]);
                case 0xCE:
                    return new(pos, first, 2, O.mov, [R.CS, R.SI], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.mov, [R.CS, R.DI], [second]);
                case 0xD0:
                    return new(pos, first, 2, O.mov, [R.SS, R.AX], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.mov, [R.SS, R.CX], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.mov, [R.SS, R.DX], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.mov, [R.SS, R.BX], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.mov, [R.SS, R.BP], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.mov, [R.SS, R.SI], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.mov, [R.SS, R.DI], [second]);
                case 0xD8:
                    return new(pos, first, 2, O.mov, [R.DS, R.AX], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.mov, [R.DS, R.CX], [second]);
                case 0xDA:
                    return new(pos, first, 2, O.mov, [R.DS, R.DX], [second]);
                case 0xDB:
                    return new(pos, first, 2, O.mov, [R.DS, R.BX], [second]);
                case 0xDC:
                    return new(pos, first, 2, O.mov, [R.DS, R.SP], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.mov, [R.DS, R.BP], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.mov, [R.DS, R.SI], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.mov, [R.DS, R.DI], [second]);
                case 0xE1:
                    return new(pos, first, 2, O.mov, [R.FS, R.CX], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.mov, [R.FS, R.DX], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.mov, [R.FS, R.BX], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.mov, [R.FS, R.SP], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.mov, [R.FS, R.BP], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.mov, [R.FS, R.SI], [second]);
                case 0xE7:
                    return new(pos, first, 2, O.mov, [R.FS, R.DI], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.mov, [R.GS, R.AX], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.mov, [R.GS, R.CX], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.mov, [R.GS, R.DX], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.mov, [R.GS, R.BX], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.mov, [R.GS, R.SP], [second]);
                case 0xED:
                    return new(pos, first, 2, O.mov, [R.GS, R.BP], [second]);
                case 0xEE:
                    return new(pos, first, 2, O.mov, [R.GS, R.SI], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.mov, [R.GS, R.DI], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.AX], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.CX], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.DX], [second]);
                case 0xF4:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.SP], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.SI], [second]);
                case 0xF7:
                    return new(pos, first, 2, O.mov, [R.Segr6, R.DI], [second]);
                case 0xF8:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.AX], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.CX], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.DX], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BX], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.SP], [second]);
                case 0xFD:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.BP], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.mov, [R.Segr7, R.SI], [second]);
            }
            return null;
        }
    }
}
