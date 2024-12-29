using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Risc
{
    public static class H1
    {
        public static void TestYou()
        {
            var m = new MachineState();
            m.AX = 0x2345;
            m.BX = 0x1292;
            m.CF = true;
            m.TF = true;

            var m3 = new MachineState3();
            m3.R0 = 0xD002980C;
            m3.R1 = 0x8C018ED4;
            m3.R2 = 0x00000003;
            m3.R3 = 0x8C019114;
            m3.R4 = 0x8C018F58;
            m3.R5 = 0x00000003;
            m3.R6 = 0xD002A07C;
            m3.R7 = 0x8C0AAFBE;
            m3.R8 = 0x8C09A8E8;
            m3.R9 = 0x8C09B468;
            m3.R10 = 0x00000001;
            m3.R11 = 0x8C025810;
            m3.R12 = 0xA008132C;
            m3.R13 = 0x8C01241C;
            m3.R14 = 0x00000210;
            m3.R15 = 0x8C0AAFA4;
            m3.GBR = 0xA4000120;
            m3.MACH = 0x00000001;
            m3.MACL = 0x00000008;
            m3.PR = 0xA008137E;
            m3.PC = 0xA008137F;
            m3.SR = 0x40001101;
            m3.R0_b = 0xFFFFFFFF;
            m3.R1_b = 0x8C09D4EC;
            m3.R2_b = 0x8C09D50C;
            m3.R3_b = 0x8C02DE58;
            m3.R4_b = 0x00000001;
            m3.R5_b = 0x8C09E1B0;
            m3.R6_b = 0x00000002;
            m3.R7_b = 0x8C0C6760;
            m3.VBR = 0x8C02470C;
            m3.SPC = 0x8C060FC2;
            m3.SSR = 0x40001101;
            m3.M = false;
            m3.Q = true;
            m3.S = false;
            m3.T = true;
        }
    }
}