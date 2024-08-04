// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;
using System;
using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;
using OpTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa>;
using OfaTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1A>;
using OfbTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1B>;
using OfcTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1C>;
using RgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Rgo>;
using SgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Sgo>;
using FgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Fgo>;
using RaTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Rga>;
using OdTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.OpDir>;
using ObTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.OpBit>;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;

namespace PoViEmu.Core.Machine
{
    public static class Intel16
    {
        public static IEnumerable<Instruction> Disassemble(Stream stream, byte[] buffer)
        {
            while (stream.ReadBytesPos(buffer) is { } pos)
            {
                var first = buffer[0];
                switch (first)
                {
                    case 0x06:
                        yield return new(pos, first, 1, O.push, args: [R.es]);
                        continue;
                    case 0x07:
                        yield return new(pos, first, 1, O.pop, args: [R.es]);
                        continue;
                    case 0x0E:
                        yield return new(pos, first, 1, O.push, args: [R.cs]);
                        continue;
                    case 0x16:
                        yield return new(pos, first, 1, O.push, args: [R.ss]);
                        continue;
                    case 0x17:
                        yield return new(pos, first, 1, O.pop, args: [R.ss]);
                        continue;
                    case 0x1E:
                        yield return new(pos, first, 1, O.push, args: [R.ds]);
                        continue;
                    case 0x1F:
                        yield return new(pos, first, 1, O.pop, args: [R.ds]);
                        continue;
                    case 0x27:
                        yield return new(pos, first, 1, O.daa);
                        continue;
                    case 0x2F:
                        yield return new(pos, first, 1, O.das);
                        continue;
                    case 0x37:
                        yield return new(pos, first, 1, O.aaa);
                        continue;
                    case 0x3F:
                        yield return new(pos, first, 1, O.aas);
                        continue;
                    case 0x40:
                        yield return new(pos, first, 1, O.inc, args: [R.ax]);
                        continue;
                    case 0x41:
                        yield return new(pos, first, 1, O.inc, args: [R.cx]);
                        continue;
                    case 0x42:
                        yield return new(pos, first, 1, O.inc, args: [R.dx]);
                        continue;
                    case 0x43:
                        yield return new(pos, first, 1, O.inc, args: [R.bx]);
                        continue;
                    case 0x44:
                        yield return new(pos, first, 1, O.inc, args: [R.sp]);
                        continue;
                    case 0x45:
                        yield return new(pos, first, 1, O.inc, args: [R.bp]);
                        continue;
                    case 0x46:
                        yield return new(pos, first, 1, O.inc, args: [R.si]);
                        continue;
                    case 0x47:
                        yield return new(pos, first, 1, O.inc, args: [R.di]);
                        continue;
                    case 0x48:
                        yield return new(pos, first, 1, O.dec, args: [R.ax]);
                        continue;
                    case 0x49:
                        yield return new(pos, first, 1, O.dec, args: [R.cx]);
                        continue;
                    case 0x4A:
                        yield return new(pos, first, 1, O.dec, args: [R.dx]);
                        continue;
                    case 0x4B:
                        yield return new(pos, first, 1, O.dec, args: [R.bx]);
                        continue;
                    case 0x4C:
                        yield return new(pos, first, 1, O.dec, args: [R.sp]);
                        continue;
                    case 0x4D:
                        yield return new(pos, first, 1, O.dec, args: [R.bp]);
                        continue;
                    case 0x4E:
                        yield return new(pos, first, 1, O.dec, args: [R.si]);
                        continue;
                    case 0x4F:
                        yield return new(pos, first, 1, O.dec, args: [R.di]);
                        continue;
                    case 0x50:
                        yield return new(pos, first, 1, O.push, args: [R.ax]);
                        continue;
                    case 0x51:
                        yield return new(pos, first, 1, O.push, args: [R.cx]);
                        continue;
                    case 0x52:
                        yield return new(pos, first, 1, O.push, args: [R.dx]);
                        continue;
                    case 0x53:
                        yield return new(pos, first, 1, O.push, args: [R.bx]);
                        continue;
                    case 0x54:
                        yield return new(pos, first, 1, O.push, args: [R.sp]);
                        continue;
                    case 0x55:
                        yield return new(pos, first, 1, O.push, args: [R.bp]);
                        continue;
                    case 0x56:
                        yield return new(pos, first, 1, O.push, args: [R.si]);
                        continue;
                    case 0x57:
                        yield return new(pos, first, 1, O.push, args: [R.di]);
                        continue;
                    case 0x58:
                        yield return new(pos, first, 1, O.pop, args: [R.ax]);
                        continue;
                    case 0x59:
                        yield return new(pos, first, 1, O.pop, args: [R.cx]);
                        continue;
                    case 0x5A:
                        yield return new(pos, first, 1, O.pop, args: [R.dx]);
                        continue;
                    case 0x5B:
                        yield return new(pos, first, 1, O.pop, args: [R.bx]);
                        continue;
                    case 0x5C:
                        yield return new(pos, first, 1, O.pop, args: [R.sp]);
                        continue;
                    case 0x5D:
                        yield return new(pos, first, 1, O.pop, args: [R.bp]);
                        continue;
                    case 0x5E:
                        yield return new(pos, first, 1, O.pop, args: [R.si]);
                        continue;
                    case 0x5F:
                        yield return new(pos, first, 1, O.pop, args: [R.di]);
                        continue;
                    case 0x60:
                        yield return new(pos, first, 1, O.pusha);
                        continue;
                    case 0x61:
                        yield return new(pos, first, 1, O.popa);
                        continue;
                    case 0x6C:
                        yield return new(pos, first, 1, O.insb);
                        continue;
                    case 0x6D:
                        yield return new(pos, first, 1, O.insw);
                        continue;
                    case 0x6E:
                        yield return new(pos, first, 1, O.outsb);
                        continue;
                    case 0x6F:
                        yield return new(pos, first, 1, O.outsw);
                        continue;
                    case 0x90:
                        yield return new(pos, first, 1, O.nop);
                        continue;
                    case 0x91:
                        yield return new(pos, first, 1, O.xchg, args: [R.ax, R.cx]);
                        continue;
                    case 0x92:
                        yield return new(pos, first, 1, O.xchg, args: [R.ax, R.dx]);
                        continue;
                    case 0x93:
                        yield return new(pos, first, 1, O.xchg, args: [R.ax, R.bx]);
                        continue;
                    case 0x94:
                        yield return new(pos, first, 1, O.xchg, args: [R.ax, R.sp]);
                        continue;
                    case 0x95:
                        yield return new(pos, first, 1, O.xchg, args: [R.ax, R.bp]);
                        continue;
                    case 0x96:
                        yield return new(pos, first, 1, O.xchg, args: [R.ax, R.si]);
                        continue;
                    case 0x97:
                        yield return new(pos, first, 1, O.xchg, args: [R.ax, R.di]);
                        continue;
                    case 0x98:
                        yield return new(pos, first, 1, O.cbw);
                        continue;
                    case 0x99:
                        yield return new(pos, first, 1, O.cwd);
                        continue;
                    case 0x9C:
                        yield return new(pos, first, 1, O.pushf);
                        continue;
                    case 0x9D:
                        yield return new(pos, first, 1, O.popf);
                        continue;
                    case 0x9E:
                        yield return new(pos, first, 1, O.sahf);
                        continue;
                    case 0x9F:
                        yield return new(pos, first, 1, O.lahf);
                        continue;
                    case 0xA4:
                        yield return new(pos, first, 1, O.movsb);
                        continue;
                    case 0xA5:
                        yield return new(pos, first, 1, O.movsw);
                        continue;
                    case 0xA6:
                        yield return new(pos, first, 1, O.cmpsb);
                        continue;
                    case 0xA7:
                        yield return new(pos, first, 1, O.cmpsw);
                        continue;
                    case 0xAA:
                        yield return new(pos, first, 1, O.stosb);
                        continue;
                    case 0xAB:
                        yield return new(pos, first, 1, O.stosw);
                        continue;
                    case 0xAC:
                        yield return new(pos, first, 1, O.lodsb);
                        continue;
                    case 0xAD:
                        yield return new(pos, first, 1, O.lodsw);
                        continue;
                    case 0xAE:
                        yield return new(pos, first, 1, O.scasb);
                        continue;
                    case 0xAF:
                        yield return new(pos, first, 1, O.scasw);
                        continue;
                    case 0xC3:
                        yield return new(pos, first, 1, O.ret);
                        continue;
                    case 0xC9:
                        yield return new(pos, first, 1, O.leave);
                        continue;
                    case 0xCB:
                        yield return new(pos, first, 1, O.retf);
                        continue;
                    case 0xCC:
                        yield return new(pos, first, 1, O.int3);
                        continue;
                    case 0xCE:
                        yield return new(pos, first, 1, O.into);
                        continue;
                    case 0xCF:
                        yield return new(pos, first, 1, O.iret);
                        continue;
                    case 0xD6:
                        yield return new(pos, first, 1, O.salc);
                        continue;
                    case 0xD7:
                        yield return new(pos, first, 1, O.xlatb);
                        continue;
                    case 0xEC:
                        yield return new(pos, first, 1, O.@in, args: [R.al, R.dx]);
                        continue;
                    case 0xED:
                        yield return new(pos, first, 1, O.@in, args: [R.ax, R.dx]);
                        continue;
                    case 0xEE:
                        yield return new(pos, first, 1, O.@out, args: [R.dx, R.al]);
                        continue;
                    case 0xEF:
                        yield return new(pos, first, 1, O.@out, args: [R.dx, R.ax]);
                        continue;
                    case 0xF1:
                        yield return new(pos, first, 1, O.int1);
                        continue;
                    case 0xF4:
                        yield return new(pos, first, 1, O.hlt);
                        continue;
                    case 0xF5:
                        yield return new(pos, first, 1, O.cmc);
                        continue;
                    case 0xF8:
                        yield return new(pos, first, 1, O.clc);
                        continue;
                    case 0xF9:
                        yield return new(pos, first, 1, O.stc);
                        continue;
                    case 0xFA:
                        yield return new(pos, first, 1, O.cli);
                        continue;
                    case 0xFB:
                        yield return new(pos, first, 1, O.sti);
                        continue;
                    case 0xFC:
                        yield return new(pos, first, 1, O.cld);
                        continue;
                    case 0xFD:
                        yield return new(pos, first, 1, O.std);
                        continue;
                    default:
                        throw new InstructionError(pos, first);
                }
            }
        }
    }
}