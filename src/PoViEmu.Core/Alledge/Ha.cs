using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;
using R = Iced.Intel.Register;
using MS = Iced.Intel.MemorySize;
using OK = Iced.Intel.OpKind;
using B8 = PoViEmu.Core.Hardware.AckNow.B8Register;
using B16 = PoViEmu.Core.Hardware.AckNow.B16Register;

namespace PoViEmu.SamCon
{
    public static class OpFetcher
    {
        private static U16Operand ToU16Op(ulong imm)
        {
            var value = (ushort)imm;
            return new U16Operand(value);
        }

        private static I16Operand ToI16Op(ulong imm)
        {
            var value = (short)imm;
            return new I16Operand(value);
        }

        private static U8Operand ToU8Op(ulong imm)
        {
            var value = (byte)imm;
            return new U8Operand(value);
        }

        private static NearOperand ToNearOp(ushort dest)
        {
            return new NearOperand(dest);
        }

        private static FarOperand ToFarOp(ushort sel, ushort dest)
        {
            return new FarOperand(sel, dest);
        }

        public static (B8? R8, B16? R16) ConvertReg(this R reg)
        {
            switch (reg)
            {
                // 8 bit low
                case R.AL: return (B8.AL, null);
                case R.BL: return (B8.BL, null);
                case R.CL: return (B8.CL, null);
                case R.DL: return (B8.DL, null);
                // 8 bit high
                case R.AH: return (B8.AH, null);
                case R.BH: return (B8.BH, null);
                case R.CH: return (B8.CH, null);
                case R.DH: return (B8.DH, null);
                // 16 bit wide
                case R.AX: return (null, B16.AX);
                case R.BX: return (null, B16.BX);
                case R.CX: return (null, B16.CX);
                case R.DX: return (null, B16.DX);
                // 16 bit index
                case R.DI: return (null, B16.DI);
                case R.SI: return (null, B16.SI);
                // 16 bit pointer
                case R.BP: return (null, B16.BP);
                case R.SP: return (null, B16.SP);
                // 16 bit segment
                case R.CS: return (null, B16.CS);
                case R.DS: return (null, B16.DS);
                case R.ES: return (null, B16.ES);
                case R.SS: return (null, B16.SS);
                // Nothing
                case R.None: return (null, null);
            }
            throw new InvalidOperationException($"{reg} ?!");
        }

        private static RegOperand ToRegOp(R reg)
        {
            switch (reg)
            {
                // 8 bit low
                case R.AL: return new Reg8Operand(B8.AL);
                case R.BL: return new Reg8Operand(B8.BL);
                case R.CL: return new Reg8Operand(B8.CL);
                case R.DL: return new Reg8Operand(B8.DL);
                // 8 bit high
                case R.AH: return new Reg8Operand(B8.AH);
                case R.BH: return new Reg8Operand(B8.BH);
                case R.CH: return new Reg8Operand(B8.CH);
                case R.DH: return new Reg8Operand(B8.DH);
                // 16 bit wide
                case R.AX: return new Reg16Operand(B16.AX);
                case R.BX: return new Reg16Operand(B16.BX);
                case R.CX: return new Reg16Operand(B16.CX);
                case R.DX: return new Reg16Operand(B16.DX);
                // 16 bit index
                case R.DI: return new Reg16Operand(B16.DI);
                case R.SI: return new Reg16Operand(B16.SI);
                // 16 bit pointer
                case R.BP: return new Reg16Operand(B16.BP);
                case R.SP: return new Reg16Operand(B16.SP);
                // 16 bit segment
                case R.CS: return new Reg16Operand(B16.CS);
                case R.DS: return new Reg16Operand(B16.DS);
                case R.ES: return new Reg16Operand(B16.ES);
                case R.SS: return new Reg16Operand(B16.SS);
            }
            throw new InvalidOperationException($"{reg} ?!");
        }

        private static MemOperand ToMemOp(MS size, R seg, R bse, R? idx = null, uint? disp = null)
        {
            var xSeg = seg.ConvertReg().R16 ?? default;
            var xBse = bse.ConvertReg().R16;
            var xIdx = (idx ?? R.None).ConvertReg().R16;
            var xDisp = (short?)disp;
            switch (size)
            {
                case MS.UInt8: return new Mu8Operand(xSeg, xBse, xIdx, xDisp);
                case MS.UInt16: return new Mu16Operand(xSeg, xBse, xIdx, xDisp);
                case MS.Int8: return new Mi8Operand(xSeg, xBse, xIdx, xDisp);
                case MS.Int16: return new Mi16Operand(xSeg, xBse, xIdx, xDisp);
            }
            throw new InvalidOperationException($"{size} ?!");
        }

        public static IEnumerable<BaseOperand> GetOps(Instruction ins)
        {
            for (var i = 0; i < ins.OpCount; i++)
            {
                var kind = ins.GetOpKind(i);
                switch (kind)
                {
                    case OK.Memory:
                        yield return ToMemOp(ins.MemorySize, ins.MemorySegment,
                            ins.MemoryBase, ins.MemoryIndex, ins.MemoryDisplacement32);
                        continue;
                    case OK.MemoryESDI:
                        yield return ToMemOp(ins.MemorySize, R.ES, R.DI);
                        continue;
                    case OK.MemorySegDI:
                        yield return ToMemOp(ins.MemorySize, ins.MemorySegment, R.DI);
                        continue;
                    case OK.MemorySegSI:
                        yield return ToMemOp(ins.MemorySize, ins.MemorySegment, R.SI);
                        continue;
                    case OK.Immediate8:
                    case OK.Immediate8_2nd:
                        yield return ToU8Op(ins.GetImmediate(i));
                        continue;
                    case OK.Immediate8to16:
                        yield return ToI16Op(ins.GetImmediate(i));
                        continue;
                    case OK.Immediate16:
                        yield return ToU16Op(ins.GetImmediate(i));
                        continue;
                    case OK.NearBranch16:
                        yield return ToNearOp(ins.NearBranch16);
                        continue;
                    case OK.FarBranch16:
                        yield return ToFarOp(ins.FarBranchSelector, ins.FarBranch16);
                        continue;
                    case OK.Register:
                        yield return ToRegOp(ins.GetOpRegister(i));
                        continue;
                }
                throw new InvalidOperationException($"{kind} ?! ({ins})");
            }
        }
    }
}