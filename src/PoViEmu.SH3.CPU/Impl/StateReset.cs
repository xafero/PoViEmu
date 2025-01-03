// ReSharper disable InconsistentNaming

namespace PoViEmu.SH3.CPU.Impl
{
    public static class StateReset
    {
        public static void InitForCom(this MachineState m)
        {
            m.R0 = m.R1 = m.R2 = m.R3 = m.R4 = m.R5 = m.R6 = m.R7 = 0x0000;
            m.R8 = m.R9 = m.R10 = m.R11 = m.R12 = m.R13 = m.R14 = m.R15 = 0x0000;
            m.R0_b = m.R1_b = m.R2_b = m.R3_b = m.R4_b = m.R5_b = m.R6_b = m.R7_b = 0x0000;
            m.PC = 0x0100;
            m.PR = 0x0000;
            m.GBR = 0x0000;
            m.VBR = 0x0000;
            m.MACL = 0x0000;
            m.MACH = 0x0000;
            m.SPC = 0x0000;
            m.SSR = 0x0000;
            m.SR = 0x0000;
            m.T = false;
            m.S = false;
            m.Q = false;
            m.M = false;
            m.I0 = true;
            m.I1 = true;
            m.I2 = true;
            m.I3 = true;
            m.BL = false;
            m.RB = false;
            m.MD = false;
        }
    }
}