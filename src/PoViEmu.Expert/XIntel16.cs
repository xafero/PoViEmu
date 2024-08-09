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
        public static IEnumerable<Instruction> Disassemble(Stream s, byte[] buff, long? start = null)
        {
            while (s.ReadBytesPos(buff, off: start) is { } pos)
            {
                var first = buff[0];
                switch (first)
                {
                    case 0x00:
                        if (Intel16x00.Parse(s, buff, pos, first) is { } x00)
                        {
                            yield return x00;
                            continue;
                        }
                        break;
                    case 0x01:
                        if (Intel16x01.Parse(s, buff, pos, first) is { } x01)
                        {
                            yield return x01;
                            continue;
                        }
                        break;
                    case 0x02:
                        if (Intel16x02.Parse(s, buff, pos, first) is { } x02)
                        {
                            yield return x02;
                            continue;
                        }
                        break;
                    case 0x03:
                        if (Intel16x03.Parse(s, buff, pos, first) is { } x03)
                        {
                            yield return x03;
                            continue;
                        }
                        break;
                    case 0x04:
                        if (Intel16x04.Parse(s, buff, pos, first) is { } x04)
                        {
                            yield return x04;
                            continue;
                        }
                        break;
                    case 0x05:
                        if (Intel16x05.Parse(s, buff, pos, first) is { } x05)
                        {
                            yield return x05;
                            continue;
                        }
                        break;
                    case 0x06:
                        yield return new(pos, first, 1, O.push, [R.ES]);
                        continue;
                    case 0x07:
                        yield return new(pos, first, 1, O.pop, [R.ES]);
                        continue;
                    case 0x08:
                        if (Intel16x08.Parse(s, buff, pos, first) is { } x08)
                        {
                            yield return x08;
                            continue;
                        }
                        break;
                    case 0x09:
                        if (Intel16x09.Parse(s, buff, pos, first) is { } x09)
                        {
                            yield return x09;
                            continue;
                        }
                        break;
                    case 0x0A:
                        if (Intel16x0A.Parse(s, buff, pos, first) is { } x0A)
                        {
                            yield return x0A;
                            continue;
                        }
                        break;
                    case 0x0B:
                        if (Intel16x0B.Parse(s, buff, pos, first) is { } x0B)
                        {
                            yield return x0B;
                            continue;
                        }
                        break;
                    case 0x0C:
                        if (Intel16x0C.Parse(s, buff, pos, first) is { } x0C)
                        {
                            yield return x0C;
                            continue;
                        }
                        break;
                    case 0x0D:
                        if (Intel16x0D.Parse(s, buff, pos, first) is { } x0D)
                        {
                            yield return x0D;
                            continue;
                        }
                        break;
                    case 0x0E:
                        yield return new(pos, first, 1, O.push, [R.CS]);
                        continue;
                    case 0x10:
                        if (Intel16x10.Parse(s, buff, pos, first) is { } x10)
                        {
                            yield return x10;
                            continue;
                        }
                        break;
                    case 0x11:
                        if (Intel16x11.Parse(s, buff, pos, first) is { } x11)
                        {
                            yield return x11;
                            continue;
                        }
                        break;
                    case 0x12:
                        if (Intel16x12.Parse(s, buff, pos, first) is { } x12)
                        {
                            yield return x12;
                            continue;
                        }
                        break;
                    case 0x13:
                        if (Intel16x13.Parse(s, buff, pos, first) is { } x13)
                        {
                            yield return x13;
                            continue;
                        }
                        break;
                    case 0x14:
                        if (Intel16x14.Parse(s, buff, pos, first) is { } x14)
                        {
                            yield return x14;
                            continue;
                        }
                        break;
                    case 0x15:
                        if (Intel16x15.Parse(s, buff, pos, first) is { } x15)
                        {
                            yield return x15;
                            continue;
                        }
                        break;
                    case 0x16:
                        yield return new(pos, first, 1, O.push, [R.SS]);
                        continue;
                    case 0x17:
                        yield return new(pos, first, 1, O.pop, [R.SS]);
                        continue;
                    case 0x18:
                        if (Intel16x18.Parse(s, buff, pos, first) is { } x18)
                        {
                            yield return x18;
                            continue;
                        }
                        break;
                    case 0x19:
                        if (Intel16x19.Parse(s, buff, pos, first) is { } x19)
                        {
                            yield return x19;
                            continue;
                        }
                        break;
                    case 0x1A:
                        if (Intel16x1A.Parse(s, buff, pos, first) is { } x1A)
                        {
                            yield return x1A;
                            continue;
                        }
                        break;
                    case 0x1B:
                        if (Intel16x1B.Parse(s, buff, pos, first) is { } x1B)
                        {
                            yield return x1B;
                            continue;
                        }
                        break;
                    case 0x1C:
                        if (Intel16x1C.Parse(s, buff, pos, first) is { } x1C)
                        {
                            yield return x1C;
                            continue;
                        }
                        break;
                    case 0x1D:
                        if (Intel16x1D.Parse(s, buff, pos, first) is { } x1D)
                        {
                            yield return x1D;
                            continue;
                        }
                        break;
                    case 0x1E:
                        yield return new(pos, first, 1, O.push, [R.DS]);
                        continue;
                    case 0x1F:
                        yield return new(pos, first, 1, O.pop, [R.DS]);
                        continue;
                    case 0x20:
                        if (Intel16x20.Parse(s, buff, pos, first) is { } x20)
                        {
                            yield return x20;
                            continue;
                        }
                        break;
                    case 0x21:
                        if (Intel16x21.Parse(s, buff, pos, first) is { } x21)
                        {
                            yield return x21;
                            continue;
                        }
                        break;
                    case 0x22:
                        if (Intel16x22.Parse(s, buff, pos, first) is { } x22)
                        {
                            yield return x22;
                            continue;
                        }
                        break;
                    case 0x23:
                        if (Intel16x23.Parse(s, buff, pos, first) is { } x23)
                        {
                            yield return x23;
                            continue;
                        }
                        break;
                    case 0x24:
                        if (Intel16x24.Parse(s, buff, pos, first) is { } x24)
                        {
                            yield return x24;
                            continue;
                        }
                        break;
                    case 0x25:
                        if (Intel16x25.Parse(s, buff, pos, first) is { } x25)
                        {
                            yield return x25;
                            continue;
                        }
                        break;
                    case 0x27:
                        yield return new(pos, first, 1, O.daa, []);
                        continue;
                    case 0x28:
                        if (Intel16x28.Parse(s, buff, pos, first) is { } x28)
                        {
                            yield return x28;
                            continue;
                        }
                        break;
                    case 0x29:
                        if (Intel16x29.Parse(s, buff, pos, first) is { } x29)
                        {
                            yield return x29;
                            continue;
                        }
                        break;
                    case 0x2A:
                        if (Intel16x2A.Parse(s, buff, pos, first) is { } x2A)
                        {
                            yield return x2A;
                            continue;
                        }
                        break;
                    case 0x2B:
                        if (Intel16x2B.Parse(s, buff, pos, first) is { } x2B)
                        {
                            yield return x2B;
                            continue;
                        }
                        break;
                    case 0x2C:
                        if (Intel16x2C.Parse(s, buff, pos, first) is { } x2C)
                        {
                            yield return x2C;
                            continue;
                        }
                        break;
                    case 0x2D:
                        if (Intel16x2D.Parse(s, buff, pos, first) is { } x2D)
                        {
                            yield return x2D;
                            continue;
                        }
                        break;
                    case 0x2F:
                        yield return new(pos, first, 1, O.das, []);
                        continue;
                    case 0x30:
                        if (Intel16x30.Parse(s, buff, pos, first) is { } x30)
                        {
                            yield return x30;
                            continue;
                        }
                        break;
                    case 0x31:
                        if (Intel16x31.Parse(s, buff, pos, first) is { } x31)
                        {
                            yield return x31;
                            continue;
                        }
                        break;
                    case 0x32:
                        if (Intel16x32.Parse(s, buff, pos, first) is { } x32)
                        {
                            yield return x32;
                            continue;
                        }
                        break;
                    case 0x33:
                        if (Intel16x33.Parse(s, buff, pos, first) is { } x33)
                        {
                            yield return x33;
                            continue;
                        }
                        break;
                    case 0x34:
                        if (Intel16x34.Parse(s, buff, pos, first) is { } x34)
                        {
                            yield return x34;
                            continue;
                        }
                        break;
                    case 0x35:
                        if (Intel16x35.Parse(s, buff, pos, first) is { } x35)
                        {
                            yield return x35;
                            continue;
                        }
                        break;
                    case 0x37:
                        yield return new(pos, first, 1, O.aaa, []);
                        continue;
                    case 0x38:
                        if (Intel16x38.Parse(s, buff, pos, first) is { } x38)
                        {
                            yield return x38;
                            continue;
                        }
                        break;
                    case 0x39:
                        if (Intel16x39.Parse(s, buff, pos, first) is { } x39)
                        {
                            yield return x39;
                            continue;
                        }
                        break;
                    case 0x3A:
                        if (Intel16x3A.Parse(s, buff, pos, first) is { } x3A)
                        {
                            yield return x3A;
                            continue;
                        }
                        break;
                    case 0x3B:
                        if (Intel16x3B.Parse(s, buff, pos, first) is { } x3B)
                        {
                            yield return x3B;
                            continue;
                        }
                        break;
                    case 0x3C:
                        if (Intel16x3C.Parse(s, buff, pos, first) is { } x3C)
                        {
                            yield return x3C;
                            continue;
                        }
                        break;
                    case 0x3D:
                        if (Intel16x3D.Parse(s, buff, pos, first) is { } x3D)
                        {
                            yield return x3D;
                            continue;
                        }
                        break;
                    case 0x3F:
                        yield return new(pos, first, 1, O.aas, []);
                        continue;
                    case 0x40:
                        yield return new(pos, first, 1, O.inc, [R.AX]);
                        continue;
                    case 0x41:
                        yield return new(pos, first, 1, O.inc, [R.CX]);
                        continue;
                    case 0x42:
                        yield return new(pos, first, 1, O.inc, [R.DX]);
                        continue;
                    case 0x43:
                        yield return new(pos, first, 1, O.inc, [R.BX]);
                        continue;
                    case 0x44:
                        yield return new(pos, first, 1, O.inc, [R.SP]);
                        continue;
                    case 0x45:
                        yield return new(pos, first, 1, O.inc, [R.BP]);
                        continue;
                    case 0x46:
                        yield return new(pos, first, 1, O.inc, [R.SI]);
                        continue;
                    case 0x47:
                        yield return new(pos, first, 1, O.inc, [R.DI]);
                        continue;
                    case 0x48:
                        yield return new(pos, first, 1, O.dec, [R.AX]);
                        continue;
                    case 0x49:
                        yield return new(pos, first, 1, O.dec, [R.CX]);
                        continue;
                    case 0x4A:
                        yield return new(pos, first, 1, O.dec, [R.DX]);
                        continue;
                    case 0x4B:
                        yield return new(pos, first, 1, O.dec, [R.BX]);
                        continue;
                    case 0x4C:
                        yield return new(pos, first, 1, O.dec, [R.SP]);
                        continue;
                    case 0x4D:
                        yield return new(pos, first, 1, O.dec, [R.BP]);
                        continue;
                    case 0x4E:
                        yield return new(pos, first, 1, O.dec, [R.SI]);
                        continue;
                    case 0x4F:
                        yield return new(pos, first, 1, O.dec, [R.DI]);
                        continue;
                    case 0x50:
                        yield return new(pos, first, 1, O.push, [R.AX]);
                        continue;
                    case 0x51:
                        yield return new(pos, first, 1, O.push, [R.CX]);
                        continue;
                    case 0x52:
                        yield return new(pos, first, 1, O.push, [R.DX]);
                        continue;
                    case 0x53:
                        yield return new(pos, first, 1, O.push, [R.BX]);
                        continue;
                    case 0x54:
                        yield return new(pos, first, 1, O.push, [R.SP]);
                        continue;
                    case 0x55:
                        yield return new(pos, first, 1, O.push, [R.BP]);
                        continue;
                    case 0x56:
                        yield return new(pos, first, 1, O.push, [R.SI]);
                        continue;
                    case 0x57:
                        yield return new(pos, first, 1, O.push, [R.DI]);
                        continue;
                    case 0x58:
                        yield return new(pos, first, 1, O.pop, [R.AX]);
                        continue;
                    case 0x59:
                        yield return new(pos, first, 1, O.pop, [R.CX]);
                        continue;
                    case 0x5A:
                        yield return new(pos, first, 1, O.pop, [R.DX]);
                        continue;
                    case 0x5B:
                        yield return new(pos, first, 1, O.pop, [R.BX]);
                        continue;
                    case 0x5C:
                        yield return new(pos, first, 1, O.pop, [R.SP]);
                        continue;
                    case 0x5D:
                        yield return new(pos, first, 1, O.pop, [R.BP]);
                        continue;
                    case 0x5E:
                        yield return new(pos, first, 1, O.pop, [R.SI]);
                        continue;
                    case 0x5F:
                        yield return new(pos, first, 1, O.pop, [R.DI]);
                        continue;
                    case 0x60:
                        yield return new(pos, first, 1, O.pusha, []);
                        continue;
                    case 0x61:
                        yield return new(pos, first, 1, O.popa, []);
                        continue;
                    case 0x62:
                        if (Intel16x62.Parse(s, buff, pos, first) is { } x62)
                        {
                            yield return x62;
                            continue;
                        }
                        break;
                    case 0x63:
                        if (Intel16x63.Parse(s, buff, pos, first) is { } x63)
                        {
                            yield return x63;
                            continue;
                        }
                        break;
                    case 0x68:
                        if (Intel16x68.Parse(s, buff, pos, first) is { } x68)
                        {
                            yield return x68;
                            continue;
                        }
                        break;
                    case 0x69:
                        if (Intel16x69.Parse(s, buff, pos, first) is { } x69)
                        {
                            yield return x69;
                            continue;
                        }
                        break;
                    case 0x6A:
                        if (Intel16x6A.Parse(s, buff, pos, first) is { } x6A)
                        {
                            yield return x6A;
                            continue;
                        }
                        break;
                    case 0x6B:
                        if (Intel16x6B.Parse(s, buff, pos, first) is { } x6B)
                        {
                            yield return x6B;
                            continue;
                        }
                        break;
                    case 0x6C:
                        yield return new(pos, first, 1, O.insb, []);
                        continue;
                    case 0x6D:
                        yield return new(pos, first, 1, O.insw, []);
                        continue;
                    case 0x6E:
                        yield return new(pos, first, 1, O.outsb, []);
                        continue;
                    case 0x6F:
                        yield return new(pos, first, 1, O.outsw, []);
                        continue;
                    case 0x70:
                        if (Intel16x70.Parse(s, buff, pos, first) is { } x70)
                        {
                            yield return x70;
                            continue;
                        }
                        break;
                    case 0x71:
                        if (Intel16x71.Parse(s, buff, pos, first) is { } x71)
                        {
                            yield return x71;
                            continue;
                        }
                        break;
                    case 0x72:
                        if (Intel16x72.Parse(s, buff, pos, first) is { } x72)
                        {
                            yield return x72;
                            continue;
                        }
                        break;
                    case 0x73:
                        if (Intel16x73.Parse(s, buff, pos, first) is { } x73)
                        {
                            yield return x73;
                            continue;
                        }
                        break;
                    case 0x74:
                        if (Intel16x74.Parse(s, buff, pos, first) is { } x74)
                        {
                            yield return x74;
                            continue;
                        }
                        break;
                    case 0x75:
                        if (Intel16x75.Parse(s, buff, pos, first) is { } x75)
                        {
                            yield return x75;
                            continue;
                        }
                        break;
                    case 0x76:
                        if (Intel16x76.Parse(s, buff, pos, first) is { } x76)
                        {
                            yield return x76;
                            continue;
                        }
                        break;
                    case 0x77:
                        if (Intel16x77.Parse(s, buff, pos, first) is { } x77)
                        {
                            yield return x77;
                            continue;
                        }
                        break;
                    case 0x78:
                        if (Intel16x78.Parse(s, buff, pos, first) is { } x78)
                        {
                            yield return x78;
                            continue;
                        }
                        break;
                    case 0x79:
                        if (Intel16x79.Parse(s, buff, pos, first) is { } x79)
                        {
                            yield return x79;
                            continue;
                        }
                        break;
                    case 0x7A:
                        if (Intel16x7A.Parse(s, buff, pos, first) is { } x7A)
                        {
                            yield return x7A;
                            continue;
                        }
                        break;
                    case 0x7B:
                        if (Intel16x7B.Parse(s, buff, pos, first) is { } x7B)
                        {
                            yield return x7B;
                            continue;
                        }
                        break;
                    case 0x7C:
                        if (Intel16x7C.Parse(s, buff, pos, first) is { } x7C)
                        {
                            yield return x7C;
                            continue;
                        }
                        break;
                    case 0x7D:
                        if (Intel16x7D.Parse(s, buff, pos, first) is { } x7D)
                        {
                            yield return x7D;
                            continue;
                        }
                        break;
                    case 0x7E:
                        if (Intel16x7E.Parse(s, buff, pos, first) is { } x7E)
                        {
                            yield return x7E;
                            continue;
                        }
                        break;
                    case 0x7F:
                        if (Intel16x7F.Parse(s, buff, pos, first) is { } x7F)
                        {
                            yield return x7F;
                            continue;
                        }
                        break;
                    case 0x80:
                        if (Intel16x80.Parse(s, buff, pos, first) is { } x80)
                        {
                            yield return x80;
                            continue;
                        }
                        break;
                    case 0x81:
                        if (Intel16x81.Parse(s, buff, pos, first) is { } x81)
                        {
                            yield return x81;
                            continue;
                        }
                        break;
                    case 0x83:
                        if (Intel16x83.Parse(s, buff, pos, first) is { } x83)
                        {
                            yield return x83;
                            continue;
                        }
                        break;
                    case 0x84:
                        if (Intel16x84.Parse(s, buff, pos, first) is { } x84)
                        {
                            yield return x84;
                            continue;
                        }
                        break;
                    case 0x85:
                        if (Intel16x85.Parse(s, buff, pos, first) is { } x85)
                        {
                            yield return x85;
                            continue;
                        }
                        break;
                    case 0x86:
                        if (Intel16x86.Parse(s, buff, pos, first) is { } x86)
                        {
                            yield return x86;
                            continue;
                        }
                        break;
                    case 0x87:
                        if (Intel16x87.Parse(s, buff, pos, first) is { } x87)
                        {
                            yield return x87;
                            continue;
                        }
                        break;
                    case 0x88:
                        if (Intel16x88.Parse(s, buff, pos, first) is { } x88)
                        {
                            yield return x88;
                            continue;
                        }
                        break;
                    case 0x89:
                        if (Intel16x89.Parse(s, buff, pos, first) is { } x89)
                        {
                            yield return x89;
                            continue;
                        }
                        break;
                    case 0x8A:
                        if (Intel16x8A.Parse(s, buff, pos, first) is { } x8A)
                        {
                            yield return x8A;
                            continue;
                        }
                        break;
                    case 0x8B:
                        if (Intel16x8B.Parse(s, buff, pos, first) is { } x8B)
                        {
                            yield return x8B;
                            continue;
                        }
                        break;
                    case 0x8C:
                        if (Intel16x8C.Parse(s, buff, pos, first) is { } x8C)
                        {
                            yield return x8C;
                            continue;
                        }
                        break;
                    case 0x8D:
                        if (Intel16x8D.Parse(s, buff, pos, first) is { } x8D)
                        {
                            yield return x8D;
                            continue;
                        }
                        break;
                    case 0x8E:
                        if (Intel16x8E.Parse(s, buff, pos, first) is { } x8E)
                        {
                            yield return x8E;
                            continue;
                        }
                        break;
                    case 0x8F:
                        if (Intel16x8F.Parse(s, buff, pos, first) is { } x8F)
                        {
                            yield return x8F;
                            continue;
                        }
                        break;
                    case 0x90:
                        yield return new(pos, first, 1, O.nop, []);
                        continue;
                    case 0x91:
                        yield return new(pos, first, 1, O.xchg, [R.AX, R.CX]);
                        continue;
                    case 0x92:
                        yield return new(pos, first, 1, O.xchg, [R.AX, R.DX]);
                        continue;
                    case 0x93:
                        yield return new(pos, first, 1, O.xchg, [R.AX, R.BX]);
                        continue;
                    case 0x94:
                        yield return new(pos, first, 1, O.xchg, [R.AX, R.SP]);
                        continue;
                    case 0x95:
                        yield return new(pos, first, 1, O.xchg, [R.AX, R.BP]);
                        continue;
                    case 0x96:
                        yield return new(pos, first, 1, O.xchg, [R.AX, R.SI]);
                        continue;
                    case 0x97:
                        yield return new(pos, first, 1, O.xchg, [R.AX, R.DI]);
                        continue;
                    case 0x98:
                        yield return new(pos, first, 1, O.cbw, []);
                        continue;
                    case 0x99:
                        yield return new(pos, first, 1, O.cwd, []);
                        continue;
                    case 0x9A:
                        if (Intel16x9A.Parse(s, buff, pos, first) is { } x9A)
                        {
                            yield return x9A;
                            continue;
                        }
                        break;
                    case 0x9C:
                        yield return new(pos, first, 1, O.pushf, []);
                        continue;
                    case 0x9D:
                        yield return new(pos, first, 1, O.popf, []);
                        continue;
                    case 0x9E:
                        yield return new(pos, first, 1, O.sahf, []);
                        continue;
                    case 0x9F:
                        yield return new(pos, first, 1, O.lahf, []);
                        continue;
                    case 0xA0:
                        if (Intel16xA0.Parse(s, buff, pos, first) is { } xA0)
                        {
                            yield return xA0;
                            continue;
                        }
                        break;
                    case 0xA1:
                        if (Intel16xA1.Parse(s, buff, pos, first) is { } xA1)
                        {
                            yield return xA1;
                            continue;
                        }
                        break;
                    case 0xA2:
                        if (Intel16xA2.Parse(s, buff, pos, first) is { } xA2)
                        {
                            yield return xA2;
                            continue;
                        }
                        break;
                    case 0xA3:
                        if (Intel16xA3.Parse(s, buff, pos, first) is { } xA3)
                        {
                            yield return xA3;
                            continue;
                        }
                        break;
                    case 0xA4:
                        yield return new(pos, first, 1, O.movsb, []);
                        continue;
                    case 0xA5:
                        yield return new(pos, first, 1, O.movsw, []);
                        continue;
                    case 0xA6:
                        yield return new(pos, first, 1, O.cmpsb, []);
                        continue;
                    case 0xA7:
                        yield return new(pos, first, 1, O.cmpsw, []);
                        continue;
                    case 0xA8:
                        if (Intel16xA8.Parse(s, buff, pos, first) is { } xA8)
                        {
                            yield return xA8;
                            continue;
                        }
                        break;
                    case 0xA9:
                        if (Intel16xA9.Parse(s, buff, pos, first) is { } xA9)
                        {
                            yield return xA9;
                            continue;
                        }
                        break;
                    case 0xAA:
                        yield return new(pos, first, 1, O.stosb, []);
                        continue;
                    case 0xAB:
                        yield return new(pos, first, 1, O.stosw, []);
                        continue;
                    case 0xAC:
                        yield return new(pos, first, 1, O.lodsb, []);
                        continue;
                    case 0xAD:
                        yield return new(pos, first, 1, O.lodsw, []);
                        continue;
                    case 0xAE:
                        yield return new(pos, first, 1, O.scasb, []);
                        continue;
                    case 0xAF:
                        yield return new(pos, first, 1, O.scasw, []);
                        continue;
                    case 0xB0:
                        if (Intel16xB0.Parse(s, buff, pos, first) is { } xB0)
                        {
                            yield return xB0;
                            continue;
                        }
                        break;
                    case 0xB1:
                        if (Intel16xB1.Parse(s, buff, pos, first) is { } xB1)
                        {
                            yield return xB1;
                            continue;
                        }
                        break;
                    case 0xB2:
                        if (Intel16xB2.Parse(s, buff, pos, first) is { } xB2)
                        {
                            yield return xB2;
                            continue;
                        }
                        break;
                    case 0xB3:
                        if (Intel16xB3.Parse(s, buff, pos, first) is { } xB3)
                        {
                            yield return xB3;
                            continue;
                        }
                        break;
                    case 0xB4:
                        if (Intel16xB4.Parse(s, buff, pos, first) is { } xB4)
                        {
                            yield return xB4;
                            continue;
                        }
                        break;
                    case 0xB5:
                        if (Intel16xB5.Parse(s, buff, pos, first) is { } xB5)
                        {
                            yield return xB5;
                            continue;
                        }
                        break;
                    case 0xB6:
                        if (Intel16xB6.Parse(s, buff, pos, first) is { } xB6)
                        {
                            yield return xB6;
                            continue;
                        }
                        break;
                    case 0xB7:
                        if (Intel16xB7.Parse(s, buff, pos, first) is { } xB7)
                        {
                            yield return xB7;
                            continue;
                        }
                        break;
                    case 0xB8:
                        if (Intel16xB8.Parse(s, buff, pos, first) is { } xB8)
                        {
                            yield return xB8;
                            continue;
                        }
                        break;
                    case 0xB9:
                        if (Intel16xB9.Parse(s, buff, pos, first) is { } xB9)
                        {
                            yield return xB9;
                            continue;
                        }
                        break;
                    case 0xBA:
                        if (Intel16xBA.Parse(s, buff, pos, first) is { } xBA)
                        {
                            yield return xBA;
                            continue;
                        }
                        break;
                    case 0xBB:
                        if (Intel16xBB.Parse(s, buff, pos, first) is { } xBB)
                        {
                            yield return xBB;
                            continue;
                        }
                        break;
                    case 0xBC:
                        if (Intel16xBC.Parse(s, buff, pos, first) is { } xBC)
                        {
                            yield return xBC;
                            continue;
                        }
                        break;
                    case 0xBD:
                        if (Intel16xBD.Parse(s, buff, pos, first) is { } xBD)
                        {
                            yield return xBD;
                            continue;
                        }
                        break;
                    case 0xBE:
                        if (Intel16xBE.Parse(s, buff, pos, first) is { } xBE)
                        {
                            yield return xBE;
                            continue;
                        }
                        break;
                    case 0xBF:
                        if (Intel16xBF.Parse(s, buff, pos, first) is { } xBF)
                        {
                            yield return xBF;
                            continue;
                        }
                        break;
                    case 0xC0:
                        if (Intel16xC0.Parse(s, buff, pos, first) is { } xC0)
                        {
                            yield return xC0;
                            continue;
                        }
                        break;
                    case 0xC1:
                        if (Intel16xC1.Parse(s, buff, pos, first) is { } xC1)
                        {
                            yield return xC1;
                            continue;
                        }
                        break;
                    case 0xC2:
                        if (Intel16xC2.Parse(s, buff, pos, first) is { } xC2)
                        {
                            yield return xC2;
                            continue;
                        }
                        break;
                    case 0xC3:
                        yield return new(pos, first, 1, O.ret, []);
                        continue;
                    case 0xC4:
                        if (Intel16xC4.Parse(s, buff, pos, first) is { } xC4)
                        {
                            yield return xC4;
                            continue;
                        }
                        break;
                    case 0xC5:
                        if (Intel16xC5.Parse(s, buff, pos, first) is { } xC5)
                        {
                            yield return xC5;
                            continue;
                        }
                        break;
                    case 0xC6:
                        if (Intel16xC6.Parse(s, buff, pos, first) is { } xC6)
                        {
                            yield return xC6;
                            continue;
                        }
                        break;
                    case 0xC7:
                        if (Intel16xC7.Parse(s, buff, pos, first) is { } xC7)
                        {
                            yield return xC7;
                            continue;
                        }
                        break;
                    case 0xC8:
                        if (Intel16xC8.Parse(s, buff, pos, first) is { } xC8)
                        {
                            yield return xC8;
                            continue;
                        }
                        break;
                    case 0xC9:
                        yield return new(pos, first, 1, O.leave, []);
                        continue;
                    case 0xCA:
                        if (Intel16xCA.Parse(s, buff, pos, first) is { } xCA)
                        {
                            yield return xCA;
                            continue;
                        }
                        break;
                    case 0xCB:
                        yield return new(pos, first, 1, O.retf, []);
                        continue;
                    case 0xCC:
                        yield return new(pos, first, 1, O.int3, []);
                        continue;
                    case 0xCD:
                        if (Intel16xCD.Parse(s, buff, pos, first) is { } xCD)
                        {
                            yield return xCD;
                            continue;
                        }
                        break;
                    case 0xCE:
                        yield return new(pos, first, 1, O.into, []);
                        continue;
                    case 0xCF:
                        yield return new(pos, first, 1, O.iret, []);
                        continue;
                    case 0xD0:
                        if (Intel16xD0.Parse(s, buff, pos, first) is { } xD0)
                        {
                            yield return xD0;
                            continue;
                        }
                        break;
                    case 0xD1:
                        if (Intel16xD1.Parse(s, buff, pos, first) is { } xD1)
                        {
                            yield return xD1;
                            continue;
                        }
                        break;
                    case 0xD2:
                        if (Intel16xD2.Parse(s, buff, pos, first) is { } xD2)
                        {
                            yield return xD2;
                            continue;
                        }
                        break;
                    case 0xD3:
                        if (Intel16xD3.Parse(s, buff, pos, first) is { } xD3)
                        {
                            yield return xD3;
                            continue;
                        }
                        break;
                    case 0xD4:
                        if (Intel16xD4.Parse(s, buff, pos, first) is { } xD4)
                        {
                            yield return xD4;
                            continue;
                        }
                        break;
                    case 0xD5:
                        if (Intel16xD5.Parse(s, buff, pos, first) is { } xD5)
                        {
                            yield return xD5;
                            continue;
                        }
                        break;
                    case 0xD6:
                        yield return new(pos, first, 1, O.salc, []);
                        continue;
                    case 0xD7:
                        yield return new(pos, first, 1, O.xlatb, []);
                        continue;
                    case 0xD8:
                        if (Intel16xD8.Parse(s, buff, pos, first) is { } xD8)
                        {
                            yield return xD8;
                            continue;
                        }
                        break;
                    case 0xD9:
                        if (Intel16xD9.Parse(s, buff, pos, first) is { } xD9)
                        {
                            yield return xD9;
                            continue;
                        }
                        break;
                    case 0xDA:
                        if (Intel16xDA.Parse(s, buff, pos, first) is { } xDA)
                        {
                            yield return xDA;
                            continue;
                        }
                        break;
                    case 0xDB:
                        if (Intel16xDB.Parse(s, buff, pos, first) is { } xDB)
                        {
                            yield return xDB;
                            continue;
                        }
                        break;
                    case 0xDC:
                        if (Intel16xDC.Parse(s, buff, pos, first) is { } xDC)
                        {
                            yield return xDC;
                            continue;
                        }
                        break;
                    case 0xDD:
                        if (Intel16xDD.Parse(s, buff, pos, first) is { } xDD)
                        {
                            yield return xDD;
                            continue;
                        }
                        break;
                    case 0xDE:
                        if (Intel16xDE.Parse(s, buff, pos, first) is { } xDE)
                        {
                            yield return xDE;
                            continue;
                        }
                        break;
                    case 0xDF:
                        if (Intel16xDF.Parse(s, buff, pos, first) is { } xDF)
                        {
                            yield return xDF;
                            continue;
                        }
                        break;
                    case 0xE0:
                        if (Intel16xE0.Parse(s, buff, pos, first) is { } xE0)
                        {
                            yield return xE0;
                            continue;
                        }
                        break;
                    case 0xE1:
                        if (Intel16xE1.Parse(s, buff, pos, first) is { } xE1)
                        {
                            yield return xE1;
                            continue;
                        }
                        break;
                    case 0xE2:
                        if (Intel16xE2.Parse(s, buff, pos, first) is { } xE2)
                        {
                            yield return xE2;
                            continue;
                        }
                        break;
                    case 0xE3:
                        if (Intel16xE3.Parse(s, buff, pos, first) is { } xE3)
                        {
                            yield return xE3;
                            continue;
                        }
                        break;
                    case 0xE4:
                        if (Intel16xE4.Parse(s, buff, pos, first) is { } xE4)
                        {
                            yield return xE4;
                            continue;
                        }
                        break;
                    case 0xE5:
                        if (Intel16xE5.Parse(s, buff, pos, first) is { } xE5)
                        {
                            yield return xE5;
                            continue;
                        }
                        break;
                    case 0xE6:
                        if (Intel16xE6.Parse(s, buff, pos, first) is { } xE6)
                        {
                            yield return xE6;
                            continue;
                        }
                        break;
                    case 0xE7:
                        if (Intel16xE7.Parse(s, buff, pos, first) is { } xE7)
                        {
                            yield return xE7;
                            continue;
                        }
                        break;
                    case 0xE8:
                        if (Intel16xE8.Parse(s, buff, pos, first) is { } xE8)
                        {
                            yield return xE8;
                            continue;
                        }
                        break;
                    case 0xE9:
                        if (Intel16xE9.Parse(s, buff, pos, first) is { } xE9)
                        {
                            yield return xE9;
                            continue;
                        }
                        break;
                    case 0xEA:
                        if (Intel16xEA.Parse(s, buff, pos, first) is { } xEA)
                        {
                            yield return xEA;
                            continue;
                        }
                        break;
                    case 0xEB:
                        if (Intel16xEB.Parse(s, buff, pos, first) is { } xEB)
                        {
                            yield return xEB;
                            continue;
                        }
                        break;
                    case 0xEC:
                        yield return new(pos, first, 1, O.@in, [R.AL, R.DX]);
                        continue;
                    case 0xED:
                        yield return new(pos, first, 1, O.@in, [R.AX, R.DX]);
                        continue;
                    case 0xEE:
                        yield return new(pos, first, 1, O.@out, [R.DX, R.AL]);
                        continue;
                    case 0xEF:
                        yield return new(pos, first, 1, O.@out, [R.DX, R.AX]);
                        continue;
                    case 0xF1:
                        yield return new(pos, first, 1, O.int1, []);
                        continue;
                    case 0xF4:
                        yield return new(pos, first, 1, O.hlt, []);
                        continue;
                    case 0xF5:
                        yield return new(pos, first, 1, O.cmc, []);
                        continue;
                    case 0xF6:
                        if (Intel16xF6.Parse(s, buff, pos, first) is { } xF6)
                        {
                            yield return xF6;
                            continue;
                        }
                        break;
                    case 0xF7:
                        if (Intel16xF7.Parse(s, buff, pos, first) is { } xF7)
                        {
                            yield return xF7;
                            continue;
                        }
                        break;
                    case 0xF8:
                        yield return new(pos, first, 1, O.clc, []);
                        continue;
                    case 0xF9:
                        yield return new(pos, first, 1, O.stc, []);
                        continue;
                    case 0xFA:
                        yield return new(pos, first, 1, O.cli, []);
                        continue;
                    case 0xFB:
                        yield return new(pos, first, 1, O.sti, []);
                        continue;
                    case 0xFC:
                        yield return new(pos, first, 1, O.cld, []);
                        continue;
                    case 0xFD:
                        yield return new(pos, first, 1, O.std, []);
                        continue;
                    case 0xFE:
                        if (Intel16xFE.Parse(s, buff, pos, first) is { } xFE)
                        {
                            yield return xFE;
                            continue;
                        }
                        break;
                    case 0xFF:
                        if (Intel16xFF.Parse(s, buff, pos, first) is { } xFF)
                        {
                            yield return xFF;
                            continue;
                        }
                        break;
                }
                throw new InstructionError(pos, first);
            }
        }
    }
}
