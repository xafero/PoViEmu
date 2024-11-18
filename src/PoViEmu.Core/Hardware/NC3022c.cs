// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Hardware.AckNow;
using PoViEmu.Core.Hardware.Errors;
using static PoViEmu.Common.JsonHelper;
using Fl = PoViEmu.Core.Hardware.Flagged;
using U8 = PoViEmu.Core.Decoding.Ops.Consts.U8Operand;
using I8 = PoViEmu.Core.Decoding.Ops.Consts.I8Operand;
using U16 = PoViEmu.Core.Decoding.Ops.Consts.U16Operand;
using I16 = PoViEmu.Core.Decoding.Ops.Consts.I16Operand;
using R8 = PoViEmu.Core.Decoding.Ops.Regs.Reg8Operand;
using R16 = PoViEmu.Core.Decoding.Ops.Regs.Reg16Operand;
using MU8 = PoViEmu.Core.Decoding.Ops.Mu8Operand;
using MI16 = PoViEmu.Core.Decoding.Ops.Mi16Operand;
using MU16 = PoViEmu.Core.Decoding.Ops.Mu16Operand;
using MF32 = PoViEmu.Core.Decoding.Ops.Mf32Operand;
using Reg = PoViEmu.Core.Hardware.AckNow.B16Register;
using Rsg = PoViEmu.Core.Hardware.AckNow.B8Register;

namespace PoViEmu.Core.Hardware
{
    /// <summary>
    /// The NC3022 is a custom 16bit CPU developed by CASIO based on NEC’s V30MZ
    /// </summary>
    public sealed class NC3022
    {
        public bool Halted { get; set; }
        public IDictionary<byte, IInterruptHandler> InterruptTable { get; }

        public NC3022c()
        {
            var dos = new DOSInterrupts();
            InterruptTable = new SortedDictionary<byte, IInterruptHandler>
            {
                [DOSInterrupts.MainIntNo] = dos
            };
        }

        public void Execute(XInstruction instruct, MachineState m)
        {
            var nextIP = instruct.Parsed.NextIP16;
            Execute(instruct, m, ref nextIP, true);
            m.IP = nextIP;
        }

