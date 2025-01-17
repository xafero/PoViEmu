using System;
using System.Collections.Generic;
using System.Linq;
using PoViEmu.Base.CPU;
using PoViEmu.SH3.CPU.Errors;
using PoViEmu.SH3.CPU.Soft;
using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Decoding;
using I8 = PoViEmu.SH3.ISA.Ops.Consts.I8Operand;
using U8 = PoViEmu.SH3.ISA.Ops.Consts.U8Operand;
using MU8 = PoViEmu.SH3.ISA.Ops.Mems.Mu8Operand;
using MU16 = PoViEmu.SH3.ISA.Ops.Mems.Mu16Operand;
using MU32 = PoViEmu.SH3.ISA.Ops.Mems.Mu32Operand;
using R = PoViEmu.SH3.ISA.Ops.Regs.Reg32Operand;

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

        internal void ExecuteInterrupt(byte key, MachineState m)
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
                    Compute.And(s, m, n);
                    return;
                case Mnemonic.And when ops is [U8 i, R n]:
                    Compute.And(s, i.Val, n);
                    return;
                case Mnemonic.AndB when ops is [U8 i, MU8 mem]:
                    Compute.Andb(s, i.Val, mem);
                    return;
                /*case Mnemonic.Bf when ops is [NO d]:
                    Jumping.BranchIfFalse(s, d.Diff, ref nextIP);
                    return;*/
                /*case Mnemonic.BfS when ops is [NO d]:
                    Jumping.BranchIfFalseDelay(s, d.Diff, ref nextIP);
                    return;*/
                /*case Mnemonic.Bra when ops is [NO d]:
                    Jumping.Branch(s, d.Diff, ref nextIP);
                    return;*/
                case Mnemonic.Braf when ops is [R m]:
                    Jumping.BranchFar(s, m, ref nextIP);
                    return;
                /*case Mnemonic.Bsr when ops is [NO d]:
                    Jumping.BranchSubroutine(s, d.Diff, ref nextIP);
                    return;*/
                case Mnemonic.Bsrf when ops is [R m]:
                    Jumping.BranchSubroutineFar(s, m, ref nextIP);
                    return;
                /*case Mnemonic.Bt when ops is [NO d]:
                    Jumping.BranchIfTrue(s, d.Diff, ref nextIP);
                    return;*/
                /*case Mnemonic.BtS when ops is [NO d]:
                    Jumping.BranchIfTrueDelay(s, d.Diff, ref nextIP);
                    return;*/
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
                    Compute.CmpEq(s, m, n);
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
                    Compute.Div1(s, m, n);
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
                    Compute.Extsb(s, m, n);
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
                    Jumping.JumpSubroutine(s, mem, ref nextIP);
                    return;
                case Mnemonic.Ldc when ops is [R m, R n]:
                    Compute.Ldc(s, m, n);
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
                case Mnemonic.MacL when ops is [MU32 m, MU32 n]:
                    Compute.Macl(s, m, n);
                    return;
                case Mnemonic.MacW when ops is [R m, R n]:
                    Compute.Macw(s, m, n);
                    return;
                case Mnemonic.MacW when ops is [MU16 m, MU16 n]:
                    Compute.Macw(s, m, n);
                    return;
                case Mnemonic.Mov when ops is [R m, R n]:
                    Compute.Mov(s, m, n);
                    return;
                case Mnemonic.Mov when ops is [I8 i, R n]:
                    Compute.Mov(s, i.Val, n);
                    return;
                case Mnemonic.MovB when ops is [R r, MU8 mem]:
                    Compute.Mov(s, r, mem);
                    return;
                case Mnemonic.MovW when ops is [R r, MU32 mem]:
                    Compute.Mov(s, r, mem);
                    return;
                case Mnemonic.MovW when ops is [MU32 mem, R r]:
                    // TODO
                    return;
                case Mnemonic.MovW when ops is [R r, MU16 mem]:
                    Compute.MovW(s, r, mem);
                    return;
                case Mnemonic.MovB when ops is [R r, MU32 mem]:
                    // TODO
                    return;
                /*case Mnemonic.MovW when ops is [NO m, R n]:
                    Compute.MovW(s, m, n);
                    return;*/
                case Mnemonic.MovB when ops is [MU32 mem, R r]:
                    // TODO
                    return;
                /*case Mnemonic.MovL when ops is [NO m, R n]:
                    Compute.MovL(s, m, n);
                    return;*/
                case Mnemonic.MovL when ops is [R r, MU32 mem]:
                    Compute.Mov(s, r, mem);
                    return;
                case Mnemonic.MovB when ops is [MU8 mem, R r]:
                    Compute.Mov(s, mem, r);
                    return;
                case Mnemonic.MovW when ops is [MU16 mem, R n]:
                    Compute.Mov(s, mem, n);
                    return;
                case Mnemonic.MovL when ops is [MU32 mem, R n]:
                    Compute.Mov(s, mem, n);
                    return;
                /*case Mnemonic.Mova when ops is [NO d, R n]:
                    Compute.Mova(s, d.Diff, n);
                    return;*/
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
                    // TODO Load cache?!
                    return;
                case Mnemonic.Rotcl when ops is [R n]:
                    Compute.Rotcl(s, n);
                    return;
                case Mnemonic.Rotcr when ops is [R n]:
                    Compute.Rotcr(s, n);
                    return;
                case Mnemonic.Rotl when ops is [R n]:
                    Compute.Rotl(s, n);
                    return;
                case Mnemonic.Rotr when ops is [R n]:
                    Compute.Rotr(s, n);
                    return;
                case Mnemonic.Rte:
                    Jumping.ReturnFromEx(s, ref nextIP);
                    return;
                case Mnemonic.Rts:
                    Jumping.ReturnSubroutine(s, ref nextIP);
                    return;
                case Mnemonic.Sets:
                    Compute.Sets(s);
                    return;
                case Mnemonic.Sett:
                    Compute.Sett(s);
                    return;
                case Mnemonic.Shad when ops is [R m, R n]:
                    Compute.Shad(s, m, n);
                    return;
                case Mnemonic.Shal when ops is [R n]:
                    Compute.Shal(s, n);
                    return;
                case Mnemonic.Shar when ops is [R n]:
                    Compute.Shar(s, n);
                    return;
                case Mnemonic.Shld when ops is [R m, R n]:
                    Compute.Shld(s, m, n);
                    return;
                case Mnemonic.Shll when ops is [R n]:
                    Compute.Shll(s, n);
                    return;
                case Mnemonic.Shll2 when ops is [R n]:
                    Compute.Shlln(s, 2, n);
                    return;
                case Mnemonic.Shll8 when ops is [R n]:
                    Compute.Shlln(s, 8, n);
                    return;
                case Mnemonic.Shll16 when ops is [R n]:
                    Compute.Shlln(s, 16, n);
                    return;
                case Mnemonic.Shlr when ops is [R n]:
                    Compute.Shlr(s, n);
                    return;
                case Mnemonic.Shlr2 when ops is [R n]:
                    Compute.Shlrn(s, 2, n);
                    return;
                case Mnemonic.Shlr8 when ops is [R n]:
                    Compute.Shlrn(s, 8, n);
                    return;
                case Mnemonic.Shlr16 when ops is [R n]:
                    Compute.Shlrn(s, 16, n);
                    return;
                case Mnemonic.Sleep:
                    Special.Sleep(s, this, ref nextIP);
                    return;
                case Mnemonic.Stc when ops is [R m, R n]:
                    Compute.Stc(s, m, n);
                    return;
                case Mnemonic.StcL when ops is [R m, MU32 mem]:
                    Compute.StcL(s, m, mem);
                    return;
                case Mnemonic.Sts when ops is [R m, R n]:
                    Compute.Sts(s, m, n);
                    return;
                case Mnemonic.StsL when ops is [R m, MU32 mem]:
                    Compute.StsL(s, m, mem);
                    return;
                case Mnemonic.Sub when ops is [R m, R n]:
                    Compute.Sub(s, m, n);
                    return;
                case Mnemonic.Subc when ops is [R m, R n]:
                    Compute.Subc(s, m, n);
                    return;
                case Mnemonic.Subv when ops is [R m, R n]:
                    Compute.Subv(s, m, n);
                    return;
                case Mnemonic.SwapB when ops is [R m, R n]:
                    Compute.Swapb(s, m, n);
                    return;
                case Mnemonic.SwapW when ops is [R m, R n]:
                    Compute.Swapw(s, m, n);
                    return;
                case Mnemonic.TasB when ops is [MU8 mem]:
                    Compute.Tasb(s, mem);
                    return;
                case Mnemonic.Trapa when ops is [I8 i]:
                    Special.Trapa(s, this, i.Val);
                    return;
                case Mnemonic.Tst when ops is [R m, R n]:
                    Compute.Tst(s, m, n);
                    return;
                case Mnemonic.Tst when ops is [U8 i, R n]:
                    Compute.Tst(s, i.Val, n);
                    return;
                case Mnemonic.TstB when ops is [U8 i, MU8 mem]:
                    Compute.Tstb(s, i.Val, mem);
                    return;
                case Mnemonic.Xor when ops is [R m, R n]:
                    Compute.Xor(s, m, n);
                    return;
                case Mnemonic.Xor when ops is [U8 i, R n]:
                    Compute.Xor(s, i.Val, n);
                    return;
                case Mnemonic.XorB when ops is [U8 i, MU8 mem]:
                    Compute.Xorb(s, i.Val, mem);
                    return;
                case Mnemonic.Xtrct when ops is [R m, R n]:
                    Compute.Xtrct(s, m, n);
                    return;
            }

            var debug = string.Join(", ", ops.Select(o => o.GetType().Name));
            throw new UnhandledOpcodeException(parsed, ops, debug);
        }
    }
}