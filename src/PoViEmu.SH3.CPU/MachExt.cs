using System;
using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Ops.Regs;
using Fl = PoViEmu.SH3.ISA.Flagged;

namespace PoViEmu.SH3.CPU
{
    public static class MachExt
    {
        public static uint Get(MachineState m, ShRegister r)
        {
            switch (r)
            {
                case ShRegister.R0: return m.R0;
                case ShRegister.R1: return m.R1;
                case ShRegister.R2: return m.R2;
                case ShRegister.R3: return m.R3;
                case ShRegister.R4: return m.R4;
                case ShRegister.R5: return m.R5;
                case ShRegister.R6: return m.R6;
                case ShRegister.R7: return m.R7;
                case ShRegister.R8: return m.R8;
                case ShRegister.R9: return m.R9;
                case ShRegister.R10: return m.R10;
                case ShRegister.R11: return m.R11;
                case ShRegister.R12: return m.R12;
                case ShRegister.R13: return m.R13;
                case ShRegister.R14: return m.R14;
                case ShRegister.R15: return m.R15;
                case ShRegister.R0_Bank: return m.R0_b;
                case ShRegister.R1_Bank: return m.R1_b;
                case ShRegister.R2_Bank: return m.R2_b;
                case ShRegister.R3_Bank: return m.R3_b;
                case ShRegister.R4_Bank: return m.R4_b;
                case ShRegister.R5_Bank: return m.R5_b;
                case ShRegister.R6_Bank: return m.R6_b;
                case ShRegister.R7_Bank: return m.R7_b;
                case ShRegister.MACH: return m.MACH;
                case ShRegister.MACL: return m.MACL;
                case ShRegister.GBR: return m.GBR;
                case ShRegister.VBR: return m.VBR;
                case ShRegister.PR: return m.PR;
                case ShRegister.SR: return (uint)m.SR;
                case ShRegister.SSR: return m.SSR;
                case ShRegister.PC: return m.PC;
                case ShRegister.SPC: return m.SPC;
                default: throw new ArgumentOutOfRangeException(nameof(r), r, null);
            }
        }

        public static uint Get(MachineState m, RegOperand r)
        {
            switch (r)
            {
                case RegOperand<ShRegister> sr: return Get(m, sr.Reg);
            }
            throw new InvalidOperationException($"{r} ?!");
        }

        public static void Set(MachineState m, ShRegister r, uint value)
        {
            switch (r)
            {
                case ShRegister.R0: m.R0 = value; break;
                case ShRegister.R1: m.R1 = value; break;
                case ShRegister.R2: m.R2 = value; break;
                case ShRegister.R3: m.R3 = value; break;
                case ShRegister.R4: m.R4 = value; break;
                case ShRegister.R5: m.R5 = value; break;
                case ShRegister.R6: m.R6 = value; break;
                case ShRegister.R7: m.R7 = value; break;
                case ShRegister.R8: m.R8 = value; break;
                case ShRegister.R9: m.R9 = value; break;
                case ShRegister.R10: m.R10 = value; break;
                case ShRegister.R11: m.R11 = value; break;
                case ShRegister.R12: m.R12 = value; break;
                case ShRegister.R13: m.R13 = value; break;
                case ShRegister.R14: m.R14 = value; break;
                case ShRegister.R15: m.R15 = value; break;
                case ShRegister.R0_Bank: m.R0_b = value; break;
                case ShRegister.R1_Bank: m.R1_b = value; break;
                case ShRegister.R2_Bank: m.R2_b = value; break;
                case ShRegister.R3_Bank: m.R3_b = value; break;
                case ShRegister.R4_Bank: m.R4_b = value; break;
                case ShRegister.R5_Bank: m.R5_b = value; break;
                case ShRegister.R6_Bank: m.R6_b = value; break;
                case ShRegister.R7_Bank: m.R7_b = value; break;
                case ShRegister.MACH: m.MACH = value; break;
                case ShRegister.MACL: m.MACL = value; break;
                case ShRegister.GBR: m.GBR = value; break;
                case ShRegister.VBR: m.VBR = value; break;
                case ShRegister.PR: m.PR = value; break;
                case ShRegister.SR: m.SR = (Fl)value; break;
                case ShRegister.SSR: m.SSR = value; break;
                case ShRegister.PC: m.PC = value; break;
                case ShRegister.SPC: m.SPC = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(r), r, null);
            }
        }

        public static void Set(MachineState m, RegOperand r, uint value)
        {
            switch (r)
            {
                case RegOperand<ShRegister> sr:
                    Set(m, sr.Reg, value);
                    return;
            }
            throw new InvalidOperationException($"{r} {value} ?!");
        }
    }
}