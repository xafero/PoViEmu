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
    internal static class Intel16xFF
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte();
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BX.Plus(R.SI))]);
                case 0x01:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BX.Plus(R.DI))]);
                case 0x02:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BP.Plus(R.SI))]);
                case 0x03:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BP.Plus(R.DI))]);
                case 0x04:
                    return new(pos, first, 2, O.inc, [M.word.On(R.SI.Box())]);
                case 0x05:
                    return new(pos, first, 2, O.inc, [M.word.On(R.DI.Box())]);
                case 0x06:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BX.Box())]);
                case 0x08:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BX.Plus(R.SI))]);
                case 0x09:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BX.Plus(R.DI))]);
                case 0x0A:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BP.Plus(R.SI))]);
                case 0x0B:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BP.Plus(R.DI))]);
                case 0x0C:
                    return new(pos, first, 2, O.dec, [M.word.On(R.SI.Box())]);
                case 0x0D:
                    return new(pos, first, 2, O.dec, [M.word.On(R.DI.Box())]);
                case 0x0E:
                    break;
                case 0x0F:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BX.Box())]);
                case 0x13:
                    return new(pos, first, 2, O.call, [R.BP.Plus(R.DI)]);
                case 0x15:
                    return new(pos, first, 2, O.call, [R.DI.Box()]);
                case 0x16:
                    break;
                case 0x17:
                    return new(pos, first, 2, O.call, [R.BX.Box()]);
                case 0x18:
                    return new(pos, first, 2, O.call, [M.far.On(R.BX.Plus(R.SI))]);
                case 0x19:
                    return new(pos, first, 2, O.call, [M.far.On(R.BX.Plus(R.DI))]);
                case 0x1B:
                    return new(pos, first, 2, O.call, [M.far.On(R.BP.Plus(R.DI))]);
                case 0x1C:
                    return new(pos, first, 2, O.call, [M.far.On(R.SI.Box())]);
                case 0x1D:
                    return new(pos, first, 2, O.call, [M.far.On(R.DI.Box())]);
                case 0x1F:
                    return new(pos, first, 2, O.call, [M.far.On(R.BX.Box())]);
                case 0x20:
                    return new(pos, first, 2, O.jmp, [R.BX.Plus(R.SI)]);
                case 0x21:
                    return new(pos, first, 2, O.jmp, [R.BX.Plus(R.DI)]);
                case 0x22:
                    return new(pos, first, 2, O.jmp, [R.BP.Plus(R.SI)]);
                case 0x23:
                    return new(pos, first, 2, O.jmp, [R.BP.Plus(R.DI)]);
                case 0x24:
                    return new(pos, first, 2, O.jmp, [R.SI.Box()]);
                case 0x25:
                    return new(pos, first, 2, O.jmp, [R.DI.Box()]);
                case 0x27:
                    return new(pos, first, 2, O.jmp, [R.BX.Box()]);
                case 0x28:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BX.Plus(R.SI))]);
                case 0x2A:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BP.Plus(R.SI))]);
                case 0x2B:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BP.Plus(R.DI))]);
                case 0x2C:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.SI.Box())]);
                case 0x2D:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.DI.Box())]);
                case 0x2E:
                    break;
                case 0x2F:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BX.Box())]);
                case 0x30:
                    return new(pos, first, 2, O.push, [M.word.On(R.BX.Plus(R.SI))]);
                case 0x31:
                    return new(pos, first, 2, O.push, [M.word.On(R.BX.Plus(R.DI))]);
                case 0x32:
                    return new(pos, first, 2, O.push, [M.word.On(R.BP.Plus(R.SI))]);
                case 0x33:
                    return new(pos, first, 2, O.push, [M.word.On(R.BP.Plus(R.DI))]);
                case 0x34:
                    return new(pos, first, 2, O.push, [M.word.On(R.SI.Box())]);
                case 0x35:
                    return new(pos, first, 2, O.push, [M.word.On(R.DI.Box())]);
                case 0x36:
                    break;
                case 0x37:
                    return new(pos, first, 2, O.push, [M.word.On(R.BX.Box())]);
                case 0x40:
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
                case 0x48:
                    break;
                case 0x49:
                    break;
                case 0x4A:
                    break;
                case 0x4B:
                    break;
                case 0x4C:
                    break;
                case 0x4D:
                    break;
                case 0x4E:
                    break;
                case 0x4F:
                    break;
                case 0x50:
                    break;
                case 0x51:
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
                case 0x60:
                    break;
                case 0x61:
                    break;
                case 0x62:
                    break;
                case 0x63:
                    break;
                case 0x64:
                    break;
                case 0x65:
                    break;
                case 0x66:
                    break;
                case 0x67:
                    break;
                case 0x68:
                    break;
                case 0x6A:
                    break;
                case 0x6B:
                    break;
                case 0x6C:
                    break;
                case 0x6D:
                    break;
                case 0x6E:
                    break;
                case 0x70:
                    break;
                case 0x71:
                    break;
                case 0x72:
                    break;
                case 0x74:
                    break;
                case 0x76:
                    break;
                case 0x77:
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
                case 0x8A:
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
                case 0x94:
                    break;
                case 0x95:
                    break;
                case 0x96:
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
                case 0x9E:
                    break;
                case 0x9F:
                    break;
                case 0xA0:
                    break;
                case 0xA1:
                    break;
                case 0xA2:
                    break;
                case 0xA3:
                    break;
                case 0xA4:
                    break;
                case 0xA5:
                    break;
                case 0xA6:
                    break;
                case 0xA8:
                    break;
                case 0xA9:
                    break;
                case 0xAA:
                    break;
                case 0xAB:
                    break;
                case 0xAC:
                    break;
                case 0xAD:
                    break;
                case 0xAE:
                    break;
                case 0xB0:
                    break;
                case 0xB1:
                    break;
                case 0xB2:
                    break;
                case 0xB3:
                    break;
                case 0xB4:
                    break;
                case 0xB5:
                    break;
                case 0xB6:
                    break;
                case 0xB7:
                    break;
                case 0xC0:
                    return new(pos, first, 2, O.inc, [R.AX]);
                case 0xC2:
                    return new(pos, first, 2, O.inc, [R.DX]);
                case 0xC3:
                    return new(pos, first, 2, O.inc, [R.BX]);
                case 0xC4:
                    return new(pos, first, 2, O.inc, [R.SP]);
                case 0xC5:
                    return new(pos, first, 2, O.inc, [R.BP]);
                case 0xC6:
                    return new(pos, first, 2, O.inc, [R.SI]);
                case 0xC7:
                    return new(pos, first, 2, O.inc, [R.DI]);
                case 0xC8:
                    return new(pos, first, 2, O.dec, [R.AX]);
                case 0xC9:
                    return new(pos, first, 2, O.dec, [R.CX]);
                case 0xCA:
                    return new(pos, first, 2, O.dec, [R.DX]);
                case 0xCC:
                    return new(pos, first, 2, O.dec, [R.SP]);
                case 0xCE:
                    return new(pos, first, 2, O.dec, [R.SI]);
                case 0xCF:
                    return new(pos, first, 2, O.dec, [R.DI]);
                case 0xD0:
                    return new(pos, first, 2, O.call, [R.AX]);
                case 0xD1:
                    return new(pos, first, 2, O.call, [R.CX]);
                case 0xD2:
                    return new(pos, first, 2, O.call, [R.DX]);
                case 0xD3:
                    return new(pos, first, 2, O.call, [R.BX]);
                case 0xD4:
                    return new(pos, first, 2, O.call, [R.SP]);
                case 0xD5:
                    return new(pos, first, 2, O.call, [R.BP]);
                case 0xD6:
                    return new(pos, first, 2, O.call, [R.SI]);
                case 0xD7:
                    return new(pos, first, 2, O.call, [R.DI]);
                case 0xE0:
                    return new(pos, first, 2, O.jmp, [R.AX]);
                case 0xE2:
                    return new(pos, first, 2, O.jmp, [R.DX]);
                case 0xE3:
                    return new(pos, first, 2, O.jmp, [R.BX]);
                case 0xE4:
                    return new(pos, first, 2, O.jmp, [R.SP]);
                case 0xE5:
                    return new(pos, first, 2, O.jmp, [R.BP]);
                case 0xE6:
                    return new(pos, first, 2, O.jmp, [R.SI]);
                case 0xE7:
                    return new(pos, first, 2, O.jmp, [R.DI]);
                case 0xF0:
                    return new(pos, first, 2, O.push, [R.AX]);
                case 0xF1:
                    return new(pos, first, 2, O.push, [R.CX]);
                case 0xF2:
                    return new(pos, first, 2, O.push, [R.DX]);
                case 0xF3:
                    return new(pos, first, 2, O.push, [R.BX]);
                case 0xF4:
                    return new(pos, first, 2, O.push, [R.SP]);
                case 0xF5:
                    return new(pos, first, 2, O.push, [R.BP]);
                case 0xF6:
                    return new(pos, first, 2, O.push, [R.SI]);
                case 0xF7:
                    return new(pos, first, 2, O.push, [R.DI]);
            }
            return null;
        }
    }
}
