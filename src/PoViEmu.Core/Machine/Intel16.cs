using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine
{
    public static class Intel16
    {
        public static IEnumerable<Instruction> Disassemble(Stream stream, byte[] buffer)
        {
            while (stream.ReadBytesPos(buffer) is { } pos)
            {
                var first = (OpCode)buffer[0];
                switch (first)
                {
                    case OpCode.a32:
                        yield return new(pos, first);
                        break;
                    case OpCode.aaa:
                        yield return new(pos, first, OpBase.aaa);
                        break;
                    case OpCode.aas:
                        yield return new(pos, first, OpBase.aas);
                        break;
                    case OpCode.add:
                        var addFirst = stream.NextRegC();
                        var addSecond = stream.NextByteC();
                        yield return new(pos, first, OpBase.add, args: [addFirst, addSecond]);
                        break;
                    case OpCode.add_ax:
                        var addaFld = stream.NextByteC().Value;
                        if (addaFld == 0xD0)
                            yield return new(pos, first, OpBase.add, args: [Register.dx, Register.ax.With(addaFld)]);
                        else if (addaFld == 0xC1)
                            yield return new(pos, first, OpBase.add, args: [Register.ax, Register.cx.With(addaFld)]);
                        else if (addaFld == 0xC3)
                            yield return new(pos, first, OpBase.add, args: [Register.ax, Register.bx.With(addaFld)]);
                        else
                            yield return new(pos, first, OpBase.add, args: [Register.ax, Register.ax.With(addaFld)]);
                        break;
                    case OpCode.adc:
                        var adcFld = stream.NextByteC().Value;
                        var adcFvl = stream.NextShortC();
                        if (adcFld == 0xC1)
                            yield return new(pos, first, OpBase.add, args: [Register.cx.With(adcFld), adcFvl]);
                        else
                            yield return new(pos, first, OpBase.adc, args: [Register.dx.With(adcFld), adcFvl]);
                        break;
                    case OpCode.and:
                        var andFld = stream.NextByteC().Value;
                        if (andFld == 0xD8)
                            yield return new(pos, first, OpBase.and, args: [Register.bx, Register.ax.With(andFld)]);
                        else if (andFld == 0xC1)
                            yield return new(pos, first, OpBase.and, args: [Register.ax, Register.cx.With(andFld)]);
                        else
                            yield return new(pos, first, OpBase.and, args: [Register.ax, Register.ax.With(andFld)]);
                        break;
                    case OpCode.cbw:
                        yield return new(pos, first, OpBase.cbw);
                        break;
                    case OpCode.clc:
                        yield return new(pos, first, OpBase.clc);
                        break;
                    case OpCode.cld:
                        yield return new(pos, first, OpBase.cld);
                        break;
                    case OpCode.cli:
                        yield return new(pos, first, OpBase.cli);
                        break;
                    case OpCode.cmc:
                        yield return new(pos, first, OpBase.cmc);
                        break;
                    case OpCode.cmp:
                        var cmpFl = stream.NextByteC().Value;
                        if (cmpFl == 0xD6)
                            yield return new(pos, first, OpBase.cmp, args: [Register.dx, Register.si.With(cmpFl)]);
                        else if (cmpFl == 0xCA)
                            yield return new(pos, first, OpBase.cmp, args: [Register.cx, Register.dx.With(cmpFl)]);
                        else if (cmpFl == 0xD3)
                            yield return new(pos, first, OpBase.cmp, args: [Register.dx, Register.bx.With(cmpFl)]);
                        else if (cmpFl == 0xDA)
                            yield return new(pos, first, OpBase.cmp, args: [Register.bx, Register.dx.With(cmpFl)]);
                        else if (cmpFl == 0xF1)
                            yield return new(pos, first, OpBase.cmp, args: [Register.si, Register.cx.With(cmpFl)]);
                        else
                            yield return new(pos, first, OpBase.cmp, args: [Register.si, Register.dx.With(cmpFl)]);
                        break;
                    case OpCode.cmp_ax:
                        var cmpsFl = stream.NextShortC();
                        yield return new(pos, first, OpBase.cmp, args: [Register.ax, cmpsFl]);
                        break;
                    case OpCode.cmp_b:
                        var cmpbFl = stream.NextByteC().Value;
                        var cmpvFl = stream.NextBytepC();
                        if (cmpbFl == 0xFF)
                            yield return new(pos, first, OpBase.cmp, args: [Register.di.With(cmpbFl), cmpvFl]);
                        else if (cmpbFl == 0xEB)
                            yield return new(pos, first, OpBase.sub, args: [Register.bx.With(cmpbFl), cmpvFl]);
                        else
                            yield return new(pos, first, OpBase.cmp, args: [Register.dx.With(cmpbFl), cmpvFl]);
                        break;
                    case OpCode.cmpsb:
                        yield return new(pos, first, OpBase.cmpsb);
                        break;
                    case OpCode.cmpsw:
                        yield return new(pos, first, OpBase.cmpsw);
                        break;
                    case OpCode.cs:
                        yield return new(pos, first);
                        break;
                    case OpCode.cwd:
                        yield return new(pos, first, OpBase.cwd);
                        break;
                    case OpCode.daa:
                        yield return new(pos, first, OpBase.daa);
                        break;
                    case OpCode.das:
                        yield return new(pos, first, OpBase.das);
                        break;
                    case OpCode.dec_ax:
                        yield return new(pos, first, OpBase.dec, args: [Register.ax]);
                        break;
                    case OpCode.dec_bp:
                        yield return new(pos, first, OpBase.dec, args: [Register.bp]);
                        break;
                    case OpCode.dec_bx:
                        yield return new(pos, first, OpBase.dec, args: [Register.bx]);
                        break;
                    case OpCode.dec_cx:
                        yield return new(pos, first, OpBase.dec, args: [Register.cx]);
                        break;
                    case OpCode.dec_di:
                        yield return new(pos, first, OpBase.dec, args: [Register.di]);
                        break;
                    case OpCode.dec_dx:
                        yield return new(pos, first, OpBase.dec, args: [Register.dx]);
                        break;
                    case OpCode.dec_si:
                        yield return new(pos, first, OpBase.dec, args: [Register.si]);
                        break;
                    case OpCode.dec_sp:
                        yield return new(pos, first, OpBase.dec, args: [Register.sp]);
                        break;
                    case OpCode.ds:
                        yield return new(pos, first);
                        break;
                    case OpCode.es:
                        yield return new(pos, first);
                        break;
                    case OpCode.fs:
                        yield return new(pos, first);
                        break;
                    case OpCode.gs:
                        yield return new(pos, first);
                        break;
                    case OpCode.hlt:
                        yield return new(pos, first, OpBase.hlt);
                        break;
                    case OpCode.in_al_dx:
                        yield return new(pos, first, OpBase.@in, args: [Register.al, Register.dx]);
                        break;
                    case OpCode.in_ax_dx:
                        yield return new(pos, first, OpBase.@in, args: [Register.ax, Register.dx]);
                        break;
                    case OpCode.inc_ax:
                        yield return new(pos, first, OpBase.inc, args: [Register.ax]);
                        break;
                    case OpCode.inc_bx:
                        yield return new(pos, first, OpBase.inc, args: [Register.bx]);
                        break;
                    case OpCode.inc_bp:
                        yield return new(pos, first, OpBase.inc, args: [Register.bp]);
                        break;
                    case OpCode.inc_cx:
                        yield return new(pos, first, OpBase.inc, args: [Register.cx]);
                        break;
                    case OpCode.inc_di:
                        yield return new(pos, first, OpBase.inc, args: [Register.di]);
                        break;
                    case OpCode.inc_dx:
                        yield return new(pos, first, OpBase.inc, args: [Register.dx]);
                        break;
                    case OpCode.inc_si:
                        yield return new(pos, first, OpBase.inc, args: [Register.si]);
                        break;
                    case OpCode.inc_sp:
                        yield return new(pos, first, OpBase.inc, args: [Register.sp]);
                        break;
                    case OpCode.insb:
                        yield return new(pos, first, OpBase.insb);
                        break;
                    case OpCode.insw:
                        yield return new(pos, first, OpBase.insw);
                        break;
                    case OpCode.int1:
                        yield return new(pos, first);
                        break;
                    case OpCode.int3:
                        yield return new(pos, first);
                        break;
                    case OpCode.into:
                        yield return new(pos, first);
                        break;
                    case OpCode.iret:
                        yield return new(pos, first, OpBase.iret);
                        break;
                    case OpCode.jng:
                        var jngFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jngFld).IsSigned = true;
                        yield return new(pos, first, args: [jngFld]);
                        break;
                    case OpCode.jmp_short:
                        yield return new(pos, first, OpBase.jmp, Modifier.@short, [stream.NextByteC(isSkip: true)]);
                        break;
                    case OpCode.jl:
                        var jlFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jlFld).IsSigned = true;
                        yield return new(pos, first, args: [jlFld]);
                        break;
                    case OpCode.jnl:
                        var jnlFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnlFld).IsSigned = true;
                        yield return new(pos, first, args: [jnlFld]);
                        break;
                    case OpCode.jnz:
                        yield return new(pos, first, args: [stream.NextByteC(isSkip: true)]);
                        break;
                    case OpCode.jz:
                        yield return new(pos, first, args: [stream.NextByteC(isSkip: true)]);
                        break;
                    case OpCode.lahf:
                        yield return new(pos, first, OpBase.lahf);
                        break;
                    case OpCode.leave:
                        yield return new(pos, first, OpBase.leave);
                        break;
                    case OpCode.@lock:
                        yield return new(pos, first, OpBase.@lock);
                        break;
                    case OpCode.lodsw:
                        yield return new(pos, first);
                        break;
                    case OpCode.lodsb:
                        yield return new(pos, first);
                        break;
                    case OpCode.mov_ah:
                        yield return new(pos, first, OpBase.mov, args: [Register.ah, stream.NextByteC()]);
                        break;
                    case OpCode.mov_al:
                        var maa = stream.NextByteC().Value;
                        var mab = stream.NextByteC().Value;
                        yield return new(pos, first, OpBase.mov, args: [Register.al.With(maa), Register.di.Plus(mab)]);
                        break;
                    case OpCode.mov:
                        var movFld = stream.NextByteC().Value;
                        if (movFld == 0xDA)
                            yield return new(pos, first, OpBase.mov, args: [Register.bx, Register.dx.With(movFld)]);
                        else if (movFld == 0xCA)
                            yield return new(pos, first, OpBase.mov, args: [Register.cx, Register.dx.With(movFld)]);
                        else if (movFld == 0xC1)
                            yield return new(pos, first, OpBase.mov, args: [Register.ax, Register.cx.With(movFld)]);
                        else if (movFld == 0xF8)
                            yield return new(pos, first, OpBase.mov, args: [Register.di, Register.ax.With(movFld)]);
                        else if (movFld == 0xC3)
                            yield return new(pos, first, OpBase.mov, args: [Register.ax, Register.bx.With(movFld)]);
                        else if (movFld == 0xC6)
                            yield return new(pos, first, OpBase.mov, args: [Register.ax, Register.si.With(movFld)]);
                        else if (movFld == 0xC8)
                            yield return new(pos, first, OpBase.mov, args: [Register.cx, Register.ax.With(movFld)]);
                        else if (movFld == 0xD8)
                            yield return new(pos, first, OpBase.mov, args: [Register.bx, Register.ax.With(movFld)]);
                        else
                            yield return new(pos, first, OpBase.mov, args: [Register.ax, Register.ax.With(movFld)]);
                        break;
                    case OpCode.mov_ax_s:
                        yield return new(pos, first, OpBase.mov, args: [Register.ax, stream.NextShortC()]);
                        break;
                    case OpCode.mov_bx:
                        yield return new(pos, first, OpBase.mov, args: [Register.bx, stream.NextShortC()]);
                        break;
                    case OpCode.mov_cx:
                        yield return new(pos, first, OpBase.mov, args: [Register.cx, stream.NextShortC()]);
                        break;
                    case OpCode.mov_dx:
                        yield return new(pos, first, OpBase.mov, args: [Register.dx, stream.NextShortC()]);
                        break;
                    case OpCode.mov_si:
                        yield return new(pos, first, OpBase.mov, args: [Register.si, stream.NextShortC()]);
                        break;
                    case OpCode.movsb:
                        yield return new(pos, first, OpBase.movsb);
                        break;
                    case OpCode.movsw:
                        yield return new(pos, first, OpBase.movsw);
                        break;
                    case OpCode.mul:
                        var mulFirst = stream.NextByteC().Value;
                        if (mulFirst == 0xD0)
                            yield return new(pos, first, OpBase.not, args: [Register.ax.With(mulFirst)]);
                        else if (mulFirst == 0xD2)
                            yield return new(pos, first, OpBase.not, args: [Register.dx.With(mulFirst)]);
                        else if (mulFirst == 0xE1)
                            yield return new(pos, first, OpBase.mul, args: [Register.cx.With(mulFirst)]);
                        else if (mulFirst == 0xE3)
                            yield return new(pos, first, OpBase.mul, args: [Register.bx.With(mulFirst)]);
                        else if (mulFirst == 0xFB)
                            yield return new(pos, first, OpBase.idiv, args: [Register.bx.With(mulFirst)]);
                        else
                            yield return new(pos, first, OpBase.idiv, args: [Register.cx.With(mulFirst)]);
                        break;
                    case OpCode.nop:
                        yield return new(pos, first, OpBase.nop);
                        break;
                    case OpCode.o32:
                        yield return new(pos, first);
                        break;
                    case OpCode.out_dx_al:
                        yield return new(pos, first, OpBase.@out, args: [Register.dx, Register.al]);
                        break;
                    case OpCode.out_dx_ax:
                        yield return new(pos, first, OpBase.@out, args: [Register.dx, Register.ax]);
                        break;
                    case OpCode.outsb:
                        yield return new(pos, first, OpBase.outsb);
                        break;
                    case OpCode.outsw:
                        yield return new(pos, first, OpBase.outsw);
                        break;
                    case OpCode.pop_ax:
                        yield return new(pos, first, OpBase.pop, args: [Register.ax]);
                        break;
                    case OpCode.pop_bp:
                        yield return new(pos, first, OpBase.pop, args: [Register.bp]);
                        break;
                    case OpCode.pop_bx:
                        yield return new(pos, first, OpBase.pop, args: [Register.bx]);
                        break;
                    case OpCode.pop_dx:
                        yield return new(pos, first, OpBase.pop, args: [Register.dx]);
                        break;
                    case OpCode.pop_es:
                        yield return new(pos, first, OpBase.pop, args: [Register.es]);
                        break;
                    case OpCode.pop_si:
                        yield return new(pos, first, OpBase.pop, args: [Register.si]);
                        break;
                    case OpCode.pop_cx:
                        yield return new(pos, first, OpBase.pop, args: [Register.cx]);
                        break;
                    case OpCode.pop_di:
                        yield return new(pos, first, OpBase.pop, args: [Register.di]);
                        break;
                    case OpCode.pop_ds:
                        yield return new(pos, first, OpBase.pop, args: [Register.ds]);
                        break;
                    case OpCode.pop_ss:
                        yield return new(pos, first, OpBase.pop, args: [Register.ss]);
                        break;
                    case OpCode.pop_sp:
                        yield return new(pos, first, OpBase.pop, args: [Register.sp]);
                        break;
                    case OpCode.popa:
                        yield return new(pos, first, OpBase.popa);
                        break;
                    case OpCode.popf:
                        yield return new(pos, first, OpBase.popf);
                        break;
                    case OpCode.push_ax:
                        yield return new(pos, first, OpBase.push, args: [Register.ax]);
                        break;
                    case OpCode.push_bx:
                        yield return new(pos, first, OpBase.push, args: [Register.bx]);
                        break;
                    case OpCode.push_bp:
                        yield return new(pos, first, OpBase.push, args: [Register.bp]);
                        break;
                    case OpCode.push_cx:
                        yield return new(pos, first, OpBase.push, args: [Register.cx]);
                        break;
                    case OpCode.push_cs:
                        yield return new(pos, first, OpBase.push, args: [Register.cs]);
                        break;
                    case OpCode.push_di:
                        yield return new(pos, first, OpBase.push, args: [Register.di]);
                        break;
                    case OpCode.push_ds:
                        yield return new(pos, first, OpBase.push, args: [Register.ds]);
                        break;
                    case OpCode.push_dx:
                        yield return new(pos, first, OpBase.push, args: [Register.dx]);
                        break;
                    case OpCode.push_es:
                        yield return new(pos, first, OpBase.push, args: [Register.es]);
                        break;
                    case OpCode.push_si:
                        yield return new(pos, first, OpBase.push, args: [Register.si]);
                        break;
                    case OpCode.push_sp:
                        yield return new(pos, first, OpBase.push, args: [Register.sp]);
                        break;
                    case OpCode.push_ss:
                        yield return new(pos, first, OpBase.push, args: [Register.ss]);
                        break;
                    case OpCode.pusha:
                        yield return new(pos, first, OpBase.pusha);
                        break;
                    case OpCode.pushf:
                        yield return new(pos, first, OpBase.pushf);
                        break;
                    case OpCode.or:
                        var orFld = stream.NextByteC().Value;
                        if (orFld == 0xC8)
                            yield return new(pos, first, OpBase.or, args: [Register.cx, Register.ax.With(orFld)]);
                        else if (orFld == 0xC1)
                            yield return new(pos, first, OpBase.or, args: [Register.ax, Register.cx.With(orFld)]);
                        else
                            yield return new(pos, first, OpBase.or, args: [Register.ax, Register.ax.With(orFld)]);
                        break;
                    case OpCode.rep:
                        yield return new(pos, first, OpBase.rep);
                        break;
                    case OpCode.repne:
                        yield return new(pos, first, OpBase.repne);
                        break;
                    case OpCode.ret:
                        yield return new(pos, first);
                        break;
                    case OpCode.retf:
                        yield return new(pos, first);
                        break;
                    case OpCode.sahf:
                        yield return new(pos, first, OpBase.sahf);
                        break;
                    case OpCode.salc:
                        yield return new(pos, first);
                        break;
                    case OpCode.scasb:
                        yield return new(pos, first, OpBase.scasb);
                        break;
                    case OpCode.scasw:
                        yield return new(pos, first, OpBase.scasw);
                        break;
                    case OpCode.shl:
                        var shlFl = stream.NextByteC().Value;
                        if (shlFl == 0xE0)
                            yield return new(pos, first, OpBase.shl, args: [Register.ax, Register.cl.With(shlFl)]);
                        else if (shlFl == 0xFB)
                            yield return new(pos, first, OpBase.sar, args: [Register.bx, Register.cl.With(shlFl)]);
                        else
                            yield return new(pos, first, OpBase.sar, args: [Register.si, Register.cl.With(shlFl)]);
                        break;
                    case OpCode.shl_one:
                        var shlbFl = stream.NextByteC().Value;
                        var shOneArg = new ImplicitArg(1);
                        if (shlbFl == 0xE3)
                            yield return new(pos, first, OpBase.shl, args: [Register.bx.With(shlbFl), shOneArg]);
                        break;
                    case OpCode.ss:
                        yield return new(pos, first);
                        break;
                    case OpCode.sti:
                        yield return new(pos, first);
                        break;
                    case OpCode.stc:
                        yield return new(pos, first);
                        break;
                    case OpCode.std:
                        yield return new(pos, first);
                        break;
                    case OpCode.stosb:
                        yield return new(pos, first, OpBase.stosb);
                        break;
                    case OpCode.stosw:
                        yield return new(pos, first, OpBase.stosw);
                        break;
                    case OpCode.sub_ax:
                        yield return new(pos, first, OpBase.sub, args: [Register.ax, stream.NextRegC()]);
                        break;
                    case OpCode.test:
                        var testMod = stream.NextByteC().Value;
                        if (testMod == 0xF6)
                            yield return new(pos, first, OpBase.test, args: [Register.si, Register.si.With(testMod)]);
                        else if (testMod == 0xC0)
                            yield return new(pos, first, OpBase.test, args: [Register.ax, Register.ax.With(testMod)]);
                        else if (testMod == 0xDB)
                            yield return new(pos, first, OpBase.test, args: [Register.bx, Register.bx.With(testMod)]);
                        else
                            yield return new(pos, first, OpBase.test, args: [Register.dx, Register.dx.With(testMod)]);
                        break;
                    case OpCode.wait:
                        yield return new(pos, first, OpBase.wait);
                        break;
                    case OpCode.xchg_ax_di:
                        yield return new(pos, first, OpBase.xchg, args: [Register.ax, Register.di]);
                        break;
                    case OpCode.xchg_ax_dx:
                        yield return new(pos, first, OpBase.xchg, args: [Register.ax, Register.dx]);
                        break;
                    case OpCode.xchg_ax_si:
                        yield return new(pos, first, OpBase.xchg, args: [Register.ax, Register.si]);
                        break;
                    case OpCode.xchg_ax_bp:
                        yield return new(pos, first, OpBase.xchg, args: [Register.ax, Register.bp]);
                        break;
                    case OpCode.xchg_ax_bx:
                        yield return new(pos, first, OpBase.xchg, args: [Register.ax, Register.bx]);
                        break;
                    case OpCode.xchg_ax_cx:
                        yield return new(pos, first, OpBase.xchg, args: [Register.ax, Register.cx]);
                        break;
                    case OpCode.xchg_ax_sp:
                        yield return new(pos, first, OpBase.xchg, args: [Register.ax, Register.sp]);
                        break;
                    case OpCode.xlatb:
                        yield return new(pos, first, OpBase.xlatb);
                        break;
                    case OpCode.xor:
                        var xorFl = stream.NextByteC().Value;
                        if (xorFl == 0xF6)
                            yield return new(pos, first, OpBase.xor, args: [Register.si, Register.si.With(xorFl)]);
                        else if (xorFl == 0xC1)
                            yield return new(pos, first, OpBase.xor, args: [Register.ax, Register.cx.With(xorFl)]);
                        else if (xorFl == 0xD2)
                            yield return new(pos, first, OpBase.xor, args: [Register.dx, Register.dx.With(xorFl)]);
                        else if (xorFl == 0xC8)
                            yield return new(pos, first, OpBase.xor, args: [Register.cx, Register.ax.With(xorFl)]);
                        else
                            yield return new(pos, first, OpBase.xor, args: [Register.ax, Register.ax.With(xorFl)]);
                        break;
                }
            }
        }
    }
}