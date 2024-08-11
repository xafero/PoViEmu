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
    internal static class Intel16xD1
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x01:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BX.Plus(R.DI), C.One)],  [second]  );
                case 0x02:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BP.Plus(R.SI), C.One)],  [second]  );
                case 0x03:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BP.Plus(R.DI), C.One)],  [second]  );
                case 0x04:
                    return new(pos, first, 2, O.rol, [M.word.On(R.SI.Box(), C.One)],  [second]  );
                case 0x05:
                    return new(pos, first, 2, O.rol, [M.word.On(R.DI.Box(), C.One)],  [second]  );
                case 0x06:
                    return new(pos, first, 4, O.rol, [M.word.On(s.NextShort(buff).Box(), C.One)],   [ second ]  );
                case 0x07:
                    return new(pos, first, 2, O.rol, [M.word.On(R.BX.Box(), C.One)],  [second]  );
                case 0x08:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BX.Plus(R.SI), C.One)],  [second]  );
                case 0x09:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BX.Plus(R.DI), C.One)],  [second]  );
                case 0x0A:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BP.Plus(R.SI), C.One)],  [second]  );
                case 0x0B:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BP.Plus(R.DI), C.One)],  [second]  );
                case 0x0C:
                    return new(pos, first, 2, O.ror, [M.word.On(R.SI.Box(), C.One)],  [second]  );
                case 0x0D:
                    return new(pos, first, 2, O.ror, [M.word.On(R.DI.Box(), C.One)],  [second]  );
                case 0x0E:
                    return new(pos, first, 4, O.ror, [M.word.On(s.NextShort(buff).Box(), C.One)],   [ second ]  );
                case 0x0F:
                    return new(pos, first, 2, O.ror, [M.word.On(R.BX.Box(), C.One)],  [second]  );
                case 0x10:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BX.Plus(R.SI), C.One)],  [second]  );
                case 0x11:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BX.Plus(R.DI), C.One)],  [second]  );
                case 0x12:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BP.Plus(R.SI), C.One)],  [second]  );
                case 0x13:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BP.Plus(R.DI), C.One)],  [second]  );
                case 0x14:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.SI.Box(), C.One)],  [second]  );
                case 0x15:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.DI.Box(), C.One)],  [second]  );
                case 0x16:
                    return new(pos, first, 4, O.rcl, [M.word.On(s.NextShort(buff).Box(), C.One)],   [ second ]  );
                case 0x17:
                    return new(pos, first, 2, O.rcl, [M.word.On(R.BX.Box(), C.One)],  [second]  );
                case 0x18:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BX.Plus(R.SI), C.One)],  [second]  );
                case 0x19:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BX.Plus(R.DI), C.One)],  [second]  );
                case 0x1A:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BP.Plus(R.SI), C.One)],  [second]  );
                case 0x1C:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.SI.Box(), C.One)],  [second]  );
                case 0x1E:
                    return new(pos, first, 4, O.rcr, [M.word.On(s.NextShort(buff).Box(), C.One)],   [ second ]  );
                case 0x1F:
                    return new(pos, first, 2, O.rcr, [M.word.On(R.BX.Box(), C.One)],  [second]  );
                case 0x20:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BX.Plus(R.SI), C.One)],  [second]  );
                case 0x21:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BX.Plus(R.DI), C.One)],  [second]  );
                case 0x22:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BP.Plus(R.SI), C.One)],  [second]  );
                case 0x23:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BP.Plus(R.DI), C.One)],  [second]  );
                case 0x24:
                    return new(pos, first, 2, O.shl, [M.word.On(R.SI.Box(), C.One)],  [second]  );
                case 0x25:
                    return new(pos, first, 2, O.shl, [M.word.On(R.DI.Box(), C.One)],  [second]  );
                case 0x26:
                    return new(pos, first, 4, O.shl, [M.word.On(s.NextShort(buff).Box(), C.One)],   [ second ]  );
                case 0x27:
                    return new(pos, first, 2, O.shl, [M.word.On(R.BX.Box(), C.One)],  [second]  );
                case 0x28:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BX.Plus(R.SI), C.One)],  [second]  );
                case 0x29:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BX.Plus(R.DI), C.One)],  [second]  );
                case 0x2A:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BP.Plus(R.SI), C.One)],  [second]  );
                case 0x2B:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BP.Plus(R.DI), C.One)],  [second]  );
                case 0x2C:
                    return new(pos, first, 2, O.shr, [M.word.On(R.SI.Box(), C.One)],  [second]  );
                case 0x2D:
                    return new(pos, first, 2, O.shr, [M.word.On(R.DI.Box(), C.One)],  [second]  );
                case 0x2E:
                    return new(pos, first, 4, O.shr, [M.word.On(s.NextShort(buff).Box(), C.One)],   [ second ]  );
                case 0x2F:
                    return new(pos, first, 2, O.shr, [M.word.On(R.BX.Box(), C.One)],  [second]  );
                case 0x38:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BX.Plus(R.SI), C.One)],  [second]  );
                case 0x3A:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BP.Plus(R.SI), C.One)],  [second]  );
                case 0x3B:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BP.Plus(R.DI), C.One)],  [second]  );
                case 0x3D:
                    return new(pos, first, 2, O.sar, [M.word.On(R.DI.Box(), C.One)],  [second]  );
                case 0x3E:
                    return new(pos, first, 4, O.sar, [M.word.On(s.NextShort(buff).Box(), C.One)],   [ second ]  );
                case 0x3F:
                    return new(pos, first, 2, O.sar, [M.word.On(R.BX.Box(), C.One)],  [second]  );
                case 0x40:
                    return new(pos, first, 3, O.rol, [M.word.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x41:
                    return new(pos, first, 3, O.rol, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x42:
                    return new(pos, first, 3, O.rol, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x43:
                    return new(pos, first, 3, O.rol, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x44:
                    return new(pos, first, 3, O.rol, [M.word.On(R.SI.Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x45:
                    return new(pos, first, 3, O.rol, [M.word.On(R.DI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x46:
                    return new(pos, first, 3, O.rol, [M.word.On(R.BP.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x47:
                    return new(pos, first, 3, O.rol, [M.word.On(R.BX.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x48:
                    return new(pos, first, 3, O.ror, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x49:
                    return new(pos, first, 3, O.ror, [M.word.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x4A:
                    return new(pos, first, 3, O.ror, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x4C:
                    return new(pos, first, 3, O.ror, [M.word.On(R.SI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x4D:
                    return new(pos, first, 3, O.ror, [M.word.On(R.DI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x4E:
                    return new(pos, first, 3, O.ror, [M.word.On(R.BP.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x50:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x51:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x52:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x53:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x54:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.SI.Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x55:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.DI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x56:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.BP.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x57:
                    return new(pos, first, 3, O.rcl, [M.word.On(R.BX.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x58:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x59:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x5A:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x5B:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x5C:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.SI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x5D:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.DI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x5E:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.BP.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x5F:
                    return new(pos, first, 3, O.rcr, [M.word.On(R.BX.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x60:
                    return new(pos, first, 3, O.shl, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x61:
                    return new(pos, first, 3, O.shl, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x62:
                    return new(pos, first, 3, O.shl, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x63:
                    return new(pos, first, 3, O.shl, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x64:
                    return new(pos, first, 3, O.shl, [M.word.On(R.SI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x65:
                    return new(pos, first, 3, O.shl, [M.word.On(R.DI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x67:
                    return new(pos, first, 3, O.shl, [M.word.On(R.BX.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x68:
                    return new(pos, first, 3, O.shr, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x69:
                    return new(pos, first, 3, O.shr, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x6A:
                    return new(pos, first, 3, O.shr, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x6B:
                    return new(pos, first, 3, O.shr, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x6C:
                    return new(pos, first, 3, O.shr, [M.word.On(R.SI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x6D:
                    return new(pos, first, 3, O.shr, [M.word.On(R.DI.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x6E:
                    return new(pos, first, 3, O.shr, [M.word.On(R.BP.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x6F:
                    return new(pos, first, 3, O.shr, [M.word.On(R.BX.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x79:
                    return new(pos, first, 3, O.sar, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x7A:
                    return new(pos, first, 3, O.sar, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x7B:
                    return new(pos, first, 3, O.sar, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x7C:
                    return new(pos, first, 3, O.sar, [M.word.On(R.SI.Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x7D:
                    return new(pos, first, 3, O.sar, [M.word.On(R.DI.Minus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x7E:
                    return new(pos, first, 3, O.sar, [M.word.On(R.BP.Plus(s.NextByte(buff)), C.One)],   [ second ]  );
                case 0x80:
                    return new(pos, first, 4, O.rol, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x81:
                    return new(pos, first, 4, O.rol, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x82:
                    return new(pos, first, 4, O.rol, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x83:
                    return new(pos, first, 4, O.rol, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x84:
                    return new(pos, first, 4, O.rol, [M.word.On(R.SI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x85:
                    return new(pos, first, 4, O.rol, [M.word.On(R.DI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x86:
                    return new(pos, first, 4, O.rol, [M.word.On(R.BP.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x87:
                    return new(pos, first, 4, O.rol, [M.word.On(R.BX.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x88:
                    return new(pos, first, 4, O.ror, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x89:
                    return new(pos, first, 4, O.ror, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x8A:
                    return new(pos, first, 4, O.ror, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x8C:
                    return new(pos, first, 4, O.ror, [M.word.On(R.SI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x8D:
                    return new(pos, first, 4, O.ror, [M.word.On(R.DI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x8E:
                    return new(pos, first, 4, O.ror, [M.word.On(R.BP.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x8F:
                    return new(pos, first, 4, O.ror, [M.word.On(R.BX.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x90:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x91:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x92:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x93:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x94:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.SI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x95:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.DI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x96:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.BP.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x97:
                    return new(pos, first, 4, O.rcl, [M.word.On(R.BX.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x98:
                    return new(pos, first, 4, O.rcr, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x99:
                    return new(pos, first, 4, O.rcr, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x9A:
                    return new(pos, first, 4, O.rcr, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0x9C:
                    return new(pos, first, 4, O.rcr, [M.word.On(R.SI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x9D:
                    return new(pos, first, 4, O.rcr, [M.word.On(R.DI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x9E:
                    return new(pos, first, 4, O.rcr, [M.word.On(R.BP.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0x9F:
                    return new(pos, first, 4, O.rcr, [M.word.On(R.BX.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xA0:
                    return new(pos, first, 4, O.shl, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xA3:
                    return new(pos, first, 4, O.shl, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xA4:
                    return new(pos, first, 4, O.shl, [M.word.On(R.SI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xA5:
                    return new(pos, first, 4, O.shl, [M.word.On(R.DI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xA6:
                    return new(pos, first, 4, O.shl, [M.word.On(R.BP.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xA7:
                    return new(pos, first, 4, O.shl, [M.word.On(R.BX.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xA8:
                    return new(pos, first, 4, O.shr, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xA9:
                    return new(pos, first, 4, O.shr, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xAA:
                    return new(pos, first, 4, O.shr, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xAB:
                    return new(pos, first, 4, O.shr, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xAC:
                    return new(pos, first, 4, O.shr, [M.word.On(R.SI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xAD:
                    return new(pos, first, 4, O.shr, [M.word.On(R.DI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xAE:
                    return new(pos, first, 4, O.shr, [M.word.On(R.BP.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xAF:
                    return new(pos, first, 4, O.shr, [M.word.On(R.BX.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xB8:
                    return new(pos, first, 4, O.sar, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xB9:
                    return new(pos, first, 4, O.sar, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xBA:
                    return new(pos, first, 4, O.sar, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff))), C.One],   [ second ]  );
                case 0xBC:
                    return new(pos, first, 4, O.sar, [M.word.On(R.SI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xBD:
                    return new(pos, first, 4, O.sar, [M.word.On(R.DI.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xBE:
                    return new(pos, first, 4, O.sar, [M.word.On(R.BP.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xBF:
                    return new(pos, first, 4, O.sar, [M.word.On(R.BX.Signed(s.NextShort(buff)), C.One)],   [ second ]  );
                case 0xC0:
                    return new(pos, first, 2, O.rol, [R.AX, C.One],  [second]  );
                case 0xC1:
                    return new(pos, first, 2, O.rol, [R.CX, C.One],  [second]  );
                case 0xC2:
                    return new(pos, first, 2, O.rol, [R.DX, C.One],  [second]  );
                case 0xC3:
                    return new(pos, first, 2, O.rol, [R.BX, C.One],  [second]  );
                case 0xC4:
                    return new(pos, first, 2, O.rol, [R.SP, C.One],  [second]  );
                case 0xC5:
                    return new(pos, first, 2, O.rol, [R.BP, C.One],  [second]  );
                case 0xC7:
                    return new(pos, first, 2, O.rol, [R.DI, C.One],  [second]  );
                case 0xC8:
                    return new(pos, first, 2, O.ror, [R.AX, C.One],  [second]  );
                case 0xC9:
                    return new(pos, first, 2, O.ror, [R.CX, C.One],  [second]  );
                case 0xCA:
                    return new(pos, first, 2, O.ror, [R.DX, C.One],  [second]  );
                case 0xCB:
                    return new(pos, first, 2, O.ror, [R.BX, C.One],  [second]  );
                case 0xCC:
                    return new(pos, first, 2, O.ror, [R.SP, C.One],  [second]  );
                case 0xCD:
                    return new(pos, first, 2, O.ror, [R.BP, C.One],  [second]  );
                case 0xCE:
                    return new(pos, first, 2, O.ror, [R.SI, C.One],  [second]  );
                case 0xCF:
                    return new(pos, first, 2, O.ror, [R.DI, C.One],  [second]  );
                case 0xD1:
                    return new(pos, first, 2, O.rcl, [R.CX, C.One],  [second]  );
                case 0xD2:
                    return new(pos, first, 2, O.rcl, [R.DX, C.One],  [second]  );
                case 0xD3:
                    return new(pos, first, 2, O.rcl, [R.BX, C.One],  [second]  );
                case 0xD4:
                    return new(pos, first, 2, O.rcl, [R.SP, C.One],  [second]  );
                case 0xD5:
                    return new(pos, first, 2, O.rcl, [R.BP, C.One],  [second]  );
                case 0xD6:
                    return new(pos, first, 2, O.rcl, [R.SI, C.One],  [second]  );
                case 0xD7:
                    return new(pos, first, 2, O.rcl, [R.DI, C.One],  [second]  );
                case 0xD8:
                    return new(pos, first, 2, O.rcr, [R.AX, C.One],  [second]  );
                case 0xD9:
                    return new(pos, first, 2, O.rcr, [R.CX, C.One],  [second]  );
                case 0xDA:
                    return new(pos, first, 2, O.rcr, [R.DX, C.One],  [second]  );
                case 0xDB:
                    return new(pos, first, 2, O.rcr, [R.BX, C.One],  [second]  );
                case 0xDC:
                    return new(pos, first, 2, O.rcr, [R.SP, C.One],  [second]  );
                case 0xDD:
                    return new(pos, first, 2, O.rcr, [R.BP, C.One],  [second]  );
                case 0xDF:
                    return new(pos, first, 2, O.rcr, [R.DI, C.One],  [second]  );
                case 0xE0:
                    return new(pos, first, 2, O.shl, [R.AX, C.One],  [second]  );
                case 0xE1:
                    return new(pos, first, 2, O.shl, [R.CX, C.One],  [second]  );
                case 0xE2:
                    return new(pos, first, 2, O.shl, [R.DX, C.One],  [second]  );
                case 0xE3:
                    return new(pos, first, 2, O.shl, [R.BX, C.One],  [second]  );
                case 0xE4:
                    return new(pos, first, 2, O.shl, [R.SP, C.One],  [second]  );
                case 0xE5:
                    return new(pos, first, 2, O.shl, [R.BP, C.One],  [second]  );
                case 0xE7:
                    return new(pos, first, 2, O.shl, [R.DI, C.One],  [second]  );
                case 0xE8:
                    return new(pos, first, 2, O.shr, [R.AX, C.One],  [second]  );
                case 0xE9:
                    return new(pos, first, 2, O.shr, [R.CX, C.One],  [second]  );
                case 0xEB:
                    return new(pos, first, 2, O.shr, [R.BX, C.One],  [second]  );
                case 0xEC:
                    return new(pos, first, 2, O.shr, [R.SP, C.One],  [second]  );
                case 0xED:
                    return new(pos, first, 2, O.shr, [R.BP, C.One],  [second]  );
                case 0xEE:
                    return new(pos, first, 2, O.shr, [R.SI, C.One],  [second]  );
                case 0xEF:
                    return new(pos, first, 2, O.shr, [R.DI, C.One],  [second]  );
                case 0xF8:
                    return new(pos, first, 2, O.sar, [R.AX, C.One],  [second]  );
                case 0xF9:
                    return new(pos, first, 2, O.sar, [R.CX, C.One],  [second]  );
                case 0xFA:
                    return new(pos, first, 2, O.sar, [R.DX, C.One],  [second]  );
                case 0xFB:
                    return new(pos, first, 2, O.sar, [R.BX, C.One],  [second]  );
                case 0xFC:
                    return new(pos, first, 2, O.sar, [R.SP, C.One],  [second]  );
                case 0xFD:
                    return new(pos, first, 2, O.sar, [R.BP, C.One],  [second]  );
                case 0xFE:
                    return new(pos, first, 2, O.sar, [R.SI, C.One],  [second]  );
            }
            return null;
        }
    }
}
