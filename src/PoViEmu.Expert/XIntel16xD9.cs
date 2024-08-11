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
    internal static class Intel16xD9
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.fld, [M.dword.On(R.BX.Plus(R.SI))], [second]);
                case 0x02:
                    return new(pos, first, 2, O.fld, [M.dword.On(R.BP.Plus(R.SI))], [second]);
                case 0x03:
                    return new(pos, first, 2, O.fld, [M.dword.On(R.BP.Plus(R.DI))], [second]);
                case 0x04:
                    return new(pos, first, 2, O.fld, [M.dword.On(R.SI.Box())], [second]);
                case 0x05:
                    return new(pos, first, 2, O.fld, [M.dword.On(R.DI.Box())], [second]);
                case 0x06:
                    return new(pos, first, 4, O.fld, [M.dword.On(s.NextShort(buff).Box())]);
                case 0x07:
                    return new(pos, first, 2, O.fld, [M.dword.On(R.BX.Box())], [second]);
                case 0x10:
                    return new(pos, first, 2, O.fst, [M.dword.On(R.BX.Plus(R.SI))], [second]);
                case 0x12:
                    return new(pos, first, 2, O.fst, [M.dword.On(R.BP.Plus(R.SI))], [second]);
                case 0x13:
                    return new(pos, first, 2, O.fst, [M.dword.On(R.BP.Plus(R.DI))], [second]);
                case 0x14:
                    return new(pos, first, 2, O.fst, [M.dword.On(R.SI.Box())], [second]);
                case 0x15:
                    return new(pos, first, 2, O.fst, [M.dword.On(R.DI.Box())], [second]);
                case 0x16:
                    return new(pos, first, 4, O.fst, [M.dword.On(s.NextShort(buff).Box())]);
                case 0x17:
                    return new(pos, first, 2, O.fst, [M.dword.On(R.BX.Box())], [second]);
                case 0x18:
                    return new(pos, first, 2, O.fstp, [M.dword.On(R.BX.Plus(R.SI))], [second]);
                case 0x19:
                    return new(pos, first, 2, O.fstp, [M.dword.On(R.BX.Plus(R.DI))], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.fstp, [M.dword.On(R.BP.Plus(R.SI))], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.fstp, [M.dword.On(R.BP.Plus(R.DI))], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.fstp, [M.dword.On(R.SI.Box())], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.fstp, [M.dword.On(R.DI.Box())], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.fstp, [M.dword.On(R.BX.Box())], [second]);
                case 0x20:
                    return new(pos, first, 2, O.fldenv, [R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.fldenv, [R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.fldenv, [R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.fldenv, [R.BP.Plus(R.DI)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.fldenv, [R.SI.Box()], [second]);
                case 0x25:
                    return new(pos, first, 2, O.fldenv, [R.DI.Box()], [second]);
                case 0x26:
                    return new(pos, first, 4, O.fldenv, [s.NextShort(buff).Box()]);
                case 0x27:
                    return new(pos, first, 2, O.fldenv, [R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.fldcw, [R.BX.Plus(R.SI)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.fldcw, [R.BX.Plus(R.DI)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.fldcw, [R.BP.Plus(R.SI)], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.fldcw, [R.BP.Plus(R.DI)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.fldcw, [R.SI.Box()], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.fldcw, [R.DI.Box()], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.fldcw, [R.BX.Box()], [second]);
                case 0x30:
                    return new(pos, first, 2, O.fnstenv, [R.BX.Plus(R.SI)], [second]);
                case 0x31:
                    return new(pos, first, 2, O.fnstenv, [R.BX.Plus(R.DI)], [second]);
                case 0x32:
                    return new(pos, first, 2, O.fnstenv, [R.BP.Plus(R.SI)], [second]);
                case 0x34:
                    return new(pos, first, 2, O.fnstenv, [R.SI.Box()], [second]);
                case 0x35:
                    return new(pos, first, 2, O.fnstenv, [R.DI.Box()], [second]);
                case 0x36:
                    return new(pos, first, 4, O.fnstenv, [s.NextShort(buff).Box()]);
                case 0x37:
                    return new(pos, first, 2, O.fnstenv, [R.BX.Box()], [second]);
                case 0x38:
                    return new(pos, first, 2, O.fnstcw, [R.BX.Plus(R.SI)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.fnstcw, [R.BX.Plus(R.DI)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.fnstcw, [R.BP.Plus(R.SI)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.fnstcw, [R.BP.Plus(R.DI)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.fnstcw, [R.SI.Box()], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.fnstcw, [R.DI.Box()], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.fnstcw, [s.NextShort(buff).Box()]);
                case 0x3F:
                    return new(pos, first, 2, O.fnstcw, [R.BX.Box()], [second]);
                case 0x40:
                    return new(pos, first, 3, O.fld, [M.dword.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x41:
                    return new(pos, first, 3, O.fld, [M.dword.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x42:
                    return new(pos, first, 3, O.fld, [M.dword.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x43:
                    return new(pos, first, 3, O.fld, [M.dword.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x44:
                    return new(pos, first, 3, O.fld, [M.dword.On(R.SI.Minus(s.NextByte(buff)))]);
                case 0x45:
                    return new(pos, first, 3, O.fld, [M.dword.On(R.DI.Plus(s.NextByte(buff)))]);
                case 0x47:
                    return new(pos, first, 3, O.fld, [M.dword.On(R.BX.Minus(s.NextByte(buff)))]);
                case 0x50:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x51:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x52:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x53:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x54:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x55:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.DI.Plus(s.NextByte(buff)))]);
                case 0x56:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.BP.Minus(s.NextByte(buff)))]);
                case 0x57:
                    return new(pos, first, 3, O.fst, [M.dword.On(R.BX.Plus(s.NextByte(buff)))]);
                case 0x58:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x59:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)))]);
                case 0x5A:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x5B:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x5C:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x5D:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.DI.Plus(s.NextByte(buff)))]);
                case 0x5E:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.BP.Plus(s.NextByte(buff)))]);
                case 0x5F:
                    return new(pos, first, 3, O.fstp, [M.dword.On(R.BX.Minus(s.NextByte(buff)))]);
                case 0x60:
                    return new(pos, first, 3, O.fldenv, [R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x61:
                    return new(pos, first, 3, O.fldenv, [R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x62:
                    return new(pos, first, 3, O.fldenv, [R.BP.Plus(R.SI).Minus(s.NextByte(buff))]);
                case 0x63:
                    return new(pos, first, 3, O.fldenv, [R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x64:
                    return new(pos, first, 3, O.fldenv, [R.SI.Plus(s.NextByte(buff))]);
                case 0x65:
                    return new(pos, first, 3, O.fldenv, [R.DI.Plus(s.NextByte(buff))]);
                case 0x66:
                    return new(pos, first, 3, O.fldenv, [R.BP.Plus(s.NextByte(buff))]);
                case 0x68:
                    return new(pos, first, 3, O.fldcw, [R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x69:
                    return new(pos, first, 3, O.fldcw, [R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x6A:
                    return new(pos, first, 3, O.fldcw, [R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x6B:
                    return new(pos, first, 3, O.fldcw, [R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x6C:
                    return new(pos, first, 3, O.fldcw, [R.SI.Plus(s.NextByte(buff))]);
                case 0x6D:
                    return new(pos, first, 3, O.fldcw, [R.DI.Plus(s.NextByte(buff))]);
                case 0x6E:
                    return new(pos, first, 3, O.fldcw, [R.BP.Plus(s.NextByte(buff))]);
                case 0x6F:
                    return new(pos, first, 3, O.fldcw, [R.BX.Minus(s.NextByte(buff))]);
                case 0x70:
                    return new(pos, first, 3, O.fnstenv, [R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x71:
                    return new(pos, first, 3, O.fnstenv, [R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x73:
                    return new(pos, first, 3, O.fnstenv, [R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x74:
                    return new(pos, first, 3, O.fnstenv, [R.SI.Plus(s.NextByte(buff))]);
                case 0x75:
                    return new(pos, first, 3, O.fnstenv, [R.DI.Plus(s.NextByte(buff))]);
                case 0x76:
                    return new(pos, first, 3, O.fnstenv, [R.BP.Plus(s.NextByte(buff))]);
                case 0x77:
                    return new(pos, first, 3, O.fnstenv, [R.BX.Plus(s.NextByte(buff))]);
                case 0x79:
                    return new(pos, first, 3, O.fnstcw, [R.BX.Plus(R.DI).Minus(s.NextByte(buff))]);
                case 0x7A:
                    return new(pos, first, 3, O.fnstcw, [R.BP.Plus(R.SI).Minus(s.NextByte(buff))]);
                case 0x7B:
                    return new(pos, first, 3, O.fnstcw, [R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x7C:
                    return new(pos, first, 3, O.fnstcw, [R.SI.Plus(s.NextByte(buff))]);
                case 0x7D:
                    return new(pos, first, 3, O.fnstcw, [R.DI.Minus(s.NextByte(buff))]);
                case 0x7E:
                    return new(pos, first, 3, O.fnstcw, [R.BP.Plus(s.NextByte(buff))]);
                case 0x7F:
                    return new(pos, first, 3, O.fnstcw, [R.BX.Plus(s.NextByte(buff))]);
                case 0x80:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.BX.Plus(R.SI).Minus(s.NextShort(buff)))]);
                case 0x81:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.BX.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0x82:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.BP.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0x83:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.BP.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0x84:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.SI.Minus(s.NextShort(buff)))]);
                case 0x85:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.DI.Minus(s.NextShort(buff)))]);
                case 0x86:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.BP.Minus(s.NextShort(buff)))]);
                case 0x87:
                    return new(pos, first, 4, O.fld, [M.dword.On(R.BX.Minus(s.NextShort(buff)))]);
                case 0x91:
                    return new(pos, first, 4, O.fst, [M.dword.On(R.BX.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0x92:
                    return new(pos, first, 4, O.fst, [M.dword.On(R.BP.Plus(R.SI).Minus(s.NextShort(buff)))]);
                case 0x93:
                    return new(pos, first, 4, O.fst, [M.dword.On(R.BP.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0x94:
                    return new(pos, first, 4, O.fst, [M.dword.On(R.SI.Minus(s.NextShort(buff)))]);
                case 0x95:
                    return new(pos, first, 4, O.fst, [M.dword.On(R.DI.Plus(s.NextShort(buff)))]);
                case 0x96:
                    return new(pos, first, 4, O.fst, [M.dword.On(R.BP.Plus(s.NextShort(buff)))]);
                case 0x97:
                    return new(pos, first, 4, O.fst, [M.dword.On(R.BX.Minus(s.NextShort(buff)))]);
                case 0x98:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.BX.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0x99:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.BX.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0x9A:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.BP.Plus(R.SI).Minus(s.NextShort(buff)))]);
                case 0x9B:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.BP.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0x9C:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.SI.Plus(s.NextShort(buff)))]);
                case 0x9D:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.DI.Minus(s.NextShort(buff)))]);
                case 0x9E:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.BP.Plus(s.NextShort(buff)))]);
                case 0x9F:
                    return new(pos, first, 4, O.fstp, [M.dword.On(R.BX.Plus(s.NextShort(buff)))]);
                case 0xA0:
                    return new(pos, first, 4, O.fldenv, [R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xA1:
                    return new(pos, first, 4, O.fldenv, [R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xA2:
                    return new(pos, first, 4, O.fldenv, [R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xA3:
                    return new(pos, first, 4, O.fldenv, [R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xA4:
                    return new(pos, first, 4, O.fldenv, [R.SI.Minus(s.NextShort(buff))]);
                case 0xA5:
                    return new(pos, first, 4, O.fldenv, [R.DI.Minus(s.NextShort(buff))]);
                case 0xA6:
                    return new(pos, first, 4, O.fldenv, [R.BP.Minus(s.NextShort(buff))]);
                case 0xA7:
                    return new(pos, first, 4, O.fldenv, [R.BX.Plus(s.NextShort(buff))]);
                case 0xA8:
                    return new(pos, first, 4, O.fldcw, [R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xA9:
                    return new(pos, first, 4, O.fldcw, [R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xAA:
                    return new(pos, first, 4, O.fldcw, [R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xAB:
                    return new(pos, first, 4, O.fldcw, [R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xAC:
                    return new(pos, first, 4, O.fldcw, [R.SI.Minus(s.NextShort(buff))]);
                case 0xAD:
                    return new(pos, first, 4, O.fldcw, [R.DI.Plus(s.NextShort(buff))]);
                case 0xAE:
                    return new(pos, first, 4, O.fldcw, [R.BP.Minus(s.NextShort(buff))]);
                case 0xAF:
                    return new(pos, first, 4, O.fldcw, [R.BX.Minus(s.NextShort(buff))]);
                case 0xB0:
                    return new(pos, first, 4, O.fnstenv, [R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xB1:
                    return new(pos, first, 4, O.fnstenv, [R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xB2:
                    return new(pos, first, 4, O.fnstenv, [R.BP.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xB3:
                    return new(pos, first, 4, O.fnstenv, [R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xB4:
                    return new(pos, first, 4, O.fnstenv, [R.SI.Plus(s.NextShort(buff))]);
                case 0xB5:
                    return new(pos, first, 4, O.fnstenv, [R.DI.Plus(s.NextShort(buff))]);
                case 0xB6:
                    return new(pos, first, 4, O.fnstenv, [R.BP.Plus(s.NextShort(buff))]);
                case 0xB7:
                    return new(pos, first, 4, O.fnstenv, [R.BX.Minus(s.NextShort(buff))]);
                case 0xB8:
                    return new(pos, first, 4, O.fnstcw, [R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xB9:
                    return new(pos, first, 4, O.fnstcw, [R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xBA:
                    return new(pos, first, 4, O.fnstcw, [R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xBB:
                    return new(pos, first, 4, O.fnstcw, [R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xBC:
                    return new(pos, first, 4, O.fnstcw, [R.SI.Minus(s.NextShort(buff))]);
                case 0xBD:
                    return new(pos, first, 4, O.fnstcw, [R.DI.Minus(s.NextShort(buff))]);
                case 0xBE:
                    return new(pos, first, 4, O.fnstcw, [R.BP.Plus(s.NextShort(buff))]);
                case 0xBF:
                    return new(pos, first, 4, O.fnstcw, [R.BX.Plus(s.NextShort(buff))]);
                case 0xC0:
                    return new(pos, first, 2, O.fld, [R.St0], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.fld, [R.St2], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.fld, [R.St3], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.fld, [R.St5], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.fld, [R.St6], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.fxch, [R.St0], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.fxch, [R.St1], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.fxch, [R.St2], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.fxch, [R.St3], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.fxch, [R.St4], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.fxch, [R.St5], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.fxch, [R.St7], [second]);
                case 0xD0:
                    return new(pos, first, 2, O.fnop, [], [second]);
                case 0xE1:
                    return new(pos, first, 2, O.fabs, [], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.ftst, [], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.fxam, [], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.fld1, [], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.fldl2t, [], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.fldl2e, [], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.fldpi, [], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.fldlg2, [], [second]);
                case 0xED:
                    return new(pos, first, 2, O.fldln2, [], [second]);
                case 0xEE:
                    return new(pos, first, 2, O.fldz, [], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.f2xm1, [], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.fyl2x, [], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.fptan, [], [second]);
                case 0xF3:
                    return new(pos, first, 2, O.fpatan, [], [second]);
                case 0xF4:
                    return new(pos, first, 2, O.fxtract, [], [second]);
                case 0xF5:
                    return new(pos, first, 2, O.fprem1, [], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.fdecstp, [], [second]);
                case 0xF7:
                    return new(pos, first, 2, O.fincstp, [], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.fyl2xp1, [], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.fsqrt, [], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.fsincos, [], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.frndint, [], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.fsin, [], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.fcos, [], [second]);
            }
            return null;
        }
    }
}
