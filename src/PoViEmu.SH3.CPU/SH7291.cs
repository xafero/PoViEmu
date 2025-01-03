using System;
using System.Collections.Generic;
using System.Linq;
using PoViEmu.Base.CPU;
using PoViEmu.SH3.CPU.Errors;
using PoViEmu.SH3.CPU.Soft;
using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Decoding;
using PoViEmu.SH3.ISA.Ops;
using MO = PoViEmu.SH3.ISA.Ops.MemoryOperand;
using DO = PoViEmu.SH3.ISA.Ops.DisplOperand;
using I8 = PoViEmu.SH3.ISA.Ops.ImmedOperand;
using RS = PoViEmu.SH3.ISA.Ops.SourceReg;
using RD = PoViEmu.SH3.ISA.Ops.DestReg;

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

        private void Execute(XInstruction instruct, MachineState m, bool ignoreUc,
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
                case Mnemonic.Add when ops is [I8 a, RD b]:
                    // TODO
                    return;
                case Mnemonic.Add when ops is [RS a, RD b]:
                    // TODO
                    return;
                case Mnemonic.Bf when ops is [DO a]:
                    // TODO
                    return;
                case Mnemonic.Bra when ops is [DO a]:
                    // TODO
                    return;
                case Mnemonic.Bsr when ops is [DO a]:
                    // TODO
                    return;
                case Mnemonic.CmpHi when ops is [RS a, RD b]:
                    // TODO
                    return;
                case Mnemonic.Jsr when ops is [MO a]:
                    // TODO
                    return;
                case Mnemonic.LdsL when ops is [MO a, RD b]:
                    // TODO
                    return;
                case Mnemonic.Mov when ops is [I8 a, RD b]:
                    // TODO
                    return;
                case Mnemonic.Mov when ops is [RS a, RD b]:
                    // TODO
                    return;
                case Mnemonic.MovB when ops is [RD a, MO b]:
                    // TODO
                    return;
                case Mnemonic.MovB when ops is [MO a, RS b]:
                    // TODO
                    return;
                case Mnemonic.MovW when ops is [DO a, RD b]:
                    // TODO
                    return;
                case Mnemonic.MovW when ops is [RS a, MO b]:
                    // TODO
                    return;
                case Mnemonic.MovW when ops is [MO a, RD b]:
                    // TODO
                    return;
                case Mnemonic.MovL when ops is [RS a, MO b]:
                    // TODO
                    return;
                case Mnemonic.MovL when ops is [RD a, MO b]:
                    // TODO
                    return;
                case Mnemonic.MovL when ops is [MO a, RD b]:
                    // TODO
                    return;
                case Mnemonic.MovL when ops is [DO a, RD b]:
                    // TODO
                    return;
                case Mnemonic.MulsW when ops is [RS a, RD b]:
                    // TODO
                    return;
                case Mnemonic.Nop:
                    return;
                case Mnemonic.Shll when ops is [RD a]:
                    // TODO
                    return;
                case Mnemonic.Sts when ops is [RD a, RD b]:
                    // TODO
                    return;
                case Mnemonic.StsL when ops is [RD a, MO b]:
                    // TODO
                    return;
                case Mnemonic.Sub when ops is [RS a, RD b]:
                    // TODO
                    return;
                case Mnemonic.Trapa when ops is [ImmedUOperand a]:
                    // TODO
                    return;
                case Mnemonic.Rts:
                    // TODO
                    return;
            }

            var debug = string.Join(", ", ops.Select(o => o.GetType().Name));
            throw new UnhandledOpcodeException(parsed, ops, debug);
        }
    }
}