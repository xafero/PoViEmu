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
using AO = PoViEmu.SH3.ISA.Ops.Places.AddressOperand;
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
            var isDelayed = m.dPC != null;
            var nextIP = instruct.Parsed.NextIP32;
            var delayIP = default(uint?);
            Execute(instruct, m, true, ref nextIP, ref delayIP);
            m.dPC = delayIP;
            if (isDelayed) return;
            m.PC = nextIP;
        }

        private void Execute(XInstruction instruct, MachineState s, bool ignoreUc,
            ref uint nextIP, ref uint? delayIP)
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
                    s.Add(m, n);
                    return;
                case Mnemonic.Add when ops is [I8 i, R r]:
                    s.Add(i.Val, r);
                    return;
                case Mnemonic.Addc when ops is [R m, R n]:
                    s.Addc(m, n);
                    return;
                case Mnemonic.Addv when ops is [R m, R n]:
                    s.Addv(m, n);
                    return;
                case Mnemonic.And when ops is [R m, R n]:
                    s.And(m, n);
                    return;
                case Mnemonic.And when ops is [U8 i, R n]:
                    s.And(i.Val, n);
                    return;
                case Mnemonic.AndB when ops is [U8 i, MU8 mem]:
                    s.Andb(i.Val, mem);
                    return;
                case Mnemonic.Bf when ops is [AO d]:
                    s.BranchIfFalse(d, ref nextIP);
                    return;
                case Mnemonic.BfS when ops is [AO d]:
                    s.BranchIfFalseDelay(d, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Bra when ops is [AO d]:
                    s.Branch(d, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Braf when ops is [R m]:
                    s.BranchFar(m, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Bsr when ops is [AO d]:
                    s.BranchSubroutine(d, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Bsrf when ops is [R m]:
                    s.BranchSubroutineFar(m, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Bt when ops is [AO d]:
                    s.BranchIfTrue(d, ref nextIP);
                    return;
                case Mnemonic.BtS when ops is [AO d]:
                    s.BranchIfTrueDelay(d, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Clrmac:
                    s.Clrmac();
                    return;
                case Mnemonic.Clrs:
                    s.Clrs();
                    return;
                case Mnemonic.Clrt:
                    s.Clrt();
                    return;
                case Mnemonic.CmpEq when ops is [R m, R n]:
                    s.CmpEq(m, n);
                    return;
                case Mnemonic.CmpGe when ops is [R m, R n]:
                    s.CmpGe(m, n);
                    return;
                case Mnemonic.CmpGt when ops is [R m, R n]:
                    s.CmpGt(m, n);
                    return;
                case Mnemonic.CmpHi when ops is [R m, R n]:
                    s.CmpHi(m, n);
                    return;
                case Mnemonic.CmpHs when ops is [R m, R n]:
                    s.CmpHs(m, n);
                    return;
                case Mnemonic.CmpPl when ops is [R n]:
                    s.CmpPl(n);
                    return;
                case Mnemonic.CmpPz when ops is [R n]:
                    s.CmpPz(n);
                    return;
                case Mnemonic.CmpStr when ops is [R m, R n]:
                    s.CmpStr(m, n);
                    return;
                case Mnemonic.CmpEq when ops is [I8 i, R n]:
                    s.CmpEq(i.Val, n);
                    return;
                case Mnemonic.Div0s when ops is [R m, R n]:
                    s.Div0s(m, n);
                    return;
                case Mnemonic.Div0u:
                    s.Div0u();
                    return;
                case Mnemonic.Div1 when ops is [R m, R n]:
                    s.Div1(m, n);
                    return;
                case Mnemonic.DmulsL when ops is [R m, R n]:
                    s.DmulsL(m, n);
                    return;
                case Mnemonic.DmuluL when ops is [R m, R n]:
                    s.DmuluL(m, n);
                    return;
                case Mnemonic.Dt when ops is [R n]:
                    s.Dt(n);
                    return;
                case Mnemonic.ExtsB when ops is [R m, R n]:
                    s.Extsb(m, n);
                    return;
                case Mnemonic.ExtsW when ops is [R m, R n]:
                    s.Extsw(m, n);
                    return;
                case Mnemonic.ExtuB when ops is [R m, R n]:
                    s.Extub(m, n);
                    return;
                case Mnemonic.ExtuW when ops is [R m, R n]:
                    s.Extuw(m, n);
                    return;
                case Mnemonic.Jmp when ops is [AO m]:
                    s.Jump(m, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Jsr when ops is [MU32 mem]:
                    s.JumpSubroutine(mem, ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Ldc when ops is [R m, R n]:
                    s.Ldc(m, n);
                    return;
                case Mnemonic.LdcL when ops is [MU32 mem, R n]:
                    s.LdcL(mem, n);
                    return;
                case Mnemonic.Lds when ops is [R m, R n]:
                    s.Lds(m, n);
                    return;
                case Mnemonic.LdsL when ops is [MU32 mem, R n]:
                    s.LdsL(mem, n);
                    return;
                case Mnemonic.Ldtlb:
                    s.Ldtlb();
                    return;
                case Mnemonic.MacL when ops is [MU32 m, MU32 n]:
                    s.Macl(m, n);
                    return;
                case Mnemonic.MacW when ops is [MU16 m, MU16 n]:
                    s.Macw(m, n);
                    return;
                case Mnemonic.Mov when ops is [R m, R n]:
                    s.Mov(m, n);
                    return;
                case Mnemonic.Mov when ops is [I8 i, R n]:
                    s.Mov(i.Val, n);
                    return;
                case Mnemonic.MovB when ops is [R r, MU8 mem]:
                    s.Mov(r, mem);
                    return;
                case Mnemonic.MovW when ops is [MU16 mem, R r]:
                    s.Mov(mem, r);
                    return;
                case Mnemonic.MovW when ops is [R r, MU16 mem]:
                    s.MovW(r, mem);
                    return;
                case Mnemonic.MovL when ops is [R r, MU32 mem]:
                    s.Mov(r, mem);
                    return;
                case Mnemonic.MovB when ops is [MU8 mem, R r]:
                    s.Mov(mem, r);
                    return;
                case Mnemonic.MovW when ops is [MU16 mem, R n]:
                    s.Mov(mem, n);
                    return;
                case Mnemonic.MovL when ops is [MU32 mem, R n]:
                    s.Mov(mem, n);
                    return;
                case Mnemonic.Mova when ops is [AO d, R n]:
                    s.Mova(d, n);
                    return;
                case Mnemonic.Movt when ops is [R n]:
                    s.Movt(n);
                    return;
                case Mnemonic.MulL when ops is [R m, R n]:
                    s.MulL(m, n);
                    return;
                case Mnemonic.MulsW when ops is [R m, R n]:
                    s.MulsW(m, n);
                    return;
                case Mnemonic.MuluW when ops is [R m, R n]:
                    s.MuluW(m, n);
                    return;
                case Mnemonic.Neg when ops is [R m, R n]:
                    s.Neg(m, n);
                    return;
                case Mnemonic.Negc when ops is [R m, R n]:
                    s.Negc(m, n);
                    return;
                case Mnemonic.Nop:
                    return;
                case Mnemonic.Not when ops is [R m, R n]:
                    s.Not(m, n);
                    return;
                case Mnemonic.Or when ops is [R m, R n]:
                    s.Or(m, n);
                    return;
                case Mnemonic.Or when ops is [U8 i, R n]:
                    s.Or(i.Val, n);
                    return;
                case Mnemonic.OrB when ops is [U8 i, MU8 mem]:
                    s.Or(i.Val, mem);
                    return;
                case Mnemonic.Pref when ops is [MU32 mem]:
                    // TODO Load cache?!
                    return;
                case Mnemonic.Rotcl when ops is [R n]:
                    s.Rotcl(n);
                    return;
                case Mnemonic.Rotcr when ops is [R n]:
                    s.Rotcr(n);
                    return;
                case Mnemonic.Rotl when ops is [R n]:
                    s.Rotl(n);
                    return;
                case Mnemonic.Rotr when ops is [R n]:
                    s.Rotr(n);
                    return;
                case Mnemonic.Rte:
                    s.ReturnFromEx(ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Rts:
                    s.ReturnSubroutine(ref nextIP, ref delayIP);
                    return;
                case Mnemonic.Sets:
                    s.Sets();
                    return;
                case Mnemonic.Sett:
                    s.Sett();
                    return;
                case Mnemonic.Shad when ops is [R m, R n]:
                    s.Shad(m, n);
                    return;
                case Mnemonic.Shal when ops is [R n]:
                    s.Shal(n);
                    return;
                case Mnemonic.Shar when ops is [R n]:
                    s.Shar(n);
                    return;
                case Mnemonic.Shld when ops is [R m, R n]:
                    s.Shld(m, n);
                    return;
                case Mnemonic.Shll when ops is [R n]:
                    s.Shll(n);
                    return;
                case Mnemonic.Shll2 when ops is [R n]:
                    s.Shlln(2, n);
                    return;
                case Mnemonic.Shll8 when ops is [R n]:
                    s.Shlln(8, n);
                    return;
                case Mnemonic.Shll16 when ops is [R n]:
                    s.Shlln(16, n);
                    return;
                case Mnemonic.Shlr when ops is [R n]:
                    s.Shlr(n);
                    return;
                case Mnemonic.Shlr2 when ops is [R n]:
                    s.Shlrn(2, n);
                    return;
                case Mnemonic.Shlr8 when ops is [R n]:
                    s.Shlrn(8, n);
                    return;
                case Mnemonic.Shlr16 when ops is [R n]:
                    s.Shlrn(16, n);
                    return;
                case Mnemonic.Sleep:
                    s.Sleep(this, ref nextIP);
                    return;
                case Mnemonic.Stc when ops is [R m, R n]:
                    s.Stc(m, n);
                    return;
                case Mnemonic.StcL when ops is [R m, MU32 mem]:
                    s.StcL(m, mem);
                    return;
                case Mnemonic.Sts when ops is [R m, R n]:
                    s.Sts(m, n);
                    return;
                case Mnemonic.StsL when ops is [R m, MU32 mem]:
                    s.StsL(m, mem);
                    return;
                case Mnemonic.Sub when ops is [R m, R n]:
                    s.Sub(m, n);
                    return;
                case Mnemonic.Subc when ops is [R m, R n]:
                    s.Subc(m, n);
                    return;
                case Mnemonic.Subv when ops is [R m, R n]:
                    s.Subv(m, n);
                    return;
                case Mnemonic.SwapB when ops is [R m, R n]:
                    s.Swapb(m, n);
                    return;
                case Mnemonic.SwapW when ops is [R m, R n]:
                    s.Swapw(m, n);
                    return;
                case Mnemonic.TasB when ops is [MU8 mem]:
                    s.Tasb(mem);
                    return;
                case Mnemonic.Trapa when ops is [U8 i]:
                    s.Trapa(this, i.Val);
                    return;
                case Mnemonic.Tst when ops is [R m, R n]:
                    s.Tst(m, n);
                    return;
                case Mnemonic.Tst when ops is [U8 i, R n]:
                    s.Tst(i.Val, n);
                    return;
                case Mnemonic.TstB when ops is [U8 i, MU8 mem]:
                    s.Tstb(i.Val, mem);
                    return;
                case Mnemonic.Xor when ops is [R m, R n]:
                    s.Xor(m, n);
                    return;
                case Mnemonic.Xor when ops is [U8 i, R n]:
                    s.Xor(i.Val, n);
                    return;
                case Mnemonic.XorB when ops is [U8 i, MU8 mem]:
                    s.Xorb(i.Val, mem);
                    return;
                case Mnemonic.Xtrct when ops is [R m, R n]:
                    s.Xtrct(m, n);
                    return;
            }

            var debug = string.Join(", ", ops.Select(o => o.GetType().Name));
            throw new UnhandledOpcodeException(parsed, ops, debug);
        }
    }
}