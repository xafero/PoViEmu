using System;
using System.Collections.Generic;
using Iced.Intel;
using PoViEmu.Core.Decoding.Ops.Consts;
using PoViEmu.Core.Decoding.Ops.Regs;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;
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
                    case OpKind.MemorySegSI:
                    case OpKind.MemoryESDI:
                    case OpKind.Memory:
                        var seg = (B16Register)instruct.MemorySegment.AsMine()!;
                        ushort? off = instruct.NearBranch16;
                        B16Register? idx = null;
                        var size = instruct.MemorySize;
                        if (kind == OpKind.MemoryESDI)
                        {
                            seg = B16Register.ES;
                            off = null;
                            idx = B16Register.DI;
                        }
                        else if (kind == OpKind.MemorySegSI)
                        {
                            off = null;
                            idx = B16Register.SI;
                        }
                        if (size == MemorySize.UInt8)
                            yield return new Mu8Operand(seg, off, idx);
                        else if (size == MemorySize.UInt16)
                            yield return new Mu16Operand(seg, off, idx);
                        else if (size == MemorySize.Int16)
                            yield return new Mi16Operand(seg, off, idx);
                        else if (size == MemorySize.Float32)
                            yield return new Mf32Operand(seg, off, idx);
                        else
                            break;
                        continue;
                    case OpKind.Register:
                        var reg = instruct.GetOpRegister(i);
                        var rop = reg.ToOperand();
                        yield return rop;
                        continue;
                    case OpKind.NearBranch16:
                        var nba = instruct.NearBranch16;
                        var nbt = instruct.IP16 + instruct.Length;
                        var nbj = (sbyte)(nba - nbt);
                        yield return new I8Operand(nbj);
                        continue;
                    case OpKind.Immediate8:
                        var imb = instruct.GetImmediate(i);
                        yield return new U8Operand((byte)imb);
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