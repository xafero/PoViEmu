using System;
using System.Collections.Generic;
using Iced.Intel;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Decoding.Ops
{
    public static class Fetcher
    {
        public static IEnumerable<XInstruction> Prefetch(this MachineState m)
        {
            foreach (var instruct in m.ToInstructions(m.CS, m.IP))
            {
                yield return instruct;
            }
        }

        public static IEnumerable<BaseOperand> GetOps(this Instruction instruct)
        {
            for (var i = 0; i < instruct.OpCount; i++)
            {
                var kind = instruct.GetOpKind(i);
                switch (kind)
                {
                    case OpKind.Register:
                        var reg = instruct.GetOpRegister(i);
                        yield return new RegOperand(reg);
                        continue;
                    case OpKind.Immediate16:
                        var imm = instruct.GetImmediate(i);
                        yield return new U16Operand((ushort)imm);
                        continue;
                    case OpKind.Immediate8to16:
                        var ims = instruct.GetImmediate(i);
                        yield return new I16Operand((short)ims);
                        continue;
                }
                throw new InvalidOperationException($"{kind} ?!");
            }
        }

        public static ref ushort GetRef(this MachineState m, Register reg)
        {
            switch (reg)
            {
                case Register.AX: return ref m.AX;
                case Register.BP: return ref m.BP;
                case Register.BX: return ref m.BX;
                case Register.CS: return ref m.CS;
                case Register.CX: return ref m.CX;
                case Register.DI: return ref m.DI;
                case Register.DS: return ref m.DS;
                case Register.DX: return ref m.DX;
                case Register.ES: return ref m.ES;
                case Register.SI: return ref m.SI;
                case Register.SP: return ref m.SP;
                case Register.SS: return ref m.SS;
            }
            throw new InvalidOperationException($"{reg} ?!");
        }

        public static ref ushort GetRef(this MachineState m, RegOperand op) => ref GetRef(m, op.Reg);
    }
}