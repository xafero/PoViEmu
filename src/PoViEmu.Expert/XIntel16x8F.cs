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
    internal static class Intel16x8F
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x01:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x02:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BP.Plus(R.SI))], [second]);
                case 0x04:
                    return new(pos, first, 2, O.pop, [M.word.On(R.SI.Box())], [second]);
                case 0x06:
                    return new(pos, first, 4, O.pop, [M.word.On(s.NextShort(buff).Box())]);
                case 0x40:
                    return new(pos, first, 3, O.pop, [M.word.On(R.BX.Plus(R.SI).Minus(s.NextByte(buff)))]);
                case 0x41:
                    return new(pos, first, 3, O.pop, [M.word.On(R.BX.Plus(R.DI).Minus(s.NextByte(buff)))]);
                case 0x42:
                    return new(pos, first, 3, O.pop, [M.word.On(R.BP.Plus(R.SI).Plus(s.NextByte(buff)))]);
                case 0x43:
                    return new(pos, first, 3, O.pop, [M.word.On(R.BP.Plus(R.DI).Plus(s.NextByte(buff)))]);
                case 0x44:
                    return new(pos, first, 3, O.pop, [M.word.On(R.SI.Plus(s.NextByte(buff)))]);
                case 0x45:
                    return new(pos, first, 3, O.pop, [M.word.On(R.DI.Minus(s.NextByte(buff)))]);
                case 0x46:
                    return new(pos, first, 3, O.pop, [M.word.On(R.BP.Plus(s.NextByte(buff)))]);
                case 0x47:
                    return new(pos, first, 3, O.pop, [M.word.On(R.BX.Plus(s.NextByte(buff)))]);
                case 0x80:
                    return new(pos, first, 4, O.pop, [M.word.On(R.BX.Plus(R.SI).Signed(s.NextShort(buff)))]);
                case 0x81:
                    return new(pos, first, 4, O.pop, [M.word.On(R.BX.Plus(R.DI).Signed(s.NextShort(buff)))]);
                case 0x82:
                    return new(pos, first, 4, O.pop, [M.word.On(R.BP.Plus(R.SI).Signed(s.NextShort(buff)))]);
                case 0x83:
                    return new(pos, first, 4, O.pop, [M.word.On(R.BP.Plus(R.DI).Signed(s.NextShort(buff)))]);
                case 0x84:
                    return new(pos, first, 4, O.pop, [M.word.On(R.SI.Signed(s.NextShort(buff)))]);
                case 0x85:
                    return new(pos, first, 4, O.pop, [M.word.On(R.DI.Signed(s.NextShort(buff)))]);
                case 0x86:
                    return new(pos, first, 4, O.pop, [M.word.On(R.BP.Signed(s.NextShort(buff)))]);
                case 0x07:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BX.Box())], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.pop, [R.AX], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.pop, [R.CX], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.pop, [R.DX], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.pop, [R.SP], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.pop, [R.BP], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.pop, [R.SI], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.pop, [R.DI], [second]);
            }
            return null;
        }
    }
}
