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
    public static class XIntel16
    {
        public static IEnumerable<Instruction> Disassemble(Stream s, byte[] buffer)
        {
            while (s.ReadBytesPos(buffer) is { } pos)
            {
                var first = buffer[0];
                switch (first)
                {
                    case 0x00:
                        continue;
                    case 0x01:
                        continue;
                    case 0x02:
                        continue;
                    case 0x03:
                        continue;
                    case 0x04:
                        continue;
                    case 0x05:
                        continue;
                    case 0x06:
                        yield return new(pos, first, 1, O.push, args: [SegReg.ES]); continue;
                        continue;
                    case 0x07:
                        yield return new(pos, first, 1, O.pop, args: [SegReg.ES]); continue;
                        continue;
                    case 0x08:
                        continue;
                    case 0x09:
                        continue;
                    case 0x0A:
                        continue;
                    case 0x0B:
                        continue;
                    case 0x0C:
                        continue;
                    case 0x0D:
                        continue;
                    case 0x0E:
                        yield return new(pos, first, 1, O.push, args: [SegReg.CS]); continue;
                        continue;
                    case 0x10:
                        continue;
                    case 0x11:
                        continue;
                    case 0x12:
                        continue;
                    case 0x13:
                        continue;
                    case 0x14:
                        continue;
                    case 0x15:
                        continue;
                    case 0x16:
                        yield return new(pos, first, 1, O.push, args: [SegReg.SS]); continue;
                        continue;
                    case 0x17:
                        yield return new(pos, first, 1, O.pop, args: [SegReg.SS]); continue;
                        continue;
                    case 0x18:
                        continue;
                    case 0x19:
                        continue;
                    case 0x1A:
                        continue;
                    case 0x1B:
                        continue;
                    case 0x1C:
                        continue;
                    case 0x1D:
                        continue;
                    case 0x1E:
                        yield return new(pos, first, 1, O.push, args: [SegReg.DS]); continue;
                        continue;
                    case 0x1F:
                        yield return new(pos, first, 1, O.pop, args: [SegReg.DS]); continue;
                        continue;
                    case 0x20:
                        continue;
                    case 0x21:
                        continue;
                    case 0x22:
                        continue;
                    case 0x23:
                        continue;
                    case 0x24:
                        continue;
                    case 0x25:
                        continue;
                    case 0x27:
                        yield return new(pos, first, 1, O.daa, args: []); continue;
                        continue;
                    case 0x28:
                        continue;
                    case 0x29:
                        continue;
                    case 0x2A:
                        continue;
                    case 0x2B:
                        continue;
                    case 0x2C:
                        continue;
                    case 0x2D:
                        continue;
                    case 0x2F:
                        yield return new(pos, first, 1, O.das, args: []); continue;
                        continue;
                    case 0x30:
                        continue;
                    case 0x31:
                        continue;
                    case 0x32:
                        continue;
                    case 0x33:
                        continue;
                    case 0x34:
                        continue;
                    case 0x35:
                        continue;
                    case 0x37:
                        yield return new(pos, first, 1, O.aaa, args: []); continue;
                        continue;
                    case 0x38:
                        continue;
                    case 0x39:
                        continue;
                    case 0x3A:
                        continue;
                    case 0x3B:
                        continue;
                    case 0x3C:
                        continue;
                    case 0x3D:
                        continue;
                    case 0x3F:
                        yield return new(pos, first, 1, O.aas, args: []); continue;
                        continue;
                    case 0x40:
                        yield return new(pos, first, 1, O.inc, args: [GenReg16.AX]); continue;
                        continue;
                    case 0x41:
                        yield return new(pos, first, 1, O.inc, args: [GenReg16.CX]); continue;
                        continue;
                    case 0x42:
                        yield return new(pos, first, 1, O.inc, args: [GenReg16.DX]); continue;
                        continue;
                    case 0x43:
                        yield return new(pos, first, 1, O.inc, args: [GenReg16.BX]); continue;
                        continue;
                    case 0x44:
                        yield return new(pos, first, 1, O.inc, args: [IdxReg.SP]); continue;
                        continue;
                    case 0x45:
                        yield return new(pos, first, 1, O.inc, args: [IdxReg.BP]); continue;
                        continue;
                    case 0x46:
                        yield return new(pos, first, 1, O.inc, args: [IdxReg.SI]); continue;
                        continue;
                    case 0x47:
                        yield return new(pos, first, 1, O.inc, args: [IdxReg.DI]); continue;
                        continue;
                    case 0x48:
                        yield return new(pos, first, 1, O.dec, args: [GenReg16.AX]); continue;
                        continue;
                    case 0x49:
                        yield return new(pos, first, 1, O.dec, args: [GenReg16.CX]); continue;
                        continue;
                    case 0x4A:
                        yield return new(pos, first, 1, O.dec, args: [GenReg16.DX]); continue;
                        continue;
                    case 0x4B:
                        yield return new(pos, first, 1, O.dec, args: [GenReg16.BX]); continue;
                        continue;
                    case 0x4C:
                        yield return new(pos, first, 1, O.dec, args: [IdxReg.SP]); continue;
                        continue;
                    case 0x4D:
                        yield return new(pos, first, 1, O.dec, args: [IdxReg.BP]); continue;
                        continue;
                    case 0x4E:
                        yield return new(pos, first, 1, O.dec, args: [IdxReg.SI]); continue;
                        continue;
                    case 0x4F:
                        yield return new(pos, first, 1, O.dec, args: [IdxReg.DI]); continue;
                        continue;
                    case 0x50:
                        yield return new(pos, first, 1, O.push, args: [GenReg16.AX]); continue;
                        continue;
                    case 0x51:
                        yield return new(pos, first, 1, O.push, args: [GenReg16.CX]); continue;
                        continue;
                    case 0x52:
                        yield return new(pos, first, 1, O.push, args: [GenReg16.DX]); continue;
                        continue;
                    case 0x53:
                        yield return new(pos, first, 1, O.push, args: [GenReg16.BX]); continue;
                        continue;
                    case 0x54:
                        yield return new(pos, first, 1, O.push, args: [IdxReg.SP]); continue;
                        continue;
                    case 0x55:
                        yield return new(pos, first, 1, O.push, args: [IdxReg.BP]); continue;
                        continue;
                    case 0x56:
                        yield return new(pos, first, 1, O.push, args: [IdxReg.SI]); continue;
                        continue;
                    case 0x57:
                        yield return new(pos, first, 1, O.push, args: [IdxReg.DI]); continue;
                        continue;
                    case 0x58:
                        yield return new(pos, first, 1, O.pop, args: [GenReg16.AX]); continue;
                        continue;
                    case 0x59:
                        yield return new(pos, first, 1, O.pop, args: [GenReg16.CX]); continue;
                        continue;
                    case 0x5A:
                        yield return new(pos, first, 1, O.pop, args: [GenReg16.DX]); continue;
                        continue;
                    case 0x5B:
                        yield return new(pos, first, 1, O.pop, args: [GenReg16.BX]); continue;
                        continue;
                    case 0x5C:
                        yield return new(pos, first, 1, O.pop, args: [IdxReg.SP]); continue;
                        continue;
                    case 0x5D:
                        yield return new(pos, first, 1, O.pop, args: [IdxReg.BP]); continue;
                        continue;
                    case 0x5E:
                        yield return new(pos, first, 1, O.pop, args: [IdxReg.SI]); continue;
                        continue;
                    case 0x5F:
                        yield return new(pos, first, 1, O.pop, args: [IdxReg.DI]); continue;
                        continue;
                    case 0x60:
                        yield return new(pos, first, 1, O.pusha, args: []); continue;
                        continue;
                    case 0x61:
                        yield return new(pos, first, 1, O.popa, args: []); continue;
                        continue;
                    case 0x62:
                        continue;
                    case 0x63:
                        continue;
                    case 0x68:
                        continue;
                    case 0x69:
                        continue;
                    case 0x6A:
                        continue;
                    case 0x6B:
                        continue;
                    case 0x6C:
                        yield return new(pos, first, 1, O.insb, args: []); continue;
                        continue;
                    case 0x6D:
                        yield return new(pos, first, 1, O.insw, args: []); continue;
                        continue;
                    case 0x6E:
                        yield return new(pos, first, 1, O.outsb, args: []); continue;
                        continue;
                    case 0x6F:
                        yield return new(pos, first, 1, O.outsw, args: []); continue;
                        continue;
                    case 0x70:
                        continue;
                    case 0x71:
                        continue;
                    case 0x72:
                        continue;
                    case 0x73:
                        continue;
                    case 0x74:
                        continue;
                    case 0x75:
                        continue;
                    case 0x76:
                        continue;
                    case 0x77:
                        continue;
                    case 0x78:
                        continue;
                    case 0x79:
                        continue;
                    case 0x7A:
                        continue;
                    case 0x7B:
                        continue;
                    case 0x7C:
                        continue;
                    case 0x7D:
                        continue;
                    case 0x7E:
                        continue;
                    case 0x7F:
                        continue;
                    case 0x80:
                        continue;
                    case 0x81:
                        continue;
                    case 0x83:
                        continue;
                    case 0x84:
                        continue;
                    case 0x85:
                        continue;
                    case 0x86:
                        continue;
                    case 0x87:
                        continue;
                    case 0x88:
                        continue;
                    case 0x89:
                        continue;
                    case 0x8A:
                        continue;
                    case 0x8B:
                        continue;
                    case 0x8C:
                        continue;
                    case 0x8D:
                        continue;
                    case 0x8E:
                        continue;
                    case 0x8F:
                        continue;
                    case 0x90:
                        yield return new(pos, first, 1, O.nop, args: []); continue;
                        continue;
                    case 0x91:
                        yield return new(pos, first, 1, O.xchg, args: [GenReg16.AX, GenReg16.CX]); continue;
                        continue;
                    case 0x92:
                        yield return new(pos, first, 1, O.xchg, args: [GenReg16.AX, GenReg16.DX]); continue;
                        continue;
                    case 0x93:
                        yield return new(pos, first, 1, O.xchg, args: [GenReg16.AX, GenReg16.BX]); continue;
                        continue;
                    case 0x94:
                        yield return new(pos, first, 1, O.xchg, args: [GenReg16.AX, IdxReg.SP]); continue;
                        continue;
                    case 0x95:
                        yield return new(pos, first, 1, O.xchg, args: [GenReg16.AX, IdxReg.BP]); continue;
                        continue;
                    case 0x96:
                        yield return new(pos, first, 1, O.xchg, args: [GenReg16.AX, IdxReg.SI]); continue;
                        continue;
                    case 0x97:
                        yield return new(pos, first, 1, O.xchg, args: [GenReg16.AX, IdxReg.DI]); continue;
                        continue;
                    case 0x98:
                        yield return new(pos, first, 1, O.cbw, args: []); continue;
                        continue;
                    case 0x99:
                        yield return new(pos, first, 1, O.cwd, args: []); continue;
                        continue;
                    case 0x9A:
                        continue;
                    case 0x9C:
                        yield return new(pos, first, 1, O.pushf, args: []); continue;
                        continue;
                    case 0x9D:
                        yield return new(pos, first, 1, O.popf, args: []); continue;
                        continue;
                    case 0x9E:
                        yield return new(pos, first, 1, O.sahf, args: []); continue;
                        continue;
                    case 0x9F:
                        yield return new(pos, first, 1, O.lahf, args: []); continue;
                        continue;
                    case 0xA0:
                        continue;
                    case 0xA1:
                        continue;
                    case 0xA2:
                        continue;
                    case 0xA3:
                        continue;
                    case 0xA4:
                        yield return new(pos, first, 1, O.movsb, args: []); continue;
                        continue;
                    case 0xA5:
                        yield return new(pos, first, 1, O.movsw, args: []); continue;
                        continue;
                    case 0xA6:
                        yield return new(pos, first, 1, O.cmpsb, args: []); continue;
                        continue;
                    case 0xA7:
                        yield return new(pos, first, 1, O.cmpsw, args: []); continue;
                        continue;
                    case 0xA8:
                        continue;
                    case 0xA9:
                        continue;
                    case 0xAA:
                        yield return new(pos, first, 1, O.stosb, args: []); continue;
                        continue;
                    case 0xAB:
                        yield return new(pos, first, 1, O.stosw, args: []); continue;
                        continue;
                    case 0xAC:
                        yield return new(pos, first, 1, O.lodsb, args: []); continue;
                        continue;
                    case 0xAD:
                        yield return new(pos, first, 1, O.lodsw, args: []); continue;
                        continue;
                    case 0xAE:
                        yield return new(pos, first, 1, O.scasb, args: []); continue;
                        continue;
                    case 0xAF:
                        yield return new(pos, first, 1, O.scasw, args: []); continue;
                        continue;
                    case 0xB0:
                        continue;
                    case 0xB1:
                        continue;
                    case 0xB2:
                        continue;
                    case 0xB3:
                        continue;
                    case 0xB4:
                        continue;
                    case 0xB5:
                        continue;
                    case 0xB6:
                        continue;
                    case 0xB7:
                        continue;
                    case 0xB8:
                        continue;
                    case 0xB9:
                        continue;
                    case 0xBA:
                        continue;
                    case 0xBB:
                        continue;
                    case 0xBC:
                        continue;
                    case 0xBD:
                        continue;
                    case 0xBE:
                        continue;
                    case 0xBF:
                        continue;
                    case 0xC0:
                        continue;
                    case 0xC1:
                        continue;
                    case 0xC2:
                        continue;
                    case 0xC3:
                        yield return new(pos, first, 1, O.ret, args: []); continue;
                        continue;
                    case 0xC4:
                        continue;
                    case 0xC5:
                        continue;
                    case 0xC6:
                        continue;
                    case 0xC7:
                        continue;
                    case 0xC8:
                        continue;
                    case 0xC9:
                        yield return new(pos, first, 1, O.leave, args: []); continue;
                        continue;
                    case 0xCA:
                        continue;
                    case 0xCB:
                        yield return new(pos, first, 1, O.retf, args: []); continue;
                        continue;
                    case 0xCC:
                        yield return new(pos, first, 1, O.int3, args: []); continue;
                        continue;
                    case 0xCD:
                        continue;
                    case 0xCE:
                        yield return new(pos, first, 1, O.into, args: []); continue;
                        continue;
                    case 0xCF:
                        yield return new(pos, first, 1, O.iret, args: []); continue;
                        continue;
                    case 0xD0:
                        continue;
                    case 0xD1:
                        continue;
                    case 0xD2:
                        continue;
                    case 0xD3:
                        continue;
                    case 0xD4:
                        continue;
                    case 0xD5:
                        continue;
                    case 0xD6:
                        yield return new(pos, first, 1, O.salc, args: []); continue;
                        continue;
                    case 0xD7:
                        yield return new(pos, first, 1, O.xlatb, args: []); continue;
                        continue;
                    case 0xD8:
                        continue;
                    case 0xD9:
                        continue;
                    case 0xDA:
                        continue;
                    case 0xDB:
                        continue;
                    case 0xDC:
                        continue;
                    case 0xDD:
                        continue;
                    case 0xDE:
                        continue;
                    case 0xDF:
                        continue;
                    case 0xE0:
                        continue;
                    case 0xE1:
                        continue;
                    case 0xE2:
                        continue;
                    case 0xE3:
                        continue;
                    case 0xE4:
                        continue;
                    case 0xE5:
                        continue;
                    case 0xE6:
                        continue;
                    case 0xE7:
                        continue;
                    case 0xE8:
                        continue;
                    case 0xE9:
                        continue;
                    case 0xEA:
                        continue;
                    case 0xEB:
                        continue;
                    case 0xEC:
                        yield return new(pos, first, 1, O.@in, args: [GenReg8.AL, GenReg16.DX]); continue;
                        continue;
                    case 0xED:
                        yield return new(pos, first, 1, O.@in, args: [GenReg16.AX, GenReg16.DX]); continue;
                        continue;
                    case 0xEE:
                        yield return new(pos, first, 1, O.@out, args: [GenReg16.DX, GenReg8.AL]); continue;
                        continue;
                    case 0xEF:
                        yield return new(pos, first, 1, O.@out, args: [GenReg16.DX, GenReg16.AX]); continue;
                        continue;
                    case 0xF1:
                        yield return new(pos, first, 1, O.int1, args: []); continue;
                        continue;
                    case 0xF4:
                        yield return new(pos, first, 1, O.hlt, args: []); continue;
                        continue;
                    case 0xF5:
                        yield return new(pos, first, 1, O.cmc, args: []); continue;
                        continue;
                    case 0xF6:
                        continue;
                    case 0xF7:
                        continue;
                    case 0xF8:
                        yield return new(pos, first, 1, O.clc, args: []); continue;
                        continue;
                    case 0xF9:
                        yield return new(pos, first, 1, O.stc, args: []); continue;
                        continue;
                    case 0xFA:
                        yield return new(pos, first, 1, O.cli, args: []); continue;
                        continue;
                    case 0xFB:
                        yield return new(pos, first, 1, O.sti, args: []); continue;
                        continue;
                    case 0xFC:
                        yield return new(pos, first, 1, O.cld, args: []); continue;
                        continue;
                    case 0xFD:
                        yield return new(pos, first, 1, O.std, args: []); continue;
                        continue;
                    case 0xFE:
                        continue;
                    case 0xFF:
                        continue;
                }
                throw new InstructionError(pos, first);
            }
            yield break;
        }
    }
}
