using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;
using R = PoViEmu.Core.Machine.Ops.Register;
using O = PoViEmu.Core.Machine.Ops.OpCode;

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
                    case 0x37:
                        yield return new(pos, first, O.aaa);
                        break;
                    case 0xD5:
                        var aadSecond = stream.NextByteC();
                        yield return new(pos, first, O.aad, args: [aadSecond]);
                        break;
                    case 0xD4:
                        var aamSecond = stream.NextByteC();
                        yield return new(pos, first, O.aam, args: [aamSecond]);
                        break;
                    case 0x3F:
                        yield return new(pos, first, O.aas);
                        break;
                    case 0x80:
                        var addFirst = stream.NextRegC();
                        var addSecond = stream.NextByteC();
                        yield return new(pos, first, O.add, args: [addFirst, addSecond]);
                        break;
                    case 0x04:
                        var adlaFld = stream.NextByteC();
                        yield return new(pos, first, O.add, args: [R.al, adlaFld]);
                        break;
                    case 0x03:
                        var addaFld = stream.NextByteC().Value;
                        if (addaFld == 0x2B)
                            yield return new(pos, first, O.add, args: [R.bp, R.bp.Plus(R.di, addaFld)]);
                        else if (addaFld == 0x3A)
                            yield return new(pos, first, O.add, args: [R.di, R.bp.Plus(R.si, addaFld)]);
                        else if (addaFld == 0xD0)
                            yield return new(pos, first, O.add, args: [R.dx, R.ax.With(addaFld)]);
                        else if (addaFld == 0xDA)
                            yield return new(pos, first, O.add, args: [R.bx, R.dx.With(addaFld)]);
                        else if (addaFld == 0xC1)
                            yield return new(pos, first, O.add, args: [R.ax, R.cx.With(addaFld)]);
                        else if (addaFld == 0xC3)
                            yield return new(pos, first, O.add, args: [R.ax, R.bx.With(addaFld)]);
                        else if (addaFld == 0xF5)
                            yield return new(pos, first, O.add, args: [R.si, R.bp.With(addaFld)]);
                        else
                            yield return new(pos, first, O.add, args: [R.ax, R.ax.With(addaFld)]);
                        break;
                    case 0x02:
                        var addcFld = stream.NextByteC().Value;
                        if (addcFld == 0xCE)
                            yield return new(pos, first, O.add, args: [R.cl, R.dh.With(addcFld)]);
                        else if (addcFld == 0x33)
                            yield return new(pos, first, O.add, args: [R.dh, R.bp.Plus(R.di, addcFld)]);
                        break;
                    case 0x01:
                        var addiFld = stream.NextByteC().Value;
                        if (addiFld == 0x28)
                            yield return new(pos, first, O.add, args: [R.bx.Plus(R.si, addiFld), R.bp]);
                        else if (addiFld == 0x38)
                            yield return new(pos, first, O.add, args: [R.bx.Plus(R.si, addiFld), R.di]);
                        else if (addiFld == 0xC6)
                            yield return new(pos, first, O.add, args: [R.si, R.ax.With(addiFld)]);
                        break;
                    case 0x81:
                        var adcFld = stream.NextByteC().Value;
                        var adcFvl = stream.NextShortC();
                        if (adcFld == 0xC1)
                            yield return new(pos, first, O.add, args: [R.cx.With(adcFld), adcFvl]);
                        else
                            yield return new(pos, first, O.adc, args: [R.dx.With(adcFld), adcFvl]);
                        break;
                    case 0x14:
                        var adaFld = stream.NextByteC();
                        yield return new(pos, first, O.adc, args: [R.al, adaFld]);
                        break;
                    case 0x00:
                        var addhFld = stream.NextByteC().Value;
                        if (addhFld == 0x34)
                            yield return new(pos, first, O.add, args: [R.si.Plus(null, addhFld), R.dh]);
                        else if (addhFld == 0xD0)
                            yield return new(pos, first, O.add, args: [R.al, R.dl.With(addhFld)]);
                        else if (addhFld == 0xCA)
                            yield return new(pos, first, O.add, args: [R.dl, R.cl.With(addhFld)]);
                        break;
                    case 0x10:
                        var adhFld = stream.NextByteC().Value;
                        if (adhFld == 0xDD)
                            yield return new(pos, first, O.adc, args: [R.ch, R.bl.With(adhFld)]);
                        else if (adhFld == 0xE5)
                            yield return new(pos, first, O.adc, args: [R.ch, R.ah.With(adhFld)]);
                        else if (adhFld == 0xD5)
                            yield return new(pos, first, O.adc, args: [R.ch, R.dl.With(adhFld)]);
                        else if (adhFld == 0x2C)
                            yield return new(pos, first, O.adc, args: [R.si.Plus(null, adhFld), R.ch]);
                        break;
                    case 0x12:
                        var adyFld = stream.NextByteC().Value;
                        if (adyFld == 0xE5)
                            yield return new(pos, first, O.adc, args: [R.ah, R.ch.With(adyFld)]);
                        else if (adyFld == 0x14)
                            yield return new(pos, first, O.adc, args: [R.dl, R.si.Plus(null, adyFld)]);
                        else if (adyFld == 0x30)
                            yield return new(pos, first, O.adc, args: [R.dh, R.bx.Plus(R.si, adyFld)]);
                        else if (adyFld == 0x38)
                            yield return new(pos, first, O.adc, args: [R.bh, R.bx.Plus(R.si, adyFld)]);
                        else if (adyFld == 0xC4)
                            yield return new(pos, first, O.adc, args: [R.al, R.ah.With(adyFld)]);
                        else if (adyFld == 0xFD)
                            yield return new(pos, first, O.adc, args: [R.bh, R.ch.With(adyFld)]);
                        else if (adyFld == 0xFF)
                            yield return new(pos, first, O.adc, args: [R.bh, R.bh.With(adyFld)]);
                        break;
                    case 0x13:
                        var adpFld = stream.NextByteC().Value;
                        if (adpFld == 0xED)
                            yield return new(pos, first, O.adc, args: [R.bp, R.bp.With(adpFld)]);
                        else if (adpFld == 0xD5)
                            yield return new(pos, first, O.adc, args: [R.dx, R.bp.With(adpFld)]);
                        else if (adpFld == 0x3A)
                            yield return new(pos, first, O.adc, args: [R.di, R.bp.Plus(R.si, adpFld)]);
                        break;
                    case 0x11:
                        var adsFld = stream.NextByteC().Value;
                        if (adsFld == 0xFE)
                            yield return new(pos, first, O.adc, args: [R.si, R.di.With(adsFld)]);
                        else if (adsFld == 0x24)
                            yield return new(pos, first, O.adc, args: [R.si.Plus(null, adsFld), R.sp]);
                        else
                            yield return new(pos, first, O.adc, args: [R.sp, R.ax.With(adsFld)]);
                        break;
                    case 0x23:
                        var andFld = stream.NextByteC().Value;
                        if (andFld == 0xD8)
                            yield return new(pos, first, O.and, args: [R.bx, R.ax.With(andFld)]);
                        else if (andFld == 0xDC)
                            yield return new(pos, first, O.and, args: [R.bx, R.sp.With(andFld)]);
                        else if (andFld == 0xC1)
                            yield return new(pos, first, O.and, args: [R.ax, R.cx.With(andFld)]);
                        else if (andFld == 0xE9)
                            yield return new(pos, first, O.and, args: [R.bp, R.cx.With(andFld)]);
                        else if (andFld == 0x19)
                            yield return new(pos, first, O.and, args: [R.bx, R.bx.Plus(R.di, andFld)]);
                        else if (andFld == 0x28)
                            yield return new(pos, first, O.and,
                                args: [R.bp, R.bx.Plus(R.si, andFld)]);
                        else if (andFld == 0x02)
                            yield return new(pos, first, O.and,
                                args: [R.ax, R.bp.Plus(R.si, andFld)]);
                        else
                            yield return new(pos, first, O.and, args: [R.ax, R.ax.With(andFld)]);
                        break;
                    case 0x21:
                        var andxFld = stream.NextByteC().Value;
                        if (andxFld == 0x10)
                            yield return new(pos, first, O.and, args: [R.bx.Plus(R.si, andxFld), R.dx]);
                        else if (andxFld == 0xF9)
                            yield return new(pos, first, O.and, args: [R.cx, R.di.With(andxFld)]);
                        else if (andxFld == 0xE3)
                            yield return new(pos, first, O.and, args: [R.bx, R.sp.With(andxFld)]);
                        break;
                    case 0x22:
                        var andhFld = stream.NextByteC().Value;
                        if (andhFld == 0xFB)
                            yield return new(pos, first, O.and, args: [R.bh, R.bl.With(andhFld)]);
                        else if (andhFld == 0x23)
                            yield return new(pos, first, O.and, args: [R.ah, R.bp.Plus(R.di, andhFld)]);
                        else if (andhFld == 0x14)
                            yield return new(pos, first, O.and, args: [R.dl, R.si.Plus(null, andhFld)]);
                        else if (andhFld == 0xC1)
                            yield return new(pos, first, O.and, args: [R.al, R.cl.With(andhFld)]);
                        else if (andhFld == 0xD1)
                            yield return new(pos, first, O.and, args: [R.dl, R.cl.With(andhFld)]);
                        break;
                    case 0x24:
                        var adlFld = stream.NextByteC();
                        yield return new(pos, first, O.and, args: [R.al, adlFld]);
                        break;
                    case 0x20:
                        var andlFld = stream.NextByteC().Value;
                        if (andlFld == 0x14)
                            yield return new(pos, first, O.and, args: [R.si.Plus(null, andlFld), R.dl]);
                        else if (andlFld == 0x20)
                            yield return new(pos, first, O.and, args: [R.bx.Plus(R.si, andlFld), R.ah]);
                        else if (andlFld == 0xFB)
                            yield return new(pos, first, O.and, args: [R.bl, R.bh.With(andlFld)]);
                        break;
                    case 0x63:
                        var apbFld = stream.NextByteC().Value;
                        if (apbFld == 0xFD)
                            yield return new(pos, first, O.arpl, args: [R.bp, R.di.With(apbFld)]);
                        else if (apbFld == 0xEE)
                            yield return new(pos, first, O.arpl, args: [R.si, R.bp.With(apbFld)]);
                        else if (apbFld == 0xFA)
                            yield return new(pos, first, O.arpl, args: [R.dx, R.di.With(apbFld)]);
                        else if (apbFld == 0xF0)
                            yield return new(pos, first, O.arpl, args: [R.ax, R.si.With(apbFld)]);
                        break;
                    case 0x98:
                        yield return new(pos, first, O.cbw);
                        break;
                    case 0xF8:
                        yield return new(pos, first, O.clc);
                        break;
                    case 0xFC:
                        yield return new(pos, first, O.cld);
                        break;
                    case 0xFA:
                        yield return new(pos, first, O.cli);
                        break;
                    case 0xF5:
                        yield return new(pos, first, O.cmc);
                        break;
                    case 0x3B:
                        var cmpFl = stream.NextByteC().Value;
                        if (cmpFl == 0xD6)
                            yield return new(pos, first, O.cmp, args: [R.dx, R.si.With(cmpFl)]);
                        else if (cmpFl == 0xC7)
                            yield return new(pos, first, O.cmp, args: [R.ax, R.di.With(cmpFl)]);
                        else if (cmpFl == 0xC9)
                            yield return new(pos, first, O.cmp, args: [R.cx, R.cx.With(cmpFl)]);
                        else if (cmpFl == 0xCA)
                            yield return new(pos, first, O.cmp, args: [R.cx, R.dx.With(cmpFl)]);
                        else if (cmpFl == 0xCD)
                            yield return new(pos, first, O.cmp, args: [R.cx, R.bp.With(cmpFl)]);
                        else if (cmpFl == 0xD3)
                            yield return new(pos, first, O.cmp, args: [R.dx, R.bx.With(cmpFl)]);
                        else if (cmpFl == 0xD8)
                            yield return new(pos, first, O.cmp, args: [R.bx, R.ax.With(cmpFl)]);
                        else if (cmpFl == 0xDA)
                            yield return new(pos, first, O.cmp, args: [R.bx, R.dx.With(cmpFl)]);
                        else if (cmpFl == 0xF1)
                            yield return new(pos, first, O.cmp, args: [R.si, R.cx.With(cmpFl)]);
                        else
                            yield return new(pos, first, O.cmp, args: [R.si, R.dx.With(cmpFl)]);
                        break;
                    case 0x3D:
                        var cmpsFl = stream.NextShortC();
                        yield return new(pos, first, O.cmp, args: [R.ax, cmpsFl]);
                        break;
                    case 0x38:
                        var cmphFl = stream.NextByteC().Value;
                        if (cmphFl == 0xCC)
                            yield return new(pos, first, O.cmp, args: [R.ah, R.cl.With(cmphFl)]);
                        else if (cmphFl == 0xE1)
                            yield return new(pos, first, O.cmp, args: [R.cl, R.ah.With(cmphFl)]);
                        break;
                    case 0x39:
                        var cmpcFl = stream.NextByteC().Value;
                        if (cmpcFl == 0xD1)
                            yield return new(pos, first, O.cmp, args: [R.cx, R.dx.With(cmpcFl)]);
                        else if (cmpcFl == 0xD4)
                            yield return new(pos, first, O.cmp, args: [R.sp, R.dx.With(cmpcFl)]);
                        else if (cmpcFl == 0x25)
                            yield return new(pos, first, O.cmp, args: [R.di.Plus(null, cmpcFl), R.sp]);
                        break;
                    case 0x3A:
                        var cmpobFl = stream.NextByteC().Value;
                        if (cmpobFl == 0x1D)
                            yield return new(pos, first, O.cmp, args: [R.bl, R.di.Plus(null, cmpobFl)]);
                        else if (cmpobFl == 0x1A)
                            yield return new(pos, first, O.cmp, args: [R.bl, R.bp.Plus(R.si, cmpobFl)]);
                        else if (cmpobFl == 0xF3)
                            yield return new(pos, first, O.cmp, args: [R.dh, R.bl.With(cmpobFl)]);
                        break;
                    case 0x3C:
                        var cmpaFl = stream.NextByteC();
                        yield return new(pos, first, O.cmp, args: [R.al, cmpaFl]);
                        break;
                    case 0x83:
                        var cmpbFl = stream.NextByteC().Value;
                        var cmpvFl = stream.NextBytepC();
                        if (cmpbFl == 0xFF)
                            yield return new(pos, first, O.cmp, args: [R.di.With(cmpbFl), cmpvFl]);
                        else if (cmpbFl == 0xEB)
                            yield return new(pos, first, O.sub, args: [R.bx.With(cmpbFl), cmpvFl]);
                        else
                            yield return new(pos, first, O.cmp, args: [R.dx.With(cmpbFl), cmpvFl]);
                        break;
                    case 0xA6:
                        yield return new(pos, first, O.cmpsb);
                        break;
                    case 0xA7:
                        yield return new(pos, first, O.cmpsw);
                        break;
                    case 0x2E:
                        yield return new(pos, first, O.cs);
                        break;
                    case 0x99:
                        yield return new(pos, first, O.cwd);
                        break;
                    case 0x27:
                        yield return new(pos, first, O.daa);
                        break;
                    case 0x2F:
                        yield return new(pos, first, O.das);
                        break;
                    case 0x48:
                        yield return new(pos, first, O.dec, args: [R.ax]);
                        break;
                    case 0x4D:
                        yield return new(pos, first, O.dec, args: [R.bp]);
                        break;
                    case 0x4B:
                        yield return new(pos, first, O.dec, args: [R.bx]);
                        break;
                    case 0x49:
                        yield return new(pos, first, O.dec, args: [R.cx]);
                        break;
                    case 0x4F:
                        yield return new(pos, first, O.dec, args: [R.di]);
                        break;
                    case 0x4A:
                        yield return new(pos, first, O.dec, args: [R.dx]);
                        break;
                    case 0x4E:
                        yield return new(pos, first, O.dec, args: [R.si]);
                        break;
                    case 0x4C:
                        yield return new(pos, first, O.dec, args: [R.sp]);
                        break;
                    case 0x3E:
                        yield return new(pos, first, O.ds);
                        break;
                    case 0x26:
                        yield return new(pos, first, O.es);
                        break;
                    case 0x64:
                        yield return new(pos, first, O.fs);
                        break;
                    case 0xDF:
                        var fcmpFld = stream.NextByteC().Value;
                        if (fcmpFld == 0xF0)
                            yield return new(pos, first, O.fcomip, args: [R.st0.With(fcmpFld)]);
                        else if (fcmpFld == 0x2F)
                            yield return new(pos, first, O.fild, Modifier.qword, args: [R.bx.Plus(null, fcmpFld)]);
                        else if (fcmpFld == 0x28)
                            yield return new(pos, first, O.fild, Modifier.qword, args: [R.bx.Plus(R.si, fcmpFld)]);
                        else if (fcmpFld == 0x12)
                            yield return new(pos, first, O.fist, Modifier.word, args: [R.bp.Plus(R.si, fcmpFld)]);
                        break;
                    case 0xDB:
                        var fcmbFld = stream.NextByteC().Value;
                        if (fcmbFld == 0xD2)
                            yield return new(pos, first, O.fcmovnbe, args: [R.st2.With(fcmbFld)]);
                        else if (fcmbFld == 0xF7)
                            yield return new(pos, first, O.fcomi, args: [R.st7.With(fcmbFld)]);
                        else if (fcmbFld == 0x01)
                            yield return new(pos, first, O.fild, Modifier.dword, args: [R.bx.Plus(R.di, fcmbFld)]);
                        break;
                    case 0xDA:
                        var fivFld = stream.NextByteC().Value;
                        if (fivFld == 0xC5)
                            yield return new(pos, first, O.fcmovb, args: [R.st5.With(fivFld)]);
                        else if (fivFld == 0xC7)
                            yield return new(pos, first, O.fcmovb, args: [R.st7.With(fivFld)]);
                        break;
                    case 0xDE:
                        var ficFld = stream.NextByteC().Value;
                        if (ficFld == 0x17)
                            yield return new(pos, first, O.ficom, mod: Modifier.word,
                                args: [R.bx.Plus(null, ficFld)]);
                        else if (ficFld == 0x12)
                            yield return new(pos, first, O.ficom, mod: Modifier.word,
                                args: [R.bp.Plus(R.si, ficFld)]);
                        else if (ficFld == 0xC5)
                            yield return new(pos, first, O.faddp, args: [R.st5.With(ficFld)]);
                        else if (ficFld == 0xEE)
                            yield return new(pos, first, O.fsubp, args: [R.st6.With(ficFld)]);
                        else if (ficFld == 0xF3)
                            yield return new(pos, first, O.fdivrp, args: [R.st3.With(ficFld)]);
                        else if (ficFld == 0x22)
                            yield return new(pos, first, O.fisub, mod: Modifier.word,
                                args: [R.bp.Plus(R.si, ficFld)]);
                        break;
                    case 0xDD:
                        var fstFld = stream.NextByteC().Value;
                        if (fstFld == 0xDF)
                            yield return new(pos, first, O.fstp, args: [R.st7.With(fstFld)]);
                        else if (fstFld == 0xDA)
                            yield return new(pos, first, O.fstp, args: [R.st2.With(fstFld)]);
                        else if (fstFld == 0x3D)
                            yield return new(pos, first, O.fnstsw, args: [R.di.Plus(null, fstFld)]);
                        else if (fstFld == 0x0D)
                            yield return new(pos, first, O.fisttp, Modifier.qword,
                                args: [R.di.Plus(null, fstFld)]);
                        else if (fstFld == 0x32)
                            yield return new(pos, first, O.fnsave, args: [R.bp.Plus(R.si, fstFld)]);
                        else if (fstFld == 0xDE)
                            yield return new(pos, first, O.fstp, args: [R.st6.With(fstFld)]);
                        else if (fstFld == 0x07)
                            yield return new(pos, first, O.fld, Modifier.qword, args: [R.bx.Plus(null, fstFld)]);
                        break;
                    case 0x65:
                        yield return new(pos, first, O.gs);
                        break;
                    case 0xF4:
                        yield return new(pos, first, O.hlt);
                        break;
                    case 0xE4:
                        var inaFl = stream.NextByteC();
                        yield return new(pos, first, O.@in, args: [R.al, inaFl]);
                        break;
                    case 0xE5:
                        var inaxFl = stream.NextByteC();
                        yield return new(pos, first, O.@in, args: [R.ax, inaxFl]);
                        break;
                    case 0xEC:
                        yield return new(pos, first, O.@in, args: [R.al, R.dx]);
                        break;
                    case 0xED:
                        yield return new(pos, first, O.@in, args: [R.ax, R.dx]);
                        break;
                    case 0x40:
                        yield return new(pos, first, O.inc, args: [R.ax]);
                        break;
                    case 0x43:
                        yield return new(pos, first, O.inc, args: [R.bx]);
                        break;
                    case 0x45:
                        yield return new(pos, first, O.inc, args: [R.bp]);
                        break;
                    case 0x41:
                        yield return new(pos, first, O.inc, args: [R.cx]);
                        break;
                    case 0x47:
                        yield return new(pos, first, O.inc, args: [R.di]);
                        break;
                    case 0x42:
                        yield return new(pos, first, O.inc, args: [R.dx]);
                        break;
                    case 0x46:
                        yield return new(pos, first, O.inc, args: [R.si]);
                        break;
                    case 0x44:
                        yield return new(pos, first, O.inc, args: [R.sp]);
                        break;
                    case 0x6C:
                        yield return new(pos, first, O.insb);
                        break;
                    case 0x6D:
                        yield return new(pos, first, O.insw);
                        break;
                    case 0xCD:
                        var intFld = stream.NextByteC();
                        yield return new(pos, first, O.@int, args: [intFld]);
                        break;
                    case 0xF1:
                        yield return new(pos, first, O.int1);
                        break;
                    case 0xCC:
                        yield return new(pos, first, O.int3);
                        break;
                    case 0xCE:
                        yield return new(pos, first, O.into);
                        break;
                    case 0xCF:
                        yield return new(pos, first, O.iret);
                        break;
                    case 0xE3:
                        var jcxFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jcxFld).IsSigned = true;
                        yield return new(pos, first, O.jcxz, args: [jcxFld]);
                        break;
                    case 0x72:
                        var jcFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jcFld).IsSigned = true;
                        yield return new(pos, first, O.jc, args: [jcFld]);
                        break;
                    case 0x7F:
                        var jgFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jgFld).IsSigned = true;
                        yield return new(pos, first, O.jg, args: [jgFld]);
                        break;
                    case 0x7E:
                        var jngFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jngFld).IsSigned = true;
                        yield return new(pos, first, O.jng, args: [jngFld]);
                        break;
                    case 0x79:
                        var jnsFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnsFld).IsSigned = true;
                        yield return new(pos, first, O.jns, args: [jnsFld]);
                        break;
                    case 0x76:
                        var jnaFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnaFld).IsSigned = true;
                        yield return new(pos, first, O.jna, args: [jnaFld]);
                        break;
                    case 0x73:
                        var jncFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jncFld).IsSigned = true;
                        yield return new(pos, first, O.jnc, args: [jncFld]);
                        break;
                    case 0xEB:
                        var jmpsa = stream.NextByteC(isSkip: true);
                        ((SkipArg)jmpsa).IsSigned = true;
                        yield return new(pos, first, O.jmp, Modifier.@short, args: [jmpsa]);
                        break;
                    case 0xFF:
                        var jfFld = stream.NextByteC().Value;
                        if (jfFld == 0x2B)
                            yield return new(pos, first, O.jmp, Modifier.far, [R.bp.Plus(R.di, jfFld)]);
                        else if (jfFld == 0x01)
                            yield return new(pos, first, O.inc, Modifier.word, [R.bx.Plus(R.di, jfFld)]);
                        else if (jfFld == 0x27)
                            yield return new(pos, first, O.jmp, args: [R.bx.Plus(null, jfFld)]);
                        else if (jfFld == 0x29)
                            yield return new(pos, first, O.jmp, Modifier.far, [R.bx.Plus(R.di, jfFld)]);
                        break;
                    case 0x7C:
                        var jlFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jlFld).IsSigned = true;
                        yield return new(pos, first, O.jl, args: [jlFld]);
                        break;
                    case 0x77:
                        var jaFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jaFld).IsSigned = true;
                        yield return new(pos, first, O.ja, args: [jaFld]);
                        break;
                    case 0x7B:
                        var jpoFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jpoFld).IsSigned = true;
                        yield return new(pos, first, O.jpo, args: [jpoFld]);
                        break;
                    case 0x7A:
                        var jpeFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jpeFld).IsSigned = true;
                        yield return new(pos, first, O.jpe, args: [jpeFld]);
                        break;
                    case 0x71:
                        var jnoFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnoFld).IsSigned = true;
                        yield return new(pos, first, O.jno, args: [jnoFld]);
                        break;
                    case 0x7D:
                        var jnlFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jnlFld).IsSigned = true;
                        yield return new(pos, first, O.jnl, args: [jnlFld]);
                        break;
                    case 0x75:
                        var jzlFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jzlFld).IsSigned = true;
                        yield return new(pos, first, O.jnz, args: [jzlFld]);
                        break;
                    case 0x70:
                        var joFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)joFld).IsSigned = true;
                        yield return new(pos, first, O.jo, args: [joFld]);
                        break;
                    case 0x78:
                        var jsFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jsFld).IsSigned = true;
                        yield return new(pos, first, O.js, args: [jsFld]);
                        break;
                    case 0x74:
                        var jzFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)jzFld).IsSigned = true;
                        yield return new(pos, first, O.jz, args: [jzFld]);
                        break;
                    case 0x9F:
                        yield return new(pos, first, O.lahf);
                        break;
                    case 0xC5:
                        var ldf = stream.NextByteC().Value;
                        if (ldf == 0x17)
                            yield return new(pos, first, O.lds, args: [R.dx, R.bx.Plus(null, ldf)]);
                        break;
                    case 0xC9:
                        yield return new(pos, first, O.leave);
                        break;
                    case 0xF0:
                        yield return new(pos, first, O.@lock);
                        break;
                    case 0xAD:
                        yield return new(pos, first, O.lodsw);
                        break;
                    case 0xAC:
                        yield return new(pos, first, O.lodsb);
                        break;
                    case 0xE2:
                        var loopFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)loopFld).IsSigned = true;
                        yield return new(pos, first, O.loop, args: [loopFld]);
                        break;
                    case 0xE1:
                        var loopeFld = stream.NextByteC(isSkip: true);
                        ((SkipArg)loopeFld).IsSigned = true;
                        yield return new(pos, first, O.loope, args: [loopeFld]);
                        break;
                    case 0xE0:
                        var loopnFld = stream.NextByteC(isSkip: true);
                        yield return new(pos, first, O.loopne, args: [loopnFld]);
                        break;
                    case 0xB4:
                        yield return new(pos, first, O.mov, args: [R.ah, stream.NextByteC()]);
                        break;
                    case 0xB7:
                        yield return new(pos, first, O.mov, args: [R.bh, stream.NextByteC()]);
                        break;
                    case 0xB5:
                        yield return new(pos, first, O.mov, args: [R.ch, stream.NextByteC()]);
                        break;
                    case 0x8A:
                        var maa = stream.NextByteC().Value;
                        if (maa == 0xEB)
                            yield return new(pos, first, O.mov, args: [R.ch.With(maa), R.bl]);
                        else if (maa == 0x30)
                            yield return new(pos, first, O.mov, args: [R.dh, R.bx.Plus(R.si, maa)]);
                        else if (maa == 0xD1)
                            yield return new(pos, first, O.mov, args: [R.dl.With(maa), R.cl]);
                        else
                            yield return new(pos, first, O.mov,
                                args: [R.al.With(maa), R.di.Plus(stream.NextByteC().Value)]);
                        break;
                    case 0x8B:
                        var movFld = stream.NextByteC().Value;
                        if (movFld == 0xDA)
                            yield return new(pos, first, O.mov, args: [R.bx, R.dx.With(movFld)]);
                        else if (movFld == 0xC4)
                            yield return new(pos, first, O.mov, args: [R.ax, R.sp.With(movFld)]);
                        else if (movFld == 0xCA)
                            yield return new(pos, first, O.mov, args: [R.cx, R.dx.With(movFld)]);
                        else if (movFld == 0xFB)
                            yield return new(pos, first, O.mov, args: [R.di, R.bx.With(movFld)]);
                        else if (movFld == 0xE2)
                            yield return new(pos, first, O.mov, args: [R.sp, R.dx.With(movFld)]);
                        else if (movFld == 0xC1)
                            yield return new(pos, first, O.mov, args: [R.ax, R.cx.With(movFld)]);
                        else if (movFld == 0xF8)
                            yield return new(pos, first, O.mov, args: [R.di, R.ax.With(movFld)]);
                        else if (movFld == 0xC3)
                            yield return new(pos, first, O.mov, args: [R.ax, R.bx.With(movFld)]);
                        else if (movFld == 0xC6)
                            yield return new(pos, first, O.mov, args: [R.ax, R.si.With(movFld)]);
                        else if (movFld == 0xC8)
                            yield return new(pos, first, O.mov, args: [R.cx, R.ax.With(movFld)]);
                        else if (movFld == 0xD8)
                            yield return new(pos, first, O.mov, args: [R.bx, R.ax.With(movFld)]);
                        else
                            yield return new(pos, first, O.mov, args: [R.ax, R.ax.With(movFld)]);
                        break;
                    case 0xB0:
                        yield return new(pos, first, O.mov, args: [R.al, stream.NextByteC()]);
                        break;
                    case 0xB3:
                        yield return new(pos, first, O.mov, args: [R.bl, stream.NextByteC()]);
                        break;
                    case 0xB8:
                        yield return new(pos, first, O.mov, args: [R.ax, stream.NextShortC()]);
                        break;
                    case 0xBB:
                        yield return new(pos, first, O.mov, args: [R.bx, stream.NextShortC()]);
                        break;
                    case 0x89:
                        var mcaFl = stream.NextByteC().Value;
                        if (mcaFl == 0xC7)
                            yield return new(pos, first, O.mov, args: [R.di, R.ax.With(mcaFl)]);
                        else if (mcaFl == 0xFA)
                            yield return new(pos, first, O.mov, args: [R.dx, R.di.With(mcaFl)]);
                        else if (mcaFl == 0x2D)
                            yield return new(pos, first, O.mov, args: [R.di.Plus(null, mcaFl), R.bp]);
                        else if (mcaFl == 0xF0)
                            yield return new(pos, first, O.mov, args: [R.ax, R.si.With(mcaFl)]);
                        else if (mcaFl == 0x31)
                            yield return new(pos, first, O.mov, args: [R.bx.Plus(R.di, mcaFl), R.si]);
                        break;
                    case 0xB1:
                        yield return new(pos, first, O.mov, args: [R.cl, stream.NextByteC()]);
                        break;
                    case 0x88:
                        var mclFl = stream.NextByteC().Value;
                        if (mclFl == 0xEE)
                            yield return new(pos, first, O.mov, args: [R.dh, R.ch.With(mclFl)]);
                        else if (mclFl == 0xFA)
                            yield return new(pos, first, O.mov, args: [R.dl, R.bh.With(mclFl)]);
                        else if (mclFl == 0xDB)
                            yield return new(pos, first, O.mov, args: [R.bl, R.bl.With(mclFl)]);
                        else if (mclFl == 0xDA)
                            yield return new(pos, first, O.mov, args: [R.dl, R.bl.With(mclFl)]);
                        else if (mclFl == 0x3B)
                            yield return new(pos, first, O.mov, args: [R.bp.Plus(R.di, mclFl), R.bh]);
                        else
                            yield return new(pos, first, O.mov, args: [R.bx.Plus(R.si, mclFl), R.cl]);
                        break;
                    case 0x8E:
                        var mcsFl = stream.NextByteC().Value;
                        if (mcsFl == 0xFA)
                            yield return new(pos, first, O.mov, args: [R.segr7, R.dx.With(mcsFl)]);
                        else if (mcsFl == 0xE1)
                            yield return new(pos, first, O.mov, args: [R.fs, R.cx.With(mcsFl)]);
                        else if (mcsFl == 0x30)
                            yield return new(pos, first, O.mov, args: [R.segr6, R.bx.Plus(R.si, mcsFl)]);
                        else
                            yield return new(pos, first, O.mov, args: [R.cs, R.si.Plus(null, mcsFl)]);
                        break;
                    case 0x8C:
                        var mcoFl = stream.NextByteC().Value;
                        if (mcoFl == 0x0C)
                            yield return new(pos, first, O.mov, args: [R.si.Plus(null, mcoFl), R.cs]);
                        break;
                    case 0xB9:
                        yield return new(pos, first, O.mov, args: [R.cx, stream.NextShortC()]);
                        break;
                    case 0xB6:
                        yield return new(pos, first, O.mov, args: [R.dh, stream.NextByteC()]);
                        break;
                    case 0xB2:
                        yield return new(pos, first, O.mov, args: [R.dl, stream.NextByteC()]);
                        break;
                    case 0xBA:
                        yield return new(pos, first, O.mov, args: [R.dx, stream.NextShortC()]);
                        break;
                    case 0xBE:
                        yield return new(pos, first, O.mov, args: [R.si, stream.NextShortC()]);
                        break;
                    case 0xA4:
                        yield return new(pos, first, O.movsb);
                        break;
                    case 0xA5:
                        yield return new(pos, first, O.movsw);
                        break;
                    case 0xD8:
                        var fmulFirst = stream.NextByteC().Value;
                        if (fmulFirst == 0x0A)
                            yield return new(pos, first, O.fmul, mod: Modifier.dword,
                                args: [R.bp.Plus(R.si, fmulFirst)]);
                        else if (fmulFirst == 0xF0)
                            yield return new(pos, first, O.fdiv, args: [R.st0.With(fmulFirst)]);
                        else if (fmulFirst == 0xFC)
                            yield return new(pos, first, O.fdivr, args: [R.st4.With(fmulFirst)]);
                        else if (fmulFirst == 0x05)
                            yield return new(pos, first, O.fadd, mod: Modifier.dword,
                                args: [R.di.Plus(null, fmulFirst)]);
                        break;
                    case 0xDC:
                        var fnsbFirst = stream.NextByteC().Value;
                        if (fnsbFirst == 0xE2)
                            yield return new(pos, first, O.fsubr, mod: Modifier.to, args: [R.st2.With(fnsbFirst)]);
                        break;
                    case 0xD9:
                        var fnsFirst = stream.NextByteC().Value;
                        if (fnsFirst == 0x31)
                            yield return new(pos, first, O.fnstenv, args: [R.bx.Plus(R.di, fnsFirst)]);
                        else if (fnsFirst == 0x3C)
                            yield return new(pos, first, O.fnstcw, args: [R.si.Plus(null, fnsFirst)]);
                        else if (fnsFirst == 0xCA)
                            yield return new(pos, first, O.fxch, args: [R.st2.With(fnsFirst)]);
                        else if (fnsFirst == 0x39)
                            yield return new(pos, first, O.fnstcw, args: [R.bx.Plus(R.di, fnsFirst)]);
                        else if (fnsFirst == 0x25)
                            yield return new(pos, first, O.fldenv, args: [R.di.Plus(null, fnsFirst)]);
                        break;
                    case 0xF6:
                        var muhFirst = stream.NextByteC().Value;
                        if (muhFirst == 0xE4)
                            yield return new(pos, first, O.mul, args: [R.ah.With(muhFirst)]);
                        else if (muhFirst == 0xFA)
                            yield return new(pos, first, O.idiv, args: [R.dl.With(muhFirst)]);
                        break;
                    case 0xF7:
                        var mulFirst = stream.NextByteC().Value;
                        if (mulFirst == 0xD0)
                            yield return new(pos, first, O.not, args: [R.ax.With(mulFirst)]);
                        else if (mulFirst == 0xD2)
                            yield return new(pos, first, O.not, args: [R.dx.With(mulFirst)]);
                        else if (mulFirst == 0xE1)
                            yield return new(pos, first, O.mul, args: [R.cx.With(mulFirst)]);
                        else if (mulFirst == 0xEB)
                            yield return new(pos, first, O.imul, args: [R.bx.With(mulFirst)]);
                        else if (mulFirst == 0xE3)
                            yield return new(pos, first, O.mul, args: [R.bx.With(mulFirst)]);
                        else if (mulFirst == 0xFB)
                            yield return new(pos, first, O.idiv, args: [R.bx.With(mulFirst)]);
                        else
                            yield return new(pos, first, O.idiv, args: [R.cx.With(mulFirst)]);
                        break;
                    case 0x90:
                        yield return new(pos, first, O.nop);
                        break;
                    case 0x66:
                        yield return new(pos, first, O.o32);
                        break;
                    case 0xE7:
                        var outaxFld = stream.NextByteC();
                        yield return new(pos, first, O.@out, args: [outaxFld, R.ax]);
                        break;
                    case 0xEE:
                        yield return new(pos, first, O.@out, args: [R.dx, R.al]);
                        break;
                    case 0xEF:
                        yield return new(pos, first, O.@out, args: [R.dx, R.ax]);
                        break;
                    case 0x6E:
                        yield return new(pos, first, O.outsb);
                        break;
                    case 0x6F:
                        yield return new(pos, first, O.outsw);
                        break;
                    case 0x58:
                        yield return new(pos, first, O.pop, args: [R.ax]);
                        break;
                    case 0x5D:
                        yield return new(pos, first, O.pop, args: [R.bp]);
                        break;
                    case 0x5B:
                        yield return new(pos, first, O.pop, args: [R.bx]);
                        break;
                    case 0x5A:
                        yield return new(pos, first, O.pop, args: [R.dx]);
                        break;
                    case 0x07:
                        yield return new(pos, first, O.pop, args: [R.es]);
                        break;
                    case 0x5E:
                        yield return new(pos, first, O.pop, args: [R.si]);
                        break;
                    case 0x59:
                        yield return new(pos, first, O.pop, args: [R.cx]);
                        break;
                    case 0x5F:
                        yield return new(pos, first, O.pop, args: [R.di]);
                        break;
                    case 0x1F:
                        yield return new(pos, first, O.pop, args: [R.ds]);
                        break;
                    case 0x17:
                        yield return new(pos, first, O.pop, args: [R.ss]);
                        break;
                    case 0x5C:
                        yield return new(pos, first, O.pop, args: [R.sp]);
                        break;
                    case 0x61:
                        yield return new(pos, first, O.popa);
                        break;
                    case 0x9D:
                        yield return new(pos, first, O.popf);
                        break;
                    case 0x50:
                        yield return new(pos, first, O.push, args: [R.ax]);
                        break;
                    case 0x53:
                        yield return new(pos, first, O.push, args: [R.bx]);
                        break;
                    case 0x55:
                        yield return new(pos, first, O.push, args: [R.bp]);
                        break;
                    case 0x51:
                        yield return new(pos, first, O.push, args: [R.cx]);
                        break;
                    case 0x0E:
                        yield return new(pos, first, O.push, args: [R.cs]);
                        break;
                    case 0x57:
                        yield return new(pos, first, O.push, args: [R.di]);
                        break;
                    case 0x1E:
                        yield return new(pos, first, O.push, args: [R.ds]);
                        break;
                    case 0x52:
                        yield return new(pos, first, O.push, args: [R.dx]);
                        break;
                    case 0x06:
                        yield return new(pos, first, O.push, args: [R.es]);
                        break;
                    case 0x56:
                        yield return new(pos, first, O.push, args: [R.si]);
                        break;
                    case 0x54:
                        yield return new(pos, first, O.push, args: [R.sp]);
                        break;
                    case 0x16:
                        yield return new(pos, first, O.push, args: [R.ss]);
                        break;
                    case 0x6A:
                        var pushbFl = stream.NextBytepC();
                        pushbFl.Signed = true;
                        yield return new(pos, first, O.push, args: [pushbFl]);
                        break;
                    case 0x60:
                        yield return new(pos, first, O.pusha);
                        break;
                    case 0x9C:
                        yield return new(pos, first, O.pushf);
                        break;
                    case 0x0B:
                        var orFld = stream.NextByteC().Value;
                        if (orFld == 0xC8)
                            yield return new(pos, first, O.or, args: [R.cx, R.ax.With(orFld)]);
                        else if (orFld == 0xC1)
                            yield return new(pos, first, O.or, args: [R.ax, R.cx.With(orFld)]);
                        else if (orFld == 0xCD)
                            yield return new(pos, first, O.or, args: [R.cx, R.bp.With(orFld)]);
                        else if (orFld == 0xF6)
                            yield return new(pos, first, O.or, args: [R.si, R.si.With(orFld)]);
                        else if (orFld == 0x11)
                            yield return new(pos, first, O.or, args: [R.dx, R.bx.Plus(R.di, orFld)]);
                        else if (orFld == 0x2C)
                            yield return new(pos, first, O.or, args: [R.bp, R.si.Plus(null, orFld)]);
                        else
                            yield return new(pos, first, O.or, args: [R.ax, R.ax.With(orFld)]);
                        break;
                    case 0x0a:
                        var orbFld = stream.NextByteC().Value;
                        if (orbFld == 0x1A)
                            yield return new(pos, first, O.or, args: [R.bl, R.bp.Plus(R.si, orbFld)]);
                        else if (orbFld == 0x14)
                            yield return new(pos, first, O.or, args: [R.dl, R.si.Plus(null, orbFld)]);
                        else if (orbFld == 0xE9)
                            yield return new(pos, first, O.or, args: [R.ch, R.cl.With(orbFld)]);
                        else if (orbFld == 0xD6)
                            yield return new(pos, first, O.or, args: [R.dl, R.dh.With(orbFld)]);
                        else if (orbFld == 0xE2)
                            yield return new(pos, first, O.or, args: [R.ah, R.dl.With(orbFld)]);
                        else if (orbFld == 0xC9)
                            yield return new(pos, first, O.or, args: [R.cl, R.cl.With(orbFld)]);
                        else if (orbFld == 0xDB)
                            yield return new(pos, first, O.or, args: [R.bl, R.bl.With(orbFld)]);
                        else if (orbFld == 0xF9)
                            yield return new(pos, first, O.or, args: [R.bh, R.cl.With(orbFld)]);
                        break;
                    case 0x08:
                        var orhFld = stream.NextByteC().Value;
                        if (orhFld == 0xCF)
                            yield return new(pos, first, O.or, args: [R.bh, R.cl.With(orhFld)]);
                        else if (orhFld == 0xEA)
                            yield return new(pos, first, O.or, args: [R.dl, R.ch.With(orhFld)]);
                        else if (orhFld == 0x10)
                            yield return new(pos, first, O.or, args: [R.bx.Plus(R.si, orhFld), R.dl]);
                        break;
                    case 0x09:
                        var orxFld = stream.NextByteC().Value;
                        if (orxFld == 0x17)
                            yield return new(pos, first, O.or, args: [R.bx.Plus(null, orxFld), R.dx]);
                        else if (orxFld == 0xDD)
                            yield return new(pos, first, O.or, args: [R.bp, R.bx.With(orxFld)]);
                        else if (orxFld == 0xE7)
                            yield return new(pos, first, O.or, args: [R.di, R.sp.With(orxFld)]);
                        break;
                    case 0x0C:
                        var oraFld = stream.NextByteC();
                        yield return new(pos, first, O.or, args: [R.al, oraFld]);
                        break;
                    case 0xE6:
                        var outaFld = stream.NextByteC();
                        yield return new(pos, first, O.@out, args: [outaFld, R.al]);
                        break;
                    case 0xD0:
                        var rclFl = stream.NextByteC().Value;
                        var rclOneArg = new ImplicitArg(1);
                        if (rclFl == 0xD6)
                            yield return new(pos, first, O.rcl, args: [R.dh.With(rclFl), rclOneArg]);
                        else if (rclFl == 0xDB)
                            yield return new(pos, first, O.rcr, args: [R.bl.With(rclFl), rclOneArg]);
                        else if (rclFl == 0xE0)
                            yield return new(pos, first, O.shl, args: [R.al.With(rclFl), rclOneArg]);
                        break;
                    case 0xF3:
                        yield return new(pos, first, O.rep);
                        break;
                    case 0xF2:
                        yield return new(pos, first, O.repne);
                        break;
                    case 0xC3:
                        yield return new(pos, first, O.ret);
                        break;
                    case 0xCB:
                        yield return new(pos, first, O.retf);
                        break;
                    case 0x9E:
                        yield return new(pos, first, O.sahf);
                        break;
                    case 0xD6:
                        yield return new(pos, first, O.salc);
                        break;
                    case 0xAE:
                        yield return new(pos, first, O.scasb);
                        break;
                    case 0xAF:
                        yield return new(pos, first, O.scasw);
                        break;
                    case 0xD3:
                        var shlFl = stream.NextByteC().Value;
                        if (shlFl == 0xE0)
                            yield return new(pos, first, O.shl, args: [R.ax, R.cl.With(shlFl)]);
                        else if (shlFl == 0xCB)
                            yield return new(pos, first, O.ror, args: [R.bx, R.cl.With(shlFl)]);
                        else if (shlFl == 0xC9)
                            yield return new(pos, first, O.ror, args: [R.cx, R.cl.With(shlFl)]);
                        else if (shlFl == 0xFB)
                            yield return new(pos, first, O.sar, args: [R.bx, R.cl.With(shlFl)]);
                        else if (shlFl == 0xE5)
                            yield return new(pos, first, O.shl, args: [R.bp, R.cl.With(shlFl)]);
                        else
                            yield return new(pos, first, O.sar, args: [R.si, R.cl.With(shlFl)]);
                        break;
                    case 0xD1:
                        var shlbFl = stream.NextByteC().Value;
                        var shOneArg = new ImplicitArg(1);
                        if (shlbFl == 0xE3)
                            yield return new(pos, first, O.shl, args: [R.bx.With(shlbFl), shOneArg]);
                        else if (shlbFl == 0x00)
                            yield return new(pos, first, O.rol, Modifier.word,
                                args: [R.bx.Plus(R.si, shlbFl), shOneArg]);
                        break;
                    case 0xD2:
                        var shbFl = stream.NextByteC().Value;
                        if (shbFl == 0x22)
                            yield return new(pos, first, O.shl, mod: Modifier.@byte,
                                args: [R.bp.Plus(R.si, shbFl), R.cl]);
                        else if (shbFl == 0x19)
                            yield return new(pos, first, O.rcr, mod: Modifier.@byte,
                                args: [R.bx.Plus(R.di, shbFl), R.cl]);
                        else if (shbFl == 0xC5)
                            yield return new(pos, first, O.rol, args: [R.ch, R.cl.With(shbFl)]);
                        break;
                    case 0x36:
                        yield return new(pos, first, O.ss);
                        break;
                    case 0xFB:
                        yield return new(pos, first, O.sti);
                        break;
                    case 0xF9:
                        yield return new(pos, first, O.stc);
                        break;
                    case 0xFD:
                        yield return new(pos, first, O.std);
                        break;
                    case 0xAA:
                        yield return new(pos, first, O.stosb);
                        break;
                    case 0xAB:
                        yield return new(pos, first, O.stosw);
                        break;
                    case 0x2B:
                        var saxFld = stream.NextByteC().Value;
                        if (saxFld == 0xF9)
                            yield return new(pos, first, O.sub, args: [R.di, R.cx.With(saxFld)]);
                        else if (saxFld == 0xE5)
                            yield return new(pos, first, O.sub, args: [R.sp, R.bp.With(saxFld)]);
                        else if (saxFld == 0xEE)
                            yield return new(pos, first, O.sub, args: [R.bp, R.si.With(saxFld)]);
                        else if (saxFld == 0x19)
                            yield return new(pos, first, O.sub, args: [R.bx, R.bx.Plus(R.di, saxFld)]);
                        else if (saxFld == 0xC1)
                            yield return new(pos, first, O.sub, args: [R.ax, R.cx.With(saxFld)]);
                        else if (saxFld == 0xC3)
                            yield return new(pos, first, O.sub, args: [R.ax, R.bx.With(saxFld)]);
                        else
                            yield return new(pos, first, O.sub, args: [R.ax, R.al.With(saxFld)]);
                        break;
                    case 0x2C:
                        var salFld = stream.NextByteC();
                        yield return new(pos, first, O.sub, args: [R.al, salFld]);
                        break;
                    case 0x2A:
                        var subaMod = stream.NextByteC().Value;
                        if (subaMod == 0xC6)
                            yield return new(pos, first, O.sub, args: [R.al, R.dh.With(subaMod)]);
                        break;
                    case 0x28:
                        var subdMod = stream.NextByteC().Value;
                        if (subdMod == 0x32)
                            yield return new(pos, first, O.sub, args: [R.bp.Plus(R.si, subdMod), R.dh]);
                        else if (subdMod == 0x37)
                            yield return new(pos, first, O.sub, args: [R.bx.Plus(null, subdMod), R.dh]);
                        else if (subdMod == 0x25)
                            yield return new(pos, first, O.sub, args: [R.di.Plus(null, subdMod), R.ah]);
                        else if (subdMod == 0x00)
                            yield return new(pos, first, O.sub, args: [R.bx.Plus(R.si, subdMod), R.al]);
                        else if (subdMod == 0xC7)
                            yield return new(pos, first, O.sub, args: [R.bh, R.al.With(subdMod)]);
                        else if (subdMod == 0xF1)
                            yield return new(pos, first, O.sub, args: [R.cl, R.dh.With(subdMod)]);
                        else if (subdMod == 0xD6)
                            yield return new(pos, first, O.sub, args: [R.dh, R.dl.With(subdMod)]);
                        break;
                    case 0x19:
                        var sbbxMod = stream.NextByteC().Value;
                        if (sbbxMod == 0x1A)
                            yield return new(pos, first, O.sbb, args: [R.bp.Plus(R.si, sbbxMod), R.bx]);
                        else if (sbbxMod == 0x28)
                            yield return new(pos, first, O.sbb, args: [R.bx.Plus(R.si, sbbxMod), R.bp]);
                        else if (sbbxMod == 0x01)
                            yield return new(pos, first, O.sbb, args: [R.bx.Plus(R.di, sbbxMod), R.ax]);
                        else if (sbbxMod == 0xE4)
                            yield return new(pos, first, O.sbb, args: [R.sp, R.sp.With(sbbxMod)]);
                        else if (sbbxMod == 0xE5)
                            yield return new(pos, first, O.sbb, args: [R.bp, R.sp.With(sbbxMod)]);
                        break;
                    case 0x1C:
                        var sbbbMod = stream.NextByteC();
                        yield return new(pos, first, O.sbb, args: [R.al, sbbbMod]);
                        break;
                    case 0x18:
                        var sbbMod = stream.NextByteC().Value;
                        if (sbbMod == 0xE8)
                            yield return new(pos, first, O.sbb, args: [R.al, R.ch.With(sbbMod)]);
                        else if (sbbMod == 0xD3)
                            yield return new(pos, first, O.sbb, args: [R.bl, R.dl.With(sbbMod)]);
                        else if (sbbMod == 0x3D)
                            yield return new(pos, first, O.sbb, args: [R.di.Plus(null, sbbMod), R.bh]);
                        else if (sbbMod == 0xEF)
                            yield return new(pos, first, O.sbb, args: [R.bh, R.ch.With(sbbMod)]);
                        else if (sbbMod == 0xFF)
                            yield return new(pos, first, O.sbb, args: [R.bh, R.bh.With(sbbMod)]);
                        break;
                    case 0x1A:
                        var sbcMod = stream.NextByteC().Value;
                        if (sbcMod == 0xC1)
                            yield return new(pos, first, O.sbb, args: [R.al, R.cl.With(sbcMod)]);
                        else if (sbcMod == 0xF4)
                            yield return new(pos, first, O.sbb, args: [R.dh, R.ah.With(sbcMod)]);
                        else if (sbcMod == 0x18)
                            yield return new(pos, first, O.sbb, args: [R.bl, R.bx.Plus(R.si, sbcMod)]);
                        else if (sbcMod == 0x27)
                            yield return new(pos, first, O.sbb, args: [R.ah, R.bx.Plus(null, sbcMod)]);
                        break;
                    case 0x1B:
                        var sbsMod = stream.NextByteC().Value;
                        if (sbsMod == 0x35)
                            yield return new(pos, first, O.sbb, args: [R.si, R.di.Plus(null, sbsMod)]);
                        else if (sbsMod == 0xF1)
                            yield return new(pos, first, O.sbb, args: [R.si, R.cx.With(sbsMod)]);
                        break;
                    case 0x85:
                        var testMod = stream.NextByteC().Value;
                        if (testMod == 0xF6)
                            yield return new(pos, first, O.test, args: [R.si, R.si.With(testMod)]);
                        else if (testMod == 0xC0)
                            yield return new(pos, first, O.test, args: [R.ax, R.ax.With(testMod)]);
                        else if (testMod == 0xED)
                            yield return new(pos, first, O.test, args: [R.bp, R.bp.With(testMod)]);
                        else if (testMod == 0x1C)
                            yield return new(pos, first, O.test, args: [R.si.Plus(null, testMod), R.bx]);
                        else if (testMod == 0xDB)
                            yield return new(pos, first, O.test, args: [R.bx, R.bx.With(testMod)]);
                        else
                            yield return new(pos, first, O.test, args: [R.dx, R.dx.With(testMod)]);
                        break;
                    case 0xA8:
                        var tsaMod = stream.NextByteC();
                        yield return new(pos, first, O.test, args: [R.al, tsaMod]);
                        break;
                    case 0x84:
                        var tsdMod = stream.NextByteC().Value;
                        if (tsdMod == 0x14)
                            yield return new(pos, first, O.test, args: [R.si.Plus(null, tsdMod), R.dl]);
                        else if (tsdMod == 0x2B)
                            yield return new(pos, first, O.test, args: [R.bp.Plus(R.di, tsdMod), R.ch]);
                        else if (tsdMod == 0x07)
                            yield return new(pos, first, O.test, args: [R.bx.Plus(null, tsdMod), R.al]);
                        break;
                    case 0x9B:
                        yield return new(pos, first, O.wait);
                        break;
                    case 0x97:
                        yield return new(pos, first, O.xchg, args: [R.ax, R.di]);
                        break;
                    case 0x92:
                        yield return new(pos, first, O.xchg, args: [R.ax, R.dx]);
                        break;
                    case 0x96:
                        yield return new(pos, first, O.xchg, args: [R.ax, R.si]);
                        break;
                    case 0x95:
                        yield return new(pos, first, O.xchg, args: [R.ax, R.bp]);
                        break;
                    case 0x93:
                        yield return new(pos, first, O.xchg, args: [R.ax, R.bx]);
                        break;
                    case 0x91:
                        yield return new(pos, first, O.xchg, args: [R.ax, R.cx]);
                        break;
                    case 0x94:
                        yield return new(pos, first, O.xchg, args: [R.ax, R.sp]);
                        break;
                    case 0x86:
                        var xdaFl = stream.NextByteC().Value;
                        if (xdaFl == 0xC6)
                            yield return new(pos, first, O.xchg, args: [R.al, R.dh.With(xdaFl)]);
                        else if (xdaFl == 0xEE)
                            yield return new(pos, first, O.xchg, args: [R.ch, R.dh.With(xdaFl)]);
                        else if (xdaFl == 0xD5)
                            yield return new(pos, first, O.xchg, args: [R.dl, R.ch.With(xdaFl)]);
                        else if (xdaFl == 0xF9)
                            yield return new(pos, first, O.xchg, args: [R.bh, R.cl.With(xdaFl)]);
                        else if (xdaFl == 0x13)
                            yield return new(pos, first, O.xchg, args: [R.dl, R.bp.Plus(R.di, xdaFl)]);
                        break;
                    case 0x87:
                        var xdbFl = stream.NextByteC().Value;
                        if (xdbFl == 0xEE)
                            yield return new(pos, first, O.xchg, args: [R.bp, R.si.With(xdbFl)]);
                        else if (xdbFl == 0x2C)
                            yield return new(pos, first, O.xchg, args: [R.bp, R.si.Plus(null, xdbFl)]);
                        else if (xdbFl == 0x0C)
                            yield return new(pos, first, O.xchg, args: [R.cx, R.si.Plus(null, xdbFl)]);
                        else if (xdbFl == 0xE6)
                            yield return new(pos, first, O.xchg, args: [R.sp, R.si.With(xdbFl)]);
                        else if (xdbFl == 0xC3)
                            yield return new(pos, first, O.xchg, args: [R.ax, R.bx.With(xdbFl)]);
                        break;
                    case 0xD7:
                        yield return new(pos, first, O.xlatb);
                        break;
                    case 0x34:
                        var xoraFl = stream.NextByteC();
                        yield return new(pos, first, O.xor, args: [R.al, xoraFl]);
                        break;
                    case 0x30:
                        var xorpFl = stream.NextByteC().Value;
                        if (xorpFl == 0x03)
                            yield return new(pos, first, O.xor, args: [R.bp.Plus(R.di, xorpFl), R.al]);
                        else if (xorpFl == 0x3C)
                            yield return new(pos, first, O.xor, args: [R.si.Plus(null, xorpFl), R.bh]);
                        else if (xorpFl == 0x35)
                            yield return new(pos, first, O.xor, args: [R.di.Plus(null, xorpFl), R.dh]);
                        else if (xorpFl == 0x27)
                            yield return new(pos, first, O.xor, args: [R.bx.Plus(null, xorpFl), R.ah]);
                        else if (xorpFl == 0xCE)
                            yield return new(pos, first, O.xor, args: [R.dh, R.cl.With(xorpFl)]);
                        else if (xorpFl == 0xE6)
                            yield return new(pos, first, O.xor, args: [R.dh, R.ah.With(xorpFl)]);
                        else if (xorpFl == 0xC6)
                            yield return new(pos, first, O.xor, args: [R.dh, R.al.With(xorpFl)]);
                        break;
                    case 0x31:
                        var xorxFl = stream.NextByteC().Value;
                        if (xorxFl == 0xD9)
                            yield return new(pos, first, O.xor, args: [R.cx, R.bx.With(xorxFl)]);
                        else if (xorxFl == 0x09)
                            yield return new(pos, first, O.xor, args: [R.bx.Plus(R.di, xorxFl), R.cx]);
                        else if (xorxFl == 0x29)
                            yield return new(pos, first, O.xor, args: [R.bx.Plus(R.di, xorxFl), R.bp]);
                        break;
                    case 0x32:
                        var xorcFl = stream.NextByteC().Value;
                        if (xorcFl == 0x2C)
                            yield return new(pos, first, O.xor, args: [R.ch, R.si.Plus(null, xorcFl)]);
                        else if (xorcFl == 0xC6)
                            yield return new(pos, first, O.xor, args: [R.al, R.dh.With(xorcFl)]);
                        else if (xorcFl == 0x09)
                            yield return new(pos, first, O.xor, args: [R.cl, R.bx.Plus(R.di, xorcFl)]);
                        else if (xorcFl == 0xE3)
                            yield return new(pos, first, O.xor, args: [R.ah, R.bl.With(xorcFl)]);
                        break;
                    case 0x33:
                        var xorFl = stream.NextByteC().Value;
                        if (xorFl == 0xF6)
                            yield return new(pos, first, O.xor, args: [R.si, R.si.With(xorFl)]);
                        else if (xorFl == 0xC1)
                            yield return new(pos, first, O.xor, args: [R.ax, R.cx.With(xorFl)]);
                        else if (xorFl == 0xFD)
                            yield return new(pos, first, O.xor, args: [R.di, R.bp.With(xorFl)]);
                        else if (xorFl == 0xDC)
                            yield return new(pos, first, O.xor, args: [R.bx, R.sp.With(xorFl)]);
                        else if (xorFl == 0x14)
                            yield return new(pos, first, O.xor, args: [R.dx, R.si.Plus(null, xorFl)]);
                        else if (xorFl == 0xD2)
                            yield return new(pos, first, O.xor, args: [R.dx, R.dx.With(xorFl)]);
                        else if (xorFl == 0xC8)
                            yield return new(pos, first, O.xor, args: [R.cx, R.ax.With(xorFl)]);
                        else
                            yield return new(pos, first, O.xor, args: [R.ax, R.ax.With(xorFl)]);
                        break;
                }
            }
        }
    }
}