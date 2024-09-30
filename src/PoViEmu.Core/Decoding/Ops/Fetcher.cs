using System;
using System.Collections.Generic;
using Iced.Intel;
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
                        if (Is8Bit(reg))
                            yield return new Reg8Operand(reg);
                        else if (Is16Bit(reg))
                            yield return new Reg16Operand(reg);
                        else
                            throw new InvalidOperationException($"{reg} ?!");
                        continue;
                    case OpKind.Memory:
                        var memBase = instruct.MemoryBase;
                        var memDspl = (short)instruct.MemoryDisplacement32;
                        yield return new MemOperand(memBase, memDspl);
                        continue;
                    case OpKind.NearBranch16:
                        var nba = instruct.NearBranch16;
                        yield return new BrU16Operand(nba);
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

        private static readonly Dictionary<R, PropHandle> Handles = new()
        {
            [R.AX] = new Prop16Handle(m => m.AX, (m, v) => m.AX = v),
            [R.BX] = new Prop16Handle(m => m.BX, (m, v) => m.BX = v),
            [R.CX] = new Prop16Handle(m => m.CX, (m, v) => m.CX = v),
            [R.DX] = new Prop16Handle(m => m.DX, (m, v) => m.DX = v),
            [R.AL] = new Prop8Handle(m => m.AL, (m, v) => m.AL = v),
            [R.BL] = new Prop8Handle(m => m.BL, (m, v) => m.BL = v),
            [R.CL] = new Prop8Handle(m => m.CL, (m, v) => m.CL = v),
            [R.DL] = new Prop8Handle(m => m.DL, (m, v) => m.DL = v),
            [R.AH] = new Prop8Handle(m => m.AH, (m, v) => m.AH = v),
            [R.BH] = new Prop8Handle(m => m.BH, (m, v) => m.BH = v),
            [R.CH] = new Prop8Handle(m => m.CH, (m, v) => m.CH = v),
            [R.DH] = new Prop8Handle(m => m.DH, (m, v) => m.DH = v),
            [R.CS] = new Prop16Handle(m => m.CS, (m, v) => m.CS = v),
            [R.DS] = new Prop16Handle(m => m.DS, (m, v) => m.DS = v),
            [R.ES] = new Prop16Handle(m => m.ES, (m, v) => m.ES = v),
            [R.SS] = new Prop16Handle(m => m.SS, (m, v) => m.SS = v),
            [R.DI] = new Prop16Handle(m => m.DI, (m, v) => m.DI = v),
            [R.SI] = new Prop16Handle(m => m.SI, (m, v) => m.SI = v),
            [R.BP] = new Prop16Handle(m => m.BP, (m, v) => m.BP = v),
            [R.SP] = new Prop16Handle(m => m.SP, (m, v) => m.SP = v)
        };

        public static PropHandle GetRef(this R reg)
        {
            if (Handles.TryGetValue(reg, out var handle))
                return handle;
            throw new InvalidOperationException($"{reg} ?!");
        }

        public static PropHandle GetRef(this MachineState m, R op)
        {
            var current = GetRef(op);
            current.State = m;
            return current;
        }

        public static PropHandle GetRef(this MachineState m, RegOperand op)
        {
            return GetRef(m, op.Reg);
        }

        public static bool IsInvalidFor16Bit(this Instruction parsed)
        {
            return parsed.IsInvalid || parsed.CodeSize != CodeSize.Code16;
        }
    }
}