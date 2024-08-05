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
using A = PoViEmu.Core.Machine.Ops.OpArg;

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
                    case 0xE8:
                        var xE8_1 = stream.NextShortC();
                        yield return new(pos, first, -4, O.call, args: [xE8_1]);
                        continue;
                    case 0xE9:
                        var xE9_1 = stream.NextShortC();
                        yield return new(pos, first, -4, O.jmp, args: [xE9_1]);
                        continue;
                    case 0xC2:
                        var xC2_1 = stream.NextShortC();
                        yield return new(pos, first, -4, O.ret, args: [xC2_1]);
                        continue;
                    case 0xCA:
                        var xCA_1 = stream.NextShortC();
                        yield return new(pos, first, -4, O.retf, args: [xCA_1]);
                        continue;
                    case 0xD9:
                        var xD9_1 = stream.NextByteC().Value;
                        switch (xD9_1)
                        {
                            case 0xD0:
                                yield return new(pos, first, -2, O.fnop);
                                continue;
                            case 0xE0:
                                yield return new(pos, first, -2, O.fchs);
                                continue;
                            case 0xE1:
                                yield return new(pos, first, -2, O.fabs);
                                continue;
                            case 0xE4:
                                yield return new(pos, first, -2, O.ftst);
                                continue;
                            case 0xE5:
                                yield return new(pos, first, -2, O.fxam);
                                continue;
                            case 0xE8:
                                yield return new(pos, first, -2, O.fld1);
                                continue;
                            case 0xE9:
                                yield return new(pos, first, -2, O.fldl2t);
                                continue;
                            case 0xEA:
                                yield return new(pos, first, -2, O.fldl2e);
                                continue;
                            case 0xEB:
                                yield return new(pos, first, -2, O.fldpi);
                                continue;
                            case 0xEC:
                                yield return new(pos, first, -2, O.fldlg2);
                                continue;
                            case 0xED:
                                yield return new(pos, first, -2, O.fldln2);
                                continue;
                            case 0xEE:
                                yield return new(pos, first, -2, O.fldz);
                                continue;
                            case 0xF0:
                                yield return new(pos, first, -2, O.f2xm1);
                                continue;
                            case 0xF1:
                                yield return new(pos, first, -2, O.fyl2x);
                                continue;
                            case 0xF2:
                                yield return new(pos, first, -2, O.fptan);
                                continue;
                            case 0xF3:
                                yield return new(pos, first, -2, O.fpatan);
                                continue;
                            case 0xF4:
                                yield return new(pos, first, -2, O.fxtract);
                                continue;
                            case 0xF5:
                                yield return new(pos, first, -2, O.fprem1);
                                continue;
                            case 0xF6:
                                yield return new(pos, first, -2, O.fdecstp);
                                continue;
                            case 0xF7:
                                yield return new(pos, first, -2, O.fincstp);
                                continue;
                            case 0xF8:
                                yield return new(pos, first, -2, O.fprem);
                                continue;
                            case 0xF9:
                                yield return new(pos, first, -2, O.fyl2xp1);
                                continue;
                            case 0xFA:
                                yield return new(pos, first, -2, O.fsqrt);
                                continue;
                            case 0xFB:
                                yield return new(pos, first, -2, O.fsincos);
                                continue;
                            case 0xFC:
                                yield return new(pos, first, -2, O.frndint);
                                continue;
                            case 0xFD:
                                yield return new(pos, first, -2, O.fscale);
                                continue;
                            case 0xFE:
                                yield return new(pos, first, -2, O.fsin);
                                continue;
                            case 0xFF:
                                yield return new(pos, first, -2, O.fcos);
                                continue;
                            case 0x24:
                                yield return new(pos, first, -2, O.fldenv, args: [R.si.Box(xD9_1)]);
                                continue;
                            case 0x25:
                                yield return new(pos, first, -2, O.fldenv, args: [R.di.Box(xD9_1)]);
                                continue;
                            case 0x27:
                                yield return new(pos, first, -2, O.fldenv, args: [R.bx.Box(xD9_1)]);
                                continue;
                            case 0x2C:
                                yield return new(pos, first, -2, O.fldcw, args: [R.si.Box(xD9_1)]);
                                continue;
                            case 0x2D:
                                yield return new(pos, first, -2, O.fldcw, args: [R.di.Box(xD9_1)]);
                                continue;
                            case 0x2F:
                                yield return new(pos, first, -2, O.fldcw, args: [R.bx.Box(xD9_1)]);
                                continue;
                            case 0x34:
                                yield return new(pos, first, -2, O.fnstenv, args: [R.si.Box(xD9_1)]);
                                continue;
                            case 0x35:
                                yield return new(pos, first, -2, O.fnstenv, args: [R.di.Box(xD9_1)]);
                                continue;
                            case 0x37:
                                yield return new(pos, first, -2, O.fnstenv, args: [R.bx.Box(xD9_1)]);
                                continue;
                            case 0x3C:
                                yield return new(pos, first, -2, O.fnstcw, args: [R.si.Box(xD9_1)]);
                                continue;
                            case 0x3D:
                                yield return new(pos, first, -2, O.fnstcw, args: [R.di.Box(xD9_1)]);
                                continue;
                            case 0x3F:
                                yield return new(pos, first, -2, O.fnstcw, args: [R.bx.Box(xD9_1)]);
                                continue;
                        }
                        if (BitsSwitch.FindOpFlag(first, xD9_1) is { } xD9_op &&
                            FgTool.FindFlag(xD9_1) is { } xD9_a)
                        {
                            yield return new(pos, first, -2, xD9_op.ToOp(), args: [xD9_a.ToReg()]);
                            continue;
                        }
                        break;
                    case 0xD4:
                    case 0xD5:
                        var xD4_op = first switch
                        {
                            0xD4 => O.aam, 0xD5 => O.aad, _ => throw new ArgumentOutOfRangeException()
                        };
                        var xD4_1 = stream.NextByteC().Value;
                        if (xD4_1 == 0x0A)
                            yield return new(pos, first, -2, xD4_op, args: [xD4_1]);
                        else
                            yield return new(pos, first, -2, xD4_op, args: [xD4_1]);
                        continue;
                    case 0xDA:
                        var xDA_1 = stream.NextByteC().Value;
                        switch (xDA_1)
                        {
                            case 0xE9:
                                yield return new(pos, first, -2, O.fucompp);
                                continue;
                        }
                        if (BitsSwitch.FindOpFlag(first, xDA_1) is { } xDA_op &&
                            FgTool.FindFlag(xDA_1) is { } xDA_a)
                        {
                            yield return new(pos, first, -2, xDA_op.ToOp(), args: [xDA_a.ToReg()]);
                            continue;
                        }
                        break;
                    case 0xDB:
                        var xDB_1 = stream.NextByteC().Value;
                        switch (xDB_1)
                        {
                            case 0xE0:
                                yield return new(pos, first, -2, O.fneni);
                                continue;
                            case 0xE1:
                                yield return new(pos, first, -2, O.fndisi);
                                continue;
                            case 0xE2:
                                yield return new(pos, first, -2, O.fnclex);
                                continue;
                            case 0xE3:
                                yield return new(pos, first, -2, O.fninit);
                                continue;
                            case 0xE4:
                                yield return new(pos, first, -2, O.fsetpm);
                                continue;
                        }
                        if (BitsSwitch.FindOpFlag(first, xDB_1) is { } xDB_op &&
                            FgTool.FindFlag(xDB_1) is { } xDB_a)
                        {
                            yield return new(pos, first, -2, xDB_op.ToOp(), args: [xDB_a.ToReg()]);
                            continue;
                        }
                        break;
                    case 0xDE:
                        var xDE_1 = stream.NextByteC().Value;
                        switch (xDE_1)
                        {
                            case 0xD9:
                                yield return new(pos, first, -2, O.fcompp);
                                continue;
                        }
                        if (BitsSwitch.FindOpFlag(first, xDE_1) is { } xDE_op &&
                            FgTool.FindFlag(xDE_1) is { } xDE_a)
                        {
                            yield return new(pos, first, -2, xDE_op.ToOp(), args: [xDE_a.ToReg()]);
                            continue;
                        }
                        if (ParseTwoArg(pos, first, stream, xDE_1) is { } xDE_instr)
                        {
                            yield return xDE_instr;
                            continue;
                        }
                        break;
                    case 0xDF:
                        var xDF_1 = stream.NextByteC().Value;
                        switch (xDF_1)
                        {
                            case 0xE0:
                                yield return new(pos, first, -2, O.fnstsw, args: [R.ax]);
                                continue;
                        }
                        if (BitsSwitch.FindOpFlag(first, xDF_1) is { } xDF_op &&
                            FgTool.FindFlag(xDF_1) is { } xDF_a)
                        {
                            yield return new(pos, first, -2, xDF_op.ToOp(), args: [xDF_a.ToReg()]);
                            continue;
                        }
                        if (ParseTwoArg(pos, first, stream, xDF_1) is { } xDF_instr)
                        {
                            yield return xDF_instr;
                            continue;
                        }
                        break;
                    case 0x8F:
                    case 0xF6:
                    case 0xF7:
                    case 0xFE:
                    case 0xFF:
                        var xFF_1 = stream.NextByteC().Value;
                        switch (xFF_1)
                        {
                            case 0x14:
                                yield return new(pos, first, -2, O.call, args: [R.si.Box(xFF_1)]);
                                continue;
                            case 0x15:
                                yield return new(pos, first, -2, O.call, args: [R.di.Box(xFF_1)]);
                                continue;
                            case 0x17:
                                yield return new(pos, first, -2, O.call, args: [R.bx.Box(xFF_1)]);
                                continue;
                            case 0x24:
                                yield return new(pos, first, -2, O.jmp, args: [R.si.Box(xFF_1)]);
                                continue;
                            case 0x25:
                                yield return new(pos, first, -2, O.jmp, args: [R.di.Box(xFF_1)]);
                                continue;
                            case 0x27:
                                yield return new(pos, first, -2, O.jmp, args: [R.bx.Box(xFF_1)]);
                                continue;
                        }
                        if (BitsSwitch.FindOpFlag(first, xFF_1) is { } xFF_op &&
                            ObTool.FindFlag(first) is { } xFF_bi)
                            if (RgTool.FindFlag(xFF_1, 3) is { } xFF_a)
                            {
                                var xFF_aR = xFF_a.ToReg(Rga.R2R, xFF_bi);
                                var xFF_opp = xFF_op.ToOp();
                                yield return new(pos, first, -2, xFF_opp, args: [xFF_aR]);
                                continue;
                            }
                        if (ParseTwoArg(pos, first, stream, xFF_1) is { } xFF_instr)
                        {
                            yield return xFF_instr;
                            continue;
                        }
                        break;
                    case 0x70:
                    case 0x71:
                    case 0x72:
                    case 0x73:
                    case 0x75:
                    case 0x76:
                    case 0x7D:
                    case 0x7A:
                    case 0x7E:
                    case 0x7F:
                    case 0x78:
                    case 0x74:
                    case 0x79:
                    case 0x7B:
                    case 0x7C:
                    case 0x77:
                    case 0xE3:
                        var x70_op = first switch
                        {
                            0x7B => O.jpo, 0x7C => O.jl, 0x70 => O.jo, 0xE3 => O.jcxz, 0x74 => O.jz,
                            0x73 => O.jnc, 0x7D => O.jnl, 0x72 => O.jc, 0x71 => O.jno, 0x7F => O.jg,
                            0x7E => O.jng, 0x75 => O.jnz, 0x76 => O.jna, 0x7A => O.jpe, 0x77 => O.ja,
                            0x78 => O.js, 0x79 => O.jns,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        var x70_1 = stream.NextSByteC();
                        yield return new(pos, first, -2, x70_op, args: [x70_1]);
                        continue;
                    case 0xE0:
                    case 0xE1:
                    case 0xE2:
                        var xE0_op = first switch
                        {
                            0xE0 => O.loopne, 0xE1 => O.loope, 0xE2 => O.loop,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        var xE0_1 = stream.NextSByteC();
                        yield return new(pos, first, -2, xE0_op, args: [xE0_1]);
                        continue;
                    case 0xDD:
                        var xDD_1 = stream.NextByteC().Value;
                        switch (xDD_1)
                        {
                            case 0x24:
                                yield return new(pos, first, -2, O.frstor, args: [R.si.Box(xDD_1)]);
                                continue;
                            case 0x25:
                                yield return new(pos, first, -2, O.frstor, args: [R.di.Box(xDD_1)]);
                                continue;
                            case 0x27:
                                yield return new(pos, first, -2, O.frstor, args: [R.bx.Box(xDD_1)]);
                                continue;
                            case 0x34:
                                yield return new(pos, first, -2, O.fnsave, args: [R.si.Box(xDD_1)]);
                                continue;
                            case 0x35:
                                yield return new(pos, first, -2, O.fnsave, args: [R.di.Box(xDD_1)]);
                                continue;
                            case 0x37:
                                yield return new(pos, first, -2, O.fnsave, args: [R.bx.Box(xDD_1)]);
                                continue;
                            case 0x3C:
                                yield return new(pos, first, -2, O.fnstsw, args: [R.si.Box(xDD_1)]);
                                continue;
                            case 0x3D:
                                yield return new(pos, first, -2, O.fnstsw, args: [R.di.Box(xDD_1)]);
                                continue;
                            case 0x3F:
                                yield return new(pos, first, -2, O.fnstsw, args: [R.bx.Box(xDD_1)]);
                                continue;
                        }
                        if (BitsSwitch.FindOpFlag(first, xDD_1) is { } xDD_op &&
                            FgTool.FindFlag(xDD_1) is { } xDD_a)
                        {
                            yield return new(pos, first, -2, xDD_op.ToOp(), args: [xDD_a.ToReg()]);
                            continue;
                        }
                        if (ParseTwoArg(pos, first, stream, xDD_1) is { } xDD_instr)
                        {
                            yield return xDD_instr;
                            continue;
                        }
                        break;
                    case 0xD8:
                        var xD8_1 = stream.NextByteC().Value;
                        if (BitsSwitch.FindOpFlag(first, xD8_1) is { } xD8_op &&
                            FgTool.FindFlag(xD8_1) is { } xD8_a)
                        {
                            var xD8_reg = xD8_a.ToReg().With(xD8_1);
                            yield return new(pos, first, -2, xD8_op.ToOp(), args: [xD8_reg]);
                            continue;
                        }
                        if (ParseTwoArg(pos, first, stream, xD8_1) is { } xD8_instr)
                        {
                            yield return xD8_instr;
                            continue;
                        }
                        break;
                    case 0xCD:
                        var xCD_1 = stream.NextByteC();
                        yield return new(pos, first, -2, O.@int, args: [xCD_1]);
                        continue;
                    case 0xD0:
                    case 0xD1:
                    case 0xD2:
                    case 0xD3:
                        var xD0_1 = stream.NextByteC().Value;
                        if (BitsSwitch.FindOpFlag(first, xD0_1) is { } xD0_op &&
                            ObTool.FindFlag(first) is { } xD0_bi)
                            if (RgTool.FindFlag(xD0_1, 3) is { } xD0_a)
                            {
                                var xD0_aR = xD0_a.ToReg(Rga.R2R, xD0_bi);
                                var xD0_opp = xD0_op.ToOp();
                                OpArg xD0_one = first is 0xD2 or 0xD3 ? R.cl : new ImplicitArg(1);
                                yield return new(pos, first, -2, xD0_opp, args: [xD0_aR, xD0_one]);
                                continue;
                            }
                        switch (first)
                        {
                            case 0xD1:
                            case 0xD2:
                                if (ParseTwoArg(pos, first, stream, xD0_1) is { } xD0_instr)
                                {
                                    yield return xD0_instr;
                                    continue;
                                }
                                break;
                        }
                        break;
                    case 0xC6:
                        var xC6_1 = stream.NextByteC().Value;
                        switch (xC6_1)
                        {
                            case 0xF8:
                                yield return new(pos, first, -4, O.xabort, args: [stream.NextByteC()]);
                                continue;
                        }
                        break;
                    case 0x00:
                    case 0x01:
                    case 0x02:
                    case 0x03:
                    case 0x08:
                    case 0x09:
                    case 0x0A:
                    case 0x0B:
                    case 0x0C:
                    case 0x10:
                    case 0x11:
                    case 0x12:
                    case 0x14:
                    case 0x18:
                    case 0x19:
                    case 0x1A:
                    case 0x20:
                    case 0x22:
                    case 0x23:
                    case 0x24:
                    case 0x26:
                    case 0x28:
                    case 0x2A:
                    case 0x2B:
                    case 0x2C:
                    case 0x2E:
                    case 0x30:
                    case 0x31:
                    case 0x32:
                    case 0x36:
                    case 0x38:
                    case 0x39:
                    case 0x3A:
                    case 0x3B:
                    case 0x3C:
                    case 0x3E:
                    case 0x63:
                    case 0x66:
                    case 0x6A:
                    case 0x80:
                    case 0x85:
                    case 0x86:
                    case 0x87:
                    case 0x88:
                    case 0x89:
                    case 0x8A:
                    case 0x8B:
                    case 0x8E:
                    case 0x9B:
                    case 0xB1:
                    case 0xB4:
                    case 0xB5:
                    case 0xB7:
                    case 0xBB:
                    case 0xBE:
                    case 0xC0:
                    case 0xC1:
                    case 0xC4:
                    case 0xC8:
                    case 0xDC:
                    case 0xA8:
                    case 0xE5:
                    case 0xE6:
                    case 0xEB:
                    case 0xF0:
                    case 0x13:
                    case 0x1B:
                    case 0x1C:
                    case 0x21:
                    case 0x33:
                    case 0x34:
                    case 0x65:
                    case 0x84:
                    case 0x8C:
                    case 0xB0:
                    case 0xB2:
                    case 0xB3:
                    case 0x29:
                    case 0xB6:
                    case 0xB8:
                    case 0x04:
                    case 0xBA:
                    case 0xC5:
                    case 0xE4:
                    case 0xE7:
                    case 0xF2:
                    case 0xF3:
                        if (ParseTwoArg(pos, first, stream) is { } xF3_instr)
                        {
                            yield return xF3_instr;
                            continue;
                        }
                        break;
                }
                throw new InstructionError(pos, first);
            }
        }

        private static Instruction? ParseTwoArg(long pos, byte first, Stream stream, byte? nextByte = null)
        {
            if (OpTool.FindFlag(first) is { } opCode &&
                ObTool.FindFlag(first) is { } opBit)
            {
                var opCod = opCode.ToOp();
                switch (first)
                {
                    case 0x87:
                    case 0x86:
                        opCod = O.xchg;
                        break;
                }
                var second = nextByte ?? stream.NextByteC().Value;
                if (RaTool.FindFlag(second) is { } opFlag)
                {
                    var args = new List<OpArg>();
                    var isSpecial = first is 0x8C or 0x8E;
                    if (BitsSwitch.FindRegFlag(first, second) is { } operA)
                    {
                        var operA1m = Rga.R2R;
                        var operA1b = opBit;
                        A operAa;
                        if (operA is Rgo rgo)
                            operAa = rgo.ToReg(operA1m, operA1b);
                        else
                            operAa = operA.ToReg();
                        args.Add(operAa.With(second));
                    }
                    if (RgTool.FindFlag(second, 3) is { } operB)
                    {
                        if (isSpecial) opBit = OpBit.b16;
                        var operBa = operB.ToReg(opFlag, opBit);
                        args.Add(operBa);
                    }
                    if (OdTool.FindFlag(first) is { } opDir)
                    {
                        if (opCode == Opa.arpl) opDir = BitsSwitch.Inverse(opDir);
                        if (opDir == OpDir.Right)
                            args.Reverse();
                    }
                    return new(pos, first, 2, opCod, args: args.ToArray());
                }
            }
            return null;
        }
    }
}