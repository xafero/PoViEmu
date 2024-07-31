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
                    case OpCode.aaa:
                        yield return new(pos, first, OpBase.aaa);
                        break;
                    case OpCode.aad:
                        var aadSecond = stream.NextByteC();
                        yield return new(pos, first, args: [aadSecond]);
                        break;
                    case OpCode.aam:
                        var aamSecond = stream.NextByteC();
                        yield return new(pos, first, OpBase.aam, args: [aamSecond]);
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
                        if (addaFld == 0x2B)
                            yield return new(pos, first, OpBase.add, args: [Register.bp, Register.bp.Plus(Register.di, addaFld)]);
                        else if (addaFld == 0xD0)
                            yield return new(pos, first, OpBase.add, args: [Register.dx, Register.ax.With(addaFld)]);
                        else if (addaFld == 0xC1)
                            yield return new(pos, first, OpBase.add, args: [Register.ax, Register.cx.With(addaFld)]);
                        else if (addaFld == 0xC3)
                            yield return new(pos, first, OpBase.add, args: [Register.ax, Register.bx.With(addaFld)]);
                        else
                            yield return new(pos, first, OpBase.add, args: [Register.ax, Register.ax.With(addaFld)]);
                        break;
                    case OpCode.add_cl:
                        var addcFld = stream.NextByteC().Value;
                        if (addcFld == 0xCE)
                            yield return new(pos, first, OpBase.add, args: [Register.cl, Register.dh.With(addcFld)]);
                        break;
                    case OpCode.adc:
                        var adcFld = stream.NextByteC().Value;
                        var adcFvl = stream.NextShortC();
                        if (adcFld == 0xC1)
                            yield return new(pos, first, OpBase.add, args: [Register.cx.With(adcFld), adcFvl]);
                        else
                            yield return new(pos, first, OpBase.adc, args: [Register.dx.With(adcFld), adcFvl]);
                        break;
                    case OpCode.adc_al:
                        var adaFld = stream.NextByteC();
                        yield return new(pos, first, OpBase.adc, args: [Register.al, adaFld]);
                        break;
                    case OpCode.add_dh:
                        var addhFld = stream.NextByteC().Value;
                        if (addhFld == 0x34)
                            yield return new(pos, first, OpBase.add,
                                args: [Register.si.Plus(null, addhFld), Register.dh]);
                        else if (addhFld == 0xD0)
                            yield return new(pos, first, OpBase.add, args: [Register.al, Register.dl.With(addhFld)]);
                        break;
                    case OpCode.adc_ch:
                        var adhFld = stream.NextByteC().Value;
                        if (adhFld == 0xDD)
                            yield return new(pos, first, OpBase.adc, args: [Register.ch, Register.bl.With(adhFld)]);
                        else if (adhFld == 0x2C)
                            yield return new(pos, first, OpBase.adc,
                                args: [Register.si.Plus(null, adhFld), Register.ch]);
                        break;
                    case OpCode.adc_ah:
                        var adyFld = stream.NextByteC().Value;
                        if (adyFld == 0xE5)
                            yield return new(pos, first, OpBase.adc, args: [Register.ah, Register.ch.With(adyFld)]);
                        else if (adyFld == 0xC4)
                            yield return new(pos, first, OpBase.adc, args: [Register.al, Register.ah.With(adyFld)]);
                        break;
                    case OpCode.adc_sp:
                        var adsFld = stream.NextByteC().Value;
                        if (adsFld == 0xFE)
                            yield return new(pos, first, OpBase.adc, args: [Register.si, Register.di.With(adsFld)]);
                        else
                            yield return new(pos, first, OpBase.adc, args: [Register.sp, Register.ax.With(adsFld)]);
                        break;
                    case OpCode.and:
                        var andFld = stream.NextByteC().Value;
                        if (andFld == 0xD8)
                            yield return new(pos, first, OpBase.and, args: [Register.bx, Register.ax.With(andFld)]);
                        else if (andFld == 0xDC)
                            yield return new(pos, first, OpBase.and, args: [Register.bx, Register.sp.With(andFld)]);
                        else if (andFld == 0xC1)
                            yield return new(pos, first, OpBase.and, args: [Register.ax, Register.cx.With(andFld)]);
                        else if (andFld == 0xE9)
                            yield return new(pos, first, OpBase.and, args: [Register.bp, Register.cx.With(andFld)]);
                        else if (andFld == 0x28)
                            yield return new(pos, first, OpBase.and,
                                args: [Register.bp, Register.bx.Plus(Register.si, andFld)]);
                        else if (andFld == 0x02)
                            yield return new(pos, first, OpBase.and,
                                args: [Register.ax, Register.bp.Plus(Register.si, andFld)]);
                        else
                            yield return new(pos, first, OpBase.and, args: [Register.ax, Register.ax.With(andFld)]);
                        break;
                    case OpCode.and_dx:
                        var andxFld = stream.NextByteC().Value;
                        if (andxFld == 0x10)
                            yield return new(pos, first, OpBase.and, args: [Register.bx.Plus(Register.si, andxFld), Register.dx]);                        
                        else if (andxFld == 0xF9)
                            yield return new(pos, first, OpBase.and, args: [Register.cx, Register.di.With(andxFld)]);
                        break;
                    case OpCode.and_ah:
                        var andhFld = stream.NextByteC().Value;
                        if (andhFld == 0xFB)
                            yield return new(pos, first, OpBase.and, args: [Register.bh, Register.bl.With(andhFld)]);
                        else if (andhFld == 0x23)
                            yield return new(pos, first, OpBase.and, args: [Register.ah, Register.bp.Plus(Register.di, andhFld)]);
                        break;
                    case OpCode.and_al:
                        var adlFld = stream.NextByteC();
                        yield return new(pos, first, OpBase.and, args: [Register.al, adlFld]);
                        break;
                    case OpCode.and_dl:
                        var andlFld = stream.NextByteC().Value;
                        if (andlFld == 0x14)
                            yield return new(pos, first, OpBase.and,
                                args: [Register.si.Plus(null, andlFld), Register.dl]);
                        break;
                    case OpCode.arpl_bp:
                        var apbFld = stream.NextByteC().Value;
                        if (apbFld == 0xFD)
                            yield return new(pos, first, OpBase.arpl, args: [Register.bp, Register.di.With(apbFld)]);
                        else if (apbFld == 0xFA)
                            yield return new(pos, first, OpBase.arpl, args: [Register.dx, Register.di.With(apbFld)]);
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
                        else if (cmpFl == 0xC7)
                            yield return new(pos, first, OpBase.cmp, args: [Register.ax, Register.di.With(cmpFl)]);
                        else if (cmpFl == 0xCA)
                            yield return new(pos, first, OpBase.cmp, args: [Register.cx, Register.dx.With(cmpFl)]);
                        else if (cmpFl == 0xCD)
                            yield return new(pos, first, OpBase.cmp, args: [Register.cx, Register.bp.With(cmpFl)]);
                        else if (cmpFl == 0xD3)
                            yield return new(pos, first, OpBase.cmp, args: [Register.dx, Register.bx.With(cmpFl)]);
                        else if (cmpFl == 0xD8)
                            yield return new(pos, first, OpBase.cmp, args: [Register.bx, Register.ax.With(cmpFl)]);
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
                    case OpCode.cmp_cx:
                        var cmpcFl = stream.NextByteC().Value;
                        if (cmpcFl == 0xD1)
                            yield return new(pos, first, OpBase.cmp, args: [Register.cx, Register.dx.With(cmpcFl)]);
                        else if (cmpcFl == 0xD4)
                            yield return new(pos, first, OpBase.cmp, args: [Register.sp, Register.dx.With(cmpcFl)]);
                        else if (cmpcFl == 0x25)
                            yield return new(pos, first, OpBase.cmp, args: [Register.di.Plus(null, cmpcFl), Register.sp]);
                        break;
                    case OpCode.cmp_bl:
                        var cmpobFl = stream.NextByteC().Value;
                        if (cmpobFl == 0x1D)
                            yield return new(pos, first, OpBase.cmp,
                                args: [Register.bl, Register.di.Plus(null, cmpobFl)]);
                        break;
                    case OpCode.cmp_al:
                        var cmpaFl = stream.NextByteC();
                        yield return new(pos, first, OpBase.cmp, args: [Register.al, cmpaFl]);
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
                    case OpCode.fcomip:
                        var fcmpFld = stream.NextByteC().Value;
                        if (fcmpFld == 0xF0)
                            yield return new(pos, first, args: [Register.st0.With(fcmpFld)]);
                        break;
                    case OpCode.ficom:
                        var ficFld = stream.NextByteC().Value;
                        if (ficFld == 0x17)
                            yield return new(pos, first, mod: Modifier.word, args: [Register.bx.Plus(null, ficFld)]);
                        else if (ficFld == 0x22)
                            yield return new(pos, first, OpBase.fisub, mod: Modifier.word,
                                args: [Register.bp.Plus(Register.si, ficFld)]);
                        break;
                    case OpCode.fstp:
                        var fstFld = stream.NextByteC().Value;
                        if (fstFld == 0xDF)
                            yield return new(pos, first, args: [Register.st7.With(fstFld)]);
                        else if (fstFld == 0xDE)
                            yield return new(pos, first, args: [Register.st6.With(fstFld)]);
                        break;
                    case OpCode.gs:
                        yield return new(pos, first);
                        break;
                    case OpCode.hlt:
                        yield return new(pos, first, OpBase.hlt);
                        break;
                    case OpCode.in_al:
                        var inaFl = stream.NextByteC();
                        yield return new(pos, first, OpBase.@in, args: [Register.al, inaFl]);
                        break;
                    case OpCode.in_ax:
                        var inaxFl = stream.NextByteC();
                        yield return new(pos, first, OpBase.@in, args: [Register.ax, inaxFl]);
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
                    case OpCode.@int:
                        var intFld = stream.NextByteC();
                        yield return new(pos, first, args: [intFld]);
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
                    case OpCode.jcxz:
                        var jcxFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jcxFld).IsSigned = true;
                        yield return new(pos, first, args: [jcxFld]);
                        break;
                    case OpCode.jc:
                        var jcFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jcFld).IsSigned = true;
                        yield return new(pos, first, args: [jcFld]);
                        break;
                    case OpCode.jg:
                        var jgFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jgFld).IsSigned = true;
                        yield return new(pos, first, args: [jgFld]);
                        break;
                    case OpCode.jng:
                        var jngFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jngFld).IsSigned = true;
                        yield return new(pos, first, args: [jngFld]);
                        break;
                    case OpCode.jna:
                        var jnaFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnaFld).IsSigned = true;
                        yield return new(pos, first, args: [jnaFld]);
                        break;
                    case OpCode.jmp_short:
                        yield return new(pos, first, OpBase.jmp, Modifier.@short, [stream.NextByteC(isSkip: true)]);
                        break;
                    case OpCode.jmp_far:
                        var jfFld = stream.NextByteC().Value;
                        if (jfFld == 0x2B)
                            yield return new(pos, first, OpBase.jmp, Modifier.far,
                                [Register.bp.Plus(Register.di, jfFld)]);
                        else if (jfFld == 0x01)
                            yield return new(pos, first, OpBase.inc, Modifier.word,
                                [Register.bx.Plus(Register.di, jfFld)]);
                        else if (jfFld == 0x29)
                            yield return new(pos, first, OpBase.jmp, Modifier.far,
                                [Register.bx.Plus(Register.di, jfFld)]);
                        break;
                    case OpCode.jl:
                        var jlFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jlFld).IsSigned = true;
                        yield return new(pos, first, args: [jlFld]);
                        break;
                    case OpCode.ja:
                        var jaFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jaFld).IsSigned = true;
                        yield return new(pos, first, args: [jaFld]);
                        break;
                    case OpCode.jpo:
                        var jpoFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jpoFld).IsSigned = true;
                        yield return new(pos, first, args: [jpoFld]);
                        break;
                    case OpCode.jno:
                        var jnoFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnoFld).IsSigned = true;
                        yield return new(pos, first, args: [jnoFld]);
                        break;
                    case OpCode.jnl:
                        var jnlFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnlFld).IsSigned = true;
                        yield return new(pos, first, args: [jnlFld]);
                        break;
                    case OpCode.jnz:
                        yield return new(pos, first, args: [stream.NextByteC(isSkip: true)]);
                        break;
                    case OpCode.jo:
                        var joFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)joFld).IsSigned = true;
                        yield return new(pos, first, args: [joFld]);
                        break;
                    case OpCode.js:
                        var jsFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jsFld).IsSigned = true;
                        yield return new(pos, first, args: [jsFld]);
                        break;
                    case OpCode.jz:
                        var jzFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jzFld).IsSigned = true;
                        yield return new(pos, first, args: [jzFld]);
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
                    case OpCode.loop:
                        var loopFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)loopFld).IsSigned = true;
                        yield return new(pos, first, args: [loopFld]);
                        break;
                    case OpCode.loope:
                        var loopeFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)loopeFld).IsSigned = true;
                        yield return new(pos, first, args: [loopeFld]);
                        break;
                    case OpCode.loopne:
                        var loopnFld = stream.NextByteC(isSkip: true);
                        yield return new(pos, first, args: [loopnFld]);
                        break;
                    case OpCode.mov_ah:
                        yield return new(pos, first, OpBase.mov, args: [Register.ah, stream.NextByteC()]);
                        break;
                    case OpCode.mov_bh:
                        yield return new(pos, first, OpBase.mov, args: [Register.bh, stream.NextByteC()]);
                        break;
                    case OpCode.mov_ch:
                        yield return new(pos, first, OpBase.mov, args: [Register.ch, stream.NextByteC()]);
                        break;
                    case OpCode.mov_al:
                        var maa = stream.NextByteC().Value;
                        if (maa == 0xEB)
                        {
                            yield return new(pos, first, OpBase.mov, args: [Register.ch.With(maa), Register.bl]);
                        }
                        else
                        {
                            var mab = stream.NextByteC().Value;
                            yield return new(pos, first, OpBase.mov,
                                args: [Register.al.With(maa), Register.di.Plus(mab)]);
                        }
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
                    case OpCode.mov_al_b:
                        yield return new(pos, first, OpBase.mov, args: [Register.al, stream.NextByteC()]);
                        break;
                    case OpCode.mov_bl:
                        yield return new(pos, first, OpBase.mov, args: [Register.bl, stream.NextByteC()]);
                        break;
                    case OpCode.mov_ax_s:
                        yield return new(pos, first, OpBase.mov, args: [Register.ax, stream.NextShortC()]);
                        break;
                    case OpCode.mov_bx:
                        yield return new(pos, first, OpBase.mov, args: [Register.bx, stream.NextShortC()]);
                        break;
                    case OpCode.mov_ax:
                        var mcaFl = stream.NextByteC().Value;
                        if (mcaFl == 0xC7)
                            yield return new(pos, first, OpBase.mov, args: [Register.di, Register.ax.With(mcaFl)]);
                        else if (mcaFl == 0xFA)
                            yield return new(pos, first, OpBase.mov, args: [Register.dx, Register.di.With(mcaFl)]);
                        break;
                    case OpCode.mov_cl:
                        yield return new(pos, first, OpBase.mov, args: [Register.cl, stream.NextByteC()]);
                        break;
                    case OpCode.mov_cl_x:
                        var mclFl = stream.NextByteC().Value;
                        if (mclFl == 0xEE)
                            yield return new(pos, first, OpBase.mov, args: [Register.dh, Register.ch.With(mclFl)]);
                        else if (mclFl == 0xFA)
                            yield return new(pos, first, OpBase.mov, args: [Register.dl, Register.bh.With(mclFl)]);
                        else if (mclFl == 0xDB)
                            yield return new(pos, first, OpBase.mov, args: [Register.bl, Register.bl.With(mclFl)]);
                        else
                            yield return new(pos, first, OpBase.mov, args: [Register.bx.Plus(Register.si, mclFl), Register.cl]);
                        break;
                    case OpCode.mov_cs:
                        var mcsFl = stream.NextByteC().Value;
                        if (mcsFl == 0xFA)
                            yield return new(pos, first, OpBase.mov, args: [Register.segr7, Register.dx.With(mcsFl)]);
                        else
                            yield return new(pos, first, OpBase.mov,
                                args: [Register.cs, Register.si.Plus(null, mcsFl)]);
                        break;
                    case OpCode.mov_cx:
                        yield return new(pos, first, OpBase.mov, args: [Register.cx, stream.NextShortC()]);
                        break;
                    case OpCode.mov_dh:
                        yield return new(pos, first, OpBase.mov, args: [Register.dh, stream.NextByteC()]);
                        break;
                    case OpCode.mov_dl:
                        yield return new(pos, first, OpBase.mov, args: [Register.dl, stream.NextByteC()]);
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
                    case OpCode.fmul:
                        var fmulFirst = stream.NextByteC().Value;
                        if (fmulFirst == 0x0A)
                            yield return new(pos, first, mod: Modifier.dword, args: [Register.bp.Plus(Register.si, fmulFirst)]);
                        else if (fmulFirst == 0xF0)
                            yield return new(pos, first, OpBase.fdiv, args: [Register.st0.With(fmulFirst)]);
                        break;
                    case OpCode.fnstenv:
                        var fnsFirst = stream.NextByteC().Value;
                        if (fnsFirst == 0x31)
                            yield return new(pos, first, args: [Register.bx.Plus(Register.di, fnsFirst)]);
                        else if (fnsFirst == 0x39)
                            yield return new(pos, first, OpBase.fnstcw, args: [Register.bx.Plus(Register.di, fnsFirst)]);
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
                    case OpCode.out_ax:
                        var outaxFld = stream.NextByteC();
                        yield return new(pos, first, OpBase.@out, args: [outaxFld, Register.ax]);
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
                    case OpCode.push:
                        var pushbFl = stream.NextBytepC();
                        pushbFl.Signed = true;
                        yield return new(pos, first, OpBase.push, args: [pushbFl]);
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
                        else if (orFld == 0x11)
                            yield return new(pos, first, OpBase.or,
                                args: [Register.dx, Register.bx.Plus(Register.di, orFld)]);
                        else if (orFld == 0x2C)
                            yield return new(pos, first, OpBase.or, args: [Register.bp, Register.si.Plus(null, orFld)]);
                        else
                            yield return new(pos, first, OpBase.or, args: [Register.ax, Register.ax.With(orFld)]);
                        break;
                    case OpCode.or_bl:
                        var orbFld = stream.NextByteC().Value;
                        if (orbFld == 0x1A)
                            yield return new(pos, first, OpBase.or, args: [Register.bl, Register.bp.Plus(Register.si, orbFld)]);
                        else if (orbFld == 0xE9)
                            yield return new(pos, first, OpBase.or, args: [Register.ch, Register.cl.With(orbFld)]);
                        break;
                    case OpCode.or_bh:
                        var orhFld = stream.NextByteC().Value;
                        if (orhFld == 0xCF)
                            yield return new(pos, first, OpBase.or, args: [Register.bh, Register.cl.With(orhFld)]);
                        break;
                    case OpCode.or_bx:
                        var orxFld = stream.NextByteC().Value;
                        if (orxFld == 0x17)
                            yield return new(pos, first, OpBase.or, args: [Register.bx.Plus(null, orxFld), Register.dx]);
                        break;
                    case OpCode.or_al:
                        var oraFld = stream.NextByteC();
                        yield return new(pos, first, OpBase.or, args: [Register.al, oraFld]);
                        break;
                    case OpCode.out_al:
                        var outaFld = stream.NextByteC();
                        yield return new(pos, first, OpBase.@out, args: [outaFld, Register.al]);
                        break;
                    case OpCode.rcl:
                        var rclFl = stream.NextByteC().Value;
                        var rclOneArg = new ImplicitArg(1);
                        if (rclFl == 0xD6)
                            yield return new(pos, first, args: [Register.dh.With(rclFl), rclOneArg]);
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
                        else if (shlbFl == 0x00)
                            yield return new(pos, first, OpBase.rol, Modifier.word,
                                args: [Register.bx.Plus(Register.si, shlbFl), shOneArg]);
                        break;
                    case OpCode.shl_bp:
                        var shbFl = stream.NextByteC().Value;
                        if (shbFl == 0x22)
                            yield return new(pos, first, OpBase.shl, mod: Modifier.@byte,
                                args: [Register.bp.Plus(Register.si, shbFl), Register.cl]);
                        else if (shbFl == 0x19)
                            yield return new(pos, first, OpBase.rcr, mod: Modifier.@byte,
                                args: [Register.bx.Plus(Register.di, shbFl), Register.cl]);
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
                        var saxFld = stream.NextByteC().Value;
                        if (saxFld == 0xF9)
                            yield return new(pos, first, OpBase.sub, args: [Register.di, Register.cx.With(saxFld)]);
                        else if (saxFld == 0xC1)
                            yield return new(pos, first, OpBase.sub, args: [Register.ax, Register.cx.With(saxFld)]);
                        else if (saxFld == 0xC3)
                            yield return new(pos, first, OpBase.sub, args: [Register.ax, Register.bx.With(saxFld)]);
                        else
                            yield return new(pos, first, OpBase.sub, args: [Register.ax, Register.al.With(saxFld)]);
                        break;
                    case OpCode.sub_al:
                        var salFld = stream.NextByteC();
                        yield return new(pos, first, OpBase.sub, args: [Register.al, salFld]);
                        break;
                    case OpCode.sub_ah:
                        var subaMod = stream.NextByteC().Value;
                        if (subaMod == 0xC6)
                            yield return new(pos, first, OpBase.sub, args: [Register.al, Register.dh.With(subaMod)]);
                        break;
                    case OpCode.sub_dh:
                        var subdMod = stream.NextByteC().Value;
                        if (subdMod == 0x32)
                            yield return new(pos, first, OpBase.sub,
                                args: [Register.bp.Plus(Register.si, subdMod), Register.dh]);
                        else if (subdMod == 0x37)
                            yield return new(pos, first, OpBase.sub,
                                args: [Register.bx.Plus(null, subdMod), Register.dh]);
                        else if (subdMod == 0xF1)
                            yield return new(pos, first, OpBase.sub, args: [Register.cl, Register.dh.With(subdMod)]);
                        break;
                    case OpCode.sbb_bx:
                        var sbbxMod = stream.NextByteC().Value;
                        if (sbbxMod == 0x1A)
                            yield return new(pos, first, OpBase.sbb, args: [Register.bp.Plus(Register.si, sbbxMod), Register.bx]);
                        break;
                    case OpCode.sbb_al:
                        var sbbMod = stream.NextByteC().Value;
                        if (sbbMod == 0xE8)
                            yield return new(pos, first, OpBase.sbb, args: [Register.al, Register.ch.With(sbbMod)]);
                        else if (sbbMod == 0xD3)
                            yield return new(pos, first, OpBase.sbb, args: [Register.bl, Register.dl.With(sbbMod)]);
                        else if (sbbMod == 0xFF)
                            yield return new(pos, first, OpBase.sbb, args: [Register.bh, Register.bh.With(sbbMod)]);
                        break;
                    case OpCode.sbb_cl:
                        var sbcMod = stream.NextByteC().Value;
                        if (sbcMod == 0xC1)
                            yield return new(pos, first, OpBase.sbb, args: [Register.al, Register.cl.With(sbcMod)]);
                        break;
                    case OpCode.test:
                        var testMod = stream.NextByteC().Value;
                        if (testMod == 0xF6)
                            yield return new(pos, first, OpBase.test, args: [Register.si, Register.si.With(testMod)]);
                        else if (testMod == 0xC0)
                            yield return new(pos, first, OpBase.test, args: [Register.ax, Register.ax.With(testMod)]);
                        else if (testMod == 0x1C)
                            yield return new(pos, first, OpBase.test,
                                args: [Register.si.Plus(null, testMod), Register.bx]);
                        else if (testMod == 0xDB)
                            yield return new(pos, first, OpBase.test, args: [Register.bx, Register.bx.With(testMod)]);
                        else
                            yield return new(pos, first, OpBase.test, args: [Register.dx, Register.dx.With(testMod)]);
                        break;
                    case OpCode.test_al:
                        var tsaMod = stream.NextByteC();
                        yield return new(pos, first, OpBase.test, args: [Register.al, tsaMod]);
                        break;
                    case OpCode.test_dl:
                        var tsdMod = stream.NextByteC().Value;
                        if (tsdMod == 0x14)
                            yield return new(pos, first, OpBase.test,
                                args: [Register.si.Plus(null, tsdMod), Register.dl]);
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
                    case OpCode.xchg_dh_al:
                        var xdaFl = stream.NextByteC().Value;
                        if (xdaFl == 0xC6)
                            yield return new(pos, first, OpBase.xchg, args: [Register.al, Register.dh.With(xdaFl)]);
                        else if (xdaFl == 0xEE)
                            yield return new(pos, first, OpBase.xchg, args: [Register.ch, Register.dh.With(xdaFl)]);
                        else if (xdaFl == 0x13)
                            yield return new(pos, first, OpBase.xchg, args: [Register.dl, Register.bp.Plus(Register.di, xdaFl)]);
                        break;
                    case OpCode.xchg_bp_si:
                        var xdbFl = stream.NextByteC().Value;
                        if (xdbFl == 0xEE)
                            yield return new(pos, first, OpBase.xchg, args: [Register.bp, Register.si.With(xdbFl)]);
                        else if (xdbFl == 0x2C)
                            yield return new(pos, first, OpBase.xchg,
                                args: [Register.bp, Register.si.Plus(null, xdbFl)]);
                        else if (xdbFl == 0xE6)
                            yield return new(pos, first, OpBase.xchg, args: [Register.sp, Register.si.With(xdbFl)]);
                        break;
                    case OpCode.xlatb:
                        yield return new(pos, first, OpBase.xlatb);
                        break;
                    case OpCode.xor_al:
                        var xoraFl = stream.NextByteC();
                        yield return new(pos, first, OpBase.xor, args: [Register.al, xoraFl]);
                        break;
                    case OpCode.xor_bp:
                        var xorpFl = stream.NextByteC().Value;
                        if (xorpFl == 0x03)
                            yield return new(pos, first, OpBase.xor, args: [Register.bp.Plus(Register.di, xorpFl), Register.al]);
                        break;
                    case OpCode.xor_ch:
                        var xorcFl = stream.NextByteC().Value;
                        if (xorcFl == 0x2C)
                            yield return new(pos, first, OpBase.xor, args: [Register.ch, Register.si.Plus(null, xorcFl)]);
                        else if (xorcFl == 0xC6)
                            yield return new(pos, first, OpBase.xor, args: [Register.al, Register.dh.With(xorcFl)]);
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