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
    internal static class Intel16xC5
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.lds, [R.AX, R.BX.Plus(R.SI)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.lds, [R.AX, R.BX.Plus(R.DI)], [second]);
                case 0x03:
                    return new(pos, first, 2, O.lds, [R.AX, R.BP.Plus(R.DI)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.lds, [R.AX, R.SI.Box()], [second]);
                case 0x06:
                case 0x0E:
                case 0x16:
                case 0x1E:
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
                case 0x48:
                case 0x49:
                case 0x4A:
                case 0x4C:
                case 0x4D:
                case 0x4E:
                case 0x4F:
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x56:
                case 0x58:
                case 0x59:
                case 0x5A:
                case 0x5B:
                case 0x5C:
                case 0x5D:
                case 0x5E:
                case 0x5F:
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
                case 0x6C:
                case 0x6D:
                case 0x6E:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x75:
                case 0x76:
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
                case 0x95:
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
                case 0xBA:
                case 0xBB:
                case 0xBC:
                case 0xBD:
                case 0xBE:
                case 0xBF:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.lds, [R.AX, R.BX.Box()], [second]);
                case 0x08:
                    return new(pos, first, 2, O.lds, [R.CX, R.BX.Plus(R.SI)], [second]);
                case 0x09:
                    return new(pos, first, 2, O.lds, [R.CX, R.BX.Plus(R.DI)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.lds, [R.CX, R.BP.Plus(R.SI)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.lds, [R.CX, R.BP.Plus(R.DI)], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.lds, [R.CX, R.DI.Box()], [second]);
                case 0x11:
                    return new(pos, first, 2, O.lds, [R.DX, R.BX.Plus(R.DI)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.lds, [R.DX, R.BP.Plus(R.SI)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.lds, [R.DX, R.SI.Box()], [second]);
                case 0x17:
                    return new(pos, first, 2, O.lds, [R.DX, R.BX.Box()], [second]);
                case 0x18:
                    return new(pos, first, 2, O.lds, [R.BX, R.BX.Plus(R.SI)], [second]);
                case 0x19:
                    return new(pos, first, 2, O.lds, [R.BX, R.BX.Plus(R.DI)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.lds, [R.BX, R.BP.Plus(R.SI)], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.lds, [R.BX, R.BP.Plus(R.DI)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.lds, [R.BX, R.SI.Box()], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.lds, [R.BX, R.DI.Box()], [second]);
                case 0x20:
                    return new(pos, first, 2, O.lds, [R.SP, R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.lds, [R.SP, R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.lds, [R.SP, R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.lds, [R.SP, R.BP.Plus(R.DI)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.lds, [R.SP, R.SI.Box()], [second]);
                case 0x25:
                    return new(pos, first, 2, O.lds, [R.SP, R.DI.Box()], [second]);
                case 0x27:
                    return new(pos, first, 2, O.lds, [R.SP, R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.lds, [R.BP, R.BX.Plus(R.SI)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.lds, [R.BP, R.BX.Plus(R.DI)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.lds, [R.BP, R.BP.Plus(R.SI)], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.lds, [R.BP, R.BP.Plus(R.DI)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.lds, [R.BP, R.SI.Box()], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.lds, [R.BP, R.DI.Box()], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.lds, [R.BP, R.BX.Box()], [second]);
                case 0x30:
                    return new(pos, first, 2, O.lds, [R.SI, R.BX.Plus(R.SI)], [second]);
                case 0x31:
                    return new(pos, first, 2, O.lds, [R.SI, R.BX.Plus(R.DI)], [second]);
                case 0x32:
                    return new(pos, first, 2, O.lds, [R.SI, R.BP.Plus(R.SI)], [second]);
                case 0x33:
                    return new(pos, first, 2, O.lds, [R.SI, R.BP.Plus(R.DI)], [second]);
                case 0x34:
                    return new(pos, first, 2, O.lds, [R.SI, R.SI.Box()], [second]);
                case 0x35:
                    return new(pos, first, 2, O.lds, [R.SI, R.DI.Box()], [second]);
                case 0x38:
                    return new(pos, first, 2, O.lds, [R.DI, R.BX.Plus(R.SI)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.lds, [R.DI, R.BX.Plus(R.DI)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.lds, [R.DI, R.BP.Plus(R.SI)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.lds, [R.DI, R.SI.Box()], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.lds, [R.DI, R.DI.Box()], [second]);
                case 0x3F:
                    return new(pos, first, 2, O.lds, [R.DI, R.BX.Box()], [second]);
            }
            return null;
        }
    }
}
