// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Expert;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;

namespace PoViEmu.X86Decoding
{
    public static class Intel16
    {
        public static IEnumerable<Instruction> Disassemble(Stream s, byte[] buff, long? start = null, bool err = true)
        {
            while (s.ReadBytesPos(buff, skip: start) is { } pos)
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
                        var add05 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.add, [R.AX, add05]);
                        continue;
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
                        yield return new(pos, first, 2, O.or, [R.AL, s.NextByte(buff)]);
                        continue;
                    case 0x0D:
                        var or0D = s.NextShort(buff);
                        yield return new(pos, first, 3, O.or, [R.AX, or0D]);
                        continue;
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
                        var sbb1C = s.NextByte(buff);
                        yield return new(pos, first, 2, O.sbb, [R.AL, sbb1C]);
                        continue;
                    case 0x1D:
                        var sbb1D = s.NextShort(buff);
                        yield return new(pos, first, 3, O.sbb, [R.AX, sbb1D]);
                        continue;
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
                        var and24 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.and, [R.AL, and24]);
                        continue;
                    case 0x25:
                        var and25 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.and, [R.AX, and25]);
                        continue;
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
                        var sub2C = s.NextByte(buff);
                        yield return new(pos, first, 2, O.sub, [R.AL, sub2C]);
                        continue;
                    case 0x2D:
                        var sub2D = s.NextShort(buff);
                        yield return new(pos, first, 3, O.sub, [R.AX, sub2D]);
                        continue;
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
                        var xor34 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.xor, [R.AL, xor34]);
                        continue;
                    case 0x35:
                        var xor35 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.xor, [R.AX, xor35]);
                        continue;
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
                        var cmp3C = s.NextByte(buff);
                        yield return new(pos, first, 2, O.cmp, [R.AL, cmp3C]);
                        continue;
                    case 0x3D:
                        var cmp3D = s.NextShort(buff);
                        yield return new(pos, first, 3, O.cmp, [R.AX, cmp3D]);
                        continue;
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
                        var pos68 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.push, [Modifier.word.On(pos68)]);
                        continue;
                    case 0x69:
                        if (Intel16x69.Parse(s, buff, pos, first) is { } x69)
                        {
                            yield return x69;
                            continue;
                        }
                        break;
                    case 0x6A:
                        var push6A = s.NextByte(buff);
                        yield return new(pos, first, 2, O.push, [push6A.SignBit()]);
                        continue;
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
                        var jo70 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jo, [jo70.Skip()]);
                        continue;
                    case 0x71:
                        var jno71 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jno, [jno71.Skip()]);
                        continue;
                    case 0x72:
                        var jc72 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jc, [jc72.Skip()]);
                        continue;
                    case 0x73:
                        var jnc73 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jnc, [jnc73.Skip()]);
                        continue;
                    case 0x74:
                        var jz74 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jz, [jz74.Skip()]);
                        continue;
                    case 0x75:
                        var jnz75 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jnz, [jnz75.Skip()]);
                        continue;
                    case 0x76:
                        var jna76 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jna, [jna76.Skip()]);
                        continue;
                    case 0x77:
                        var ja77 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.ja, [ja77.Skip()]);
                        continue;
                    case 0x78:
                        var js78 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.js, [js78.Skip()]);
                        continue;
                    case 0x79:
                        var jns79 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jns, [jns79.Skip()]);
                        continue;
                    case 0x7A:
                        var jpe7A = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jpe, [jpe7A.Skip()]);
                        continue;
                    case 0x7B:
                        var jpo7B = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jpo, [jpo7B.Skip()]);
                        continue;
                    case 0x7C:
                        var jl7C = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jl, [jl7C.Skip()]);
                        continue;
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
                        var jg7F = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jg, [jg7F.Skip()]);
                        continue;
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
                        var call9A = s.NextShort(buff);
                        var call9B = s.NextShort(buff);
                        yield return new(pos, first, 5, O.call, [call9A.ToMem(call9B)]);
                        continue;
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
                        var movA0 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.mov, [R.AL, movA0.Box()]);
                        continue;
                    case 0xA1:
                        var movA1 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.mov, [R.AX, movA1.Box()]);
                        continue;
                    case 0xA2:
                        var movA2 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.mov, [movA2.Box(), R.AL]);
                        continue;
                    case 0xA3:
                        var movA3 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.mov, [movA3.Box(), R.AX]);
                        continue;
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
                        yield return new(pos, first, 2, O.test, [R.AL, s.NextByte(buff)]);
                        continue;
                    case 0xA9:
                        var testA9 = s.NextShort(buff);
                        yield return new(pos, first, 3, O.test, [R.AX, testA9]);
                        continue;
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
                        yield return new(pos, first, 2, O.mov, [R.AL, s.NextByte(buff)]);
                        continue;
                    case 0xB1:
                        yield return new(pos, first, 2, O.mov, [R.CL, s.NextByte(buff)]);
                        continue;
                    case 0xB2:
                        yield return new(pos, first, 2, O.mov, [R.DL, s.NextByte(buff)]);
                        continue;
                    case 0xB3:
                        yield return new(pos, first, 2, O.mov, [R.BL, s.NextByte(buff)]);
                        continue;
                    case 0xB4:
                        yield return new(pos, first, 2, O.mov, [R.AH, s.NextByte(buff)]);
                        continue;
                    case 0xB5:
                        yield return new(pos, first, 2, O.mov, [R.CH, s.NextByte(buff)]);
                        continue;
                    case 0xB6:
                        yield return new(pos, first, 2, O.mov, [R.DH, s.NextByte(buff)]);
                        continue;
                    case 0xB7:
                        yield return new(pos, first, 2, O.mov, [R.BH, s.NextByte(buff)]);
                        continue;
                    case 0xB8:
                        yield return new(pos, first, 3, O.mov, [R.AX, s.NextShort(buff)]);
                        continue;
                    case 0xB9:
                        yield return new(pos, first, 3, O.mov, [R.CX, s.NextShort(buff)]);
                        continue;
                    case 0xBA:
                        yield return new(pos, first, 3, O.mov, [R.DX, s.NextShort(buff)]);
                        continue;
                    case 0xBB:
                        yield return new(pos, first, 3, O.mov, [R.BX, s.NextShort(buff)]);
                        continue;
                    case 0xBC:
                        yield return new(pos, first, 3, O.mov, [R.SP, s.NextShort(buff)]);
                        continue;
                    case 0xBD:
                        yield return new(pos, first, 3, O.mov, [R.BP, s.NextShort(buff)]);
                        continue;
                    case 0xBE:
                        yield return new(pos, first, 3, O.mov, [R.SI, s.NextShort(buff)]);
                        continue;
                    case 0xBF:
                        yield return new(pos, first, 3, O.mov, [R.DI, s.NextShort(buff)]);
                        continue;
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
                        var retA = s.NextShort(buff);
                        yield return new(pos, first, 3, O.ret, [retA]);
                        continue;
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
                        var enterA = s.NextShort(buff);
                        var enterB = s.NextByte(buff);
                        yield return new(pos, first, 4, O.enter, [enterA, enterB]);
                        continue;
                    case 0xC9:
                        yield return new(pos, first, 1, O.leave, []);
                        continue;
                    case 0xCA:
                        var retfA = s.NextShort(buff);
                        yield return new(pos, first, 3, O.retf, [retfA]);
                        continue;
                    case 0xCB:
                        yield return new(pos, first, 1, O.retf, []);
                        continue;
                    case 0xCC:
                        yield return new(pos, first, 1, O.int3, []);
                        continue;
                    case 0xCD:
                        yield return new(pos, first, 2, O.@int, [s.NextByte(buff)]);
                        continue;
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
                        var aamD4 = s.NextByte(buff);
                        if (aamD4 == 0x0A)
                            yield return new(pos, first, 2, O.aam, [], [aamD4]);
                        else
                            yield return new(pos, first, 2, O.aam, [aamD4]);
                        continue;
                    case 0xD5:
                        var aadD5 = s.NextByte(buff);
                        if (aadD5 == 0x0A)
                            yield return new(pos, first, 2, O.aad, [], [aadD5]);
                        else
                            yield return new(pos, first, 2, O.aad, [aadD5]);
                        continue;
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
                        var loopneE0 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.loopne, [loopneE0.Skip()]);
                        continue;
                    case 0xE1:
                        var loopeE1 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.loope, [loopeE1.Skip()]);
                        continue;
                    case 0xE2:
                        var loopE2 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.loop, [loopE2.Skip()]);
                        continue;
                    case 0xE3:
                        var jcxzE3 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.jcxz, [jcxzE3.Skip()]);
                        continue;
                    case 0xE4:
                        var inE4 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.@in, [R.AL, inE4]);
                        continue;
                    case 0xE5:
                        var inE5 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.@in, [R.AX, inE5]);
                        continue;
                    case 0xE6:
                        var outE6 = s.NextByte(buff);
                        yield return new(pos, first, 2, O.@out, [outE6, R.AL]);
                        continue;
                    case 0xE7:
                        var second = s.NextByte(buff);
                        yield return new(pos, first, 2, O.@out, [second, R.AX]);
                        continue;
                    case 0xE8:
                        var callA = s.NextShort(buff).Skip();
                        yield return new(pos, first, 3, O.call, [callA]);
                        continue;
                    case 0xE9:
                        var jmpE9 = s.NextShort(buff).Skip();
                        yield return new(pos, first, 3, O.jmp, [jmpE9]);
                        continue;
                    case 0xEA:
                        var jmpA = s.NextShort(buff);
                        var jmpB = s.NextShort(buff);
                        yield return new(pos, first, 5, O.jmp, [jmpA.ToMem(jmpB)]);
                        continue;
                    case 0xEB:
                        yield return new(pos, first, 2, O.jmp, [Modifier.@short.On(s.NextByte(buff).Skip())]);
                        continue;
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
                if (err)
                    throw new InstructionError(pos, buff);
                yield return default;
            }
        }
    }
}
