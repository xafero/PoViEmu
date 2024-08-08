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
    internal static class Intel16xDB
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte();
            switch (second)
            {
                case 0x01:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BX.Plus(R.DI))]);
                case 0x02:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BP.Plus(R.SI))]);
                case 0x03:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BP.Plus(R.DI))]);
                case 0x05:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.DI.Box())]);
                case 0x06:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BX.Box())]);
                case 0x08:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BX.Plus(R.SI))]);
                case 0x0A:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BP.Plus(R.SI))]);
                case 0x0B:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BP.Plus(R.DI))]);
                case 0x0C:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.SI.Box())]);
                case 0x0D:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.DI.Box())]);
                case 0x0E:
                    break;
                case 0x0F:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BX.Box())]);
                case 0x10:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BX.Plus(R.SI))]);
                case 0x11:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BX.Plus(R.DI))]);
                case 0x12:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BP.Plus(R.SI))]);
                case 0x13:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BP.Plus(R.DI))]);
                case 0x14:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.SI.Box())]);
                case 0x16:
                    break;
                case 0x17:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BX.Box())]);
                case 0x18:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BX.Plus(R.SI))]);
                case 0x19:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BX.Plus(R.DI))]);
                case 0x1A:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BP.Plus(R.SI))]);
                case 0x1B:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BP.Plus(R.DI))]);
                case 0x1C:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.SI.Box())]);
                case 0x1D:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.DI.Box())]);
                case 0x1E:
                    break;
                case 0x1F:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BX.Box())]);
                case 0x28:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BX.Plus(R.SI))]);
                case 0x29:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BX.Plus(R.DI))]);
                case 0x2A:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BP.Plus(R.SI))]);
                case 0x2B:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BP.Plus(R.DI))]);
                case 0x2C:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.SI.Box())]);
                case 0x2D:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.DI.Box())]);
                case 0x2E:
                    break;
                case 0x2F:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BX.Box())]);
                case 0x38:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BX.Plus(R.SI))]);
                case 0x3A:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BP.Plus(R.SI))]);
                case 0x3B:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BP.Plus(R.DI))]);
                case 0x3C:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.SI.Box())]);
                case 0x3D:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.DI.Box())]);
                case 0x3E:
                    break;
                case 0x3F:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BX.Box())]);
                case 0x40:
                    break;
                case 0x42:
                    break;
                case 0x43:
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
                case 0x68:
                    break;
                case 0x69:
                    break;
                case 0x6A:
                    break;
                case 0x6B:
                    break;
                case 0x6D:
                    break;
                case 0x6E:
                    break;
                case 0x6F:
                    break;
                case 0x78:
                    break;
                case 0x79:
                    break;
                case 0x7A:
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
                case 0x8B:
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
                case 0x9E:
                    break;
                case 0x9F:
                    break;
                case 0xA8:
                    break;
                case 0xA9:
                    break;
                case 0xAA:
                    break;
                case 0xAC:
                    break;
                case 0xAD:
                    break;
                case 0xAF:
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
                case 0xC0:
                    return new(pos, first, 2, O.fcmovnb, [R.St0]);
                case 0xC1:
                    return new(pos, first, 2, O.fcmovnb, [R.St1]);
                case 0xC2:
                    return new(pos, first, 2, O.fcmovnb, [R.St2]);
                case 0xC3:
                    return new(pos, first, 2, O.fcmovnb, [R.St3]);
                case 0xC4:
                    return new(pos, first, 2, O.fcmovnb, [R.St4]);
                case 0xC5:
                    return new(pos, first, 2, O.fcmovnb, [R.St5]);
                case 0xC8:
                    return new(pos, first, 2, O.fcmovne, [R.St0]);
                case 0xC9:
                    return new(pos, first, 2, O.fcmovne, [R.St1]);
                case 0xCA:
                    return new(pos, first, 2, O.fcmovne, [R.St2]);
                case 0xCB:
                    return new(pos, first, 2, O.fcmovne, [R.St3]);
                case 0xCC:
                    return new(pos, first, 2, O.fcmovne, [R.St4]);
                case 0xCD:
                    return new(pos, first, 2, O.fcmovne, [R.St5]);
                case 0xCE:
                    return new(pos, first, 2, O.fcmovne, [R.St6]);
                case 0xCF:
                    return new(pos, first, 2, O.fcmovne, [R.St7]);
                case 0xD0:
                    return new(pos, first, 2, O.fcmovnbe, [R.St0]);
                case 0xD1:
                    return new(pos, first, 2, O.fcmovnbe, [R.St1]);
                case 0xD2:
                    return new(pos, first, 2, O.fcmovnbe, [R.St2]);
                case 0xD3:
                    return new(pos, first, 2, O.fcmovnbe, [R.St3]);
                case 0xD4:
                    return new(pos, first, 2, O.fcmovnbe, [R.St4]);
                case 0xD5:
                    return new(pos, first, 2, O.fcmovnbe, [R.St5]);
                case 0xD7:
                    return new(pos, first, 2, O.fcmovnbe, [R.St7]);
                case 0xD9:
                    return new(pos, first, 2, O.fcmovnu, [R.St1]);
                case 0xDA:
                    return new(pos, first, 2, O.fcmovnu, [R.St2]);
                case 0xDC:
                    return new(pos, first, 2, O.fcmovnu, [R.St4]);
                case 0xDD:
                    return new(pos, first, 2, O.fcmovnu, [R.St5]);
                case 0xDE:
                    return new(pos, first, 2, O.fcmovnu, [R.St6]);
                case 0xDF:
                    return new(pos, first, 2, O.fcmovnu, [R.St7]);
                case 0xE1:
                    return new(pos, first, 2, O.fndisi, []);
                case 0xE3:
                    return new(pos, first, 2, O.fninit, []);
                case 0xE4:
                    return new(pos, first, 2, O.fsetpm, []);
                case 0xE8:
                    return new(pos, first, 2, O.fucomi, [R.St0]);
                case 0xE9:
                    return new(pos, first, 2, O.fucomi, [R.St1]);
                case 0xEA:
                    return new(pos, first, 2, O.fucomi, [R.St2]);
                case 0xEC:
                    return new(pos, first, 2, O.fucomi, [R.St4]);
                case 0xED:
                    return new(pos, first, 2, O.fucomi, [R.St5]);
                case 0xEF:
                    return new(pos, first, 2, O.fucomi, [R.St7]);
                case 0xF0:
                    return new(pos, first, 2, O.fcomi, [R.St0]);
                case 0xF1:
                    return new(pos, first, 2, O.fcomi, [R.St1]);
                case 0xF2:
                    return new(pos, first, 2, O.fcomi, [R.St2]);
                case 0xF3:
                    return new(pos, first, 2, O.fcomi, [R.St3]);
                case 0xF5:
                    return new(pos, first, 2, O.fcomi, [R.St5]);
                case 0xF6:
                    return new(pos, first, 2, O.fcomi, [R.St6]);
                case 0xF7:
                    return new(pos, first, 2, O.fcomi, [R.St7]);
            }
            return null;
        }
    }
}
