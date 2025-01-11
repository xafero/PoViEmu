using System;
using System.Collections.Generic;
using System.Linq;
using PoViEmu.Base.CPU;
using PoViEmu.SH3.CPU.Errors;
using PoViEmu.SH3.CPU.Soft;
using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Decoding;
using static PoViEmu.SH3.CPU.MachExt;
using Fl = PoViEmu.SH3.ISA.Flagged;
using I8 = PoViEmu.SH3.ISA.Ops.Consts.I8Operand;
using U8 = PoViEmu.SH3.ISA.Ops.Consts.U8Operand;
using NO = PoViEmu.SH3.ISA.Ops.Jumps.NearOperand;
using MU8 = PoViEmu.SH3.ISA.Ops.Mems.Mu8Operand;
using MU16 = PoViEmu.SH3.ISA.Ops.Mems.Mu16Operand;
using MU32 = PoViEmu.SH3.ISA.Ops.Mems.Mu32Operand;
using R = PoViEmu.SH3.ISA.Ops.Regs.Reg32Operand;
using PoViEmu.SH3.ISA.Ops.Mems;
using static PoViEmu.SH3.ISA.ShRegister;

// ReSharper disable InconsistentNaming

namespace PoViEmu.SH3.CPU
{
    /// <summary>
    /// The SH7291 is a custom 32bit CPU developed by CASIO based on Hitachi’s SH3
    /// </summary>
    public sealed class SH7291 : ICpu<XInstruction, MachineState>
    {
        public bool Halted { get; set; }
        public IDictionary<byte, IInterruptHandler> InterruptTable { get; }

        public SH7291()
        {
            var dos = new DOSInterrupts();
            InterruptTable = new SortedDictionary<byte, IInterruptHandler>
            {
                [DOSInterrupts.MainIntNo] = dos
            };
        }

        private void ExecuteInterrupt(byte key, MachineState m)
        {
            if (!InterruptTable.TryGetValue(key, out var handler))
                throw new InvalidOperationException($"Missing interrupt 0x{key:X2}!");
            handler.Handle(key, m);
        }

        public void Execute(XInstruction instruct, MachineState m)
        {
            var nextIP = instruct.Parsed.NextIP32;
            Execute(instruct, m, true, ref nextIP);
            m.PC = nextIP;
        }

