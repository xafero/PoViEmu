using System;
using System.Collections.Generic;
using Iced.Intel;
using PoViEmu.Core.Decoding.Ops.Consts;
using PoViEmu.Core.Decoding.Ops.Regs;
using PoViEmu.Core.Hardware;
using R = Iced.Intel.Register;

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
                        var rop = reg.ToOperand();
                        yield return rop;
                        continue;
                    case OpKind.Memory:
                        var memBase = instruct.MemoryBase;
                        var memDspl = (short)instruct.MemoryDisplacement32;
                        yield return new MemOperand(memBase, memDspl);
                        continue;
                    case OpKind.NearBranch16:
                        var nba = instruct.NearBranch16;
                        yield return new U16Operand(nba);
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

        private static bool Is8Bit(R reg)
        {
            switch (reg)
            {
                case R.AL:
                case R.BL:
                case R.CL:
                case R.DL:
                case R.AH:
                case R.BH:
                case R.CH:
                case R.DH:
                    return true;
            }
            return false;
        }

        private static bool Is16Bit(R reg)
        {
            switch (reg)
            {
                case R.AX:
                case R.BX:
                case R.CX:
                case R.DX:
                case R.BP:
                case R.SP:
                case R.DI:
                case R.SI:
                case R.CS:
                case R.DS:
                case R.ES:
                case R.SS:
                    return true;
            }
            return false;
        }

        public static bool IsInvalidFor16Bit(this Instruction parsed)
        {
            return parsed.IsInvalid || parsed.CodeSize != CodeSize.Code16;
        }
    }
}