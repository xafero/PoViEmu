using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.Tools
{
    internal static class Defaults
    {
        public static MachineStateSH3 StateSh3 { get; } = new()
        {
            I0 = true, I1 = true, I2 = true, I3 = true, M = true,
            Q = true, T = true, R0 = 0x10, R1 = 0x11, R2 = 0x12,
            R3 = 0x13, R4 = 0x14, R5 = 0x15, R6 = 0x16, R7 = 0x17,
            R8 = 0x18, R9 = 0x19, R10 = 0x20, R11 = 0x21, R12 = 0x22,
            R13 = 0x23, R14 = 0x24, R15 = 0x25, R0_b = 0x30, R1_b = 0x31,
            R2_b = 0x32, R3_b = 0x33, R4_b = 0x34, R5_b = 0x35, R6_b = 0x36,
            R7_b = 0x37, S = true, MD = true, PC = 0x38, PR = 0x39,
            BL = true, SSR = 0x40, dPC = 0x41, GBR = 0x42, RB = true,
            SPC = 0x43, VBR = 0x44, MACH = 0x45, MACL = 0x46
        };

        public static MachineStateI86 StateI86 { get; } = new()
        {
            Bk0 = 0x10, Bk1 = 0x11, Bk2 = 0x12, Bk3 = 0x13,
            Bk4 = 0x14, Bk5 = 0x15, Bk6 = 0x16, Fr0 = 0x20,
            Fr1 = 0x21, Fr2 = 0x22, Fr3 = 0x23, Fr4 = 0x24,
            Fr5 = 0x25, Fr6 = 0x26, Fr7 = 0x27, Fr8 = 0x28,
            Fr9 = 0x29, Fr10 = 0x30, Fr11 = 0x31, AX = 0x3201,
            BX = 0x3302, CX = 0x3403, DX = 0x3504,
            CF = true, OF = true, DF = true, SF = true,
            AF = true, ES = 0x36, DS = 0x37, SP = 0x38,
            BP = 0x39, CS = 0x40, DI = 0x41, SI = 0x42,
            IF = true, IP = 0x43, SS = 0x44, TF = true,
            PF = true, ZF = true
        };
    }
}