        private void Execute(XInstruction instruct, MachineState s, bool ignoreUc,
            ref uint nextIP)
        {
            var parsed = instruct.Parsed;
            if (parsed.IsSimplyInvalid())
            {
                if (ignoreUc) return;
                throw new InvalidOpcodeException(instruct);
            }

            var ops = parsed.GetOps().ToArray();
            switch (parsed.Mnemonic)
            {
                case Mnemonic.Add when ops is [R m, R n]:
                    Compute.Add(s, m, n);
                    return;
                case Mnemonic.Add when ops is [I8 i, R r]:
                    Compute.Add(s, i.Val, r);
                    return;
                case Mnemonic.Addc when ops is [R m, R n]:
                    Compute.Addc(s, m, n);
                    return;
                case Mnemonic.Addv when ops is [R m, R n]:
                    Compute.Addv(s, m, n);
                    return;
                case Mnemonic.And when ops is [R m, R n]:
                    Compute.And(s,m,n);
                    return;
                case Mnemonic.And when ops is [U8 i, R n]:
                    Compute.And(s, i.Val, n);
                    return;
                case Mnemonic.AndB when ops is [U8 i, MU8 mem]:
                    Compute.Andb(s, i.Val, mem);
                    return;
                case Mnemonic.Bf when ops is [NO d]:
                    Jumping.BranchIfFalse(s, d.Diff, ref nextIP);
                    return;
                case Mnemonic.BfS when ops is [NO d]:
                    Jumping.BranchIfFalseDelay(s, d.Diff, ref nextIP);
                    return;
                case Mnemonic.Bra when ops is [NO d]:
                    Jumping.Branch(s, d.Diff, ref nextIP);
                    return;
                case Mnemonic.Braf when ops is [R m]:
                    Jumping.BranchFar(s, m, ref nextIP);
                    return;
                case Mnemonic.Bsr when ops is [NO d]:
                    Jumping.BranchSubroutine(s, d.Diff, ref nextIP);
                    return;
                case Mnemonic.Bsrf when ops is [R m]:
                    Jumping.BranchSubroutineFar(s, m, ref nextIP);
                    return;
                case Mnemonic.Bt when ops is [NO d]:
                    Jumping.BranchIfTrue(s, d.Diff, ref nextIP);
                    return;
                case Mnemonic.BtS when ops is [NO d]:
                    Jumping.BranchIfTrueDelay(s, d.Diff, ref nextIP);
                    return;
                case Mnemonic.Clrmac:
                    Compute.Clrmac(s);
                    return;
                case Mnemonic.Clrs:
                    Compute.Clrs(s);
                    return;
                case Mnemonic.Clrt:
                    Compute.Clrt(s);
                    return;
                case Mnemonic.CmpEq when ops is [R m, R n]:
                    Compute.CmpEq(s,m,n);
                    return;
                case Mnemonic.CmpGe when ops is [R m, R n]:
                    Compute.CmpGe(s, m, n);
                    return;
                case Mnemonic.CmpGt when ops is [R m, R n]:
                    Compute.CmpGt(s, m, n);
                    return;
                case Mnemonic.CmpHi when ops is [R m, R n]:
                    Compute.CmpHi(s, m, n);
                    return;
                case Mnemonic.CmpHs when ops is [R m, R n]:
                    Compute.CmpHs(s, m, n);
                    return;
                case Mnemonic.CmpPl when ops is [R n]:
                    Compute.CmpPl(s, n);
                    return;
                case Mnemonic.CmpPz when ops is [R n]:
                    Compute.CmpPz(s, n);
                    return;
                case Mnemonic.CmpStr when ops is [R m, R n]:
                    Compute.CmpStr(s, m, n);
                    return;
                case Mnemonic.CmpEq when ops is [I8 i, R n]:
                    Compute.CmpEq(s, i.Val, n);
                    return;
                case Mnemonic.Div0s when ops is [R m, R n]:
                    Compute.Div0s(s, m, n);
                    return;
                case Mnemonic.Div0u:
                    Compute.Div0u(s);
                    return;
                case Mnemonic.Div1 when ops is [R m, R n]:
                    Compute.Div1(s,m,n);
                    return;
                case Mnemonic.DmulsL when ops is [R m, R n]:
                    Compute.DmulsL(s, m, n);
                    return;
                case Mnemonic.DmuluL when ops is [R m, R n]:
                    Compute.DmuluL(s, m, n);
                    return;
                case Mnemonic.Dt when ops is [R n]:
                    Compute.Dt(s, n);
                    return;
                case Mnemonic.ExtsB when ops is [R m, R n]:
                    Compute.Extsb(s,m,n);
                    return;
                case Mnemonic.ExtsW when ops is [R m, R n]:
                    Compute.Extsw(s, m, n);
                    return;
                case Mnemonic.ExtuB when ops is [R m, R n]:
                    Compute.Extub(s, m, n);
                    return;
                case Mnemonic.ExtuW when ops is [R m, R n]:
                    Compute.Extuw(s, m, n);
                    return;
                case Mnemonic.Jmp when ops is [R m]:
                    Jumping.Jump(s, m, ref nextIP);
                    return;
                case Mnemonic.Jsr when ops is [MU32 mem]:
                    Jumping.JumpSubroutine(s, mem,ref nextIP);
                    return;
                case Mnemonic.Ldc when ops is [R m, R n]:
                    Compute.Ldc(s,m,n);
                    return;
                case Mnemonic.LdcL when ops is [MU32 mem, R n]:
                    Compute.LdcL(s, mem, n);
                    return;
                case Mnemonic.Lds when ops is [R m, R n]:
                    Compute.Lds(s, m, n);
                    return;
                case Mnemonic.LdsL when ops is [MU32 mem, R n]:
                    Compute.LdsL(s, mem, n);
                    return;
                case Mnemonic.Ldtlb:
                    Compute.Ldtlb(s);
                    return;
                case Mnemonic.MacL when ops is [R m, R n]:
                    Compute.Macl(s, m, n);
                    return;
                case Mnemonic.MacW when ops is [R m, R n]:
                    Compute.Macw(s, m, n);
                    return;
                case Mnemonic.Mov when ops is [R m, R n]:
                    Compute.Mov(s, m, n);
                    return;
                case Mnemonic.MovB when ops is [R r, MU8 mem]:
                    Compute.Mov(s, r, mem);
                    return;
                case Mnemonic.MovW when ops is [R r, MU32 mem]:
                    Compute.Mov(s, r, mem);
                    return;
                case Mnemonic.MovL when ops is [R r, MU32 mem]:
                    Compute.Mov(s, r, mem);
                    return;
                case Mnemonic.MovB when ops is [MU8 mem, R r]:
                    Compute.Mov(s, mem, r);
                    return;
                case Mnemonic.MovW when ops is [MU16 mem, R r]:
                    Compute.Mov(s, mem, r);
                    return;
                case Mnemonic.MovL when ops is [MU32 mem, R r]:
                    Compute.Mov(s, mem, r);
                    return;
                case Mnemonic.Mova when ops is [NO d, R n]:
                    Compute.Mova(s, d.Diff, n);
                    return;
                case Mnemonic.Movt when ops is [R n]:
                    Compute.Movt(s, n);
                    return;
                case Mnemonic.MulL when ops is [R m, R n]:
                    Compute.MulL(s, m, n);
                    return;
                case Mnemonic.MulsW when ops is [R m, R n]:
                    Compute.MulsW(s, m, n);
                    return;
                case Mnemonic.MuluW when ops is [R m, R n]:
                    Compute.MuluW(s, m, n);
                    return;
                case Mnemonic.Neg when ops is [R m, R n]:
                    Compute.Neg(s, m, n);
                    return;
                case Mnemonic.Negc when ops is [R m, R n]:
                    Compute.Negc(s, m, n);
                    return;
                case Mnemonic.Nop:
                    return;
                case Mnemonic.Not when ops is [R m, R n]:
                    Compute.Not(s, m, n);
                    return;
                case Mnemonic.Or when ops is [R m, R n]:
                    Compute.Or(s, m, n);
                    return;
                case Mnemonic.Or when ops is [U8 i, R n]:
                    Compute.Or(s, i.Val, n);
                    return;
                case Mnemonic.OrB when ops is [U8 i, MU8 mem]:
                    Compute.Or(s, i.Val, mem);
                    return;
                case Mnemonic.Pref when ops is [MU32 mem]:
                    // TODO Handle cache?
                    return;
                
                
                
                
                
                
                
                /*                                                                           
                case Mnemonic.Mov when ops is [I8 i, R r]:
                    s[r] = (uint)i.Val;
                    return;
                case Mnemonic.MovL when ops is [R r, MU32 mem]:
                    mem[s] = s[r];
                    return;
                case Mnemonic.MovL when ops is [MU32 mem, R r]:
                    s[r] = mem[s];
                    return;
                case Mnemonic.MovL when ops is [NO d, R n]:
                    var dispWl = 0x000000FF & (long)d.Diff;
                    s[n] = s.U32[(uint)((s.PC & 0xFFFFFFFC) + (dispWl << 2))];
                    return;                                                            
                
                case Mnemonic.StsL when ops is [R r, MU32 mem]:
                    mem[s] = s[r];
                    return;
                case Mnemonic.Sub when ops is [R m, R n]:
                    s[n] -= s[m];
                    return;
               
               
                case Mnemonic.LdsL when ops is [R m, R n]:
                    switch (n.Reg)
                    {
                        case MACH:
                            s.MACH = (uint)(s.U32[s[m]]);
                            if ((s.MACH & 0x00000200) == 0)
                                s.MACH &= 0x000003FF;
                            else
                                s.MACH |= 0xFFFFFC00;
                            s[m] += 4;
                            break;
                        default:
                            s[n] = (uint)(s.U32[s[m]]);
                            s[m] += 4;
                            break;
                    }
                    return;
              
              
              
                case Mnemonic.MovB when ops is [R m, R n]:
                    s.U8[s[n]] = (byte)s[m];
                    return;
                case Mnemonic.MovW when ops is [R m, R n]:
                    s.U16[s[n]] = (ushort)s[m];
                    return;
                case Mnemonic.MovL when ops is [R m, R n]:
                    s.U32[s[n]] = s[m];
                    return;
                case Mnemonic.MovB when ops is [R m, R n]:
                    s[n] = (uint)(s.U8[s[m]]);
                    if ((s[n] & 0x80) == 0)
                        s[n] &= 0x000000FF;
                    else
                        s[n] |= 0xFFFFFF00;
                    return;
                case Mnemonic.MovW when ops is [R m, R n]:
                    s[n] = (uint)(s.U16[s[m]]);
                    if ((s[n] & 0x8000) == 0)
                        s[n] &= 0x0000FFFF;
                    else
                        s[n] |= 0xFFFF0000;
                    return;
                case Mnemonic.MovL when ops is [R m, R n]:
                    s[n] = (uint)(s.U32[s[m]]);
                    return;
                case Mnemonic.MovB when ops is [R m, R n]:
                    s.U8[s[n] - 1] = (byte)s[m];
                    s[n] -= 1;
                    return;
                case Mnemonic.MovW when ops is [R m, R n]:
                    s.U16[s[n] - 2] = (ushort)s[m];
                    s[n] -= 2;
                    return;
                case Mnemonic.MovL when ops is [R m, R n]:
                    s.U32[s[n] - 4] = s[m];
                    s[n] -= 4;
                    return;
                case Mnemonic.MovB when ops is [R m, R n]:
                    s[n] = (uint)(s.U8[s[m]]);
                    if ((s[n] & 0x80) == 0)
                        s[n] &= 0x000000FF;
                    else
                        s[n] |= 0xFFFFFF00;
                    if (n != m)
                        s[m] += 1;
                    return;
                case Mnemonic.MovW when ops is [R m, R n]:
                    s[n] = (uint)(s.U16[s[m]]);
                    if ((s[n] & 0x8000) == 0)
                        s[n] &= 0x0000FFFF;
                    else
                        s[n] |= 0xFFFF0000;
                    if (n != m)
                        s[m] += 2;
                    return;
                case Mnemonic.MovL when ops is [R m, R n]:
                    s[n] = (uint)(s.U32[s[m]]);
                    if (n != m)
                        s[m] += 4;
                    return;
                case Mnemonic.MovB when ops is [R m, R n]:
                    s.U8[s[n] + s[R0]] = (byte)s[m];
                    return;
                case Mnemonic.MovW when ops is [R m, R n]:
                    s.U16[s[n] + s[R0]] = (ushort)s[m];
                    return;
                case Mnemonic.MovL when ops is [R m, R n]:
                    s.U32[s[n] + s[R0]] = s[m];
                    return;
                case Mnemonic.MovB when ops is [R m, R n]:
                    s[n] = (uint)(s.U8[s[m] + s[R0]]);
                    if ((s[n] & 0x80) == 0)
                        s[n] &= 0x000000FF;
                    else
                        s[n] |= 0xFFFFFF00;
                    return;
                case Mnemonic.MovW when ops is [R m, R n]:
                    s[n] = (uint)(s.U16[s[m] + s[R0]]);
                    if ((s[n] & 0x8000) == 0)
                        s[n] &= 0x0000FFFF;
                    else
                        s[n] |= 0xFFFF0000;
                    return;
                case Mnemonic.MovL when ops is [R m, R n]:
                    s[n] = (uint)(s.U32[s[m] + s[R0]]);
                    return;
                case Mnemonic.Mov when ops is [I8 i, R n]:
                    if ((i.Val & 0x80) == 0)
                        s[n] = (uint)(0x000000FF & (long)i.Val);
                    else
                        s[n] = (uint)(0xFFFFFF00 | i.Val);
                    return;
                case Mnemonic.MovW when ops is [NO d, R n]:
                    long dispWw;
                    dispWw = 0x000000FF & (long)d.Diff;
                    s[n] = (uint)(s.U16[(uint)(s.PC + (dispWw << 1))]);
                    if ((s[n] & 0x8000) == 0)
                        s[n] &= 0x0000FFFF;
                    else
                        s[n] |= 0xFFFF0000;
                    return;
                case Mnemonic.MovB when ops is [NO d]:
                    long dispBb;
                    dispBb = (0x000000FF & (long)d.Diff);
                    s[R0] = (uint)(s.U8[(uint)(s[GBR] + dispBb)]);
                    if ((s[R0] & 0x80) == 0)
                        s[R0] &= 0x000000FF;
                    else
                        s[R0] |= 0xFFFFFF00;
                    return;
                case Mnemonic.MovW when ops is [NO d]:
                    long dispWy;
                    dispWy = (0x000000FF & (long)d.Diff);
                    s[R0] = (uint)(s.U16[(uint)(s[GBR] + (dispWy << 1))]);
                    if ((s[R0] & 0x8000) == 0)
                        s[R0] &= 0x0000FFFF;
                    else
                        s[R0] |= 0xFFFF0000;
                    return;
                case Mnemonic.MovL when ops is [NO d]:
                    long dispWz;
                    dispWz = (0x000000FF & (long)d.Diff);
                    s[R0] = (uint)(s.U32[(uint)(s[GBR] + (dispWz << 2))]);
                    return;
                case Mnemonic.MovB when ops is [NO d]:
                    long dispWk;
                    dispWk = (0x000000FF & (long)d.Diff);
                    s.U8[(uint)(s[GBR] + dispWk)] = (byte)s[R0];
                    return;
                case Mnemonic.MovW when ops is [NO d]:
                    long dispWn;
                    dispWn = (0x000000FF & (long)d.Diff);
                    s.U16[(uint)(s[GBR] + (dispWn << 1))] = (ushort)s[R0];
                    return;
                case Mnemonic.MovL when ops is [NO d]:
                    long dispWf;
                    dispWf = (0x000000FF & (long)d.Diff);
                    s.U32[(uint)(s[GBR] + (dispWf << 2))] = s[R0];
                    return;
                case Mnemonic.MovB when ops is [NO d, R n]:
                    long dispWg;
                    dispWg = (0x0000000F & (long)d.Diff);
                    s.U8[(uint)(s[n] + dispWg)] = (byte)s[R0];
                    return;
                case Mnemonic.MovW when ops is [NO d, R n]:
                    long dispWh;
                    dispWh = (0x0000000F & (long)d.Diff);
                    s.U16[(uint)(s[n] + (dispWh << 1))] = (ushort)s[R0];
                    return;
                case Mnemonic.MovL when ops is [R m, NO d, R n]:
                    long dispWi;
                    dispWi = (0x0000000F & (long)d.Diff);
                    s.U32[(uint)(s[n] + (dispWi << 2))] = s[m];
                    return;
                case Mnemonic.MovB when ops is [R m, NO d]:
                    long dispWj;
                    dispWj = (0x0000000F & (long)d.Diff);
                    s[R0] = (uint)(s.U8[(uint)(s[m] + dispWj)]);
                    if ((s[R0] & 0x80) == 0)
                        s[R0] &= 0x000000FF;
                    else
                        s[R0] |= 0xFFFFFF00;
                    return;
                case Mnemonic.MovW when ops is [R m, NO d]:
                    long dispWp;
                    dispWp = (0x0000000F & (long)d.Diff);
                    s[R0] = (uint)(s.U16[(uint)(s[m] + (dispWp << 1))]);
                    if ((s[R0] & 0x8000) == 0)
                        s[R0] &= 0x0000FFFF;
                    else
                        s[R0] |= 0xFFFF0000;
                    return;
                case Mnemonic.MovL when ops is [R m, NO d, R n]:
                    long dispWo;
                    dispWo = (0x0000000F & (long)d.Diff);
                    s[n] = (uint)(s.U32[(uint)(s[m] + (dispWo << 2))]);
                    return;
                case Mnemonic.Mova when ops is [NO d]:
                    long dispWm;
                    dispWm = (0x000000FF & (long)d.Diff);
                    s[R0] = (uint)((s.PC & 0xFFFFFFFC) + (dispWm << 2));
                    return;
                case Mnemonic.Or when ops is [I8 i]:
                    s[R0] = (uint)(s[R0] | (0x000000FF & (long)i.Val));
                    return;
                case Mnemonic.OrB when ops is [I8 i]:
                    long tempOo;
                    tempOo = (long)(s.U8[s[GBR] + s[R0]]);
                    tempOo |= (0x000000FF & (long)i.Val);
                    s.U8[s[GBR] + s[R0]] = (byte)tempOo;
                    return;
                case Mnemonic.Pref when ops is [R n]:
                    // TODO Load cache?!
                    return;
                case Mnemonic.Rotcl when ops is [R n]:
                    long tempYx;
                    tempYx = (s[n] & 0x80000000) == 0 ? 0 : 1;
                    s[n] <<= 1;
                    if (s.T)
                        s[n] |= 0x00000001;
                    else
                        s[n] &= 0xFFFFFFFE;
                    s.T = tempYx == 1;
                    return;
                case Mnemonic.Rotcr when ops is [R n]:
                    long tempYy;
                    tempYy = (s[n] & 0x00000001) == 0 ? 0 : 1;
                    s[n] >>= 1;
                    if (s.T)
                        s[n] |= 0x80000000;
                    else
                        s[n] &= 0x7FFFFFFF;
                    s.T = tempYy == 1;
                    return;
                case Mnemonic.Rotl when ops is [R n]:
                    s.T = (s[n] & 0x80000000) != 0;
                    s[n] <<= 1;
                    if (s.T)
                        s[n] |= 0x00000001;
                    else
                        s[n] &= 0xFFFFFFFE;
                    return;
                case Mnemonic.Rotr when ops is [R n]:
                    s.T = (s[n] & 0x00000001) != 0;
                    s[n] >>= 1;
                    if (s.T)
                        s[n] |= 0x80000000;
                    else
                        s[n] &= 0x7FFFFFFF;
                    return;
                case Mnemonic.Rte:
                    ulong tempRt;
                    tempRt = s.PC;
                    nextIP = s.SPC;
                    s.SR = (Fl)s.SSR;
                    // TODO Delay_Slot(temp+2);
                    return;
                case Mnemonic.Rts:
                    ulong tempRx;
                    tempRx = s.PC;
                    nextIP = s.PR + 4;
                    // TODO Delay_Slot(temp+2);
                    return;
                case Mnemonic.Sets:
                    s.S = true;
                    return;
                case Mnemonic.Sett:
                    s.T = true;
                    return;
                case Mnemonic.Shad when ops is [R m, R n]:
                    long cnt, sgn;
                    sgn = s[m] & 0x80000000;
                    cnt = s[m] & 0x0000001F;
                    if (sgn == 0)
                        s[n] <<= (int)cnt;
                    else
                        s[n] = (uint)((long)s[n] >> (int)((~cnt + 1) & 0x1F));
                    return;
                case Mnemonic.Shal when ops is [R n]:
                    s.T = (s[n] & 0x80000000) != 0;
                    s[n] <<= 1;
                    return;
                case Mnemonic.Shar when ops is [R n]:
                    long tempWq;
                    s.T = (s[n] & 0x00000001) != 0;
                    tempWq = (s[n] & 0x80000000) == 0 ? 0 : 1;
                    s[n] >>= 1;
                    if (tempWq == 1)
                        s[n] |= 0x80000000;
                    else
                        s[n] &= 0x7FFFFFFF;
                    return;
                case Mnemonic.Shld when ops is [R m, R n]:
                    long cntX, sgnX;
                    sgnX = s[m] & 0x80000000;
                    cntX = s[m] & 0x0000001F;
                    if (sgnX == 0)
                        s[n] <<= (int)cntX;
                    else
                        s[n] >>= (int)((~cntX + 1) & 0x1F);
                    return;
                case Mnemonic.Shll when ops is [R n]:
                    s.T = (s[n] & 0x80000000) != 0;
                    s[n] <<= 1;
                    return;
                case Mnemonic.Shll2 when ops is [R n]:
                    s[n] <<= 2;
                    return;
                case Mnemonic.Shll8 when ops is [R n]:
                    s[n] <<= 8;
                    return;
                case Mnemonic.Shll16 when ops is [R n]:
                    s[n] <<= 16;
                    return;
                case Mnemonic.Shlr when ops is [R n]:
                    s.T = (s[n] & 0x00000001) != 0;
                    s[n] >>= 1;
                    s[n] &= 0x7FFFFFFF;
                    return;
                case Mnemonic.Shlr2 when ops is [R n]:
                    s[n] >>= 2;
                    s[n] &= 0x3FFFFFFF;
                    return;
                case Mnemonic.Shlr8 when ops is [R n]:
                    s[n] >>= 8;
                    s[n] &= 0x00FFFFFF;
                    return;
                case Mnemonic.Shlr16 when ops is [R n]:
                    s[n] >>= 16;
                    s[n] &= 0x0000FFFF;
                    return;
                case Mnemonic.Sleep:
                    nextIP -= 2;
                    // TODO Sleep Mode
                    return;
                case Mnemonic.Stc when ops is [R m, R n]:
                    s[n] = s[m];
                    return;
                case Mnemonic.StcL when ops is [R m, R n]:
                    s[n] -= 4;
                    s.U32[s[n]] = s[m];
                    return;
                case Mnemonic.Sts when ops is [R m, R n]:
                    switch (m.Reg)
                    {
                        case MACH:
                            s[n] = s.MACH;
                            if ((s[n] & 0x00000200) == 0)
                                s[n] &= 0x000003FF;
                            else
                                s[n] |= 0xFFFFFC00;
                            break;
                        default:
                            s[n] = s[m];
                            break;
                    }
                    return;
                case Mnemonic.StsL when ops is [R m, R n]:
                    switch (m.Reg)
                    {
                        case MACH:
                            s[n] -= 4;
                            if ((s.MACH & 0x00000200) == 0)
                                s.U32[s[n]] = s.MACH & 0x000003FF;
                            else
                                s.U32[s[n]] = s.MACH | 0xFFFFFC00;
                            break;
                        default:
                            s[n] -= 4;
                            s.U32[s[n]] = s[m];
                            break;
                    }
                    return;
                case Mnemonic.Subc when ops is [R m, R n]:
                    ulong tmp0s, tmp1s;
                    tmp1s = s[n] - s[m];
                    tmp0s = s[n];
                    s[n] = (uint)(tmp1s - (ulong)s.T.ToNum());
                    s.T = tmp0s < tmp1s || (tmp1s < s[n]);
                    return;
                case Mnemonic.Subv when ops is [R m, R n]:
                    long destK, srcK, ansK;
                    destK = (long)s[n] >= 0 ? 0 : 1;
                    srcK = (long)s[m] >= 0 ? 0 : 1;
                    srcK += destK;
                    s[n] -= s[m];
                    ansK = (long)s[n] >= 0 ? 0 : 1;
                    ansK += destK;
                    if (srcK == 1)
                        s.T = ansK == 1;
                    else
                        s.T = false;
                    return;
                case Mnemonic.SwapB when ops is [R m, R n]:
                    ulong temp0Sb, temp1Sb;
                    temp0Sb = s[m] & 0xffff0000;
                    temp1Sb = (s[m] & 0x000000ff) << 8;
                    s[n] = (s[m] & 0x0000ff00) >> 8;
                    s[n] = (uint)(s[n] | temp1Sb | temp0Sb);
                    return;
                case Mnemonic.SwapW when ops is [R m, R n]:
                    ulong tempSw;
                    tempSw = (s[m] >> 16) & 0x0000FFFF;
                    s[n] = s[m] << 16;
                    s[n] = (uint)(s[n] | tempSw);
                    return;
                case Mnemonic.TasB when ops is [R n]:
                    long tempBs;
                    tempBs = (long)(s.U8[s[n]]);
                    s.T = tempBs == 0;
                    tempBs |= 0x00000080;
                    s.U8[s[n]] = (byte)tempBs;
                    return;
                case Mnemonic.Trapa when ops is [I8 i]:*/
                    /*
                    // TODO
                    //
                    long immT;
                    immT=(0x000000FF & i);
                    // TODO TRA=immT<<2;
                    s.SSR = (uint)s.SR;
                    s.SPC = s.PC;
                    s.MD = true;
                    s.BL = true;
                    s.RB = true;
                    s.EXPEVT=0x00000160;
                    s.PC=s.VBR+0x00000100;
                    //
                    */
                    /*ExecuteInterrupt((byte)i.Val, s);
                    if ((InterruptTable[0x21] as DOSInterrupts)?.ReturnCode is not null)
                        Halted = true;
                    return;
                case Mnemonic.Tst when ops is [R m, R n]:
                    s.T = (s[n] & s[m]) == 0;
                    return;
                case Mnemonic.Tst when ops is [I8 i, R n]:
                    long tempTs;
                    tempTs = s[R0] & (0x000000FF & (long)i.Val);
                    s.T = tempTs == 0;
                    return;
                case Mnemonic.TstB when ops is [I8 i, R n]:
                    long tempTb;
                    tempTb = (long)(s.U8[s[GBR] + s[R0]]);
                    tempTb &= (0x000000FF & (long)i.Val);
                    s.T = tempTb == 0;
                    return;
                case Mnemonic.Xor when ops is [R m, R n]:
                    s[n] ^= s[m];
                    return;
                case Mnemonic.Xor when ops is [I8 i, R n]:
                    s[R0] = (uint)(s[R0] ^ (0x000000FF & (long)i.Val));
                    return;
                case Mnemonic.XorB when ops is [I8 i, R n]:
                    long tempQt;
                    tempQt = (long)(s.U8[s[GBR] + s[R0]]);
                    tempQt ^= (0x000000FF & (long)i.Val);
                    s.U8[s[GBR] + s[R0]] = (byte)tempQt;
                    return;
                case Mnemonic.Xtrct when ops is [R m, R n]:
                    ulong tempXx;
                    tempXx = (s[m] << 16) & 0xFFFF0000;
                    s[n] = (s[n] >> 16) & 0x0000FFFF;
                    s[n] = (uint)(s[n] | tempXx);
                    return;*/
            }

            var debug = string.Join(", ", ops.Select(o => o.GetType().Name));
            throw new UnhandledOpcodeException(parsed, ops, debug);
        }
    }
}