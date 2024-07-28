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
                        yield return new(pos, first, OpBase.add, args: [Register.ax, stream.NextRegC()]);
                        break;
                    case OpCode.and_ax:
                        yield return new(pos, first, OpBase.and, args: [Register.ax, stream.NextRegC()]);
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
                        else
                            yield return new(pos, first, OpBase.cmp, args: [Register.si, Register.dx.With(cmpFl)]);
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
                    case OpCode.jmp_short:
                        yield return new(pos, first, OpBase.jmp, Modifier.@short, [stream.NextByteC(isSkip: true)]);
                        break;
                    case OpCode.jl:
                        yield return new(pos, first, args: [stream.NextByteC(isSkip: true)]);
                        break;
                    case OpCode.jnl:
                        yield return new(pos, first, args: [stream.NextByteC(isSkip: true)]);
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
                    case OpCode.mov_ax:
                        yield return new(pos, first, OpBase.mov, args: [Register.ax, stream.NextRegC()]);
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
                        else if (mulFirst == 0xE1)
                            yield return new(pos, first, OpBase.mul, args: [Register.cx.With(mulFirst)]);
                        else
                            yield return new(pos, first, OpBase.idiv, args: [Register.cx.With(0xF9)]);
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
                    case OpCode.or_ax:
                        yield return new(pos, first, OpBase.or, args: [Register.ax, stream.NextRegC()]);
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
                        else
                            yield return new(pos, first, OpBase.sar, args: [Register.si, Register.cl.With(shlFl)]);
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
                    case OpCode.xor_ax:
                        yield return new(pos, first, OpBase.xor, args: [Register.ax, stream.NextRegC()]);
                        break;
                }
            }
        }
    }
}