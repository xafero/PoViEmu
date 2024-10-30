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
using U8 = PoViEmu.Core.Decoding.Ops.Consts.U8Operand;
using U16 = PoViEmu.Core.Decoding.Ops.Consts.U16Operand;
using I16 = PoViEmu.Core.Decoding.Ops.Consts.I16Operand;
using R8 = PoViEmu.Core.Decoding.Ops.Regs.Reg8Operand;
using R16 = PoViEmu.Core.Decoding.Ops.Regs.Reg16Operand;
using MO = PoViEmu.Core.Decoding.Ops.MemOperand;
using MU16 = PoViEmu.Core.Decoding.Ops.Mu16Operand;
using System.ComponentModel;
using System.Diagnostics.Metrics;

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
                case Mnemonic.Mov when ops is [MU16 mem, R16 r]:
                    mem[m] = m[r];
                    return;
                case Mnemonic.Pushf:
                    var pushE = m.F;
                    var pushT = (ushort)pushE;
                    m.Push(pushT);
                    return;
                case Mnemonic.Add when ops is [R16 r, U16 u]:
                    var addE = m[r] + u.Val;
                    var addT = (ushort)addE;
                    m[r] = addT;
                    return;
                case Mnemonic.Sub when ops is [R16 r, MU16 mem]:
                    var subE = m[r] - mem[m];
                    var subT = (ushort)subE;
                    m[r] = subT;
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