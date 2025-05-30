using PoViEmu.SH3.ISA;

namespace PoViEmu.SH3.CPU
{
    public static class CpuInit
    {
        public static void Reset(this MachineState s)
        {
            s.R0 = 0x00000000;
            s.R1 = 0x00000000;
            s.R2 = 0x00000000;
            s.R3 = 0x00000000;
            s.R4 = 0x00000000;
            s.R5 = 0x00000000;
            s.R6 = 0x00000000;
            s.R7 = 0x00000000;
            s.R8 = 0x00000000;
            s.R9 = 0x00000000;
            s.R10 = 0x00000000;
            s.R11 = 0x00000000;
            s.R12 = 0x00000000;
            s.R13 = 0x00000000;
            s.R14 = 0x00000000;
            s.R15 = 0x00000000;
            s.GBR = 0x00000000;
            s.MACH = 0x00000000;
            s.MACL = 0x00000000;
            s.PR = 0x00000000;
            s.PC = 0xA0000000;
            s.SR = (Flagged)0x700000F0;
            s.M = false;
            s.Q = false;
            s.S = false;
            s.T = false;
            s.R0_b = 0x00000000;
            s.R1_b = 0x00000000;
            s.R2_b = 0x00000000;
            s.R3_b = 0x00000000;
            s.R4_b = 0x00000000;
            s.R5_b = 0x00000000;
            s.R6_b = 0x00000000;
            s.R7_b = 0x00000000;
            s.VBR = 0x00000000;
            s.SPC = 0x00000000;
            s.SSR = 0x00000000;
        }
    }
}