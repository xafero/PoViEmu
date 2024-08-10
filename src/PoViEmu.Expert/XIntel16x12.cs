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
    internal static class Intel16x12
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.adc, [R.AL, R.BX.Plus(R.SI)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.adc, [R.AL, R.BX.Plus(R.DI)], [second]);
                case 0x02:
                    return new(pos, first, 2, O.adc, [R.AL, R.BP.Plus(R.SI)], [second]);
                case 0x03:
                    return new(pos, first, 2, O.adc, [R.AL, R.BP.Plus(R.DI)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.adc, [R.AL, R.SI.Box()], [second]);
                case 0x05:
                    return new(pos, first, 2, O.adc, [R.AL, R.DI.Box()], [second]);
                case 0x06:
                case 0x0E:
                case 0x1E:
                case 0x26:
                case 0x2E:
                case 0x36:
                case 0x3E:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 0x46:
                case 0x47:
                case 0x49:
                case 0x4A:
                case 0x4B:
                case 0x4C:
                case 0x4F:
                case 0x50:
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
                case 0x5C:
                case 0x5D:
                case 0x5F:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x65:
                case 0x66:
                case 0x67:
                case 0x68:
                case 0x69:
                case 0x6A:
                case 0x6B:
                case 0x6C:
                case 0x6E:
                case 0x6F:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x74:
                case 0x75:
                case 0x77:
                case 0x78:
                case 0x79:
                case 0x7A:
                case 0x7B:
                case 0x7C:
                case 0x7D:
                case 0x7E:
                case 0x7F:
                case 0x80:
                case 0x81:
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
                case 0x8D:
                case 0x8E:
                case 0x8F:
                case 0x90:
                case 0x91:
                case 0x92:
                case 0x93:
                case 0x94:
                case 0x96:
                case 0x97:
                case 0x98:
                case 0x99:
                case 0x9A:
                case 0x9B:
                case 0x9C:
                case 0x9D:
                case 0x9E:
                case 0x9F:
                case 0xA0:
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
                case 0xAE:
                case 0xAF:
                case 0xB0:
                case 0xB1:
                case 0xB2:
                case 0xB3:
                case 0xB4:
                case 0xB5:
                case 0xB6:
                case 0xB7:
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xBB:
                case 0xBC:
                case 0xBD:
                case 0xBE:
                case 0xBF:
                    break;
                case 0x08:
                    return new(pos, first, 2, O.adc, [R.CL, R.BX.Plus(R.SI)], [second]);
                case 0x09:
                    return new(pos, first, 2, O.adc, [R.CL, R.BX.Plus(R.DI)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.adc, [R.CL, R.BP.Plus(R.SI)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.adc, [R.CL, R.BP.Plus(R.DI)], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.adc, [R.CL, R.SI.Box()], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.adc, [R.CL, R.DI.Box()], [second]);
                case 0x10:
                    return new(pos, first, 2, O.adc, [R.DL, R.BX.Plus(R.SI)], [second]);
                case 0x11:
                    return new(pos, first, 2, O.adc, [R.DL, R.BX.Plus(R.DI)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.adc, [R.DL, R.BP.Plus(R.SI)], [second]);
                case 0x13:
                    return new(pos, first, 2, O.adc, [R.DL, R.BP.Plus(R.DI)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.adc, [R.DL, R.SI.Box()], [second]);
                case 0x15:
                    return new(pos, first, 2, O.adc, [R.DL, R.DI.Box()], [second]);
                case 0x17:
                    return new(pos, first, 2, O.adc, [R.DL, R.BX.Box()], [second]);
                case 0x18:
                    return new(pos, first, 2, O.adc, [R.BL, R.BX.Plus(R.SI)], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.adc, [R.BL, R.BP.Plus(R.DI)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.adc, [R.BL, R.SI.Box()], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.adc, [R.BL, R.DI.Box()], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.adc, [R.BL, R.BX.Box()], [second]);
                case 0x20:
                    return new(pos, first, 2, O.adc, [R.AH, R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.adc, [R.AH, R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.adc, [R.AH, R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.adc, [R.AH, R.BP.Plus(R.DI)], [second]);
                case 0x25:
                    return new(pos, first, 2, O.adc, [R.AH, R.DI.Box()], [second]);
                case 0x27:
                    return new(pos, first, 2, O.adc, [R.AH, R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.adc, [R.CH, R.BX.Plus(R.SI)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.adc, [R.CH, R.BX.Plus(R.DI)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.adc, [R.CH, R.BP.Plus(R.SI)], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.adc, [R.CH, R.BP.Plus(R.DI)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.adc, [R.CH, R.SI.Box()], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.adc, [R.CH, R.DI.Box()], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.adc, [R.CH, R.BX.Box()], [second]);
                case 0x30:
                    return new(pos, first, 2, O.adc, [R.DH, R.BX.Plus(R.SI)], [second]);
                case 0x31:
                    return new(pos, first, 2, O.adc, [R.DH, R.BX.Plus(R.DI)], [second]);
                case 0x32:
                    return new(pos, first, 2, O.adc, [R.DH, R.BP.Plus(R.SI)], [second]);
                case 0x33:
                    return new(pos, first, 2, O.adc, [R.DH, R.BP.Plus(R.DI)], [second]);
                case 0x34:
                    return new(pos, first, 2, O.adc, [R.DH, R.SI.Box()], [second]);
                case 0x35:
                    return new(pos, first, 2, O.adc, [R.DH, R.DI.Box()], [second]);
                case 0x38:
                    return new(pos, first, 2, O.adc, [R.BH, R.BX.Plus(R.SI)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.adc, [R.BH, R.BX.Plus(R.DI)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.adc, [R.BH, R.BP.Plus(R.SI)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.adc, [R.BH, R.BP.Plus(R.DI)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.adc, [R.BH, R.SI.Box()], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.adc, [R.BH, R.DI.Box()], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.adc, [R.AL, R.CL], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.adc, [R.AL, R.DL], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.adc, [R.AL, R.BL], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.adc, [R.AL, R.AH], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.adc, [R.AL, R.DH], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.adc, [R.AL, R.BH], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.adc, [R.CL, R.AL], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.adc, [R.CL, R.CL], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.adc, [R.CL, R.BL], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.adc, [R.CL, R.AH], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.adc, [R.CL, R.CH], [second]);
                case 0xCE:
                    return new(pos, first, 2, O.adc, [R.CL, R.DH], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.adc, [R.CL, R.BH], [second]);
                case 0xD0:
                    return new(pos, first, 2, O.adc, [R.DL, R.AL], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.adc, [R.DL, R.CL], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.adc, [R.DL, R.DL], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.adc, [R.DL, R.BL], [second]);
                case 0xD4:
                    return new(pos, first, 2, O.adc, [R.DL, R.AH], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.adc, [R.DL, R.CH], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.adc, [R.DL, R.DH], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.adc, [R.DL, R.BH], [second]);
                case 0xD8:
                    return new(pos, first, 2, O.adc, [R.BL, R.AL], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.adc, [R.BL, R.CL], [second]);
                case 0xDA:
                    return new(pos, first, 2, O.adc, [R.BL, R.DL], [second]);
                case 0xDB:
                    return new(pos, first, 2, O.adc, [R.BL, R.BL], [second]);
                case 0xDC:
                    return new(pos, first, 2, O.adc, [R.BL, R.AH], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.adc, [R.BL, R.CH], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.adc, [R.BL, R.DH], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.adc, [R.BL, R.BH], [second]);
                case 0xE0:
                    return new(pos, first, 2, O.adc, [R.AH, R.AL], [second]);
                case 0xE1:
                    return new(pos, first, 2, O.adc, [R.AH, R.CL], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.adc, [R.AH, R.AH], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.adc, [R.AH, R.CH], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.adc, [R.AH, R.DH], [second]);
                case 0xE7:
                    return new(pos, first, 2, O.adc, [R.AH, R.BH], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.adc, [R.CH, R.AL], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.adc, [R.CH, R.CL], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.adc, [R.CH, R.DL], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.adc, [R.CH, R.BL], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.adc, [R.CH, R.AH], [second]);
                case 0xED:
                    return new(pos, first, 2, O.adc, [R.CH, R.CH], [second]);
                case 0xEE:
                    return new(pos, first, 2, O.adc, [R.CH, R.DH], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.adc, [R.CH, R.BH], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.adc, [R.DH, R.AL], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.adc, [R.DH, R.CL], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.adc, [R.DH, R.DL], [second]);
                case 0xF3:
                    return new(pos, first, 2, O.adc, [R.DH, R.BL], [second]);
                case 0xF4:
                    return new(pos, first, 2, O.adc, [R.DH, R.AH], [second]);
                case 0xF5:
                    return new(pos, first, 2, O.adc, [R.DH, R.CH], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.adc, [R.DH, R.DH], [second]);
                case 0xF7:
                    return new(pos, first, 2, O.adc, [R.DH, R.BH], [second]);
                case 0xF8:
                    return new(pos, first, 2, O.adc, [R.BH, R.AL], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.adc, [R.BH, R.CL], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.adc, [R.BH, R.DL], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.adc, [R.BH, R.BL], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.adc, [R.BH, R.AH], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.adc, [R.BH, R.DH], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.adc, [R.BH, R.BH], [second]);
            }
            return null;
        }
    }
}
