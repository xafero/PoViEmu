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
    internal static class Intel16xDF
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte();
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.fild, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x01:
                    return new(pos, first, 2, O.fild, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x03:
                    return new(pos, first, 2, O.fild, [M.word.On(R.BP.Plus(R.DI))], [second]);
                case 0x04:
                    return new(pos, first, 2, O.fild, [M.word.On(R.SI.Box())], [second]);
                case 0x05:
                    return new(pos, first, 2, O.fild, [M.word.On(R.DI.Box())], [second]);
                case 0x06:
                case 0x0E:
                case 0x1E:
                case 0x26:
                case 0x36:
                case 0x3E:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x45:
                case 0x46:
                case 0x47:
                case 0x49:
                case 0x4A:
                case 0x4B:
                case 0x4C:
                case 0x4D:
                case 0x4E:
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
                case 0x6B:
                case 0x6C:
                case 0x6D:
                case 0x6E:
                case 0x6F:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x74:
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
                case 0x85:
                case 0x86:
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
                case 0x94:
                case 0x95:
                case 0x96:
                case 0x97:
                case 0x98:
                case 0x99:
                case 0x9A:
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
                case 0xA9:
                case 0xAA:
                case 0xAB:
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
                case 0xBF:
                    break;
                case 0x08:
                    return new(pos, first, 2, O.fisttp, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x09:
                    return new(pos, first, 2, O.fisttp, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.fisttp, [M.word.On(R.BP.Plus(R.SI))], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.fisttp, [M.word.On(R.BP.Plus(R.DI))], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.fisttp, [M.word.On(R.SI.Box())], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.fisttp, [M.word.On(R.DI.Box())], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.fisttp, [M.word.On(R.BX.Box())], [second]);
                case 0x10:
                    return new(pos, first, 2, O.fist, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x11:
                    return new(pos, first, 2, O.fist, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x12:
                    return new(pos, first, 2, O.fist, [M.word.On(R.BP.Plus(R.SI))], [second]);
                case 0x13:
                    return new(pos, first, 2, O.fist, [M.word.On(R.BP.Plus(R.DI))], [second]);
                case 0x14:
                    return new(pos, first, 2, O.fist, [M.word.On(R.SI.Box())], [second]);
                case 0x15:
                    return new(pos, first, 2, O.fist, [M.word.On(R.DI.Box())], [second]);
                case 0x17:
                    return new(pos, first, 2, O.fist, [M.word.On(R.BX.Box())], [second]);
                case 0x18:
                    return new(pos, first, 2, O.fistp, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x19:
                    return new(pos, first, 2, O.fistp, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.fistp, [M.word.On(R.BP.Plus(R.SI))], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.fistp, [M.word.On(R.BP.Plus(R.DI))], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.fistp, [M.word.On(R.SI.Box())], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.fistp, [M.word.On(R.DI.Box())], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.fistp, [M.word.On(R.BX.Box())], [second]);
                case 0x20:
                    return new(pos, first, 2, O.fbld, [M.tword.On(R.BX.Plus(R.SI))], [second]);
                case 0x22:
                    return new(pos, first, 2, O.fbld, [M.tword.On(R.BP.Plus(R.SI))], [second]);
                case 0x23:
                    return new(pos, first, 2, O.fbld, [M.tword.On(R.BP.Plus(R.DI))], [second]);
                case 0x24:
                    return new(pos, first, 2, O.fbld, [M.tword.On(R.SI.Box())], [second]);
                case 0x25:
                    return new(pos, first, 2, O.fbld, [M.tword.On(R.DI.Box())], [second]);
                case 0x28:
                    return new(pos, first, 2, O.fild, [M.qword.On(R.BX.Plus(R.SI))], [second]);
                case 0x29:
                    return new(pos, first, 2, O.fild, [M.qword.On(R.BX.Plus(R.DI))], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.fild, [M.qword.On(R.BP.Plus(R.SI))], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.fild, [M.qword.On(R.BP.Plus(R.DI))], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.fild, [M.qword.On(R.SI.Box())], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.fild, [M.qword.On(R.DI.Box())], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.fild, [M.qword.On(R.BX.Box())], [second]);
                case 0x30:
                    return new(pos, first, 2, O.fbstp, [M.tword.On(R.BX.Plus(R.SI))], [second]);
                case 0x31:
                    return new(pos, first, 2, O.fbstp, [M.tword.On(R.BX.Plus(R.DI))], [second]);
                case 0x32:
                    return new(pos, first, 2, O.fbstp, [M.tword.On(R.BP.Plus(R.SI))], [second]);
                case 0x33:
                    return new(pos, first, 2, O.fbstp, [M.tword.On(R.BP.Plus(R.DI))], [second]);
                case 0x34:
                    return new(pos, first, 2, O.fbstp, [M.tword.On(R.SI.Box())], [second]);
                case 0x35:
                    return new(pos, first, 2, O.fbstp, [M.tword.On(R.DI.Box())], [second]);
                case 0x37:
                    return new(pos, first, 2, O.fbstp, [M.tword.On(R.BX.Box())], [second]);
                case 0x38:
                    return new(pos, first, 2, O.fistp, [M.qword.On(R.BX.Plus(R.SI))], [second]);
                case 0x39:
                    return new(pos, first, 2, O.fistp, [M.qword.On(R.BX.Plus(R.DI))], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.fistp, [M.qword.On(R.BP.Plus(R.SI))], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.fistp, [M.qword.On(R.BP.Plus(R.DI))], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.fistp, [M.qword.On(R.SI.Box())], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.fistp, [M.qword.On(R.DI.Box())], [second]);
                case 0x3F:
                    return new(pos, first, 2, O.fistp, [M.qword.On(R.BX.Box())], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.ffreep, [R.St0], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.ffreep, [R.St1], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.ffreep, [R.St2], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.ffreep, [R.St3], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.ffreep, [R.St4], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.ffreep, [R.St5], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.ffreep, [R.St6], [second]);
                case 0xE0:
                    return new(pos, first, 2, O.fnstsw, [R.AX], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.fucomip, [R.St0], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.fucomip, [R.St1], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.fucomip, [R.St2], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.fucomip, [R.St3], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.fucomip, [R.St4], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.fucomip, [R.St7], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.fcomip, [R.St0], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.fcomip, [R.St1], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.fcomip, [R.St2], [second]);
                case 0xF3:
                    return new(pos, first, 2, O.fcomip, [R.St3], [second]);
                case 0xF5:
                    return new(pos, first, 2, O.fcomip, [R.St5], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.fcomip, [R.St6], [second]);
                case 0xF7:
                    return new(pos, first, 2, O.fcomip, [R.St7], [second]);
            }
            return null;
        }
    }
}
