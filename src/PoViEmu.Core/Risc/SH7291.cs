using System;
using System.Collections.Generic;
using System.Linq;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.Errors;
using System;
using System.Collections.Generic;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Decoding.Ops.Jumps;
using PoViEmu.Core.Decoding.Ops.Mems;
using PoViEmu.Core.Hardware.Errors;
using Fl = PoViEmu.Core.Hardware.Flagged;
using U8 = PoViEmu.Core.Decoding.Ops.Consts.U8Operand;
using U16 = PoViEmu.Core.Decoding.Ops.Consts.U16Operand;
using I16 = PoViEmu.Core.Decoding.Ops.Consts.I16Operand;
using R8 = PoViEmu.Core.Decoding.Ops.Regs.Reg8Operand;
using R16 = PoViEmu.Core.Decoding.Ops.Regs.Reg16Operand;
using MU8 = PoViEmu.Core.Decoding.Ops.Mems.Mu8Operand;
using MI16 = PoViEmu.Core.Decoding.Ops.Mems.Mi16Operand;
using MU16 = PoViEmu.Core.Decoding.Ops.Mems.Mu16Operand;
using MU16b = PoViEmu.Core.Decoding.Ops.Mems.Mu16BOperand;
using MF32 = PoViEmu.Core.Decoding.Ops.Mems.Mf32Operand;
using NJ = PoViEmu.Core.Decoding.Ops.Jumps.NearOperand;
using FJ = PoViEmu.Core.Decoding.Ops.Jumps.FarOperand;
using Reg = PoViEmu.Core.Hardware.AckNow.B16Register;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Risc
{
    /// <summary>
    /// The SH7291 is a custom 32bit CPU developed by CASIO based on Hitachi�s SH3
    /// </summary>
    public sealed class SH7291
    {
        public bool Halted { get; set; }
        public IDictionary<byte, IInterruptHandler> InterruptTable { get; }

        public SH7291()
        {
            InterruptTable = new SortedDictionary<byte, IInterruptHandler>
            {
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
            switch ((Mnemonic)parsed.Mnemonic)
            {
                // TODO
                /* case Mnemonic.Addc when ops is [R8 r, U8 u]:
                    m[r] = CpuIntern.Add8(m, withCarry: true, m[r], u.Val);
                    return;
                    */
            }

            throw new UnhandledOpcodeException(parsed, ops);
        }
    }
}
