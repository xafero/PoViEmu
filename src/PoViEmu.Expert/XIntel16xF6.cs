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
    internal static class Intel16xF6
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 3, O.test, [M.@byte.On(R.BX.Plus(R.SI), s.NextByte(buff))]);
                case 0x01:
                    return new(pos, first, 3, O.test, [M.@byte.On(R.BX.Plus(R.DI), s.NextByte(buff))]);
                case 0x02:
                    return new(pos, first, 3, O.test, [M.@byte.On(R.BP.Plus(R.SI), s.NextByte(buff))]);
                case 0x03:
                    return new(pos, first, 3, O.test, [M.@byte.On(R.BP.Plus(R.DI), s.NextByte(buff))]);
                case 0x04:
                    return new(pos, first, 3, O.test, [M.@byte.On(R.SI.Box(), s.NextByte(buff))]);
                case 0x05:
                    return new(pos, first, 3, O.test, [M.@byte.On(R.DI.Box(), s.NextByte(buff))]);
                case 0x07:
                    return new(pos, first, 3, O.test, [M.@byte.On(R.BX.Box(), s.NextByte(buff))]);
                case 0x10:
                    return new(pos, first, 2, O.not, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x11:
                    return new(pos, first, 2, O.not, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x12:
                    return new(pos, first, 2, O.not, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x13:
                    return new(pos, first, 2, O.not, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x14:
                    return new(pos, first, 2, O.not, [M.@byte.On(R.SI.Box())], [second]);
                case 0x15:
                    return new(pos, first, 2, O.not, [M.@byte.On(R.DI.Box())], [second]);
                case 0x16:
                    return new(pos, first, 4, O.not, [M.@byte.On(s.NextShort(buff).Box())]);
                case 0x17:
                    return new(pos, first, 2, O.not, [M.@byte.On(R.BX.Box())], [second]);
                case 0x18:
                    return new(pos, first, 2, O.neg, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x19:
                    return new(pos, first, 2, O.neg, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.neg, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.neg, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.neg, [M.@byte.On(R.SI.Box())], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.neg, [M.@byte.On(R.DI.Box())], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.neg, [M.@byte.On(R.BX.Box())], [second]);
                case 0x20:
                    return new(pos, first, 2, O.mul, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x21:
                    return new(pos, first, 2, O.mul, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x22:
                    return new(pos, first, 2, O.mul, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x23:
                    return new(pos, first, 2, O.mul, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x24:
                    return new(pos, first, 2, O.mul, [M.@byte.On(R.SI.Box())], [second]);
                case 0x25:
                    return new(pos, first, 2, O.mul, [M.@byte.On(R.DI.Box())], [second]);
                case 0x26:
                    return new(pos, first, 4, O.mul, [M.@byte.On(s.NextShort(buff).Box())]);
                case 0x27:
                    return new(pos, first, 2, O.mul, [M.@byte.On(R.BX.Box())], [second]);
                case 0x28:
                    return new(pos, first, 2, O.imul, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x29:
                    return new(pos, first, 2, O.imul, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.imul, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.imul, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.imul, [M.@byte.On(R.SI.Box())], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.imul, [M.@byte.On(R.DI.Box())], [second]);
                case 0x2E:
                    return new(pos, first, 4, O.imul, [M.@byte.On(s.NextShort(buff).Box())]);
                case 0x2F:
                    return new(pos, first, 2, O.imul, [M.@byte.On(R.BX.Box())], [second]);
                case 0x30:
                    return new(pos, first, 2, O.div, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x31:
                    return new(pos, first, 2, O.div, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x32:
                    return new(pos, first, 2, O.div, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x33:
                    return new(pos, first, 2, O.div, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x35:
                    return new(pos, first, 2, O.div, [M.@byte.On(R.DI.Box())], [second]);
                case 0x36:
                    return new(pos, first, 4, O.div, [M.@byte.On(s.NextShort(buff).Box())]);
                case 0x37:
                    return new(pos, first, 2, O.div, [M.@byte.On(R.BX.Box())], [second]);
                case 0x38:
                    return new(pos, first, 2, O.idiv, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x39:
                    return new(pos, first, 2, O.idiv, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.idiv, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.idiv, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.idiv, [M.@byte.On(R.SI.Box())], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.idiv, [M.@byte.On(R.DI.Box())], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.idiv, [M.@byte.On(s.NextShort(buff).Box())]);
                case 0x3F:
                    return new(pos, first, 2, O.idiv, [M.@byte.On(R.BX.Box())], [second]);
                case 0x40:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), s.NextByte(buff)]);
                case 0x41:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff))), s.NextByte(buff)]);
                case 0x42:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff))), s.NextByte(buff)]);
                case 0x43:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), s.NextByte(buff)]);
                case 0x44:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.SI.Plus(s.NextByte(buff)), s.NextByte(buff))]);
                case 0x45:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.DI.Plus(s.NextByte(buff)), s.NextByte(buff))]);
                case 0x46:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.BP.Plus(s.NextByte(buff)), s.NextByte(buff))]);
                case 0x47:
                    return new(pos, first, 4, O.test, [M.@byte.On(R.BX.Plus(s.NextByte(buff)), s.NextByte(buff))]);
                case 0x50:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x51:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x52:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x53:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)))]);
                case 0x54:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x55:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.DI.Plus(s.NextByte(buff)))]);
                case 0x56:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.BP.Plus(s.NextByte(buff)))]);
                case 0x57:
                    return new(pos, first, 3, O.not, [M.@byte.On(R.BX.Plus(s.NextByte(buff)))]);
                case 0x58:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)))]);
                case 0x59:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x5A:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x5B:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x5C:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x5D:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.DI.Plus(s.NextByte(buff)))]);
                case 0x5E:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.BP.Plus(s.NextByte(buff)))]);
                case 0x5F:
                    return new(pos, first, 3, O.neg, [M.@byte.On(R.BX.Plus(s.NextByte(buff)))]);
                case 0x60:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)))]);
                case 0x61:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x62:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x63:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x64:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x65:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.DI.Minus(s.NextByte(buff)))]);
                case 0x66:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.BP.Plus(s.NextByte(buff)))]);
                case 0x67:
                    return new(pos, first, 3, O.mul, [M.@byte.On(R.BX.Minus(s.NextByte(buff)))]);
                case 0x68:
                    return new(pos, first, 3, O.imul, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x69:
                    return new(pos, first, 3, O.imul, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x6A:
                    return new(pos, first, 3, O.imul, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x6C:
                    return new(pos, first, 3, O.imul, [M.@byte.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x6D:
                    return new(pos, first, 3, O.imul, [M.@byte.On(R.DI.Minus(s.NextByte(buff)))]);
                case 0x6E:
                    return new(pos, first, 3, O.imul, [M.@byte.On(R.BP.Plus(s.NextByte(buff)))]);
                case 0x6F:
                    return new(pos, first, 3, O.imul, [M.@byte.On(R.BX.Minus(s.NextByte(buff)))]);
                case 0x70:
                    return new(pos, first, 3, O.div, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x72:
                    return new(pos, first, 3, O.div, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x73:
                    return new(pos, first, 3, O.div, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x74:
                    return new(pos, first, 3, O.div, [M.@byte.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x75:
                    return new(pos, first, 3, O.div, [M.@byte.On(R.DI.Minus(s.NextByte(buff)))]);
                case 0x76:
                    return new(pos, first, 3, O.div, [M.@byte.On(R.BP.Plus(s.NextByte(buff)))]);
                case 0x77:
                    return new(pos, first, 3, O.div, [M.@byte.On(R.BX.Minus(s.NextByte(buff)))]);
                case 0x78:
                    return new(pos, first, 3, O.idiv, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x79:
                    return new(pos, first, 3, O.idiv, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x7B:
                    return new(pos, first, 3, O.idiv, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x7C:
                    return new(pos, first, 3, O.idiv, [M.@byte.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x7D:
                    return new(pos, first, 3, O.idiv, [M.@byte.On(R.DI.Plus(s.NextByte(buff)))]);
                case 0x7E:
                    return new(pos, first, 3, O.idiv, [M.@byte.On(R.BP.Minus(s.NextByte(buff)))]);
                case 0x7F:
                    return new(pos, first, 3, O.idiv, [M.@byte.On(R.BX.Plus(s.NextByte(buff)))]);
                case 0x80:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x81:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x82:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x83:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x84:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.SI.Plus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x85:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.DI.Plus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x86:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.BP.Plus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x87:
                    return new(pos, first, 5, O.test, [M.@byte.On(R.BX.Minus(s.NextShort(buff))), s.NextByte(buff)]);
                case 0x90:
                    return new(pos, first, 4, O.not, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0x91:
                    return new(pos, first, 4, O.not, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0x92:
                    return new(pos, first, 4, O.not, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextShort(buff)))]);
                case 0x93:
                    return new(pos, first, 4, O.not, [M.@byte.On(R.BP.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0x94:
                    return new(pos, first, 4, O.not, [M.@byte.On(R.SI.Minus(s.NextShort(buff)))]);
                case 0x95:
                    return new(pos, first, 4, O.not, [M.@byte.On(R.DI.Minus(s.NextShort(buff)))]);
                case 0x96:
                    return new(pos, first, 4, O.not, [M.@byte.On(R.BP.Plus(s.NextShort(buff)))]);
                case 0x98:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0x99:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0x9A:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0x9B:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0x9C:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.SI.Plus(s.NextShort(buff)))]);
                case 0x9D:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.DI.Plus(s.NextShort(buff)))]);
                case 0x9E:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.BP.Plus(s.NextShort(buff)))]);
                case 0x9F:
                    return new(pos, first, 4, O.neg, [M.@byte.On(R.BX.Plus(s.NextShort(buff)))]);
                case 0xA0:
                    return new(pos, first, 4, O.mul, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0xA1:
                    return new(pos, first, 4, O.mul, [M.@byte.On(R.BX.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0xA2:
                    return new(pos, first, 4, O.mul, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)))]);
                case 0xA4:
                    return new(pos, first, 4, O.mul, [M.@byte.On(R.SI.Minus(s.NextShort(buff)))]);
                case 0xA5:
                    return new(pos, first, 4, O.mul, [M.@byte.On(R.DI.Minus(s.NextShort(buff)))]);
                case 0xA6:
                    return new(pos, first, 4, O.mul, [M.@byte.On(R.BP.Plus(s.NextShort(buff)))]);
                case 0xA8:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.BX.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0xA9:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0xAA:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.BP.Plus(R.SI).Plus(s.NextShort(buff)))]);
                case 0xAB:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0xAC:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.SI.Plus(s.NextShort(buff)))]);
                case 0xAD:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.DI.Minus(s.NextShort(buff)))]);
                case 0xAE:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.BP.Minus(s.NextShort(buff)))]);
                case 0xAF:
                    return new(pos, first, 4, O.imul, [M.@byte.On(R.BX.Minus(s.NextShort(buff)))]);
                case 0xB0:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextShort(buff)))]);
                case 0xB1:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0xB2:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.BP.Plus(R.SI).Minus(s.NextShort(buff)))]);
                case 0xB3:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0xB4:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.SI.Minus(s.NextShort(buff)))]);
                case 0xB5:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.DI.Plus(s.NextShort(buff)))]);
                case 0xB6:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.BP.Minus(s.NextShort(buff)))]);
                case 0xB7:
                    return new(pos, first, 4, O.div, [M.@byte.On(R.BX.Minus(s.NextShort(buff)))]);
                case 0xB8:
                    return new(pos, first, 4, O.idiv, [M.@byte.On(R.BX.Plus(R.SI).Minus(s.NextShort(buff)))]);
                case 0xB9:
                    return new(pos, first, 4, O.idiv, [M.@byte.On(R.BX.Plus(R.DI).Minus(s.NextShort(buff)))]);
                case 0xBB:
                    return new(pos, first, 4, O.idiv, [M.@byte.On(R.BP.Plus(R.DI).Plus(s.NextShort(buff)))]);
                case 0xBD:
                    return new(pos, first, 4, O.idiv, [M.@byte.On(R.DI.Minus(s.NextShort(buff)))]);
                case 0xBE:
                    return new(pos, first, 4, O.idiv, [M.@byte.On(R.BP.Minus(s.NextShort(buff)))]);
                case 0xBF:
                    return new(pos, first, 4, O.idiv, [M.@byte.On(R.BX.Plus(s.NextShort(buff)))]);
                case 0xC0:
                    return new(pos, first, 3, O.test, [R.AL, s.NextByte(buff)]);
                case 0xC1:
                    return new(pos, first, 3, O.test, [R.CL, s.NextByte(buff)]);
                case 0xC2:
                    return new(pos, first, 3, O.test, [R.DL, s.NextByte(buff)]);
                case 0xC3:
                    return new(pos, first, 3, O.test, [R.BL, s.NextByte(buff)]);
                case 0xC5:
                    return new(pos, first, 3, O.test, [R.CH, s.NextByte(buff)]);
                case 0xC6:
                    return new(pos, first, 3, O.test, [R.DH, s.NextByte(buff)]);
                case 0xC7:
                    return new(pos, first, 3, O.test, [R.BH, s.NextByte(buff)]);
                case 0xD0:
                    return new(pos, first, 2, O.not, [R.AL], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.not, [R.CL], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.not, [R.DL], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.not, [R.BL], [second]);
                case 0xD4:
                    return new(pos, first, 2, O.not, [R.AH], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.not, [R.CH], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.not, [R.DH], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.not, [R.BH], [second]);
                case 0xD8:
                    return new(pos, first, 2, O.neg, [R.AL], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.neg, [R.CL], [second]);
                case 0xDA:
                    return new(pos, first, 2, O.neg, [R.DL], [second]);
                case 0xDB:
                    return new(pos, first, 2, O.neg, [R.BL], [second]);
                case 0xDC:
                    return new(pos, first, 2, O.neg, [R.AH], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.neg, [R.CH], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.neg, [R.DH], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.neg, [R.BH], [second]);
                case 0xE1:
                    return new(pos, first, 2, O.mul, [R.CL], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.mul, [R.DL], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.mul, [R.BL], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.mul, [R.AH], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.mul, [R.CH], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.mul, [R.DH], [second]);
                case 0xE7:
                    return new(pos, first, 2, O.mul, [R.BH], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.imul, [R.AL], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.imul, [R.CL], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.imul, [R.DL], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.imul, [R.BL], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.imul, [R.AH], [second]);
                case 0xED:
                    return new(pos, first, 2, O.imul, [R.CH], [second]);
                case 0xEE:
                    return new(pos, first, 2, O.imul, [R.DH], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.imul, [R.BH], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.div, [R.AL], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.div, [R.CL], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.div, [R.DL], [second]);
                case 0xF3:
                    return new(pos, first, 2, O.div, [R.BL], [second]);
                case 0xF4:
                    return new(pos, first, 2, O.div, [R.AH], [second]);
                case 0xF5:
                    return new(pos, first, 2, O.div, [R.CH], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.div, [R.DH], [second]);
                case 0xF7:
                    return new(pos, first, 2, O.div, [R.BH], [second]);
                case 0xF8:
                    return new(pos, first, 2, O.idiv, [R.AL], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.idiv, [R.CL], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.idiv, [R.DL], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.idiv, [R.BL], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.idiv, [R.AH], [second]);
                case 0xFD:
                    return new(pos, first, 2, O.idiv, [R.CH], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.idiv, [R.DH], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.idiv, [R.BH], [second]);
            }
            return null;
        }
    }
}
