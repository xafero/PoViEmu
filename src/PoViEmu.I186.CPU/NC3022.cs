using System;
using System.Collections.Generic;
using System.Linq;
using Iced.Intel;
using PoViEmu.Base.CPU;
using PoViEmu.I186.CPU.Errors;
using PoViEmu.I186.CPU.Soft;
using PoViEmu.I186.ISA.Decoding;
using PoViEmu.I186.ISA.Ops.Jumps;
using PoViEmu.I186.ISA.Ops.Mems;
using Fl = PoViEmu.I186.ISA.Flagged;
using U8 = PoViEmu.I186.ISA.Ops.Consts.U8Operand;
using U16 = PoViEmu.I186.ISA.Ops.Consts.U16Operand;
using I16 = PoViEmu.I186.ISA.Ops.Consts.I16Operand;
using R8 = PoViEmu.I186.ISA.Ops.Regs.Reg8Operand;
using R16 = PoViEmu.I186.ISA.Ops.Regs.Reg16Operand;
using MU8 = PoViEmu.I186.ISA.Ops.Mems.Mu8Operand;
using MI16 = PoViEmu.I186.ISA.Ops.Mems.Mi16Operand;
using MU16 = PoViEmu.I186.ISA.Ops.Mems.Mu16Operand;
using MU16b = PoViEmu.I186.ISA.Ops.Mems.Mu16BOperand;
using MU16o = PoViEmu.I186.ISA.Ops.Mems.Mu16OOperand;
using MF32 = PoViEmu.I186.ISA.Ops.Mems.Mf32Operand;
using NJ = PoViEmu.I186.ISA.Ops.Jumps.NearOperand;
using FJ = PoViEmu.I186.ISA.Ops.Jumps.FarOperand;

// ReSharper disable InconsistentNaming

