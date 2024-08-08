﻿// ReSharper disable InconsistentNaming

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

namespace PoViEmu.Expert
{
    internal static class Intel16xD1
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte();
            switch (second)
            {
                case 0x01:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BX.Plus(R.DI), Constants.One)]);
                case 0x02:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BP.Plus(R.SI), Constants.One)]);
                case 0x03:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BP.Plus(R.DI), Constants.One)]);
                case 0x04:
                    return new(pos, first, 2, O.rol, [M.word.On(R.SI.Box(), Constants.One)]);
                case 0x05:
                    return new(pos, first, 2, O.rol, [M.word.On(R.DI.Box(), Constants.One)]);
                case 0x06:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BX.Box(), Constants.One)]);
                case 0x08:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BX.Plus(R.SI), Constants.One)]);
                case 0x09:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BX.Plus(R.DI), Constants.One)]);
                case 0x0A:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BP.Plus(R.SI), Constants.One)]);
                case 0x0B:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BP.Plus(R.DI), Constants.One)]);
                case 0x0C:
                    return new(pos, first, 2, O.ror, [M.word.On(R.SI.Box(), Constants.One)]);
                case 0x0D:
                    return new(pos, first, 2, O.ror, [M.word.On(R.DI.Box(), Constants.One)]);
                case 0x0E:
                    break;
                case 0x0F:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BX.Box(), Constants.One)]);
                case 0x10:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BX.Plus(R.SI), Constants.One)]);
                case 0x11:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BX.Plus(R.DI), Constants.One)]);
                case 0x12:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BP.Plus(R.SI), Constants.One)]);
                case 0x13:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BP.Plus(R.DI), Constants.One)]);
                case 0x14:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.SI.Box(), Constants.One)]);
                case 0x15:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.DI.Box(), Constants.One)]);
                case 0x16:
                    break;
                case 0x17:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BX.Box(), Constants.One)]);
                case 0x18:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BX.Plus(R.SI), Constants.One)]);
                case 0x19:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BX.Plus(R.DI), Constants.One)]);
                case 0x1A:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BP.Plus(R.SI), Constants.One)]);
                case 0x1C:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.SI.Box(), Constants.One)]);
                case 0x1E:
                    break;
                case 0x1F:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BX.Box(), Constants.One)]);
                case 0x20:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BX.Plus(R.SI), Constants.One)]);
                case 0x21:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BX.Plus(R.DI), Constants.One)]);
                case 0x22:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BP.Plus(R.SI), Constants.One)]);
                case 0x23:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BP.Plus(R.DI), Constants.One)]);
                case 0x24:
                    return new(pos, first, 2, O.shl, [M.word.On(R.SI.Box(), Constants.One)]);
                case 0x25:
                    return new(pos, first, 2, O.shl, [M.word.On(R.DI.Box(), Constants.One)]);
                case 0x26:
                    break;
                case 0x27:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BX.Box(), Constants.One)]);
                case 0x28:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BX.Plus(R.SI), Constants.One)]);
                case 0x29:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BX.Plus(R.DI), Constants.One)]);
                case 0x2A:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BP.Plus(R.SI), Constants.One)]);
                case 0x2B:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BP.Plus(R.DI), Constants.One)]);
                case 0x2C:
                    return new(pos, first, 2, O.shr, [M.word.On(R.SI.Box(), Constants.One)]);
                case 0x2D:
                    return new(pos, first, 2, O.shr, [M.word.On(R.DI.Box(), Constants.One)]);
                case 0x2E:
                    break;
                case 0x2F:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BX.Box(), Constants.One)]);
                case 0x38:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BX.Plus(R.SI), Constants.One)]);
                case 0x3A:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BP.Plus(R.SI), Constants.One)]);
                case 0x3B:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BP.Plus(R.DI), Constants.One)]);
                case 0x3D:
                    return new(pos, first, 2, O.sar, [M.word.On(R.DI.Box(), Constants.One)]);
                case 0x3E:
                    break;
                case 0x3F:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BX.Box(), Constants.One)]);
                case 0x40:
                    break;
                case 0x41:
                    break;
                case 0x42:
                    break;
                case 0x43:
                    break;
                case 0x44:
                    break;
                case 0x45:
                    break;
                case 0x46:
                    break;
                case 0x47:
                    break;
                case 0x48:
                    break;
                case 0x49:
                    break;
                case 0x4A:
                    break;
                case 0x4C:
                    break;
                case 0x4D:
                    break;
                case 0x4E:
                    break;
                case 0x50:
                    break;
                case 0x51:
                    break;
                case 0x52:
                    break;
                case 0x53:
                    break;
                case 0x54:
                    break;
                case 0x55:
                    break;
                case 0x56:
                    break;
                case 0x57:
                    break;
                case 0x58:
                    break;
                case 0x59:
                    break;
                case 0x5A:
                    break;
                case 0x5B:
                    break;
                case 0x5C:
                    break;
                case 0x5D:
                    break;
                case 0x5E:
                    break;
                case 0x5F:
                    break;
                case 0x60:
                    break;
                case 0x61:
                    break;
                case 0x62:
                    break;
                case 0x63:
                    break;
                case 0x64:
                    break;
                case 0x65:
                    break;
                case 0x67:
                    break;
                case 0x68:
                    break;
                case 0x69:
                    break;
                case 0x6A:
                    break;
                case 0x6B:
                    break;
                case 0x6C:
                    break;
                case 0x6D:
                    break;
                case 0x6E:
                    break;
                case 0x6F:
                    break;
                case 0x79:
                    break;
                case 0x7A:
                    break;
                case 0x7B:
                    break;
                case 0x7C:
                    break;
                case 0x7D:
                    break;
                case 0x7E:
                    break;
                case 0x80:
                    break;
                case 0x81:
                    break;
                case 0x82:
                    break;
                case 0x83:
                    break;
                case 0x84:
                    break;
                case 0x85:
                    break;
                case 0x86:
                    break;
                case 0x87:
                    break;
                case 0x88:
                    break;
                case 0x89:
                    break;
                case 0x8A:
                    break;
                case 0x8C:
                    break;
                case 0x8D:
                    break;
                case 0x8E:
                    break;
                case 0x8F:
                    break;
                case 0x90:
                    break;
                case 0x91:
                    break;
                case 0x92:
                    break;
                case 0x93:
                    break;
                case 0x94:
                    break;
                case 0x95:
                    break;
                case 0x96:
                    break;
                case 0x97:
                    break;
                case 0x98:
                    break;
                case 0x99:
                    break;
                case 0x9A:
                    break;
                case 0x9C:
                    break;
                case 0x9D:
                    break;
                case 0x9E:
                    break;
                case 0x9F:
                    break;
                case 0xA0:
                    break;
                case 0xA3:
                    break;
                case 0xA4:
                    break;
                case 0xA5:
                    break;
                case 0xA6:
                    break;
                case 0xA7:
                    break;
                case 0xA8:
                    break;
                case 0xA9:
                    break;
                case 0xAA:
                    break;
                case 0xAB:
                    break;
                case 0xAC:
                    break;
                case 0xAD:
                    break;
                case 0xAE:
                    break;
                case 0xAF:
                    break;
                case 0xB8:
                    break;
                case 0xB9:
                    break;
                case 0xBA:
                    break;
                case 0xBC:
                    break;
                case 0xBD:
                    break;
                case 0xBE:
                    break;
                case 0xBF:
                    break;
                case 0xC0:
                    return new(pos, first, 2, O.rol, [R.AX, Constants.One]);
                case 0xC1:
                    return new(pos, first, 2, O.rol, [R.CX, Constants.One]);
                case 0xC2:
                    return new(pos, first, 2, O.rol, [R.DX, Constants.One]);
                case 0xC3:
                    return new(pos, first, 2, O.rol, [R.BX, Constants.One]);
                case 0xC4:
                    return new(pos, first, 2, O.rol, [R.SP, Constants.One]);
                case 0xC5:
                    return new(pos, first, 2, O.rol, [R.BP, Constants.One]);
                case 0xC7:
                    return new(pos, first, 2, O.rol, [R.DI, Constants.One]);
                case 0xC8:
                    return new(pos, first, 2, O.ror, [R.AX, Constants.One]);
                case 0xC9:
                    return new(pos, first, 2, O.ror, [R.CX, Constants.One]);
                case 0xCA:
                    return new(pos, first, 2, O.ror, [R.DX, Constants.One]);
                case 0xCB:
                    return new(pos, first, 2, O.ror, [R.BX, Constants.One]);
                case 0xCC:
                    return new(pos, first, 2, O.ror, [R.SP, Constants.One]);
                case 0xCD:
                    return new(pos, first, 2, O.ror, [R.BP, Constants.One]);
                case 0xCE:
                    return new(pos, first, 2, O.ror, [R.SI, Constants.One]);
                case 0xCF:
                    return new(pos, first, 2, O.ror, [R.DI, Constants.One]);
                case 0xD1:
                    return new(pos, first, 2, O.rcl, [R.CX, Constants.One]);
                case 0xD2:
                    return new(pos, first, 2, O.rcl, [R.DX, Constants.One]);
                case 0xD3:
                    return new(pos, first, 2, O.rcl, [R.BX, Constants.One]);
                case 0xD4:
                    return new(pos, first, 2, O.rcl, [R.SP, Constants.One]);
                case 0xD5:
                    return new(pos, first, 2, O.rcl, [R.BP, Constants.One]);
                case 0xD6:
                    return new(pos, first, 2, O.rcl, [R.SI, Constants.One]);
                case 0xD7:
                    return new(pos, first, 2, O.rcl, [R.DI, Constants.One]);
                case 0xD8:
                    return new(pos, first, 2, O.rcr, [R.AX, Constants.One]);
                case 0xD9:
                    return new(pos, first, 2, O.rcr, [R.CX, Constants.One]);
                case 0xDA:
                    return new(pos, first, 2, O.rcr, [R.DX, Constants.One]);
                case 0xDB:
                    return new(pos, first, 2, O.rcr, [R.BX, Constants.One]);
                case 0xDC:
                    return new(pos, first, 2, O.rcr, [R.SP, Constants.One]);
                case 0xDD:
                    return new(pos, first, 2, O.rcr, [R.BP, Constants.One]);
                case 0xDF:
                    return new(pos, first, 2, O.rcr, [R.DI, Constants.One]);
                case 0xE0:
                    return new(pos, first, 2, O.shl, [R.AX, Constants.One]);
                case 0xE1:
                    return new(pos, first, 2, O.shl, [R.CX, Constants.One]);
                case 0xE2:
                    return new(pos, first, 2, O.shl, [R.DX, Constants.One]);
                case 0xE3:
                    return new(pos, first, 2, O.shl, [R.BX, Constants.One]);
                case 0xE4:
                    return new(pos, first, 2, O.shl, [R.SP, Constants.One]);
                case 0xE5:
                    return new(pos, first, 2, O.shl, [R.BP, Constants.One]);
                case 0xE7:
                    return new(pos, first, 2, O.shl, [R.DI, Constants.One]);
                case 0xE8:
                    return new(pos, first, 2, O.shr, [R.AX, Constants.One]);
                case 0xE9:
                    return new(pos, first, 2, O.shr, [R.CX, Constants.One]);
                case 0xEB:
                    return new(pos, first, 2, O.shr, [R.BX, Constants.One]);
                case 0xEC:
                    return new(pos, first, 2, O.shr, [R.SP, Constants.One]);
                case 0xED:
                    return new(pos, first, 2, O.shr, [R.BP, Constants.One]);
                case 0xEE:
                    return new(pos, first, 2, O.shr, [R.SI, Constants.One]);
                case 0xEF:
                    return new(pos, first, 2, O.shr, [R.DI, Constants.One]);
                case 0xF8:
                    return new(pos, first, 2, O.sar, [R.AX, Constants.One]);
                case 0xF9:
                    return new(pos, first, 2, O.sar, [R.CX, Constants.One]);
                case 0xFA:
                    return new(pos, first, 2, O.sar, [R.DX, Constants.One]);
                case 0xFB:
                    return new(pos, first, 2, O.sar, [R.BX, Constants.One]);
                case 0xFC:
                    return new(pos, first, 2, O.sar, [R.SP, Constants.One]);
                case 0xFD:
                    return new(pos, first, 2, O.sar, [R.BP, Constants.One]);
                case 0xFE:
                    return new(pos, first, 2, O.sar, [R.SI, Constants.One]);
            }
            return null;
        }
    }
}
