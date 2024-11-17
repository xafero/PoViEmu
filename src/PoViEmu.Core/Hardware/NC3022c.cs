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
using I8 = PoViEmu.Core.Decoding.Ops.Consts.I8Operand;
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
                [DOSInterrupts.MainIntNo] = dos
            };
        }

        public void Execute(XInstruction instruct, MachineState m)
        {
            var nextIP = instruct.Parsed.NextIP16;
            Execute(instruct, m, ref nextIP);
            m.IP = nextIP;
        }

        private void Execute(XInstruction instruct, MachineState m, ref ushort nextIP)
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
                case Mnemonic.Aaa: return;
                case Mnemonic.Aad: return;
                case Mnemonic.Aam: return;
                case Mnemonic.Aas: return;
                case Mnemonic.Adc: return;
                case Mnemonic.Add: return;
                case Mnemonic.And: return;
                case Mnemonic.Call: return;
                case Mnemonic.Cbw: return;
                case Mnemonic.Clc: return;
                case Mnemonic.Cld: return;
                case Mnemonic.Cli: return;
                case Mnemonic.Cmc: return;
                case Mnemonic.Cmp: return;
                case Mnemonic.Cmpsb: return;
                case Mnemonic.Cmpsw: return;
                case Mnemonic.Cwd: return;
                case Mnemonic.Daa: return;
                case Mnemonic.Das: return;
                case Mnemonic.Dec: return;
                case Mnemonic.Div: return;
                case Mnemonic.Enter: return;
                case Mnemonic.Idiv: return;
                case Mnemonic.Imul: return;
                case Mnemonic.In: return;
                case Mnemonic.Inc: return;
                case Mnemonic.Insb: return;
                case Mnemonic.Insw: return;
                case Mnemonic.Int: return;
                case Mnemonic.Into: return;
                case Mnemonic.Iret: return;
                case Mnemonic.Ja: return;
                case Mnemonic.Jae: return;
                case Mnemonic.Jb: return;
                case Mnemonic.Jbe: return;
                case Mnemonic.Jcxz: return;
                case Mnemonic.Je: return;
                case Mnemonic.Jg: return;
                case Mnemonic.Jge: return;
                case Mnemonic.Jl: return;
                case Mnemonic.Jle: return;
                case Mnemonic.Jmp: return;
                case Mnemonic.Jne: return;
                case Mnemonic.Jno: return;
                case Mnemonic.Jnp: return;
                case Mnemonic.Jns: return;
                case Mnemonic.Jo: return;
                case Mnemonic.Jp: return;
                case Mnemonic.Js: return;
                case Mnemonic.Lahf: return;
                case Mnemonic.Leave: return;
                case Mnemonic.Lodsb: return;
                case Mnemonic.Lodsw: return;
                case Mnemonic.Loop: return;
                case Mnemonic.Loope: return;
                case Mnemonic.Loopne: return;
                case Mnemonic.Mov: return;
                case Mnemonic.Movsb: return;
                case Mnemonic.Movsw: return;
                case Mnemonic.Mul: return;
                case Mnemonic.Neg: return;
                case Mnemonic.Nop: return;
                case Mnemonic.Not: return;
                case Mnemonic.Or: return;
                case Mnemonic.Out: return;
                case Mnemonic.Outsb: return;
                case Mnemonic.Outsw: return;
                case Mnemonic.Pop: return;
                case Mnemonic.Popa: return;
                case Mnemonic.Popf: return;
                case Mnemonic.Push: return;
                case Mnemonic.Pusha: return;
                case Mnemonic.Pushf: return;
                case Mnemonic.Rcl: return;
                case Mnemonic.Rcr: return;
                case Mnemonic.Ret: return;
                case Mnemonic.Rol: return;
                case Mnemonic.Ror: return;
                case Mnemonic.Sahf: return;
                case Mnemonic.Sar: return;
                case Mnemonic.Sbb: return;
                case Mnemonic.Scasb: return;
                case Mnemonic.Scasw: return;
                case Mnemonic.Shl: return;
                case Mnemonic.Shr: return;
                case Mnemonic.Stc: return;
                case Mnemonic.Std: return;
                case Mnemonic.Sti: return;
                case Mnemonic.Stosb: return;
                case Mnemonic.Stosw: return;
                case Mnemonic.Sub: return;
                case Mnemonic.Test: return;
                case Mnemonic.Wait: return;
                case Mnemonic.Xchg: return;
                case Mnemonic.Xlatb: return;
                case Mnemonic.Xor: return;
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