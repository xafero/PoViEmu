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
    internal static class Intel16xC7
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 4, O.mov, [M.word.On(R.BX.Plus(R.SI), s.NextShort(buff))]);
                case 0x01:
                    return new(pos, first, 4, O.mov, [M.word.On(R.BX.Plus(R.DI), s.NextShort(buff))]);
                case 0x02:
                    return new(pos, first, 4, O.mov, [M.word.On(R.BP.Plus(R.SI), s.NextShort(buff))]);
                case 0x03:
                    return new(pos, first, 4, O.mov, [M.word.On(R.BP.Plus(R.DI), s.NextShort(buff))]);
                case 0x04:
                    return new(pos, first, 4, O.mov, [M.word.On(R.SI.Box(), s.NextShort(buff))]);
                case 0x05:
                    return new(pos, first, 4, O.mov, [M.word.On(R.DI.Box(), s.NextShort(buff))]);
                case 0x06:
                    return new(pos, first, 6, O.mov, [M.word.On(s.NextShort(buff).Box()), s.NextShort(buff)]);
                case 0x07:
                    return new(pos, first, 4, O.mov, [M.word.On(R.BX.Box(), s.NextShort(buff))]);
                case 0x40:
                    return new(pos, first, 5, O.mov, [M.word.On(R.BX.Plus(R.SI).Plus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x41:
                    return new(pos, first, 5, O.mov, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x42:
                    return new(pos, first, 5, O.mov, [M.word.On(R.BP.Plus(R.SI).Minus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x43:
                    return new(pos, first, 5, O.mov, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x44:
                    return new(pos, first, 5, O.mov, [M.word.On(R.SI.Plus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x45:
                    return new(pos, first, 5, O.mov, [M.word.On(R.DI.Plus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x46:
                    return new(pos, first, 5, O.mov, [M.word.On(R.BP.Plus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x47:
                    return new(pos, first, 5, O.mov, [M.word.On(R.BX.Plus(s.NextByte(buff))), s.NextShort(buff)]);
                case 0x80:
                    return new(pos, first, 6, O.mov, [M.word.On(R.BX.Plus(R.SI).Minus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0x81:
                    return new(pos, first, 6, O.mov, [M.word.On(R.BX.Plus(R.DI).Plus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0x82:
                    return new(pos, first, 6, O.mov, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0x83:
                    return new(pos, first, 6, O.mov, [M.word.On(R.BP.Plus(R.DI).Minus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0x84:
                    return new(pos, first, 6, O.mov, [M.word.On(R.SI.Minus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0x85:
                    return new(pos, first, 6, O.mov, [M.word.On(R.DI.Minus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0x86:
                    return new(pos, first, 6, O.mov, [M.word.On(R.BP.Plus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0x87:
                    return new(pos, first, 6, O.mov, [M.word.On(R.BX.Plus(s.NextShort(buff))), s.NextShort(buff)]);
                case 0xC0:
                    return new(pos, first, 4, O.mov, [R.AX, s.NextShort(buff)]);
                case 0xC1:
                    return new(pos, first, 4, O.mov, [R.CX, s.NextShort(buff)]);
                case 0xC2:
                    return new(pos, first, 4, O.mov, [R.DX, s.NextShort(buff)]);
                case 0xC3:
                    return new(pos, first, 4, O.mov, [R.BX, s.NextShort(buff)]);
                case 0xC4:
                    return new(pos, first, 4, O.mov, [R.SP, s.NextShort(buff)]);
                case 0xC5:
                    return new(pos, first, 4, O.mov, [R.BP, s.NextShort(buff)]);
                case 0xC6:
                    return new(pos, first, 4, O.mov, [R.SI, s.NextShort(buff)]);
                case 0xC7:
                    return new(pos, first, 4, O.mov, [R.DI, s.NextShort(buff)]);
            }
            return null;
        }
    }
}