        private void Execute(XInstruction instruct, MachineState m, ref ushort nextIP, bool ignoreUc)
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
                // case Mnemonic.Aaa: return;
                // case Mnemonic.Aad: return;
                // case Mnemonic.Aam: return;
                // case Mnemonic.Aas: return;
                case Mnemonic.Adc when ops is [R8 r, U8 u]:
                    var adc8E = m[r] + u.Val;
                    var adc8T = (byte)adc8E;
                    m[r] = adc8T;
                    return;
                case Mnemonic.Adc when ops is [R16 r, MU16 mem]:
                    var adc16E = m[r] + mem[m];
                    var adc16T = (ushort)adc16E;
                    m[r] = adc16T;
                    return;
                case Mnemonic.Add when ops is [R16 r, MU16 mem]:
                    var add16E = m[r] + mem[m];
                    var add16T = (ushort)add16E;
                    m[r] = add16T;
                    return;
                case Mnemonic.Add when ops is [MU16 mem, R16 r]:
                    var add16tE = mem[m] + m[r];
                    var add16tT = (ushort)add16tE;
                    mem[m] = add16tT;
                    return;
                case Mnemonic.Add when ops is [R8 r, U8 u]:
                    var add8E = m[r] + u.Val;
                    var add8T = (byte)add8E;
                    m[r] = add8T;
                    return;
                case Mnemonic.Add when ops is [R16 r, U16 u]:
                    var addE = m[r] + u.Val;
                    var addT = (ushort)addE;
                    m[r] = addT;
                    return;
                case Mnemonic.Add when ops is [R16 r, R16 t]:
                    var add16E2 = m[r] + m[t];
                    var add16T2 = (ushort)add16E2;
                    m[r] = add16T2;
                    return;
                case Mnemonic.Add when ops is [R16 r, I16 u]:
                    var addE2 = m[r] + u.Val;
                    var addT2 = (ushort)addE2;
                    m[r] = addT2;
                    return;
                case Mnemonic.And when ops is [R8 r, U8 u]:
                    var andE = m[r] & u.Val;
                    var andT = (byte)andE;
                    m[r] = andT;
                    return;
                case Mnemonic.And when ops is [R16 r, MU16 mem]:
                    var andE2 = m[r] & mem[m];
                    var andT2 = (byte)andE2;
                    m[r] = andT2;
                    return;
                // case Mnemonic.Call: return;
                // case Mnemonic.Cbw: return;
                // case Mnemonic.Clc: return;
                // case Mnemonic.Cld: return;
                // case Mnemonic.Cli: return;
                // case Mnemonic.Cmc: return;
                // case Mnemonic.Cmp: return;
                // case Mnemonic.Cmpsb: return;
                // case Mnemonic.Cmpsw: return;
                // case Mnemonic.Cwd: return;
                case Mnemonic.Daa:
                    m.DoDecimalAdjust();
                    return;
                // case Mnemonic.Das: return;
                case Mnemonic.Dec when ops is [MU16 mem]:
                    var decE2 = mem[m] - 1;
                    var decT2 = (ushort)decE2;
                    mem[m] = decT2;
                    return;
                case Mnemonic.Dec when ops is [R16 r]:
                    var decE = m[r] - 1;
                    var decT = (ushort)decE;
                    m[r] = decT;
                    return;
                case Mnemonic.Div when ops is [MI16 mem]:
                    var divE = m[Reg.AX] / mem[m];
                    var divT = (ushort)divE;
                    m[Reg.AX] = divT;
                    return;
                // case Mnemonic.Enter: return;
                case Mnemonic.Hlt:
                    Halted = true;
                    return;
                case Mnemonic.Idiv when ops is [MI16 mem]:
                    var idivE = m[Reg.AX] / mem[m];
                    var idivT = (ushort)idivE;
                    m[Reg.AX] = idivT;
                    return;
                case Mnemonic.Imul when ops is [MI16 mem]:
                    var imulE = m[Reg.AX] * mem[m];
                    var imulT = (ushort)imulE;
                    m[Reg.AX] = imulT;
                    return;
                // case Mnemonic.In: return;
                case Mnemonic.Inc when ops is [MU16 mem]:
                    var incE2 = mem[m] + 1;
                    var incT2 = (ushort)incE2;
                    mem[m] = incT2;
                    return;
                case Mnemonic.Inc when ops is [R16 r]:
                    var incE1 = m[r] + 1;
                    var incT1 = (ushort)incE1;
                    m[r] = incT1;
                    return;
                // case Mnemonic.Insb: return;
                // case Mnemonic.Insw: return;
                case Mnemonic.Int when ops is [U8 u]:
                    ExecuteInterrupt(u.Val, m);
                    if ((InterruptTable[0x21] as DOSInterrupts)?.ReturnCode is not null)
                        Halted = true;
                    return;
                // case Mnemonic.Into: return;
                // case Mnemonic.Iret: return;
                // case Mnemonic.Ja: return;
                // case Mnemonic.Jae: return;
                // case Mnemonic.Jb: return;
                // case Mnemonic.Jbe: return;
                // case Mnemonic.Jcxz: return;
                // case Mnemonic.Je: return;
                // case Mnemonic.Jg: return;
                // case Mnemonic.Jge: return;
                // case Mnemonic.Jl: return;
                // case Mnemonic.Jle: return;
                // case Mnemonic.Jmp: return;
                // case Mnemonic.Jne: return;
                // case Mnemonic.Jno: return;
                // case Mnemonic.Jnp: return;
                // case Mnemonic.Jns: return;
                // case Mnemonic.Jo: return;
                // case Mnemonic.Jp: return;
                // case Mnemonic.Js: return;
                // case Mnemonic.Lahf: return;
                // case Mnemonic.Leave: return;
                // case Mnemonic.Lodsb: return;
                // case Mnemonic.Lodsw: return;
                case Mnemonic.Loop when ops is [I8 u]:
                    if (m.CX < 1)
                        return;
                    m.CX--;
                    if (m.CX == 0)
                        return;
                    var loopDst = nextIP + u.Val;
                    nextIP = (ushort)loopDst;
                    return;
                // case Mnemonic.Loope: return;
                // case Mnemonic.Loopne: return;
                case Mnemonic.Mov when ops is [R8 r, MU8 mem]:
                    m[r] = mem[m];
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
                // case Mnemonic.Movsb: return;
                case Mnemonic.Movsw when ops is [MU16 nT, MU16 nS]:
                    nT[m] = nS[m];
                    m.IncOrDec(2, useSi: true, useDi: true);
                    return;
                case Mnemonic.Mul when ops is [MU16 mem]:
                    var mulE = m[Reg.AX] * mem[m];
                    var mulT = (ushort)mulE;
                    m[Reg.AX] = mulT;
                    return;
                // case Mnemonic.Neg: return;
                case Mnemonic.Nop:
                    return;
                // case Mnemonic.Not: return;
                case Mnemonic.Or when ops is [R16 r, MU16 mem]:
                    var orE = m[r] | mem[m];
                    var orT = (ushort)orE;
                    m[r] = orT;
                    return;
                // case Mnemonic.Out: return;
                // case Mnemonic.Outsb: return;
                // case Mnemonic.Outsw: return;
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
                // case Mnemonic.Rcl: return;
                // case Mnemonic.Rcr: return;
                // case Mnemonic.Ret: return;
                case Mnemonic.Rol when ops is [R16 r, U8 u]:
                    m[r] = MachTool.ShiftLeft(m[r], u.Val);
                    return;
                // case Mnemonic.Ror: return;
                // case Mnemonic.Sahf: return;
                // case Mnemonic.Sar: return;
                // case Mnemonic.Sbb: return;
                // case Mnemonic.Scasb: return;
                // case Mnemonic.Scasw: return;
                case Mnemonic.Shl when ops is [R16 r, U8 u]:
                    var shlE = m[r] << u.Val;
                    var shlT = (ushort)shlE;
                    m[r] = shlT;
                    return;
                case Mnemonic.Shr when ops is [R16 r, U8 u]:
                    var shrE = m[r] >> u.Val;
                    var shrT = (ushort)shrE;
                    m[r] = shrT;
                    return;
                // case Mnemonic.Stc: return;
                // case Mnemonic.Std: return;
                // case Mnemonic.Sti: return;
                case Mnemonic.Stosb when ops is [MU8 mem, R8 r]:
                    mem[m] = m[r];
                    m.IncOrDec(1, useSi: false, useDi: true);
                    return;
                case Mnemonic.Stosw when ops is [MU16 mem, R16 r]:
                    mem[m] = m[r];
                    m.IncOrDec(2, useSi: false, useDi: true);
                    return;
                case Mnemonic.Sub when ops is [R16 r, MU16 mem]:
                    var subE = m[r] - mem[m];
                    var subT = (ushort)subE;
                    m[r] = subT;
                    return;
                case Mnemonic.Sub when ops is [MU16 mem, I16 u]:
                    var subE3 = mem[m] - u.Val;
                    var subT3 = (ushort)subE3;
                    mem[m] = subT3;
                    return;
                case Mnemonic.Sub when ops is [R8 r, U8 u]:
                    var subE2 = m[r] - u.Val;
                    var subT2 = (byte)subE2;
                    m[r] = subT2;
                    return;
                case Mnemonic.Test when ops is [R16 r, U16 u]:
                    var testR = m[r] & u.Val;
                    m.SetTestFlags(testR);
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
                    var xorE = m[r] ^ mem[m];
                    var xorT = (ushort)xorE;
                    m[r] = xorT;
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
