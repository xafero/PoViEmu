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
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Plus(R.SI), Constants.One)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Plus(R.DI), Constants.One)], [second]);
                case 0x02:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BP.Plus(R.SI), Constants.One)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.SI.Box()), Constants.One], [second]);
                case 0x05:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.DI.Box()), Constants.One], [second]);
                case 0x06:
                case 0x0E:
                case 0x16:
                case 0x1E:
                case 0x26:
                case 0x2E:
                case 0x3E:
                case 0x40:
                case 0x42:
                case 0x43:
                case 0x45:
                case 0x46:
                case 0x47:
                case 0x48:
                case 0x49:
                case 0x4A:
                case 0x4B:
                case 0x4C:
                case 0x4D:
                case 0x4E:
                case 0x4F:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x59:
                case 0x5A:
                case 0x5B:
                case 0x5C:
                case 0x5D:
                case 0x5E:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                case 0x66:
                case 0x67:
                case 0x68:
                case 0x69:
                case 0x6A:
                case 0x6B:
                case 0x6C:
                case 0x6D:
                case 0x6E:
                case 0x6F:
                case 0x79:
                case 0x7A:
                case 0x7B:
                case 0x7C:
                case 0x7D:
                case 0x7E:
                case 0x80:
                case 0x82:
                case 0x83:
                case 0x84:
                case 0x85:
                case 0x86:
                case 0x87:
                case 0x88:
                case 0x89:
                case 0x8A:
                case 0x8B:
                case 0x8C:
                case 0x8F:
                case 0x90:
                case 0x91:
                case 0x92:
                case 0x93:
                case 0x94:
                case 0x95:
                case 0x96:
                case 0x97:
                case 0x98:
                case 0x99:
                case 0x9A:
                case 0x9B:
                case 0x9C:
                case 0x9D:
                case 0x9F:
                case 0xA1:
                case 0xA2:
                case 0xA3:
                case 0xA4:
                case 0xA5:
                case 0xA6:
                case 0xA7:
                case 0xA8:
                case 0xA9:
                case 0xAA:
                case 0xAB:
                case 0xAC:
                case 0xAD:
                case 0xAF:
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xBB:
                case 0xBC:
                case 0xBD:
                case 0xBE:
                case 0xBF:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.rol, [M.@byte.On(R.BX.Box()), Constants.One], [second]);
                case 0x09:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BX.Plus(R.DI), Constants.One)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BP.Plus(R.SI), Constants.One)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BP.Plus(R.DI), Constants.One)], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.SI.Box()), Constants.One], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.DI.Box()), Constants.One], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.ror, [M.@byte.On(R.BX.Box()), Constants.One], [second]);
                case 0x10:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BX.Plus(R.SI), Constants.One)], [second]);
                case 0x11:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BX.Plus(R.DI), Constants.One)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BP.Plus(R.SI), Constants.One)], [second]);
                case 0x13:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.BP.Plus(R.DI), Constants.One)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.SI.Box()), Constants.One], [second]);
                case 0x15:
                    return new(pos, first, 2, O.rcl, [M.@byte.On(R.DI.Box()), Constants.One], [second]);
                case 0x18:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Plus(R.SI), Constants.One)], [second]);
                case 0x19:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Plus(R.DI), Constants.One)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BP.Plus(R.SI), Constants.One)], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BP.Plus(R.DI), Constants.One)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.SI.Box()), Constants.One], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.DI.Box()), Constants.One], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.rcr, [M.@byte.On(R.BX.Box()), Constants.One], [second]);
                case 0x20:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BX.Plus(R.SI), Constants.One)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BX.Plus(R.DI), Constants.One)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.BP.Plus(R.SI), Constants.One)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.SI.Box()), Constants.One], [second]);
                case 0x25:
                    return new(pos, first, 2, O.shl, [M.@byte.On(R.DI.Box()), Constants.One], [second]);
                case 0x28:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Plus(R.SI), Constants.One)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Plus(R.DI), Constants.One)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BP.Plus(R.SI), Constants.One)], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BP.Plus(R.DI), Constants.One)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.SI.Box()), Constants.One], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.DI.Box()), Constants.One], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.shr, [M.@byte.On(R.BX.Box()), Constants.One], [second]);
                case 0x38:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Plus(R.SI), Constants.One)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Plus(R.DI), Constants.One)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BP.Plus(R.SI), Constants.One)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BP.Plus(R.DI), Constants.One)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.SI.Box()), Constants.One], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.DI.Box()), Constants.One], [second]);
                case 0x3F:
                    return new(pos, first, 2, O.sar, [M.@byte.On(R.BX.Box()), Constants.One], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.rol, [R.AL, Constants.One], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.rol, [R.CL, Constants.One], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.rol, [R.DL, Constants.One], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.rol, [R.BL, Constants.One], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.rol, [R.CH, Constants.One], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.rol, [R.DH, Constants.One], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.rol, [R.BH, Constants.One], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.ror, [R.AL, Constants.One], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.ror, [R.CL, Constants.One], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.ror, [R.DL, Constants.One], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.ror, [R.BL, Constants.One], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.ror, [R.AH, Constants.One], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.ror, [R.CH, Constants.One], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.ror, [R.BH, Constants.One], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.rcl, [R.CL, Constants.One], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.rcl, [R.DL, Constants.One], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.rcl, [R.BL, Constants.One], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.rcl, [R.CH, Constants.One], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.rcl, [R.DH, Constants.One], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.rcl, [R.BH, Constants.One], [second]);
                case 0xD8:
                    return new(pos, first, 2, O.rcr, [R.AL, Constants.One], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.rcr, [R.CL, Constants.One], [second]);
                case 0xDA:
                    return new(pos, first, 2, O.rcr, [R.DL, Constants.One], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.rcr, [R.CH, Constants.One], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.rcr, [R.DH, Constants.One], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.rcr, [R.BH, Constants.One], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.shl, [R.DL, Constants.One], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.shl, [R.BL, Constants.One], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.shl, [R.AH, Constants.One], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.shl, [R.CH, Constants.One], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.shl, [R.DH, Constants.One], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.shr, [R.AL, Constants.One], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.shr, [R.CL, Constants.One], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.shr, [R.DL, Constants.One], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.shr, [R.BL, Constants.One], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.shr, [R.AH, Constants.One], [second]);
                case 0xED:
                    return new(pos, first, 2, O.shr, [R.CH, Constants.One], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.shr, [R.BH, Constants.One], [second]);
                case 0xF8:
                    return new(pos, first, 2, O.sar, [R.AL, Constants.One], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.sar, [R.CL, Constants.One], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.sar, [R.DL, Constants.One], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.sar, [R.BL, Constants.One], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.sar, [R.AH, Constants.One], [second]);
                case 0xFD:
                    return new(pos, first, 2, O.sar, [R.CH, Constants.One], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.sar, [R.DH, Constants.One], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.sar, [R.BH, Constants.One], [second]);
            }
            return null;
        }
    }
}
