// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Hardware.Errors;
using static PoViEmu.Common.JsonHelper;
using Fl = PoViEmu.Core.Hardware.Flagged;
using U8 = PoViEmu.Core.Decoding.Ops.Consts.U8Operand;
using U16 = PoViEmu.Core.Decoding.Ops.Consts.U16Operand;
using I16 = PoViEmu.Core.Decoding.Ops.Consts.I16Operand;
using R8 = PoViEmu.Core.Decoding.Ops.Regs.Reg8Operand;
using R16 = PoViEmu.Core.Decoding.Ops.Regs.Reg16Operand;
using MU8 = PoViEmu.Core.Decoding.Ops.Mu8Operand;
using MI16 = PoViEmu.Core.Decoding.Ops.Mi16Operand;
using MU16 = PoViEmu.Core.Decoding.Ops.Mu16Operand;
using MF32 = PoViEmu.Core.Decoding.Ops.Mf32Operand;

namespace PoViEmu.Core.Hardware
{
    public sealed class NC3022c
    {
        public bool Halted { get; set; }
        public IDictionary<byte, IInterruptHandler> InterruptTable { get; }

        public NC3022c()
        {
            var dos = new DOSInterrupts();
            InterruptTable = new SortedDictionary<byte, IInterruptHandler>
            {
                [0x21] = dos
            };
        }

