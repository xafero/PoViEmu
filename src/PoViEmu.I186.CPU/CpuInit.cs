using IM = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.I186.CPU
{
    public static class CpuInit
    {
        public static void Reset(this IM s)
        {
            s.AX = 0x0000;
            s.BX = 0x0000;
            s.CX = 0x0000;
            s.DX = 0x0000;
            s.SI = 0x0000;
            s.DI = 0x0000;
            s.DS = 0x0000;
            s.ES = 0x0000;
            s.SS = 0x0000;
            s.SP = 0x0000;
            s.BP = 0x0000;
            s.CS = 0xFFFF;
            s.IP = 0x0000;
            s.CF = false;
            s.ZF = false;
            s.SF = false;
            s.DF = false;
            s.IF = false;
            s.OF = false;
            s.PF = false;
            s.AF = false;
            s.Bk0 = 0x0000;
            s.Bk1 = 0x0000;
            s.Bk2 = 0x0000;
            s.Bk3 = 0x0000;
            s.Bk4 = 0x0000;
            s.Bk5 = 0x0000;
            s.Bk6 = 0x0000;
            s.Fr0 = 0x0000;
            s.Fr1 = 0x0000;
            s.Fr2 = 0x0000;
            s.Fr3 = 0x0000;
            s.Fr4 = 0x0000;
            s.Fr5 = 0x0000;
            s.Fr6 = 0x0000;
            s.Fr7 = 0x0000;
            s.Fr8 = 0x0000;
            s.Fr9 = 0x0000;
            s.Fr10 = 0x0000;
            s.Fr11 = 0x0000;
        }
    }
}