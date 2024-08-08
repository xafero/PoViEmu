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
    internal static class Intel16x85
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte();
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.SI), R.AX]);
                case 0x01:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.DI), R.AX]);
                case 0x02:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.SI), R.AX]);
                case 0x03:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.DI), R.AX]);
                case 0x04:
                    return new(pos, first, 2, O.test, [R.SI.Box(), R.AX]);
                case 0x05:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.AX]);
                case 0x06:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.AX]);
                case 0x09:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.DI), R.CX]);
                case 0x0A:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.SI), R.CX]);
                case 0x0B:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.DI), R.CX]);
                case 0x0C:
                    return new(pos, first, 2, O.test, [R.SI.Box(), R.CX]);
                case 0x0D:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.CX]);
                case 0x0E:
                    break;
                case 0x0F:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.CX]);
                case 0x10:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.SI), R.DX]);
                case 0x11:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.DI), R.DX]);
                case 0x12:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.SI), R.DX]);
                case 0x13:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.DI), R.DX]);
                case 0x15:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.DX]);
                case 0x16:
                    break;
                case 0x17:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.DX]);
                case 0x18:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.SI), R.BX]);
                case 0x19:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.DI), R.BX]);
                case 0x1A:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.SI), R.BX]);
                case 0x1B:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.DI), R.BX]);
                case 0x1C:
                    return new(pos, first, 2, O.test, [R.SI.Box(), R.BX]);
                case 0x1D:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.BX]);
                case 0x1E:
                    break;
                case 0x1F:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.BX]);
                case 0x21:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.DI), R.SP]);
                case 0x22:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.SI), R.SP]);
                case 0x24:
                    return new(pos, first, 2, O.test, [R.SI.Box(), R.SP]);
                case 0x25:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.SP]);
                case 0x26:
                    break;
                case 0x27:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.SP]);
                case 0x28:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.SI), R.BP]);
                case 0x29:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.DI), R.BP]);
                case 0x2A:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.SI), R.BP]);
                case 0x2B:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.DI), R.BP]);
                case 0x2C:
                    return new(pos, first, 2, O.test, [R.SI.Box(), R.BP]);
                case 0x2D:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.BP]);
                case 0x2E:
                    break;
                case 0x2F:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.BP]);
                case 0x30:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.SI), R.SI]);
                case 0x31:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.DI), R.SI]);
                case 0x33:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.DI), R.SI]);
                case 0x34:
                    return new(pos, first, 2, O.test, [R.SI.Box(), R.SI]);
                case 0x35:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.SI]);
                case 0x36:
                    break;
                case 0x37:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.SI]);
                case 0x38:
                    return new(pos, first, 2, O.test, [R.BX.Plus(R.SI), R.DI]);
                case 0x3A:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.SI), R.DI]);
                case 0x3B:
                    return new(pos, first, 2, O.test, [R.BP.Plus(R.DI), R.DI]);
                case 0x3C:
                    return new(pos, first, 2, O.test, [R.SI.Box(), R.DI]);
                case 0x3D:
                    return new(pos, first, 2, O.test, [R.DI.Box(), R.DI]);
                case 0x3E:
                    break;
                case 0x3F:
                    return new(pos, first, 2, O.test, [R.BX.Box(), R.DI]);
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
                case 0x4B:
                    break;
                case 0x4C:
                    break;
                case 0x4D:
                    break;
                case 0x4E:
                    break;
                case 0x4F:
                    break;
                case 0x50:
                    break;
                case 0x52:
                    break;
                case 0x53:
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
                case 0x6F:
                    break;
                case 0x70:
                    break;
                case 0x71:
                    break;
                case 0x72:
                    break;
                case 0x74:
                    break;
                case 0x75:
                    break;
                case 0x76:
                    break;
                case 0x77:
                    break;
                case 0x78:
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
                case 0x7F:
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
                case 0x9B:
                    break;
                case 0x9C:
                    break;
                case 0x9D:
                    break;
                case 0x9E:
                    break;
                case 0xA0:
                    break;
                case 0xA1:
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
                case 0xB0:
                    break;
                case 0xB1:
                    break;
                case 0xB2:
                    break;
                case 0xB3:
                    break;
                case 0xB4:
                    break;
                case 0xB5:
                    break;
                case 0xB6:
                    break;
                case 0xB8:
                    break;
                case 0xB9:
                    break;
                case 0xBA:
                    break;
                case 0xBB:
                    break;
                case 0xBD:
                    break;
                case 0xBE:
                    break;
                case 0xBF:
                    break;
                case 0xC0:
                    return new(pos, first, 2, O.test, [R.AX, R.AX]);
                case 0xC1:
                    return new(pos, first, 2, O.test, [R.CX, R.AX]);
                case 0xC2:
                    return new(pos, first, 2, O.test, [R.DX, R.AX]);
                case 0xC3:
                    return new(pos, first, 2, O.test, [R.BX, R.AX]);
                case 0xC4:
                    return new(pos, first, 2, O.test, [R.SP, R.AX]);
                case 0xC5:
                    return new(pos, first, 2, O.test, [R.BP, R.AX]);
                case 0xC6:
                    return new(pos, first, 2, O.test, [R.SI, R.AX]);
                case 0xC7:
                    return new(pos, first, 2, O.test, [R.DI, R.AX]);
                case 0xC8:
                    return new(pos, first, 2, O.test, [R.AX, R.CX]);
                case 0xC9:
                    return new(pos, first, 2, O.test, [R.CX, R.CX]);
                case 0xCA:
                    return new(pos, first, 2, O.test, [R.DX, R.CX]);
                case 0xCB:
                    return new(pos, first, 2, O.test, [R.BX, R.CX]);
                case 0xCC:
                    return new(pos, first, 2, O.test, [R.SP, R.CX]);
                case 0xCD:
                    return new(pos, first, 2, O.test, [R.BP, R.CX]);
                case 0xCE:
                    return new(pos, first, 2, O.test, [R.SI, R.CX]);
                case 0xCF:
                    return new(pos, first, 2, O.test, [R.DI, R.CX]);
                case 0xD0:
                    return new(pos, first, 2, O.test, [R.AX, R.DX]);
                case 0xD1:
                    return new(pos, first, 2, O.test, [R.CX, R.DX]);
                case 0xD2:
                    return new(pos, first, 2, O.test, [R.DX, R.DX]);
                case 0xD3:
                    return new(pos, first, 2, O.test, [R.BX, R.DX]);
                case 0xD4:
                    return new(pos, first, 2, O.test, [R.SP, R.DX]);
                case 0xD5:
                    return new(pos, first, 2, O.test, [R.BP, R.DX]);
                case 0xD6:
                    return new(pos, first, 2, O.test, [R.SI, R.DX]);
                case 0xD7:
                    return new(pos, first, 2, O.test, [R.DI, R.DX]);
                case 0xD8:
                    return new(pos, first, 2, O.test, [R.AX, R.BX]);
                case 0xD9:
                    return new(pos, first, 2, O.test, [R.CX, R.BX]);
                case 0xDB:
                    return new(pos, first, 2, O.test, [R.BX, R.BX]);
                case 0xDC:
                    return new(pos, first, 2, O.test, [R.SP, R.BX]);
                case 0xDD:
                    return new(pos, first, 2, O.test, [R.BP, R.BX]);
                case 0xDE:
                    return new(pos, first, 2, O.test, [R.SI, R.BX]);
                case 0xDF:
                    return new(pos, first, 2, O.test, [R.DI, R.BX]);
                case 0xE0:
                    return new(pos, first, 2, O.test, [R.AX, R.SP]);
                case 0xE1:
                    return new(pos, first, 2, O.test, [R.CX, R.SP]);
                case 0xE2:
                    return new(pos, first, 2, O.test, [R.DX, R.SP]);
                case 0xE3:
                    return new(pos, first, 2, O.test, [R.BX, R.SP]);
                case 0xE4:
                    return new(pos, first, 2, O.test, [R.SP, R.SP]);
                case 0xE5:
                    return new(pos, first, 2, O.test, [R.BP, R.SP]);
                case 0xE7:
                    return new(pos, first, 2, O.test, [R.DI, R.SP]);
                case 0xE8:
                    return new(pos, first, 2, O.test, [R.AX, R.BP]);
                case 0xE9:
                    return new(pos, first, 2, O.test, [R.CX, R.BP]);
                case 0xEA:
                    return new(pos, first, 2, O.test, [R.DX, R.BP]);
                case 0xEB:
                    return new(pos, first, 2, O.test, [R.BX, R.BP]);
                case 0xEC:
                    return new(pos, first, 2, O.test, [R.SP, R.BP]);
                case 0xED:
                    return new(pos, first, 2, O.test, [R.BP, R.BP]);
                case 0xEE:
                    return new(pos, first, 2, O.test, [R.SI, R.BP]);
                case 0xEF:
                    return new(pos, first, 2, O.test, [R.DI, R.BP]);
                case 0xF0:
                    return new(pos, first, 2, O.test, [R.AX, R.SI]);
                case 0xF1:
                    return new(pos, first, 2, O.test, [R.CX, R.SI]);
                case 0xF2:
                    return new(pos, first, 2, O.test, [R.DX, R.SI]);
                case 0xF3:
                    return new(pos, first, 2, O.test, [R.BX, R.SI]);
                case 0xF4:
                    return new(pos, first, 2, O.test, [R.SP, R.SI]);
                case 0xF5:
                    return new(pos, first, 2, O.test, [R.BP, R.SI]);
                case 0xF6:
                    return new(pos, first, 2, O.test, [R.SI, R.SI]);
                case 0xF7:
                    return new(pos, first, 2, O.test, [R.DI, R.SI]);
                case 0xF8:
                    return new(pos, first, 2, O.test, [R.AX, R.DI]);
                case 0xF9:
                    return new(pos, first, 2, O.test, [R.CX, R.DI]);
                case 0xFA:
                    return new(pos, first, 2, O.test, [R.DX, R.DI]);
                case 0xFB:
                    return new(pos, first, 2, O.test, [R.BX, R.DI]);
                case 0xFC:
                    return new(pos, first, 2, O.test, [R.SP, R.DI]);
                case 0xFF:
                    return new(pos, first, 2, O.test, [R.DI, R.DI]);
            }
            return null;
        }
    }
}
