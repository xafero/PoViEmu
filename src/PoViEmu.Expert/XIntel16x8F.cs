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
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 0x46:
                case 0x47:
                case 0x80:
                case 0x81:
                case 0x82:
                case 0x83:
                case 0x84:
                case 0x85:
                case 0x86:
                    break;
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
