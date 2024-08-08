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
            var second = s.NextByte();
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BX.Plus(R.SI))]);
                case 0x01:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BX.Plus(R.DI))]);
                case 0x02:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BP.Plus(R.SI))]);
                case 0x04:
                    return new(pos, first, 2, O.pop, [M.word.On(R.SI.Box())]);
                case 0x06:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.pop, [M.word.On(R.BX.Box())]);
                case 0x40:
                    break;
                case 0x41:
                    break;
                case 0x42:
                    break;
                case 0x43:
                    break;
                case 0x44:
                    break;
                case 0x45:
                    break;
                case 0x46:
                    break;
                case 0x47:
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
                case 0xC0:
                    return new(pos, first, 2, O.pop, [R.AX]);
                case 0xC1:
                    return new(pos, first, 2, O.pop, [R.CX]);
                case 0xC2:
                    return new(pos, first, 2, O.pop, [R.DX]);
                case 0xC4:
                    return new(pos, first, 2, O.pop, [R.SP]);
                case 0xC5:
                    return new(pos, first, 2, O.pop, [R.BP]);
                case 0xC6:
                    return new(pos, first, 2, O.pop, [R.SI]);
                case 0xC7:
                    return new(pos, first, 2, O.pop, [R.DI]);
            }
            return null;
        }
    }
}
