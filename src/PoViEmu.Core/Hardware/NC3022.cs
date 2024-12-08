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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Decoding.Ops.Jumps;
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

        private void ExecuteInterrupt(byte key, MachineState m)
        {
            if (!InterruptTable.TryGetValue(key, out var handler))
                throw new InvalidOperationException($"Missing interrupt 0x{key:X2}!");
            handler.Handle(key, m);
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
                    CpuIntern.Aaa(m, isSubtract: false);
                    return;
                case Mnemonic.Aad when ops is [U8 u]:
                    CpuIntern.Aad(m, u.Val);
                    return;
                case Mnemonic.Aam when ops is [U8 u]:
                    CpuIntern.Aam(m, u.Val, ExecuteInterrupt);
                    return;
                case Mnemonic.Aas:
                    CpuIntern.Aaa(m, isSubtract: true);
                    return;
                case Mnemonic.Adc when ops is [R8 r, U8 u]:
                    m[r] = CpuIntern.Add8(m, withCarry: true, m[r], u.Val);
                    return;
                case Mnemonic.Adc when ops is [R16 r, MU16 mem]:
                    m[r] = CpuIntern.Add16(m, withCarry: true, m[r], mem[m]);
                    return;
                case Mnemonic.Add when ops is [R8 r, U8 u]:
                    m[r] = CpuIntern.Add8(m, withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Add when ops is [R16 r, R16 t]:
                    m[r] = CpuIntern.Add16(m, withCarry: false, m[r], m[t]);
                    return;
                case Mnemonic.Add when ops is [R16 r, U16 u]:
                    m[r] = CpuIntern.Add16(m, withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Add when ops is [R16 r, MU16 mem]:
                    m[r] = CpuIntern.Add16(m, withCarry: false, m[r], mem[m]);
                    return;
                case Mnemonic.Add when ops is [R16 r, I16 u]:
                    m[r] = CpuIntern.Add16(m, withCarry: false, m[r], (ushort)u.Val);
                    return;
                case Mnemonic.And when ops is [R8 r, U8 u]:
                    m[r] = CpuIntern.And8(m, m[r], u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, U16 u]:
                    m[r] = CpuIntern.And16(m, m[r], u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, I16 u]:
                    m[r] = CpuIntern.And16(m, m[r], (ushort)u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, MU16 mem]:
                    m[r] = CpuIntern.And16(m, m[r], mem[m]);
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
                    CpuIntern.Cbw(m);
                    return;
                case Mnemonic.Clc:
                    CpuIntern.Clc(m);
                    return;
                case Mnemonic.Cld:
                    CpuIntern.Cld(m);
                    return;
                case Mnemonic.Cli:
                    CpuIntern.Cli(m);
                    return;
                case Mnemonic.Cmc:
                    CpuIntern.Cmc(m);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, MU16 mem]:
                    _ = CpuIntern.Sub16(m, withBorrow: false, m[r], mem[m]);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, U16 u]:
                    _ = CpuIntern.Sub16(m, withBorrow: false, m[r], u.Val);
                    return;
                case Mnemonic.Cmp when ops is [MU16 mem, I16 u]:
                    _ = CpuIntern.Sub16(m, withBorrow: false, mem[m], (ushort)u.Val);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, I16 u]:
                    _ = CpuIntern.Sub16(m, withBorrow: false, m[r], (ushort)u.Val);
                    return;
                case Mnemonic.Cmpsb when ops is [MU8 mem, MU8 src]:
                    m.CompareByte(mem[m], src[m]);
                    return;
                case Mnemonic.Cmpsw when ops is [MU16 mem, MU16 src]:
                    m.CompareWord(mem[m], src[m]);
                    return;
                case Mnemonic.Cwd:
                    CpuIntern.Cwd(m);
                    return;
                case Mnemonic.Daa:
                    CpuIntern.Daa(m, isSubtract: false);
                    return;
                case Mnemonic.Das:
                    CpuIntern.Daa(m, isSubtract: true);
                    return;
                case Mnemonic.Dec when ops is [R16 r]:
                    m[r] = CpuIntern.Dec16(m, m[r]);
                    return;
                case Mnemonic.Dec when ops is [MU16 mem]:
                    mem[m] = CpuIntern.Dec16(m, mem[m]);
                    return;
                case Mnemonic.Div when ops is [R16 r]:
                    var dividend = CpuIntern.GetDivi(m);
                    (m.AX, m.DX) = CpuIntern.Split(CpuIntern.Div16(m, signed: false, dividend, m[r], ExecuteInterrupt));
                    return;
                case Mnemonic.Enter when ops is [U16 n, U8 l]:
                    if (l.Val == 0)
                    {
                        m.Push(m.BP);
                        m.BP = m.SP;
                        m.SP -= n.Val;
                    }
                    return;
                case Mnemonic.Fadd when ops is [MF32 mem]:
                    // TODO Floating point?!
                    return;
                case Mnemonic.Fdiv when ops is [MF32 mem]:
                    // TODO Floating point?!
                    return;
                case Mnemonic.Fld when ops is [MF32 mem]:
                    // TODO Floating point?!
                    return;
                case Mnemonic.Fild when ops is [MI16 mem]:
                    // TODO Floating point?!
                    return;
                case Mnemonic.Fmul when ops is [MF32 mem]:
                    // TODO Floating point?!
                    return;
                case Mnemonic.Fstp when ops is [MF32 mem]:
                    // TODO Floating point?!
                    return;
                case Mnemonic.Fsub when ops is [MF32 mem]:
                    // TODO Floating point?!
                    return;
                case Mnemonic.Hlt:
                    Halted = true;
                    return;
                case Mnemonic.Idiv when ops is [R16 r]:
                    var iDivi1 = CpuIntern.GetDivi(m);
                    (m.AX, m.DX) = CpuIntern.Split(CpuIntern.Div16(m, signed: true, iDivi1, m[r],
                        ExecuteInterrupt));
                    return;
                case Mnemonic.Idiv when ops is [MI16 mem]:
                    var iDivi2 = CpuIntern.GetDivi(m);
                    (m.AX, m.DX) = CpuIntern.Split(CpuIntern.Div16(m, signed: true, iDivi2, (ushort)mem[m],
                        ExecuteInterrupt));
                    return;
                case Mnemonic.Imul when ops is [R16 r]:
                    (m.AX, m.DX) = CpuIntern.Split(CpuIntern.Mul16(m, signed: true, m.AX, m[r]));
                    return;
                case Mnemonic.Imul when ops is [MI16 mem]:
                    (m.AX, m.DX) = CpuIntern.Split(CpuIntern.Mul16(m, signed: true, m.AX, (ushort)mem[m]));
                    return;
                case Mnemonic.In when ops is [R8 r, U8 u]:
                    m[r] = OutsideCompute.ReadByteFromPort(null, u.Val);
                    return;
                case Mnemonic.In when ops is [R8 r, R16 t]:
                    m[r] = OutsideCompute.ReadByteFromPort(null, m[t]);
                    return;
                case Mnemonic.Inc when ops is [R16 r]:
                    m[r] = CpuIntern.Inc16(m, m[r]);
                    return;
                case Mnemonic.Inc when ops is [MU16 mem]:
                    mem[m] = CpuIntern.Inc16(m, mem[m]);
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
                case Mnemonic.Mul when ops is [R8 r]:
                    m.AX = CpuIntern.Mul8(m, signed: false, m.AL, m[r]);
                    return;
                case Mnemonic.Mul when ops is [MU16 mem]:
                    (m.AX, m.DX) = CpuIntern.Split(CpuIntern.Mul16(m, signed: false, m.AX, mem[m]));
                    return;
                case Mnemonic.Neg when ops is [R16 r]:
                    m[r] = CpuIntern.Neg16(m, m[r]);
                    return;
                case Mnemonic.Nop:
                    return;
                case Mnemonic.Not when ops is [R16 r]:
                    m[r] = CpuIntern.Not16(m, m[r]);
                    return;
                case Mnemonic.Or when ops is [R16 r, U16 u]:
                    m[r] = CpuIntern.Or16(m, m[r], u.Val);
                    return;
                case Mnemonic.Or when ops is [R16 r, R16 t]:
                    m[r] = CpuIntern.Or16(m, m[r], m[t]);
                    return;
                case Mnemonic.Or when ops is [R16 r, MU16 mem]:
                    m[r] = CpuIntern.Or16(m, m[r], mem[m]);
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
                    m[r] = m.Pop();
                    return;
                case Mnemonic.Popa:
                    m.PopAll();
                    return;
                case Mnemonic.Popf:
                    var popFE = m.Pop();
                    var popT = (Fl)popFE;
                    m.F = popT;
                    return;
                case Mnemonic.Push when ops is [R16 r]:
                    m.Push(m[r]);
                    return;
                case Mnemonic.Push when ops is [I16 u]:
                    var pushV = (ushort)u.Val;
                    m.Push(pushV);
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
                    m[r] = CpuIntern.Rol16(m, withCarry: true, m[r], u.Val);
                    return;
                case Mnemonic.Rcr when ops is [R16 r, U8 u]:
                    m[r] = CpuIntern.Ror16(m, withCarry: true, m[r], u.Val);
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
                    m[r] = CpuIntern.Rol16(m, withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Ror when ops is [R16 r, U8 u]:
                    m[r] = CpuIntern.Ror16(m, withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Sahf:
                    m.StoreStatusFlags(m.AH);
                    return;
                case Mnemonic.Sar when ops is [R16 r, R8 t]:
                    m[r] = CpuIntern.Shr16(m, signed: true, m[r], m[t]);
                    return;
                case Mnemonic.Sar when ops is [R16 r, U8 u]:
                    m[r] = CpuIntern.Shr16(m, signed: true, m[r], u.Val);
                    return;
                case Mnemonic.Sbb when ops is [R16 r, R16 t]:
                    m[r] = CpuIntern.Sub16(m, withBorrow: true, m[r], m[t]);
                    return;
                case Mnemonic.Sbb when ops is [R16 r, U16 u]:
                    m[r] = CpuIntern.Sub16(m, withBorrow: true, m[r], u.Val);
                    return;
                case Mnemonic.Scasb when ops is [R8 r, MU8 mem]:
                    // TODO m[r] = m.ScanByteStr(mem[m]);
                    return;
                case Mnemonic.Scasw when ops is [R16 r, MU16 mem]:
                    // TODO m[r] = m.ScanWordStr(mem[m]);
                    return;
                case Mnemonic.Shl when ops is [R16 r, R8 t]:
                    m[r] = CpuIntern.Shl16(m, m[r], m[t]);
                    return;
                case Mnemonic.Shl when ops is [R16 r, U8 u]:
                    m[r] = CpuIntern.Shl16(m, m[r], u.Val);
                    return;
                case Mnemonic.Shr when ops is [R16 r, R8 t]:
                    m[r] = CpuIntern.Shr16(m, signed: false, m[r], m[t]);
                    return;
                case Mnemonic.Shr when ops is [R16 r, U8 u]:
                    m[r] = CpuIntern.Shr16(m, signed: false, m[r], u.Val);
                    return;
                case Mnemonic.Stc:
                    CpuIntern.Stc(m);
                    return;
                case Mnemonic.Std:
                    CpuIntern.Std(m);
                    return;
                case Mnemonic.Sti:
                    CpuIntern.Sti(m);
                    return;
                case Mnemonic.Stosb when ops is [MU8 mem, R8 r]:
                    mem[m] = m[r];
                    m.IncOrDec(1, useSi: false, useDi: true);
                    return;
                case Mnemonic.Stosw when ops is [MU16 mem, R16 r]:
                    mem[m] = m[r];
                    m.IncOrDec(2, useSi: false, useDi: true);
                    return;
                case Mnemonic.Sub when ops is [R8 r, U8 u]:
                    m[r] = CpuIntern.Sub8(m, withBorrow: false, m[r], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, I16 u]:
                    m[r] = CpuIntern.Sub16(m, withBorrow: false, m[r], (ushort)u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, U16 u]:
                    m[r] = CpuIntern.Sub16(m, withBorrow: false, m[r], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, R16 t]:
                    m[r] = CpuIntern.Sub16(m, withBorrow: false, m[r], m[t]);
                    return;
                case Mnemonic.Sub when ops is [R16 r, MU16 mem]:
                    m[r] = CpuIntern.Sub16(m, withBorrow: false, m[r], mem[m]);
                    return;
                case Mnemonic.Test when ops is [MU16 mem, R16 r]:
                    _ = CpuIntern.And16(m, mem[m], m[r]);
                    return;
                case Mnemonic.Test when ops is [R16 r, R16 t]:
                    _ = CpuIntern.And16(m, m[r], m[t]);
                    return;
                case Mnemonic.Wait:
                    return;
                case Mnemonic.Xlatb when ops is [MU8 mem]:
                    CpuIntern.Xlat8(m, mem.Seg, mem.Base!.Value);
                    return;
                case Mnemonic.Xchg when ops is [R16 r, R16 t]:
                    (m[t], m[r]) = (m[r], m[t]);
                    return;
                case Mnemonic.Xor when ops is [R16 r, R16 t]:
                    m[r] = CpuIntern.Xor16(m, m[r], m[t]);
                    return;
                case Mnemonic.Xor when ops is [R16 r, U16 u]:
                    m[r] = CpuIntern.Xor16(m, m[r], u.Val);
                    return;
                case Mnemonic.Xor when ops is [R16 r, MU16 mem]:
                    m[r] = CpuIntern.Xor16(m, m[r], mem[m]);
                    return;
            }

            throw new UnhandledOpcodeException(parsed, ops);
        }
    }
}