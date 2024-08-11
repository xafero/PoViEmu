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
    internal static class Intel16xF7
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 4, O.test, [M.word.On(R.BX.Plus(R.SI), s.NextShort(buff))], [ second ]);
                case 0x01:
                    return new(pos, first, 4, O.test, [M.word.On(R.BX.Plus(R.DI), s.NextShort(buff))], [ second ]);
                case 0x02:
                    return new(pos, first, 4, O.test, [M.word.On(R.BP.Plus(R.SI), s.NextShort(buff))], [ second ]);
                case 0x03:
                    return new(pos, first, 4, O.test, [M.word.On(R.BP.Plus(R.DI), s.NextShort(buff))], [ second ]);
                case 0x04:
                    return new(pos, first, 4, O.test, [M.word.On(R.SI.Box(), s.NextShort(buff))], [ second ]);
                case 0x05:
                    return new(pos, first, 4, O.test, [M.word.On(R.DI.Box(), s.NextShort(buff))], [ second ]);
                case 0x06:
                    return new(pos, first, 6, O.test, [M.word.On(s.NextShort(buff).Box()), s.NextShort(buff)], [ second ]);
                case 0x07:
                    return new(pos, first, 4, O.test, [M.word.On(R.BX.Box(), s.NextShort(buff))], [ second ]);
                case 0x10:
                    return new(pos, first, 2, O.not, [M.word.On(R.BX.Plus(R.SI))], [second] );
                case 0x11:
                    return new(pos, first, 2, O.not, [M.word.On(R.BX.Plus(R.DI))], [second] );
                case 0x12:
                    return new(pos, first, 2, O.not, [M.word.On(R.BP.Plus(R.SI))], [second] );
                case 0x13:
                    return new(pos, first, 2, O.not, [M.word.On(R.BP.Plus(R.DI))], [second] );
                case 0x14:
                    return new(pos, first, 2, O.not, [M.word.On(R.SI.Box())], [second] );
                case 0x15:
                    return new(pos, first, 2, O.not, [M.word.On(R.DI.Box())], [second] );
                case 0x16:
                    return new(pos, first, 4, O.not, [M.word.On(s.NextShort(buff).Box())], [ second ]);
                case 0x17:
                    return new(pos, first, 2, O.not, [M.word.On(R.BX.Box())], [second] );
                case 0x18:
                    return new(pos, first, 2, O.neg, [M.word.On(R.BX.Plus(R.SI))], [second] );
                case 0x19:
                    return new(pos, first, 2, O.neg, [M.word.On(R.BX.Plus(R.DI))], [second] );
                case 0x1A:
                    return new(pos, first, 2, O.neg, [M.word.On(R.BP.Plus(R.SI))], [second] );
                case 0x1B:
                    return new(pos, first, 2, O.neg, [M.word.On(R.BP.Plus(R.DI))], [second] );
                case 0x1C:
                    return new(pos, first, 2, O.neg, [M.word.On(R.SI.Box())], [second] );
                case 0x1D:
                    return new(pos, first, 2, O.neg, [M.word.On(R.DI.Box())], [second] );
                case 0x1E:
                    return new(pos, first, 4, O.neg, [M.word.On(s.NextShort(buff).Box())], [ second ]);
                case 0x1F:
                    return new(pos, first, 2, O.neg, [M.word.On(R.BX.Box())], [second] );
                case 0x20:
                    return new(pos, first, 2, O.mul, [M.word.On(R.BX.Plus(R.SI))], [second] );
                case 0x21:
                    return new(pos, first, 2, O.mul, [M.word.On(R.BX.Plus(R.DI))], [second] );
                case 0x22:
                    return new(pos, first, 2, O.mul, [M.word.On(R.BP.Plus(R.SI))], [second] );
                case 0x24:
                    return new(pos, first, 2, O.mul, [M.word.On(R.SI.Box())], [second] );
                case 0x25:
                    return new(pos, first, 2, O.mul, [M.word.On(R.DI.Box())], [second] );
                case 0x26:
                    return new(pos, first, 4, O.mul, [M.word.On(s.NextShort(buff).Box())], [ second ]);
                case 0x28:
                    return new(pos, first, 2, O.imul, [M.word.On(R.BX.Plus(R.SI))], [second] );
                case 0x29:
                    return new(pos, first, 2, O.imul, [M.word.On(R.BX.Plus(R.DI))], [second] );
                case 0x2A:
                    return new(pos, first, 2, O.imul, [M.word.On(R.BP.Plus(R.SI))], [second] );
                case 0x2B:
                    return new(pos, first, 2, O.imul, [M.word.On(R.BP.Plus(R.DI))], [second] );
                case 0x2C:
                    return new(pos, first, 2, O.imul, [M.word.On(R.SI.Box())], [second] );
                case 0x2E:
                    return new(pos, first, 4, O.imul, [M.word.On(s.NextShort(buff).Box())], [ second ]);
                case 0x2F:
                    return new(pos, first, 2, O.imul, [M.word.On(R.BX.Box())], [second] );
                case 0x30:
                    return new(pos, first, 2, O.div, [M.word.On(R.BX.Plus(R.SI))], [second] );
                case 0x31:
                    return new(pos, first, 2, O.div, [M.word.On(R.BX.Plus(R.DI))], [second] );
                case 0x33:
                    return new(pos, first, 2, O.div, [M.word.On(R.BP.Plus(R.DI))], [second] );
                case 0x34:
                    return new(pos, first, 2, O.div, [M.word.On(R.SI.Box())], [second] );
                case 0x35:
                    return new(pos, first, 2, O.div, [M.word.On(R.DI.Box())], [second] );
                case 0x36:
                    return new(pos, first, 4, O.div, [M.word.On(s.NextShort(buff).Box())], [ second ]);
                case 0x37:
                    return new(pos, first, 2, O.div, [M.word.On(R.BX.Box())], [second] );
                case 0x38:
                    return new(pos, first, 2, O.idiv, [M.word.On(R.BX.Plus(R.SI))], [second] );
                case 0x3A:
                    return new(pos, first, 2, O.idiv, [M.word.On(R.BP.Plus(R.SI))], [second] );
                case 0x3B:
                    return new(pos, first, 2, O.idiv, [M.word.On(R.BP.Plus(R.DI))], [second] );
                case 0x3D:
                    return new(pos, first, 2, O.idiv, [M.word.On(R.DI.Box())], [second] );
                case 0x3E:
                    return new(pos, first, 4, O.idiv, [M.word.On(s.NextShort(buff).Box())], [ second ]);
                case 0x3F:
                    return new(pos, first, 2, O.idiv, [M.word.On(R.BX.Box())], [second] );
                case 0x40:
                    return new(pos, first, 5, O.test, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), s.NextShort(buff)], [ second ]);
                case 0x41:
                    return new(pos, first, 5, O.test, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), s.NextShort(buff)], [ second ]);
                case 0x42:
                    return new(pos, first, 5, O.test, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff))), s.NextShort(buff)], [ second ]);
                case 0x43:
                    return new(pos, first, 5, O.test, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), s.NextShort(buff)], [ second ]);
                case 0x44:
                    return new(pos, first, 5, O.test, [M.word.On(R.SI.Minus(s.NextByte(buff))), s.NextShort(buff)], [ second ]);
                case 0x50:
                    return new(pos, first, 3, O.not, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x51:
                    return new(pos, first, 3, O.not, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x52:
                    return new(pos, first, 3, O.not, [M.word.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x53:
                    return new(pos, first, 3, O.not, [M.word.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x54:
                    return new(pos, first, 3, O.not, [M.word.On(R.SI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x55:
                    return new(pos, first, 3, O.not, [M.word.On(R.DI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x56:
                    return new(pos, first, 3, O.not, [M.word.On(R.BP.Plus(s.NextByte(buff)))], [ second ]);
                case 0x58:
                    return new(pos, first, 3, O.neg, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x59:
                    return new(pos, first, 3, O.neg, [M.word.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x5A:
                    return new(pos, first, 3, O.neg, [M.word.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x5B:
                    return new(pos, first, 3, O.neg, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x5D:
                    return new(pos, first, 3, O.neg, [M.word.On(R.DI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x5E:
                    return new(pos, first, 3, O.neg, [M.word.On(R.BP.Minus(s.NextByte(buff)))], [ second ]);
                case 0x5F:
                    return new(pos, first, 3, O.neg, [M.word.On(R.BX.Plus(s.NextByte(buff)))], [ second ]);
                case 0x60:
                    return new(pos, first, 3, O.mul, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x61:
                    return new(pos, first, 3, O.mul, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x62:
                    return new(pos, first, 3, O.mul, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x63:
                    return new(pos, first, 3, O.mul, [M.word.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x64:
                    return new(pos, first, 3, O.mul, [M.word.On(R.SI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x65:
                    return new(pos, first, 3, O.mul, [M.word.On(R.DI.Minus(s.NextByte(buff)))], [ second ]);
                case 0x66:
                    return new(pos, first, 3, O.mul, [M.word.On(R.BP.Plus(s.NextByte(buff)))], [ second ]);
                case 0x67:
                    return new(pos, first, 3, O.mul, [M.word.On(R.BX.Plus(s.NextByte(buff)))], [ second ]);
                case 0x68:
                    return new(pos, first, 3, O.imul, [M.word.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x69:
                    return new(pos, first, 3, O.imul, [M.word.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x6A:
                    return new(pos, first, 3, O.imul, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x6B:
                    return new(pos, first, 3, O.imul, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x6C:
                    return new(pos, first, 3, O.imul, [M.word.On(R.SI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x6D:
                    return new(pos, first, 3, O.imul, [M.word.On(R.DI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x6E:
                    return new(pos, first, 3, O.imul, [M.word.On(R.BP.Plus(s.NextByte(buff)))], [ second ]);
                case 0x6F:
                    return new(pos, first, 3, O.imul, [M.word.On(R.BX.Plus(s.NextByte(buff)))], [ second ]);
                case 0x70:
                    return new(pos, first, 3, O.div, [M.word.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x71:
                    return new(pos, first, 3, O.div, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x73:
                    return new(pos, first, 3, O.div, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x74:
                    return new(pos, first, 3, O.div, [M.word.On(R.SI.Minus(s.NextByte(buff)))], [ second ]);
                case 0x75:
                    return new(pos, first, 3, O.div, [M.word.On(R.DI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x76:
                    return new(pos, first, 3, O.div, [M.word.On(R.BP.Plus(s.NextByte(buff)))], [ second ]);
                case 0x78:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x79:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x7A:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))], [ second ]);
                case 0x7B:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.BP.Plus(R.DI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x7C:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.SI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x7D:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.DI.Plus(s.NextByte(buff)))], [ second ]);
                case 0x7E:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.BP.Plus(s.NextByte(buff)))], [ second ]);
                case 0x7F:
                    return new(pos, first, 3, O.idiv, [M.word.On(R.BX.Plus(s.NextByte(buff)))], [ second ]);
                case 0x80:
                    return new(pos, first, 6, O.test, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff))), s.NextShort(buff)], [ second ]);
                case 0x81:
                    return new(pos, first, 6, O.test, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff))), s.NextShort(buff)], [ second ]);
                case 0x83:
                    return new(pos, first, 6, O.test, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff))), s.NextShort(buff)], [ second ]);
                case 0x84:
                    return new(pos, first, 6, O.test, [M.word.On(R.SI.Signed(s.NextShort(buff))), s.NextShort(buff)], [ second ]);
                case 0x85:
                    return new(pos, first, 6, O.test, [M.word.On(R.DI.Signed(s.NextShort(buff))), s.NextShort(buff)], [ second ]);
                case 0x86:
                    return new(pos, first, 6, O.test, [M.word.On(R.BP.Signed(s.NextShort(buff))), s.NextShort(buff)], [ second ]);
                case 0x87:
                    return new(pos, first, 6, O.test, [M.word.On(R.BX.Signed(s.NextShort(buff))), s.NextShort(buff)], [ second ]);
                case 0x90:
                    return new(pos, first, 4, O.not, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0x91:
                    return new(pos, first, 4, O.not, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0x92:
                    return new(pos, first, 4, O.not, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0x93:
                    return new(pos, first, 4, O.not, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0x94:
                    return new(pos, first, 4, O.not, [M.word.On(R.SI.Signed(s.NextShort(buff)))], [ second ]);
                case 0x95:
                    return new(pos, first, 4, O.not, [M.word.On(R.DI.Signed(s.NextShort(buff)))], [ second ]);
                case 0x96:
                    return new(pos, first, 4, O.not, [M.word.On(R.BP.Signed(s.NextShort(buff)))], [ second ]);
                case 0x98:
                    return new(pos, first, 4, O.neg, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0x99:
                    return new(pos, first, 4, O.neg, [M.word.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)))], [ second ]);
                case 0x9A:
                    return new(pos, first, 4, O.neg, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0x9B:
                    return new(pos, first, 4, O.neg, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0x9C:
                    return new(pos, first, 4, O.neg, [M.word.On(R.SI.Signed(s.NextShort(buff)))], [ second ]);
                case 0x9D:
                    return new(pos, first, 4, O.neg, [M.word.On(R.DI.Signed(s.NextShort(buff)))], [ second ]);
                case 0x9E:
                    return new(pos, first, 4, O.neg, [M.word.On(R.BP.Signed(s.NextShort(buff)))], [ second ]);
                case 0x9F:
                    return new(pos, first, 4, O.neg, [M.word.On(R.BX.Signed(s.NextShort(buff)))], [ second ]);
                case 0xA0:
                    return new(pos, first, 4, O.mul, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xA1:
                    return new(pos, first, 4, O.mul, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xA2:
                    return new(pos, first, 4, O.mul, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xA3:
                    return new(pos, first, 4, O.mul, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xA4:
                    return new(pos, first, 4, O.mul, [M.word.On(R.SI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xA5:
                    return new(pos, first, 4, O.mul, [M.word.On(R.DI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xA6:
                    return new(pos, first, 4, O.mul, [M.word.On(R.BP.Signed(s.NextShort(buff)))], [ second ]);
                case 0xA7:
                    return new(pos, first, 4, O.mul, [M.word.On(R.BX.Signed(s.NextShort(buff)))], [ second ]);
                case 0xA8:
                    return new(pos, first, 4, O.imul, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xA9:
                    return new(pos, first, 4, O.imul, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xAA:
                    return new(pos, first, 4, O.imul, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xAB:
                    return new(pos, first, 4, O.imul, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xAC:
                    return new(pos, first, 4, O.imul, [M.word.On(R.SI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xAD:
                    return new(pos, first, 4, O.imul, [M.word.On(R.DI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xAE:
                    return new(pos, first, 4, O.imul, [M.word.On(R.BP.Signed(s.NextShort(buff)))], [ second ]);
                case 0xAF:
                    return new(pos, first, 4, O.imul, [M.word.On(R.BX.Signed(s.NextShort(buff)))], [ second ]);
                case 0xB0:
                    return new(pos, first, 4, O.div, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xB2:
                    return new(pos, first, 4, O.div, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xB3:
                    return new(pos, first, 4, O.div, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xB4:
                    return new(pos, first, 4, O.div, [M.word.On(R.SI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xB5:
                    return new(pos, first, 4, O.div, [M.word.On(R.DI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xB6:
                    return new(pos, first, 4, O.div, [M.word.On(R.BP.Signed(s.NextShort(buff)))], [ second ]);
                case 0xB7:
                    return new(pos, first, 4, O.div, [M.word.On(R.BX.Signed(s.NextShort(buff)))], [ second ]);
                case 0xB8:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xB9:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xBA:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xBB:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))], [ second ]);
                case 0xBC:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.SI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xBD:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.DI.Signed(s.NextShort(buff)))], [ second ]);
                case 0xBE:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.BP.Signed(s.NextShort(buff)))], [ second ]);
                case 0xBF:
                    return new(pos, first, 4, O.idiv, [M.word.On(R.BX.Signed(s.NextShort(buff)))], [ second ]);
                case 0xC0:
                    return new(pos, first, 4, O.test, [R.AX, s.NextShort(buff)], [ second ]);
                case 0xC1:
                    return new(pos, first, 4, O.test, [R.CX, s.NextShort(buff)], [ second ]);
                case 0xC3:
                    return new(pos, first, 4, O.test, [R.BX, s.NextShort(buff)], [ second ]);
                case 0xC4:
                    return new(pos, first, 4, O.test, [R.SP, s.NextShort(buff)], [ second ]);
                case 0xC5:
                    return new(pos, first, 4, O.test, [R.BP, s.NextShort(buff)], [ second ]);
                case 0xC6:
                    return new(pos, first, 4, O.test, [R.SI, s.NextShort(buff)], [ second ]);
                case 0xC7:
                    return new(pos, first, 4, O.test, [R.DI, s.NextShort(buff)], [ second ]);
                case 0xD0:
                    return new(pos, first, 2, O.not, [R.AX], [second] );
                case 0xD1:
                    return new(pos, first, 2, O.not, [R.CX], [second] );
                case 0xD2:
                    return new(pos, first, 2, O.not, [R.DX], [second] );
                case 0xD3:
                    return new(pos, first, 2, O.not, [R.BX], [second] );
                case 0xD4:
                    return new(pos, first, 2, O.not, [R.SP], [second] );
                case 0xD5:
                    return new(pos, first, 2, O.not, [R.BP], [second] );
                case 0xD6:
                    return new(pos, first, 2, O.not, [R.SI], [second] );
                case 0xD7:
                    return new(pos, first, 2, O.not, [R.DI], [second] );
                case 0xD8:
                    return new(pos, first, 2, O.neg, [R.AX], [second] );
                case 0xD9:
                    return new(pos, first, 2, O.neg, [R.CX], [second] );
                case 0xDA:
                    return new(pos, first, 2, O.neg, [R.DX], [second] );
                case 0xDB:
                    return new(pos, first, 2, O.neg, [R.BX], [second] );
                case 0xDC:
                    return new(pos, first, 2, O.neg, [R.SP], [second] );
                case 0xDD:
                    return new(pos, first, 2, O.neg, [R.BP], [second] );
                case 0xDE:
                    return new(pos, first, 2, O.neg, [R.SI], [second] );
                case 0xDF:
                    return new(pos, first, 2, O.neg, [R.DI], [second] );
                case 0xE0:
                    return new(pos, first, 2, O.mul, [R.AX], [second] );
                case 0xE1:
                    return new(pos, first, 2, O.mul, [R.CX], [second] );
                case 0xE2:
                    return new(pos, first, 2, O.mul, [R.DX], [second] );
                case 0xE3:
                    return new(pos, first, 2, O.mul, [R.BX], [second] );
                case 0xE4:
                    return new(pos, first, 2, O.mul, [R.SP], [second] );
                case 0xE5:
                    return new(pos, first, 2, O.mul, [R.BP], [second] );
                case 0xE6:
                    return new(pos, first, 2, O.mul, [R.SI], [second] );
                case 0xE8:
                    return new(pos, first, 2, O.imul, [R.AX], [second] );
                case 0xE9:
                    return new(pos, first, 2, O.imul, [R.CX], [second] );
                case 0xEA:
                    return new(pos, first, 2, O.imul, [R.DX], [second] );
                case 0xEB:
                    return new(pos, first, 2, O.imul, [R.BX], [second] );
                case 0xEC:
                    return new(pos, first, 2, O.imul, [R.SP], [second] );
                case 0xED:
                    return new(pos, first, 2, O.imul, [R.BP], [second] );
                case 0xEE:
                    return new(pos, first, 2, O.imul, [R.SI], [second] );
                case 0xEF:
                    return new(pos, first, 2, O.imul, [R.DI], [second] );
                case 0xF0:
                    return new(pos, first, 2, O.div, [R.AX], [second] );
                case 0xF1:
                    return new(pos, first, 2, O.div, [R.CX], [second] );
                case 0xF2:
                    return new(pos, first, 2, O.div, [R.DX], [second] );
                case 0xF3:
                    return new(pos, first, 2, O.div, [R.BX], [second] );
                case 0xF4:
                    return new(pos, first, 2, O.div, [R.SP], [second] );
                case 0xF5:
                    return new(pos, first, 2, O.div, [R.BP], [second] );
                case 0xF6:
                    return new(pos, first, 2, O.div, [R.SI], [second] );
                case 0xF7:
                    return new(pos, first, 2, O.div, [R.DI], [second] );
                case 0xF8:
                    return new(pos, first, 2, O.idiv, [R.AX], [second] );
                case 0xF9:
                    return new(pos, first, 2, O.idiv, [R.CX], [second] );
                case 0xFA:
                    return new(pos, first, 2, O.idiv, [R.DX], [second] );
                case 0xFB:
                    return new(pos, first, 2, O.idiv, [R.BX], [second] );
                case 0xFC:
                    return new(pos, first, 2, O.idiv, [R.SP], [second] );
                case 0xFD:
                    return new(pos, first, 2, O.idiv, [R.BP], [second] );
                case 0xFE:
                    return new(pos, first, 2, O.idiv, [R.SI], [second] );
                case 0xFF:
                    return new(pos, first, 2, O.idiv, [R.DI], [second] );
            }
            return null;
        }
    }
}