        public void Execute(XInstruction instruct, MachineState m)
        {
            var parsed = instruct.Parsed;
            if (parsed.IsInvalidFor16Bit())
            {
                // TODO throw new InvalidOpcodeException(instruct);
                return;
            }
            var ops = parsed.GetOps().ToArray();
            switch (parsed.Mnemonic)
            {
                case Mnemonic.Mov when ops is [R8 r, U8 u]:
                    m[r] = u.Val;
                    return;
                case Mnemonic.Mov when ops is [R8 r, R8 q]:
                    m[r] = m[q];
                    return;
                case Mnemonic.Mov when ops is [R16 r, U16 u]:
                    m[r] = u.Val;
                    return;
                case Mnemonic.Mov when ops is [R16 r, MU16 mem]:
                    m[r] = mem[m];
                    return;
                case Mnemonic.Mov when ops is [R8 r, MU8 mem]:
                    m[r] = mem[m];
                    return;
                case Mnemonic.Mov when ops is [MU16 mem, R16 r]:
                    mem[m] = m[r];
                    return;
                case Mnemonic.Mov when ops is [MU16 mem, U16 u]:
                    mem[m] = u.Val;
                    return;
                case Mnemonic.Wait:
                    // TODO ignore?
                    return;
                case Mnemonic.Pushf:
                    var pushFE = m.F;
                    var pushT = (ushort)pushFE;
                    m.Push(pushT);
                    return;
                case Mnemonic.Popf:
                    var popEF = m.Pop();
                    m.F = (Fl)popEF;
                    return;
                case Mnemonic.Pusha:
                    m.PushAll();
                    return;
                case Mnemonic.Popa:
                    // TODO m.PopAll();
                    return;
                case Mnemonic.Call when ops is [U16 u]:
                    // TODO ignore jmp?
                    return;
                case Mnemonic.Push when ops is [I16 v]:
                    var pushV = v.Val;
                    m.Push((ushort)pushV);
                    return;
                case Mnemonic.Push when ops is [R16 r]:
                    var pushE = m[r];
                    m.Push(pushE);
                    return;
                case Mnemonic.Fld when ops is [MF32 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Fadd when ops is [MF32 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Fsub when ops is [MF32 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Fild when ops is [MI16 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Fdiv when ops is [MF32 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Fmul when ops is [MF32 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Fstp when ops is [MF32 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Nop:
                    // TODO ignore?
                    return;
                case Mnemonic.Pop when ops is [R16 r]:
                    var popE = m.Pop();
                    m[r] = popE;
                    return;
                case Mnemonic.And when ops is [R16 r, MU16 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Cmp when ops is [R16 r, MU16 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Xor when ops is [R16 r, MU16 mem]:
                    // TODO ignore?
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
                case Mnemonic.And when ops is [R8 r, U8 u]:
                    var andE = m[r] & u.Val;
                    var andT = (byte)andE;
                    m[r] = andT;
                    return;
                case Mnemonic.Rol when ops is [R16 r, U8 u]:
                    m[r] = MachTool.ShiftLeft(m[r], u.Val);
                    return;
                case Mnemonic.Ror when ops is [R16 r, U8 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Rcr when ops is [R16 r, U8 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Rcl when ops is [R16 r, U8 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Shr when ops is [R16 r, U8 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Shl when ops is [R16 r, U8 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Shl when ops is [R16 r, R8 t]:
                    // TODO ignore?
                    return;
                case Mnemonic.Sbb when ops is [R16 r, U16 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Sbb when ops is [R16 r, R16 t]:
                    // TODO ignore?
                    return;
                case Mnemonic.Sar when ops is [R16 r, U8 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Sar when ops is [R16 r, R8 t]:
                    // TODO ignore?
                    return;
                case Mnemonic.Xchg when ops is [R16 r, R16 t]:
                    // TODO ignore?
                    return;
                case Mnemonic.Mul when ops is [MU16 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Imul when ops is [MI16 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Imul when ops is [R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Idiv when ops is [MI16 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Div when ops is [R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Cli:
                    // TODO ignore?
                    return;
                case Mnemonic.Cwd:
                    // TODO ignore?
                    return;
                case Mnemonic.Cbw:
                    // TODO ignore?
                    return;
                case Mnemonic.Sti:
                    // TODO ignore?
                    return;
                case Mnemonic.Not when ops is [R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Xlatb when ops is [MU8 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Out when ops is [U8 u, R8 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Idiv when ops is [R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Test when ops is [MU16 mem, R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Cmp when ops is [MU16 mem, I16 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Cmp when ops is [MU16 mem, R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Cmp when ops is [R16 r, I16 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Cmp when ops is [R16 r, U16 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Add when ops is [R16 r, I16 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Or when ops is [R16 r, MU16 mem]:
                    // TODO ignore?
                    return;
                case Mnemonic.Or when ops is [R16 r, R16 t]:
                    // TODO ignore?
                    return;
                case Mnemonic.Xor when ops is [R16 r, R16 t]:
                    // TODO ignore?
                    return;
                case Mnemonic.In when ops is [R8 r, U8 u]:
                    // TODO ignore?
                    return;
                case Mnemonic.Neg when ops is [R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Inc when ops is [R16 r]:
                    // TODO ignore?
                    return;
                case Mnemonic.Inc when ops is [MU16 mem]:
                    var incE2 = mem[m] + 1;
                    var incT2 = (ushort)incE2;
                    mem[m] = incT2;
                    return;
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
                case Mnemonic.Jne when ops is [U16 u]:
                    // TODO ignore jump? if m.zf not set
                    return;
                case Mnemonic.Je when ops is [U16 u]:
                    // TODO ignore jump?
                    return;
                case Mnemonic.Jge when ops is [U16 u]:
                    // TODO ignore jump?
                    return;
                case Mnemonic.Jle when ops is [U16 u]:
                    // TODO ignore jump?
                    return;
                case Mnemonic.Jl when ops is [U16 u]:
                    // TODO ignore jump?
                    return;
                case Mnemonic.Jg when ops is [U16 u]:
                    // TODO ignore jump?
                    return;
                case Mnemonic.Lahf:
                    // TODO ignore?
                    return;
                case Mnemonic.Sahf:
                    // TODO ignore?
                    return;
                case Mnemonic.Add when ops is [R16 r, R16 t]:
                    // TODO ignore?
                    return;
                case Mnemonic.Jmp when ops is [U16 u]:
                    // TODO ignore jump?
                    return;
                case Mnemonic.Loop when ops is [U16 u]:
                    // TODO ignore jump? dec CX and repeat until CX is zero
                    return;
                case Mnemonic.Daa:
                    m.DoDecimalAdjust();
                    return;
                case Mnemonic.Stosb when ops is [MU8 mem, R8 r]:
                    mem[m] = m[r];
                    m.IncOrDec(1, useSi: false, useDi: true);
                    return;
                case Mnemonic.Stosw when ops is [MU16 mem, R16 r]:
                    mem[m] = m[r];
                    m.IncOrDec(2, useSi: false, useDi: true);
                    return;
                case Mnemonic.Movsw when ops is [MU16 nT, MU16 nS]:
                    nT[m] = nS[m];
                    m.IncOrDec(2, useSi: true, useDi: true);
                    return;
                case Mnemonic.Int when ops is [U8 u]:
                    ExecuteInterrupt(u.Val, m);
                    if ((InterruptTable[0x21] as DOSInterrupts)?.ReturnCode is not null)
                        Halted = true;
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