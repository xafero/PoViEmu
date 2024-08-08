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
    internal static class Intel16x62
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte();
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.bound, [R.AX, R.BX.Plus(R.SI)]);
                case 0x01:
                    return new(pos, first, 2, O.bound, [R.AX, R.BX.Plus(R.DI)]);
                case 0x02:
                    return new(pos, first, 2, O.bound, [R.AX, R.BP.Plus(R.SI)]);
                case 0x03:
                    return new(pos, first, 2, O.bound, [R.AX, R.BP.Plus(R.DI)]);
                case 0x04:
                    return new(pos, first, 2, O.bound, [R.AX, R.SI.Box()]);
                case 0x05:
                    return new(pos, first, 2, O.bound, [R.AX, R.DI.Box()]);
                case 0x06:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.bound, [R.AX, R.BX.Box()]);
                case 0x08:
                    return new(pos, first, 2, O.bound, [R.CX, R.BX.Plus(R.SI)]);
                case 0x09:
                    return new(pos, first, 2, O.bound, [R.CX, R.BX.Plus(R.DI)]);
                case 0x0A:
                    return new(pos, first, 2, O.bound, [R.CX, R.BP.Plus(R.SI)]);
                case 0x0B:
                    return new(pos, first, 2, O.bound, [R.CX, R.BP.Plus(R.DI)]);
                case 0x0C:
                    return new(pos, first, 2, O.bound, [R.CX, R.SI.Box()]);
                case 0x0D:
                    return new(pos, first, 2, O.bound, [R.CX, R.DI.Box()]);
                case 0x0E:
                    break;
                case 0x0F:
                    return new(pos, first, 2, O.bound, [R.CX, R.BX.Box()]);
                case 0x10:
                    return new(pos, first, 2, O.bound, [R.DX, R.BX.Plus(R.SI)]);
                case 0x11:
                    return new(pos, first, 2, O.bound, [R.DX, R.BX.Plus(R.DI)]);
                case 0x13:
                    return new(pos, first, 2, O.bound, [R.DX, R.BP.Plus(R.DI)]);
                case 0x14:
                    return new(pos, first, 2, O.bound, [R.DX, R.SI.Box()]);
                case 0x16:
                    break;
                case 0x17:
                    return new(pos, first, 2, O.bound, [R.DX, R.BX.Box()]);
                case 0x18:
                    return new(pos, first, 2, O.bound, [R.BX, R.BX.Plus(R.SI)]);
                case 0x19:
                    return new(pos, first, 2, O.bound, [R.BX, R.BX.Plus(R.DI)]);
                case 0x1A:
                    return new(pos, first, 2, O.bound, [R.BX, R.BP.Plus(R.SI)]);
                case 0x1B:
                    return new(pos, first, 2, O.bound, [R.BX, R.BP.Plus(R.DI)]);
                case 0x1C:
                    return new(pos, first, 2, O.bound, [R.BX, R.SI.Box()]);
                case 0x1D:
                    return new(pos, first, 2, O.bound, [R.BX, R.DI.Box()]);
                case 0x1E:
                    break;
                case 0x1F:
                    return new(pos, first, 2, O.bound, [R.BX, R.BX.Box()]);
                case 0x20:
                    return new(pos, first, 2, O.bound, [R.SP, R.BX.Plus(R.SI)]);
                case 0x21:
                    return new(pos, first, 2, O.bound, [R.SP, R.BX.Plus(R.DI)]);
                case 0x22:
                    return new(pos, first, 2, O.bound, [R.SP, R.BP.Plus(R.SI)]);
                case 0x23:
                    return new(pos, first, 2, O.bound, [R.SP, R.BP.Plus(R.DI)]);
                case 0x24:
                    return new(pos, first, 2, O.bound, [R.SP, R.SI.Box()]);
                case 0x25:
                    return new(pos, first, 2, O.bound, [R.SP, R.DI.Box()]);
                case 0x27:
                    return new(pos, first, 2, O.bound, [R.SP, R.BX.Box()]);
                case 0x28:
                    return new(pos, first, 2, O.bound, [R.BP, R.BX.Plus(R.SI)]);
                case 0x29:
                    return new(pos, first, 2, O.bound, [R.BP, R.BX.Plus(R.DI)]);
                case 0x2A:
                    return new(pos, first, 2, O.bound, [R.BP, R.BP.Plus(R.SI)]);
                case 0x2B:
                    return new(pos, first, 2, O.bound, [R.BP, R.BP.Plus(R.DI)]);
                case 0x2E:
                    break;
                case 0x2F:
                    return new(pos, first, 2, O.bound, [R.BP, R.BX.Box()]);
                case 0x30:
                    return new(pos, first, 2, O.bound, [R.SI, R.BX.Plus(R.SI)]);
                case 0x31:
                    return new(pos, first, 2, O.bound, [R.SI, R.BX.Plus(R.DI)]);
                case 0x32:
                    return new(pos, first, 2, O.bound, [R.SI, R.BP.Plus(R.SI)]);
                case 0x33:
                    return new(pos, first, 2, O.bound, [R.SI, R.BP.Plus(R.DI)]);
                case 0x34:
                    return new(pos, first, 2, O.bound, [R.SI, R.SI.Box()]);
                case 0x35:
                    return new(pos, first, 2, O.bound, [R.SI, R.DI.Box()]);
                case 0x36:
                    break;
                case 0x37:
                    return new(pos, first, 2, O.bound, [R.SI, R.BX.Box()]);
                case 0x38:
                    return new(pos, first, 2, O.bound, [R.DI, R.BX.Plus(R.SI)]);
                case 0x39:
                    return new(pos, first, 2, O.bound, [R.DI, R.BX.Plus(R.DI)]);
                case 0x3B:
                    return new(pos, first, 2, O.bound, [R.DI, R.BP.Plus(R.DI)]);
                case 0x3C:
                    return new(pos, first, 2, O.bound, [R.DI, R.SI.Box()]);
                case 0x3D:
                    return new(pos, first, 2, O.bound, [R.DI, R.DI.Box()]);
                case 0x3E:
                    break;
                case 0x3F:
                    return new(pos, first, 2, O.bound, [R.DI, R.BX.Box()]);
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
                case 0x70:
                    break;
                case 0x71:
                    break;
                case 0x72:
                    break;
                case 0x73:
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
                case 0x9B:
                    break;
                case 0x9C:
                    break;
                case 0x9D:
                    break;
                case 0x9F:
                    break;
                case 0xA0:
                    break;
                case 0xA1:
                    break;
                case 0xA2:
                    break;
                case 0xA3:
                    break;
                case 0xA4:
                    break;
                case 0xA5:
                    break;
                case 0xA6:
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
                case 0xB4:
                    break;
                case 0xB5:
                    break;
                case 0xB6:
                    break;
                case 0xB7:
                    break;
                case 0xB8:
                    break;
                case 0xB9:
                    break;
                case 0xBA:
                    break;
                case 0xBB:
                    break;
                case 0xBC:
                    break;
                case 0xBD:
                    break;
                case 0xBE:
                    break;
                case 0xBF:
                    break;
            }
            return null;
        }
    }
}