namespace PoViEmu.I186.CPU
{
    /// <summary>
    /// The NC3022 is a custom 16bit CPU developed by CASIO based on NEC’s V30MZ
    /// </summary>
    public sealed class NC3022 : ICpu<XInstruction, MachineState>
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
                    m.Aaa(isSubtract: false);
                    return;
                case Mnemonic.Aad when ops is [U8 u]:
                    m.Aad(u.Val);
                    return;
                case Mnemonic.Aam when ops is [U8 u]:
                    m.Aam(u.Val, ExecuteInterrupt);
                    return;
                case Mnemonic.Aas:
                    m.Aaa(isSubtract: true);
                    return;
                case Mnemonic.Adc when ops is [R8 r, U8 u]:
                    m[r] = m.Add8(withCarry: true, m[r], u.Val);
                    return;
                case Mnemonic.Adc when ops is [R16 r, MU16 mem]:
                    m[r] = m.Add16(withCarry: true, m[r], mem[m]);
                    return;
                case Mnemonic.Add when ops is [R8 r, R8 t]:
                    m[r] = m.Add8(withCarry: false, m[r], m[t]);
                    return;
                case Mnemonic.Add when ops is [R8 r, U8 u]:
                    m[r] = m.Add8(withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Add when ops is [R16 r, R16 t]:
                    m[r] = m.Add16(withCarry: false, m[r], m[t]);
                    return;
                case Mnemonic.Add when ops is [R16 r, U16 u]:
                    m[r] = m.Add16(withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Add when ops is [R16 r, MU16 mem]:
                    m[r] = m.Add16(withCarry: false, m[r], mem[m]);
                    return;
                case Mnemonic.Add when ops is [R16 r, I16 u]:
                    m[r] = m.Add16(withCarry: false, m[r], (ushort)u.Val);
                    return;
                case Mnemonic.And when ops is [R8 r, U8 u]:
                    m[r] = m.And8(m[r], u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, U16 u]:
                    m[r] = m.And16(m[r], u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, I16 u]:
                    m[r] = m.And16(m[r], (ushort)u.Val);
                    return;
                case Mnemonic.And when ops is [R16 r, MU16 mem]:
                    m[r] = m.And16(m[r], mem[m]);
                    return;
                case Mnemonic.Bound when ops is [R16 r, MU16b mem]:
                    // TODO bound ?!
                    return;
                case Mnemonic.Call when ops is [MU16o mem]:
                    // TODO Weird jumps ?!
                    /*
                    m.Push(nextCS);
                    m.Push(nextIP);
                    var vFj = mem[m];
                    var mFj = new FJ(vFj.Item1, vFj.Item2);                    
                    mFj.Jump(ref nextCS, ref nextIP);
                    */
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
                    m.Cbw();
                    return;
                case Mnemonic.Clc:
                    m.Clc();
                    return;
                case Mnemonic.Cld:
                    m.Cld();
                    return;
                case Mnemonic.Cli:
                    m.Cli();
                    return;
                case Mnemonic.Cmc:
                    m.Cmc();
                    return;
                case Mnemonic.Cmp when ops is [R8 r, U8 u]:
                    _ = m.Sub8(withBorrow: false, m[r], u.Val);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, MU16 mem]:
                    _ = m.Sub16(withBorrow: false, m[r], mem[m]);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, U16 u]:
                    _ = m.Sub16(withBorrow: false, m[r], u.Val);
                    return;
                case Mnemonic.Cmp when ops is [MU16 mem, U16 u]:
                    _ = m.Sub16(withBorrow: false, mem[m], u.Val);
                    return;                
                case Mnemonic.Cmp when ops is [MU16 mem, I16 u]:
                    _ = m.Sub16(withBorrow: false, mem[m], (ushort)u.Val);
                    return;
                case Mnemonic.Cmp when ops is [R16 r, I16 u]:
                    _ = m.Sub16(withBorrow: false, m[r], (ushort)u.Val);
                    return;
                case Mnemonic.Cmpsb when ops is [MU8 mem, MU8 src]:
                    m.CompareByte(mem[m], src[m]);
                    return;
                case Mnemonic.Cmpsw when ops is [MU16 mem, MU16 src]:
                    m.CompareWord(mem[m], src[m]);
                    return;
                case Mnemonic.Cwd:
                    m.Cwd();
                    return;
                case Mnemonic.Daa:
                    m.Daa(isSubtract: false);
                    return;
                case Mnemonic.Das:
                    m.Daa(isSubtract: true);
                    return;
                case Mnemonic.Dec when ops is [R16 r]:
                    m[r] = m.Dec16(m[r]);
                    return;
                case Mnemonic.Dec when ops is [MU16 mem]:
                    mem[m] = m.Dec16(mem[m]);
                    return;
                case Mnemonic.Div when ops is [R16 r]:
                    var dividend = m.GetDivi();
                    (m.AX, m.DX) = m.Div16(signed: false, dividend, m[r], ExecuteInterrupt).Split();
                    return;
                case Mnemonic.Div when ops is [MU16 mem]:
                    var dividendM = m.GetDivi();
                    (m.AX, m.DX) = m.Div16(signed: false, dividendM, mem[m], ExecuteInterrupt).Split();
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
                    var iDivi1 = m.GetDivi();
                    (m.AX, m.DX) = m.Div16(signed: true, iDivi1, m[r],
                        ExecuteInterrupt).Split();
                    return;
                case Mnemonic.Idiv when ops is [MI16 mem]:
                    var iDivi2 = m.GetDivi();
                    (m.AX, m.DX) = m.Div16(signed: true, iDivi2, (ushort)mem[m],
                        ExecuteInterrupt).Split();
                    return;
                case Mnemonic.Imul when ops is [R16 r]:
                    (m.AX, m.DX) = m.Mul16(signed: true, m.AX, m[r]).Split();
                    return;
                case Mnemonic.Imul when ops is [MI16 mem]:
                    (m.AX, m.DX) = m.Mul16(signed: true, m.AX, (ushort)mem[m]).Split();
                    return;
                case Mnemonic.In when ops is [R8 r, U8 u]:
                    m[r] = OutsideCompute.ReadByteFromPort(null, u.Val);
                    return;
                case Mnemonic.In when ops is [R8 r, R16 t]:
                    m[r] = OutsideCompute.ReadByteFromPort(null, m[t]);
                    return;
                case Mnemonic.Inc when ops is [R8 r]:
                    m[r] = m.Inc8(m[r]);
                    return;
                case Mnemonic.Inc when ops is [R16 r]:
                    m[r] = m.Inc16(m[r]);
                    return;
                case Mnemonic.Inc when ops is [MU16 mem]:
                    mem[m] = m.Inc16(mem[m]);
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
                    if (!m.CF)
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
                    if (m.CX == 0)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Je when ops is [NJ u]:
                    if (m.ZF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jg when ops is [NJ u]:
                    if (!m.ZF && m.SF == m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jge when ops is [NJ u]:
                    if (m.ZF || m.SF == m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jl when ops is [NJ u]:
                    if (!m.ZF && m.SF != m.OF)
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
                    if (!m.ZF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jno when ops is [NJ u]:
                    if (!m.OF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jnp when ops is [NJ u]:
                    if (!m.PF)
                        u.Jump(ref nextIP);
                    return;
                case Mnemonic.Jns when ops is [NJ u]:
                    if (!m.SF)
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
                case Mnemonic.Lea when ops is [R16 r, MU16 mem]:
                    m[r] = mem.OffA(m);
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
                    if (m.CX != 0 && !m.ZF)
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
                    m.AX = m.Mul8(signed: false, m.AL, m[r]);
                    return;
                case Mnemonic.Mul when ops is [MU16 mem]:
                    (m.AX, m.DX) = m.Mul16(signed: false, m.AX, mem[m]).Split();
                    return;
                case Mnemonic.Neg when ops is [R16 r]:
                    m[r] = m.Neg16(m[r]);
                    return;
                case Mnemonic.Nop:
                    return;
                case Mnemonic.Not when ops is [R16 r]:
                    m[r] = m.Not16(m[r]);
                    return;
                case Mnemonic.Or when ops is [R16 r, U16 u]:
                    m[r] = m.Or16(m[r], u.Val);
                    return;
                case Mnemonic.Or when ops is [R16 r, R16 t]:
                    m[r] = m.Or16(m[r], m[t]);
                    return;
                case Mnemonic.Or when ops is [R16 r, MU16 mem]:
                    m[r] = m.Or16(m[r], mem[m]);
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
                    m[r] = m.Rol16(withCarry: true, m[r], u.Val);
                    return;
                case Mnemonic.Rcr when ops is [R16 r, U8 u]:
                    m[r] = m.Ror16(withCarry: true, m[r], u.Val);
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
                    m[r] = m.Rol16(withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Ror when ops is [R16 r, U8 u]:
                    m[r] = m.Ror16(withCarry: false, m[r], u.Val);
                    return;
                case Mnemonic.Sahf:
                    m.StoreStatusFlags(m.AH);
                    return;
                case Mnemonic.Sar when ops is [R16 r, R8 t]:
                    m[r] = m.Shr16(signed: true, m[r], m[t]);
                    return;
                case Mnemonic.Sar when ops is [R16 r, U8 u]:
                    m[r] = m.Shr16(signed: true, m[r], u.Val);
                    return;
                case Mnemonic.Sbb when ops is [R16 r, R16 t]:
                    m[r] = m.Sub16(withBorrow: true, m[r], m[t]);
                    return;
                case Mnemonic.Sbb when ops is [R16 r, U16 u]:
                    m[r] = m.Sub16(withBorrow: true, m[r], u.Val);
                    return;
                case Mnemonic.Scasb when ops is [R8 r, MU8 mem]:
                    // TODO m[r] = m.ScanByteStr(mem[m]);
                    return;
                case Mnemonic.Scasw when ops is [R16 r, MU16 mem]:
                    // TODO m[r] = m.ScanWordStr(mem[m]);
                    return;
                case Mnemonic.Shl when ops is [R16 r, R8 t]:
                    m[r] = m.Shl16(m[r], m[t]);
                    return;
                case Mnemonic.Shl when ops is [R16 r, U8 u]:
                    m[r] = m.Shl16(m[r], u.Val);
                    return;
                case Mnemonic.Shr when ops is [R16 r, R8 t]:
                    m[r] = m.Shr16(signed: false, m[r], m[t]);
                    return;
                case Mnemonic.Shr when ops is [R16 r, U8 u]:
                    m[r] = m.Shr16(signed: false, m[r], u.Val);
                    return;
                case Mnemonic.Stc:
                    m.Stc();
                    return;
                case Mnemonic.Std:
                    m.Std();
                    return;
                case Mnemonic.Sti:
                    m.Sti();
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
                    m[r] = m.Sub8(withBorrow: false, m[r], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, I16 u]:
                    m[r] = m.Sub16(withBorrow: false, m[r], (ushort)u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, U16 u]:
                    m[r] = m.Sub16(withBorrow: false, m[r], u.Val);
                    return;
                case Mnemonic.Sub when ops is [R16 r, R16 t]:
                    m[r] = m.Sub16(withBorrow: false, m[r], m[t]);
                    return;
                case Mnemonic.Sub when ops is [R16 r, MU16 mem]:
                    m[r] = m.Sub16(withBorrow: false, m[r], mem[m]);
                    return;
                case Mnemonic.Test when ops is [MU16 mem, R16 r]:
                    _ = m.And16(mem[m], m[r]);
                    return;
                case Mnemonic.Test when ops is [R16 r, R16 t]:
                    _ = m.And16(m[r], m[t]);
                    return;
                case Mnemonic.Wait:
                    return;
                case Mnemonic.Xlatb when ops is [MU8 mem]:
                    m.Xlat8(mem.Seg, mem.Base!.Value);
                    return;
                case Mnemonic.Xchg when ops is [R16 r, R16 t]:
                    (m[t], m[r]) = (m[r], m[t]);
                    return;
                case Mnemonic.Xor when ops is [R8 r, R8 t]:
                    m[r] = m.Xor8(m[r], m[t]);
                    return;
                case Mnemonic.Xor when ops is [R16 r, R16 t]:
                    m[r] = m.Xor16(m[r], m[t]);
                    return;
                case Mnemonic.Xor when ops is [R16 r, U16 u]:
                    m[r] = m.Xor16(m[r], u.Val);
                    return;
                case Mnemonic.Xor when ops is [R16 r, MU16 mem]:
                    m[r] = m.Xor16(m[r], mem[m]);
                    return;
            }

            throw new UnhandledOpcodeException(parsed, ops);
        }
    }
}
