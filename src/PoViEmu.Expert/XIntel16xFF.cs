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
                    return new(pos, first, 2, O.inc, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x01:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x02:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BP.Plus(R.SI))], [second]);
                case 0x03:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BP.Plus(R.DI))], [second]);
                case 0x04:
                    return new(pos, first, 2, O.inc, [M.word.On(R.SI.Box())], [second]);
                case 0x05:
                    return new(pos, first, 2, O.inc, [M.word.On(R.DI.Box())], [second]);
                case 0x06:
                case 0x0E:
                case 0x16:
                case 0x2E:
                case 0x36:
                case 0x40:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 0x46:
                case 0x47:
                case 0x48:
                case 0x49:
                case 0x4A:
                case 0x4B:
                case 0x4C:
                case 0x4D:
                case 0x4E:
                case 0x4F:
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x59:
                case 0x5A:
                case 0x5B:
                case 0x5C:
                case 0x5D:
                case 0x5E:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                case 0x66:
                case 0x67:
                case 0x68:
                case 0x6A:
                case 0x6B:
                case 0x6C:
                case 0x6D:
                case 0x6E:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x74:
                case 0x76:
                case 0x77:
                case 0x80:
                case 0x81:
                case 0x82:
                case 0x83:
                case 0x84:
                case 0x85:
                case 0x86:
                case 0x87:
                case 0x88:
                case 0x89:
                case 0x8A:
                case 0x8B:
                case 0x8C:
                case 0x8D:
                case 0x8E:
                case 0x8F:
                case 0x90:
                case 0x91:
                case 0x92:
                case 0x93:
                case 0x94:
                case 0x95:
                case 0x96:
                case 0x98:
                case 0x99:
                case 0x9A:
                case 0x9B:
                case 0x9C:
                case 0x9E:
                case 0x9F:
                case 0xA0:
                case 0xA1:
                case 0xA2:
                case 0xA3:
                case 0xA4:
                case 0xA5:
                case 0xA6:
                case 0xA8:
                case 0xA9:
                case 0xAA:
                case 0xAB:
                case 0xAC:
                case 0xAD:
                case 0xAE:
                case 0xB0:
                case 0xB1:
                case 0xB2:
                case 0xB3:
                case 0xB4:
                case 0xB5:
                case 0xB6:
                case 0xB7:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.inc, [M.word.On(R.BX.Box())], [second]);
                case 0x08:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x09:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BP.Plus(R.SI))], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BP.Plus(R.DI))], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.dec, [M.word.On(R.SI.Box())], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.dec, [M.word.On(R.DI.Box())], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.dec, [M.word.On(R.BX.Box())], [second]);
                case 0x13:
                    return new(pos, first, 2, O.call, [R.BP.Plus(R.DI)], [second]);
                case 0x15:
                    return new(pos, first, 2, O.call, [R.DI.Box()], [second]);
                case 0x17:
                    return new(pos, first, 2, O.call, [R.BX.Box()], [second]);
                case 0x18:
                    return new(pos, first, 2, O.call, [M.far.On(R.BX.Plus(R.SI))], [second]);
                case 0x19:
                    return new(pos, first, 2, O.call, [M.far.On(R.BX.Plus(R.DI))], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.call, [M.far.On(R.BP.Plus(R.DI))], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.call, [M.far.On(R.SI.Box())], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.call, [M.far.On(R.DI.Box())], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.call, [M.far.On(R.BX.Box())], [second]);
                case 0x20:
                    return new(pos, first, 2, O.jmp, [R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.jmp, [R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.jmp, [R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.jmp, [R.BP.Plus(R.DI)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.jmp, [R.SI.Box()], [second]);
                case 0x25:
                    return new(pos, first, 2, O.jmp, [R.DI.Box()], [second]);
                case 0x27:
                    return new(pos, first, 2, O.jmp, [R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BX.Plus(R.SI))], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BP.Plus(R.SI))], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BP.Plus(R.DI))], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.SI.Box())], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.DI.Box())], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.jmp, [M.far.On(R.BX.Box())], [second]);
                case 0x30:
                    return new(pos, first, 2, O.push, [M.word.On(R.BX.Plus(R.SI))], [second]);
                case 0x31:
                    return new(pos, first, 2, O.push, [M.word.On(R.BX.Plus(R.DI))], [second]);
                case 0x32:
                    return new(pos, first, 2, O.push, [M.word.On(R.BP.Plus(R.SI))], [second]);
                case 0x33:
                    return new(pos, first, 2, O.push, [M.word.On(R.BP.Plus(R.DI))], [second]);
                case 0x34:
                    return new(pos, first, 2, O.push, [M.word.On(R.SI.Box())], [second]);
                case 0x35:
                    return new(pos, first, 2, O.push, [M.word.On(R.DI.Box())], [second]);
                case 0x37:
                    return new(pos, first, 2, O.push, [M.word.On(R.BX.Box())], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.inc, [R.AX], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.inc, [R.DX], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.inc, [R.BX], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.inc, [R.SP], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.inc, [R.BP], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.inc, [R.SI], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.inc, [R.DI], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.dec, [R.AX], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.dec, [R.CX], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.dec, [R.DX], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.dec, [R.SP], [second]);
                case 0xCE:
                    return new(pos, first, 2, O.dec, [R.SI], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.dec, [R.DI], [second]);
                case 0xD0:
                    return new(pos, first, 2, O.call, [R.AX], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.call, [R.CX], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.call, [R.DX], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.call, [R.BX], [second]);
                case 0xD4:
                    return new(pos, first, 2, O.call, [R.SP], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.call, [R.BP], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.call, [R.SI], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.call, [R.DI], [second]);
                case 0xE0:
                    return new(pos, first, 2, O.jmp, [R.AX], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.jmp, [R.DX], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.jmp, [R.BX], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.jmp, [R.SP], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.jmp, [R.BP], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.jmp, [R.SI], [second]);
                case 0xE7:
                    return new(pos, first, 2, O.jmp, [R.DI], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.push, [R.AX], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.push, [R.CX], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.push, [R.DX], [second]);
                case 0xF3:
                    return new(pos, first, 2, O.push, [R.BX], [second]);
                case 0xF4:
                    return new(pos, first, 2, O.push, [R.SP], [second]);
                case 0xF5:
                    return new(pos, first, 2, O.push, [R.BP], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.push, [R.SI], [second]);
                case 0xF7:
                    return new(pos, first, 2, O.push, [R.DI], [second]);
            }
            return null;
        }
    }
}
