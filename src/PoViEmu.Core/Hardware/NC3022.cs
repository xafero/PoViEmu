using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Decoding.Ops.Jumps;
using PoViEmu.Core.Decoding.Ops.Mems;
using PoViEmu.Core.Decoding.Ops.Regs;
using PoViEmu.Core.Hardware.Errors;
using static PoViEmu.Common.JsonHelper;
using Fl = PoViEmu.Core.Hardware.Flagged;
using U8 = PoViEmu.Core.Decoding.Ops.Consts.U8Operand;
using U16 = PoViEmu.Core.Decoding.Ops.Consts.U16Operand;
using I16 = PoViEmu.Core.Decoding.Ops.Consts.I16Operand;
using R8 = PoViEmu.Core.Decoding.Ops.Regs.Reg8Operand;
using R16 = PoViEmu.Core.Decoding.Ops.Regs.Reg16Operand;
using MU8 = PoViEmu.Core.Decoding.Ops.Mems.Mu8Operand;
using MI16 = PoViEmu.Core.Decoding.Ops.Mems.Mi16Operand;
using MU16 = PoViEmu.Core.Decoding.Ops.Mems.Mu16Operand;
using MF32 = PoViEmu.Core.Decoding.Ops.Mems.Mf32Operand;
using NJ = PoViEmu.Core.Decoding.Ops.Jumps.NearOperand;
using FJ = PoViEmu.Core.Decoding.Ops.Jumps.FarOperand;
using Reg = PoViEmu.Core.Hardware.AckNow.B16Register;
using Rsg = PoViEmu.Core.Hardware.AckNow.B8Register;
using C = PoViEmu.Core.Hardware.Compute;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Hardware
{
    /// <summary>
    /// The NC3022 is a custom 16bit CPU developed by CASIO based on NEC’s V30MZ
    /// </summary>
    public sealed class NC3022
    {
        public bool Halted { get; set; }
        public IDictionary<byte, IInterruptHandler> InterruptTable { get; }

        public NC3022()
        {
            var dos = new DOSInterrupts();
            var math = new MathInterrupts();
            InterruptTable = new SortedDictionary<byte, IInterruptHandler>
            {
                [DOSInterrupts.MainIntNo] = dos,
                [MathInterrupts.OverflowNo] = math
            };
        }

        public void Execute(XInstruction instruct, MachineState m)
        {
            var nextCS = m.CS;
            var nextIP = instruct.Parsed.NextIP16;
            Execute(instruct, m, true, ref nextCS, ref nextIP);
            m.CS = nextCS;
            m.IP = nextIP;
        }

        private void Execute(XInstruction instruct, MachineState m, bool ignoreUc,
            ref ushort nextCS, ref ushort nextIP)
        {
            var parsed = instruct.Parsed;
            if (parsed.IsInvalidFor16Bit())
            {
                if (ignoreUc) return;
                throw new InvalidOpcodeException(instruct);
            }

            var ops = parsed.GetOps().ToArray();
            switch (parsed.Mnemonic)
            {
                case Mnemonic.Aaa:
                    m.AsciiAdjustAfterAdd();
                    return;
                case Mnemonic.Aad when ops is [U8 u]:
                    m.AsciiAdjustBeforeDiv(u.Val);
                    return;
                case Mnemonic.Aam when ops is [U8 u]:
                    m.AsciiAdjustAfterMul(u.Val);
                    return;
                case Mnemonic.Aas:
                    m.AsciiAdjustAfterSub();
                    return;
                case Mnemonic.Adc when ops is [R8 r, U8 u]:
                    m[r] = C.AddWithCarry(m[r], u.Val, m.CF);
                    return;
                case Mnemonic.Adc when ops is [R16 r, MU16 mem]:
                    m[r] = C.AddWithCarry(m[r], mem[m], m.CF);
                    return;
                case Mnemonic.Add when ops is [R16 r, MU16 mem]:
                    m[r] = C.Add(m[r], mem[m]);
                    return;
                case Mnemonic.Add when ops is [MU16 mem, R16 r]:
                    mem[m] = C.Add(mem[m], m[r]);
                    return;
                case Mnemonic.Add when ops is [MU8 mem, R8 r]:
                    mem[m] = C.Add(mem[m], m[r]);
                    return;
                case Mnemonic.Add when ops is [R8 r, U8 u]:
                    m[r] = C.Add(m[r], u.Val);
                    return;
                case Mnemonic.Add when ops is [R16 r, U16 u]:
                    m[r] = C.Add(m[r], u.Val);
                    return;
                case Mnemonic.Add when ops is [R16 r, R16 t]:
                    m[r] = C.Add(m[r], m[t]);
                    return;
                case Mnemonic.Add when ops is [R8 r, R8 t]:
                    m[r] = C.Add(m[r], m[t]);
                    return;
                case Mnemonic.Add when ops is [R16 r, I16 u]:
                    m[r] = C.Add(m[r], u.Val);
                    return;
                case Mnemonic.And when ops is [R8 r, U8 u]:
                    m[r] = C.LogicalAnd(m[r], u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, MU16 mem]:
                    m[r] = C.LogicalAnd(m[r], mem[m]);
                    return;
                case Mnemonic.And when ops is [R16 r, R16 t]:
                    m[r] = C.LogicalAnd(m[r], m[t]);
                    return;
                case Mnemonic.And when ops is [MU16 mem, R16 t]:
                    mem[m] = C.LogicalAnd(mem[m], m[t]);
                    return;
                case Mnemonic.And when ops is [R16 r, U16 u]:
                    m[r] = C.LogicalAnd(m[r], u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, I16 u]:
                    m[r] = C.LogicalAnd(m[r], u.Val);
                    return;
                case Mnemonic.Call when ops is [FJ u]:
                    m.Push(nextCS);
                    m.Push(nextIP);
                    u.Jump(ref nextCS, ref nextIP);
                    return;
                case Mnemonic.Call when ops is [NJ u]:
                    m.Push(nextIP);
                    u.Jump(ref nextIP);
                    return;
                case Mnemonic.Cbw:
                    m.ConvertByteToWord();
                    return;
                case Mnemonic.Clc:
                    m.ClearCarryFlag();
                    return;
                case Mnemonic.Cld:
                    m.ClearDirectionFlag();
                    return;
                case Mnemonic.Cli:
                    m.ClearInterruptFlag();
                    return;
                case Mnemonic.Cmc:
                    m.ComplementCarryFlag();
                    return;
                case Mnemonic.Cmp when ops is [R16 r, R16 t]:
                    m.Compare(m[r], m[t]);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, U16 t]:
                    m.Compare(m[r], t.Val);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, I16 t]:
                    m.Compare(m[r], t.Val);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, MU16 mem]:
                    m.Compare(m[r], mem[m]);
                    return;
                case Mnemonic.Cmp when ops is [MU16 mem, I16 u]:
                    m.Compare(mem[m], u.Val);
                    return;
                case Mnemonic.Cmpsb when ops is [MU8 mem, MU8 src]:
                    m.CompareByte(mem[m], src[m]);
                    return;
                case Mnemonic.Cmpsw when ops is [MU16 mem, MU16 src]:
                    m.CompareWord(mem[m], src[m]);
                    return;
                case Mnemonic.Cwd:
                    m.ConvertWordToDouble();
                    return;
                case Mnemonic.Daa:
                    m.DecimalAdjustAfterAdd();
                    return;
                case Mnemonic.Das:
                    m.DecimalAdjustAfterSub();
                    return;
                case Mnemonic.Dec when ops is [MU16 mem]:
                    mem[m] = C.Decrement(mem[m]);
                    return;
                case Mnemonic.Dec when ops is [R16 r]:
                    m[r] = C.Decrement(m[r]);
                    return;
                case Mnemonic.Div when ops is [MI16 mem]:
                    m.DivUnsigned(mem[m]);
                    return;
                case Mnemonic.Div when ops is [R16 r]:
                    m.DivUnsigned(m[r]);
                    return;
                case Mnemonic.Enter when ops is [U16 n, U8 l]:
                    if (l.Val == 0)
                    {
                        m.Push(m.BP);
                        m.BP = m.SP;
                        m.SP -= n.Val;
                    }
                    return;
                case Mnemonic.Hlt:
                    Halted = true;
                    return;
                case Mnemonic.Idiv when ops is [MI16 mem]:
                    m.DivSigned(mem[m]);
                    return;
                case Mnemonic.Idiv when ops is [R16 r]:
                    m.DivSigned(m[r]);
                    return;
                case Mnemonic.Imul when ops is [MI16 mem]:
                    m.MulSigned(mem[m]);
                    return;
                case Mnemonic.Imul when ops is [R16 r]:
                    m.MulSigned(m[r]);
                    return;
                case Mnemonic.In when ops is [R8 r, U8 u]:
                    m[r] = OutsideCompute.ReadByteFromPort(null, u.Val);
                    return;
                case Mnemonic.In when ops is [R8 r, R16 t]:
                    m[r] = OutsideCompute.ReadByteFromPort(null, m[t]);
                    return;
                case Mnemonic.Inc when ops is [MU16 mem]:
                    mem[m] = C.Increment(mem[m]);
                    return;
                case Mnemonic.Inc when ops is [R16 r]:
                    m[r] = C.Increment(m[r]);
                    return;
                case Mnemonic.Insb when ops is [MU8 mem, R16 r]:
                    mem[m] = OutsideCompute.ReadByteFromPortToStr(m, m[r]);
                    return;
                case Mnemonic.Insw when ops is [MU16 mem, R16 r]:
                    mem[m] = OutsideCompute.ReadWordFromPortToStr(m, m[r]);
                    return;
                case Mnemonic.Int when ops is [U8 u]:
                    ExecuteInterrupt(u.Val, m);
                    if ((InterruptTable[0x21] as DOSInterrupts)?.ReturnCode is not null)
                        Halted = true;
                    return;
                case Mnemonic.Into:
                    if (m.OF)
                    {
                        m.Push((ushort)m.F);
                        m.TF = false;
                        m.IF = false;
                        ExecuteInterrupt(0x04, m);
                    }
                    return;
                case Mnemonic.Iret:
                    m.F = (Fl)m.Pop();
                    return;
                case Mnemonic.Ja when ops is [NJ u]:
                    if (m is { CF: false, ZF: false })
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jae when ops is [NJ u]:
                    if (m.CF == false)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jb when ops is [NJ u]:
                    if (m.CF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jbe when ops is [NJ u]:
                    if (m.CF || m.ZF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jcxz when ops is [NJ u]:
                    if (m[Reg.CX] == 0)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Je when ops is [NJ u]:
                    if (m.ZF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jg when ops is [NJ u]:
                    if (m.ZF == false && m.SF == m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jge when ops is [NJ u]:
                    if (m.SF == m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jl when ops is [NJ u]:
                    if (m.SF != m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jle when ops is [NJ u]:
                    if (m.ZF || m.SF != m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jmp when ops is [NJ u]:
                    u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jne when ops is [NJ u]:
                    if (m.ZF == false)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jno when ops is [NJ u]:
                    if (m.OF == false)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jnp when ops is [NJ u]:
                    if (m.PF == false)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jns when ops is [NJ u]:
                    if (m.SF == false)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jo when ops is [NJ u]:
                    if (m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jp when ops is [NJ u]:
                    if (m.PF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Js when ops is [NJ u]:
                    if (m.SF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Lahf:
                    m.AH = m.LoadStatusFlags();
                    return;
                case Mnemonic.Leave:
                    m.SP = m.BP;
                    m.BP = m.Pop();
                    return;
                case Mnemonic.Lodsb when ops is [R8 r, MU8 mem]:
                    m[r] = m.LoadByteStr(mem[m]);
                    return;
                case Mnemonic.Lodsw when ops is [R16 r, MU16 mem]:
                    m[r] = m.LoadWordStr(mem[m]);
                    return;
                case Mnemonic.Loop when ops is [NJ u]:
                    if (m.CX < 1)
                        return;
                    m.CX--;
                    if (m.CX != 0)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Loope when ops is [NJ u]:
                    if (m.CX < 1)
                        return;
                    m.CX--;
                    if (m.CX != 0 && m.ZF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Loopne when ops is [NJ u]:
                    if (m.CX < 1)
                        return;
                    m.CX--;
                    if (m.CX != 0 && m.ZF == false)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Mov when ops is [R8 r, MU8 mem]:
                    m[r] = mem[m];
                    return;
                case Mnemonic.Mov when ops is [MU8 mem, R8 r]:
                    mem[m] = m[r];
                    return;
                case Mnemonic.Mov when ops is [MU16 mem, R16 r]:
                    mem[m] = m[r];
                    return;
                case Mnemonic.Mov when ops is [MU16 mem, U16 u]:
                    mem[m] = u.Val;
                    return;
                case Mnemonic.Mov when ops is [R16 r, U16 u]:
                    m[r] = u.Val;
                    return;
                case Mnemonic.Mov when ops is [R16 r, MU16 mem]:
                    m[r] = mem[m];
                    return;
                case Mnemonic.Mov when ops is [R8 r, U8 u]:
                    m[r] = u.Val;
                    return;
                case Mnemonic.Mov when ops is [R8 r, R8 q]:
                    m[r] = m[q];
                    return;
                case Mnemonic.Mov when ops is [R16 r, R16 t]:
                    m[r] = m[t];
                    return;
                case Mnemonic.Mov when ops is [R16 r, U16 t]:
                    m[r] = t.Val;
                    return;
                case Mnemonic.Movsb when ops is [MU8 nT, MU8 nS]:
                    nT[m] = nS[m];
                    m.IncOrDec(1, useSi: true, useDi: true);
                    return;
                case Mnemonic.Movsw when ops is [MU16 nT, MU16 nS]:
                    nT[m] = nS[m];
                    m.IncOrDec(2, useSi: true, useDi: true);
                    return;
                case Mnemonic.Mul when ops is [MU16 mem]:
                    m.UnsignedMul(mem[m]);
                    return;
                case Mnemonic.Mul when ops is [R8 r]:
                    m.UnsignedMul(m[r]);
                    return;
                case Mnemonic.Mul when ops is [R16 r]:
                    m.UnsignedMul(m[r]);
                    return;
                case Mnemonic.Neg when ops is [R16 r]:
                    m[r] = m.TwoComplNeg(m[r]);
                    return;
                case Mnemonic.Nop:
                    return;
                case Mnemonic.Not when ops is [R16 r]:
                    m[r] = m.OneComplNeg(m[r]);
                    return;
                case Mnemonic.Or when ops is [R16 r, MU16 mem]:
                    m[r] = C.LogicalInclOr(m[r], mem[m]);
                    return;
                case Mnemonic.Or when ops is [R16 r, R16 t]:
                    m[r] = C.LogicalInclOr(m[r], m[t]);
                    return;
                case Mnemonic.Or when ops is [R16 r, U16 u]:
                    m[r] = C.LogicalInclOr(m[r], u.Val);
                    return;
                case Mnemonic.Out when ops is [U8 u, R8 r]:
                    OutsideCompute.WriteByteToPort(null, u.Val, m[r]);
                    return;
                case Mnemonic.Outsb when ops is [R16 r, MU8 mem]:
                    m.WriteByteToPortStr(m[r], mem[m]);
                    return;
                case Mnemonic.Outsw when ops is [R16 r, MU16 mem]:
                    m.WriteWordToPortStr(m[r], mem[m]);
                    return;
                case Mnemonic.Pop when ops is [R16 r]:
                    var popE = m.Pop();
                    m[r] = popE;
                    return;
                case Mnemonic.Popa:
                    m.PopAll();
                    return;
                case Mnemonic.Popf:
                    var popEF = m.Pop();
                    m.F = (Fl)popEF;
                    return;
                case Mnemonic.Push when ops is [R16 r]:
                    var pushE = m[r];
                    m.Push(pushE);
                    return;
                case Mnemonic.Push when ops is [I16 v]:
                    var pushV = v.Val;
                    m.Push((ushort)pushV);
                    return;
                case Mnemonic.Pusha:
                    m.PushAll();
                    return;
                case Mnemonic.Pushf:
                    var pushFE = m.F;
                    var pushT = (ushort)pushFE;
                    m.Push(pushT);
                    return;
                case Mnemonic.Rcl when ops is [R16 r, U8 u]:
                    m[r] = MachTool.ShiftLeft(m[r], u.Val);
                    return;
                case Mnemonic.Rcr when ops is [R16 r, U8 u]:
                    m[r] = MachTool.ShiftRight(m[r], u.Val);
                    return;
                case Mnemonic.Retf:
                    var retFar = m.Pop();
                    var retSeg = m.Pop();
                    nextCS = retSeg;
                    nextIP = retFar;
                    return;
                case Mnemonic.Ret:
                    var retNear = m.Pop();
                    nextIP = retNear;
                    return;
                case Mnemonic.Rol when ops is [R16 r, U8 u]:
                    m[r] = MachTool.ShiftLeft(m[r], u.Val);
                    return;
                case Mnemonic.Ror when ops is [R16 r, U8 u]:
                    m[r] = MachTool.ShiftRight(m[r], u.Val);
                    return;
                case Mnemonic.Sahf:
                    m.StoreStatusFlags(m.AH);
                    return;
                case Mnemonic.Sar when ops is [R16 r, R16 t]:
                    m[r] = C.ShiftRight(m[r], m[t]);
                    return;
                case Mnemonic.Sar when ops is [R16 r, R8 t]:
                    m[r] = C.ShiftRight(m[r], m[t]);
                    return;
                case Mnemonic.Sar when ops is [R16 r, U8 t]:
                    m[r] = C.ShiftRight(m[r], t.Val);
                    return;
                case Mnemonic.Sbb when ops is [R16 r, U16 u]:
                    m[r] = C.SubWithBorrow(m[r], u.Val, m.CF);
                    return;
                case Mnemonic.Sbb when ops is [R16 r, R16 t]:
                    m[r] = C.SubWithBorrow(m[r], m[t], m.CF);
                    return;
                case Mnemonic.Scasb when ops is [R8 r, MU8 mem]:
                    m[r] = m.ScanByteStr(mem[m]);
                    return;
                case Mnemonic.Scasw when ops is [R16 r, MU16 mem]:
                    m[r] = m.ScanWordStr(mem[m]);
                    return;
                case Mnemonic.Shl when ops is [R16 r, U8 u]:
                    m[r] = C.ShiftLeft(m[r], u.Val);
                    return;
                case Mnemonic.Shl when ops is [R16 r, R16 t]:
                    m[r] = C.ShiftLeft(m[r], m[t]);
                    return;
                case Mnemonic.Shl when ops is [R16 r, R8 t]:
                    m[r] = C.ShiftLeft(m[r], m[t]);
                    return;
                case Mnemonic.Shr when ops is [R16 r, U8 u]:
                    m[r] = C.ShiftRight(m[r], u.Val);
                    return;
                case Mnemonic.Shr when ops is [R16 r, R8 t]:
                    m[r] = C.ShiftRight(m[r], m[t]);
                    return;
                case Mnemonic.Stc:
                    m.SetCarryFlag();
                    return;
                case Mnemonic.Std:
                    m.SetDirectionFlag();
                    return;
                case Mnemonic.Sti:
                    m.SetInterruptFlag();
                    return;
                case Mnemonic.Stosb when ops is [MU8 mem, R8 r]:
                    mem[m] = m[r];
                    m.IncOrDec(1, useSi: false, useDi: true);
                    return;
                case Mnemonic.Stosw when ops is [MU16 mem, R16 r]:
                    mem[m] = m[r];
                    m.IncOrDec(2, useSi: false, useDi: true);
                    return;
                case Mnemonic.Sub when ops is [R16 r, MU16 mem]:
                    m[r] = C.Sub(m[r], mem[m]);
                    return;
                case Mnemonic.Sub when ops is [MU16 mem, I16 u]:
                    mem[m] = C.Sub(mem[m], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R8 r, U8 u]:
                    m[r] = C.Sub(m[r], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, I16 u]:
                    m[r] = C.Sub(m[r], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, U16 u]:
                    m[r] = C.Sub(m[r], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, R16 t]:
                    m[r] = C.Sub(m[r], m[t]);
                    return;
                case Mnemonic.Test when ops is [R16 r, U16 u]:
                    m.Test(m[r], u.Val);
                    return;
                case Mnemonic.Test when ops is [R16 r, R16 t]:
                    m.Test(m[r], m[t]);
                    return;
                case Mnemonic.Test when ops is [MU16 mem, R16 t]:
                    m.Test(mem[m], m[t]);
                    return;
                case Mnemonic.Wait:
                    return;
                case Mnemonic.Xchg when ops is [R16 r, R16 t]:
                    var xchgA = m[t];
                    var xchgB = m[r];
                    m[r] = xchgA;
                    m[t] = xchgB;
                    return;
                case Mnemonic.Xlatb:
                    var xlatAddr = (ushort)(m[Reg.BX] + m[Rsg.AL]);
                    m[Rsg.AL] = m.U8[m.DS, xlatAddr];
                    return;
                case Mnemonic.Xor when ops is [R16 r, MU16 mem]:
                    m[r] = C.LogicalExclOr(m[r], mem[m]);
                    return;
                case Mnemonic.Xor when ops is [R16 r, R16 t]:
                    m[r] = C.LogicalExclOr(m[r], m[t]);
                    return;
                case Mnemonic.Xor when ops is [R16 r, U16 u]:
                    m[r] = C.LogicalExclOr(m[r], u.Val);
                    return;
            }

            File.WriteAllText("op.json", ToJson(parsed, noDefaults: true), Encoding.UTF8);
            throw new UnhandledOpcodeException(parsed, ops);
        }

        private void ExecuteInterrupt(byte key, MachineState m)
        {
            if (!InterruptTable.TryGetValue(key, out var handler))
                throw new InvalidOperationException($"Missing interrupt 0x{key:X2}!");
            handler.Handle(key, m);
        }
    }
}