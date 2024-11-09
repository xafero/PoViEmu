using PoViEmu.Core.Hardware;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Compat
{
    public static class StateReset
    {
        public static void InitForCom(this MachineState m, ushort loadSeg = 0x0665, ushort cxInit = 0x001C)
        {
            m.AX = m.BX = m.DX = m.BP = m.SI = m.DI = 0x0000;
            m.CX = cxInit;
            m.DS = m.ES = m.SS = m.CS = loadSeg;
            m.SP = 0xFFFE;
            m.IP = 0x0100;
            m.OF = false;
            m.DF = false;
            m.IF = true;
            m.SF = false;
            m.ZF = true;
            m.AF = false;
            m.PF = true;
            m.CF = false;
        }
    }
}