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
    internal static class Intel16xDB
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x01:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BX.Plus(R.DI))],  [second]  );
                case 0x02:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BP.Plus(R.SI))],  [second]  );
                case 0x03:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BP.Plus(R.DI))],  [second]  );
                case 0x05:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.DI.Box())],  [second]  );
                case 0x06:
                    return new(pos, first, 4, O.fild, [M.dword.On(s.NextShort(buff).Box())],   [ second ]  );
                case 0x07:
                    return new(pos, first, 2, O.fild, [M.dword.On(R.BX.Box())],  [second]  );
                case 0x08:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BX.Plus(R.SI))],  [second]  );
                case 0x0A:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BP.Plus(R.SI))],  [second]  );
                case 0x0B:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BP.Plus(R.DI))],  [second]  );
                case 0x0C:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.SI.Box())],  [second]  );
                case 0x0D:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.DI.Box())],  [second]  );
                case 0x0E:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(s.NextShort(buff).Box())],   [ second ]  );
                case 0x0F:
                    return new(pos, first, 2, O.fisttp, [M.dword.On(R.BX.Box())],  [second]  );
                case 0x10:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BX.Plus(R.SI))],  [second]  );
                case 0x11:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BX.Plus(R.DI))],  [second]  );
                case 0x12:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BP.Plus(R.SI))],  [second]  );
                case 0x13:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BP.Plus(R.DI))],  [second]  );
                case 0x14:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.SI.Box())],  [second]  );
                case 0x16:
                    return new(pos, first, 4, O.fist, [M.dword.On(s.NextShort(buff).Box())],   [ second ]  );
                case 0x17:
                    return new(pos, first, 2, O.fist, [M.dword.On(R.BX.Box())],  [second]  );
                case 0x18:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BX.Plus(R.SI))],  [second]  );
                case 0x19:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BX.Plus(R.DI))],  [second]  );
                case 0x1A:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BP.Plus(R.SI))],  [second]  );
                case 0x1B:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BP.Plus(R.DI))],  [second]  );
                case 0x1C:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.SI.Box())],  [second]  );
                case 0x1D:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.DI.Box())],  [second]  );
                case 0x1E:
                    return new(pos, first, 4, O.fistp, [M.dword.On(s.NextShort(buff).Box())],   [ second ]  );
                case 0x1F:
                    return new(pos, first, 2, O.fistp, [M.dword.On(R.BX.Box())],  [second]  );
                case 0x28:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BX.Plus(R.SI))],  [second]  );
                case 0x29:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BX.Plus(R.DI))],  [second]  );
                case 0x2A:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BP.Plus(R.SI))],  [second]  );
                case 0x2B:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BP.Plus(R.DI))],  [second]  );
                case 0x2C:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.SI.Box())],  [second]  );
                case 0x2D:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.DI.Box())],  [second]  );
                case 0x2E:
                    return new(pos, first, 4, O.fld, [M.tword.On(s.NextShort(buff).Box())],   [ second ]  );
                case 0x2F:
                    return new(pos, first, 2, O.fld, [M.tword.On(R.BX.Box())],  [second]  );
                case 0x38:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BX.Plus(R.SI))],  [second]  );
                case 0x3A:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BP.Plus(R.SI))],  [second]  );
                case 0x3B:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BP.Plus(R.DI))],  [second]  );
                case 0x3C:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.SI.Box())],  [second]  );
                case 0x3D:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.DI.Box())],  [second]  );
                case 0x3E:
                    return new(pos, first, 4, O.fstp, [M.tword.On(s.NextShort(buff).Box())],   [ second ]  );
                case 0x3F:
                    return new(pos, first, 2, O.fstp, [M.tword.On(R.BX.Box())],  [second]  );
                case 0x40:
                    return new(pos, first, 3, O.fild, [M.dword.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x42:
                    return new(pos, first, 3, O.fild, [M.dword.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x43:
                    return new(pos, first, 3, O.fild, [M.dword.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x45:
                    return new(pos, first, 3, O.fild, [M.dword.On(R.DI.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x46:
                    return new(pos, first, 3, O.fild, [M.dword.On(R.BP.Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x47:
                    return new(pos, first, 3, O.fild, [M.dword.On(R.BX.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x48:
                    return new(pos, first, 3, O.fisttp, [M.dword.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x49:
                    return new(pos, first, 3, O.fisttp, [M.dword.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x4A:
                    return new(pos, first, 3, O.fisttp, [M.dword.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x4B:
                    return new(pos, first, 3, O.fisttp, [M.dword.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x4D:
                    return new(pos, first, 3, O.fisttp, [M.dword.On(R.DI.Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x4E:
                    return new(pos, first, 3, O.fisttp, [M.dword.On(R.BP.Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x4F:
                    return new(pos, first, 3, O.fisttp, [M.dword.On(R.BX.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x50:
                    return new(pos, first, 3, O.fist, [M.dword.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x52:
                    return new(pos, first, 3, O.fist, [M.dword.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x53:
                    return new(pos, first, 3, O.fist, [M.dword.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x54:
                    return new(pos, first, 3, O.fist, [M.dword.On(R.SI.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x55:
                    return new(pos, first, 3, O.fist, [M.dword.On(R.DI.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x56:
                    return new(pos, first, 3, O.fist, [M.dword.On(R.BP.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x57:
                    return new(pos, first, 3, O.fist, [M.dword.On(R.BX.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x58:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x59:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x5A:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x5B:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x5C:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.SI.Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x5D:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.DI.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x5E:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.BP.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x5F:
                    return new(pos, first, 3, O.fistp, [M.dword.On(R.BX.Minus(s.NextByte(buff)))],   [ second ]  );
                case 0x68:
                    return new(pos, first, 3, O.fld, [M.tword.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x69:
                    return new(pos, first, 3, O.fld, [M.tword.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x6A:
                    return new(pos, first, 3, O.fld, [M.tword.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x6B:
                    return new(pos, first, 3, O.fld, [M.tword.On(R.BP.Plus(R.DI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x6D:
                    return new(pos, first, 3, O.fld, [M.tword.On(R.DI.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x6E:
                    return new(pos, first, 3, O.fld, [M.tword.On(R.BP.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x6F:
                    return new(pos, first, 3, O.fld, [M.tword.On(R.BX.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x78:
                    return new(pos, first, 3, O.fstp, [M.tword.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x79:
                    return new(pos, first, 3, O.fstp, [M.tword.On(R.BX.Plus(R.DI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x7A:
                    return new(pos, first, 3, O.fstp, [M.tword.On(R.BP.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x7C:
                    return new(pos, first, 3, O.fstp, [M.tword.On(R.SI.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x7D:
                    return new(pos, first, 3, O.fstp, [M.tword.On(R.DI.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x7E:
                    return new(pos, first, 3, O.fstp, [M.tword.On(R.BP.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x7F:
                    return new(pos, first, 3, O.fstp, [M.tword.On(R.BX.Plus(s.NextByte(buff)))],   [ second ]  );
                case 0x80:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.BX.Plus(R.SI).Signed(s.NextByte(buff)))],   [ second ]  );
                case 0x81:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x82:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x83:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x84:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.SI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x85:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.DI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x86:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.BP.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x87:
                    return new(pos, first, 4, O.fild, [M.dword.On(R.BX.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x88:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x89:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x8B:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x8C:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(R.SI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x8D:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(R.DI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x8E:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(R.BP.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x8F:
                    return new(pos, first, 4, O.fisttp, [M.dword.On(R.BX.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x90:
                    return new(pos, first, 4, O.fist, [M.dword.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x91:
                    return new(pos, first, 4, O.fist, [M.dword.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x92:
                    return new(pos, first, 4, O.fist, [M.dword.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x93:
                    return new(pos, first, 4, O.fist, [M.dword.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x97:
                    return new(pos, first, 4, O.fist, [M.dword.On(R.BX.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x98:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x99:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x9A:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x9B:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x9C:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.SI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x9D:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.DI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x9E:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.BP.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0x9F:
                    return new(pos, first, 4, O.fistp, [M.dword.On(R.BX.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xA8:
                    return new(pos, first, 4, O.fld, [M.tword.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xA9:
                    return new(pos, first, 4, O.fld, [M.tword.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xAA:
                    return new(pos, first, 4, O.fld, [M.tword.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xAC:
                    return new(pos, first, 4, O.fld, [M.tword.On(R.SI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xAD:
                    return new(pos, first, 4, O.fld, [M.tword.On(R.DI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xAF:
                    return new(pos, first, 4, O.fld, [M.tword.On(R.BX.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xB8:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xB9:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xBA:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xBB:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xBC:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.SI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xBD:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.DI.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xBE:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.BP.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xBF:
                    return new(pos, first, 4, O.fstp, [M.tword.On(R.BX.Signed(s.NextShort(buff)))],   [ second ]  );
                case 0xC0:
                    return new(pos, first, 2, O.fcmovnb, [R.St0],  [second]  );
                case 0xC1:
                    return new(pos, first, 2, O.fcmovnb, [R.St1],  [second]  );
                case 0xC2:
                    return new(pos, first, 2, O.fcmovnb, [R.St2],  [second]  );
                case 0xC3:
                    return new(pos, first, 2, O.fcmovnb, [R.St3],  [second]  );
                case 0xC4:
                    return new(pos, first, 2, O.fcmovnb, [R.St4],  [second]  );
                case 0xC5:
                    return new(pos, first, 2, O.fcmovnb, [R.St5],  [second]  );
                case 0xC8:
                    return new(pos, first, 2, O.fcmovne, [R.St0],  [second]  );
                case 0xC9:
                    return new(pos, first, 2, O.fcmovne, [R.St1],  [second]  );
                case 0xCA:
                    return new(pos, first, 2, O.fcmovne, [R.St2],  [second]  );
                case 0xCB:
                    return new(pos, first, 2, O.fcmovne, [R.St3],  [second]  );
                case 0xCC:
                    return new(pos, first, 2, O.fcmovne, [R.St4],  [second]  );
                case 0xCD:
                    return new(pos, first, 2, O.fcmovne, [R.St5],  [second]  );
                case 0xCE:
                    return new(pos, first, 2, O.fcmovne, [R.St6],  [second]  );
                case 0xCF:
                    return new(pos, first, 2, O.fcmovne, [R.St7],  [second]  );
                case 0xD0:
                    return new(pos, first, 2, O.fcmovnbe, [R.St0],  [second]  );
                case 0xD1:
                    return new(pos, first, 2, O.fcmovnbe, [R.St1],  [second]  );
                case 0xD2:
                    return new(pos, first, 2, O.fcmovnbe, [R.St2],  [second]  );
                case 0xD3:
                    return new(pos, first, 2, O.fcmovnbe, [R.St3],  [second]  );
                case 0xD4:
                    return new(pos, first, 2, O.fcmovnbe, [R.St4],  [second]  );
                case 0xD5:
                    return new(pos, first, 2, O.fcmovnbe, [R.St5],  [second]  );
                case 0xD7:
                    return new(pos, first, 2, O.fcmovnbe, [R.St7],  [second]  );
                case 0xD9:
                    return new(pos, first, 2, O.fcmovnu, [R.St1],  [second]  );
                case 0xDA:
                    return new(pos, first, 2, O.fcmovnu, [R.St2],  [second]  );
                case 0xDC:
                    return new(pos, first, 2, O.fcmovnu, [R.St4],  [second]  );
                case 0xDD:
                    return new(pos, first, 2, O.fcmovnu, [R.St5],  [second]  );
                case 0xDE:
                    return new(pos, first, 2, O.fcmovnu, [R.St6],  [second]  );
                case 0xDF:
                    return new(pos, first, 2, O.fcmovnu, [R.St7],  [second]  );
                case 0xE1:
                    return new(pos, first, 2, O.fndisi, [],  [second]  );
                case 0xE3:
                    return new(pos, first, 2, O.fninit, [],  [second]  );
                case 0xE4:
                    return new(pos, first, 2, O.fsetpm, [],  [second]  );
                case 0xE8:
                    return new(pos, first, 2, O.fucomi, [R.St0],  [second]  );
                case 0xE9:
                    return new(pos, first, 2, O.fucomi, [R.St1],  [second]  );
                case 0xEA:
                    return new(pos, first, 2, O.fucomi, [R.St2],  [second]  );
                case 0xEC:
                    return new(pos, first, 2, O.fucomi, [R.St4],  [second]  );
                case 0xED:
                    return new(pos, first, 2, O.fucomi, [R.St5],  [second]  );
                case 0xEF:
                    return new(pos, first, 2, O.fucomi, [R.St7],  [second]  );
                case 0xF0:
                    return new(pos, first, 2, O.fcomi, [R.St0],  [second]  );
                case 0xF1:
                    return new(pos, first, 2, O.fcomi, [R.St1],  [second]  );
                case 0xF2:
                    return new(pos, first, 2, O.fcomi, [R.St2],  [second]  );
                case 0xF3:
                    return new(pos, first, 2, O.fcomi, [R.St3],  [second]  );
                case 0xF5:
                    return new(pos, first, 2, O.fcomi, [R.St5],  [second]  );
                case 0xF6:
                    return new(pos, first, 2, O.fcomi, [R.St6],  [second]  );
                case 0xF7:
                    return new(pos, first, 2, O.fcomi, [R.St7],  [second]  );
            }
            return null;
        }
    }
}
