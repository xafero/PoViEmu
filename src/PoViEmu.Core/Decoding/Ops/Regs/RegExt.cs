using System;
using Iced.Intel;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops.Regs
{
    public static class RegExt
    {
        public static RegOperand ToOperand(this Register reg)
        {
            var res = reg.AsMine();
            if (res is B8Register b8)
            {
                return new Reg8Operand(b8);
            }
            if (res is B16Register b16)
            {
                return new Reg16Operand(b16);
            }
            throw new InvalidOperationException($"{reg} ?!");
        }

        public static object AsMine(this Register register)
        {
            switch (register)
            {
                case Register.AL: return B8Register.AL;
                case Register.BL: return B8Register.BL;
                case Register.CL: return B8Register.CL;
                case Register.DL: return B8Register.DL;
                case Register.AH: return B8Register.AH;
                case Register.BH: return B8Register.BH;
                case Register.CH: return B8Register.CH;
                case Register.DH: return B8Register.DH;
                case Register.SPL: return B16Register.SP;
                case Register.BPL: return B16Register.BP;
                case Register.SIL: return B16Register.SI;
                case Register.DIL: return B16Register.DI;
                case Register.AX: return B16Register.AX;
                case Register.BX: return B16Register.BX;
                case Register.CX: return B16Register.CX;
                case Register.DX: return B16Register.DX;
                case Register.SP: return B16Register.SP;
                case Register.BP: return B16Register.BP;
                case Register.SI: return B16Register.SI;
                case Register.DI: return B16Register.DI;
                case Register.EAX: return B16Register.AX;
                case Register.EBX: return B16Register.BX;
                case Register.ECX: return B16Register.CX;
                case Register.EDX: return B16Register.DX;
                case Register.ESP: return B16Register.SP;
                case Register.EBP: return B16Register.BP;
                case Register.ESI: return B16Register.SI;
                case Register.EDI: return B16Register.DI;
                case Register.RAX: return B16Register.AX;
                case Register.RBX: return B16Register.BX;
                case Register.RCX: return B16Register.CX;
                case Register.RDX: return B16Register.DX;
                case Register.RSP: return B16Register.SP;
                case Register.RBP: return B16Register.BP;
                case Register.RSI: return B16Register.SI;
                case Register.RDI: return B16Register.DI;
                case Register.EIP: return B16Register.IP;
                case Register.RIP: return B16Register.IP;
                case Register.CS: return B16Register.CS;
                case Register.DS: return B16Register.DS;
                case Register.ES: return B16Register.ES;
                case Register.SS: return B16Register.SS;
                case Register.FS: case Register.GS: case Register.None: return B16Register.None;
            }
            throw new InvalidOperationException($"{register} ?!");
        }

        public static ushort Get(this MachineState m, Reg16Operand reg)
            => m.Get(reg.Reg);

        public static void Set(this MachineState m, Reg16Operand reg, ushort val)
            => m.Set(reg.Reg, val);

        public static byte Get(this MachineState m, Reg8Operand reg)
            => m.Get(reg.Reg);

        public static void Set(this MachineState m, Reg8Operand reg, byte val)
            => m.Set(reg.Reg, val);
    }
}