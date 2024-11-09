using System.IO;
using PoViEmu.Core.Hardware;

// ReSharper disable InconsistentNaming

namespace PoViEmu.CpuFan
{
    public static class StateTool
    {
        public static DOSInterrupts GetDOS(this NC3022c c)
        {
            var handler = c.InterruptTable[0x21];
            return (DOSInterrupts)handler;
        }

        public static (string Output, byte? Return, ChangeList Changes)
            Execute(byte[] bytes, int maxLimit = 1151)
        {
            var c = new NC3022c();
            var m = new MachineState();
            m.InitForCom();
            m.WriteMemory(m.CS, m.IP, bytes);

            var l = m.Collect();
            var reader = new StateCodeReader(m);

            var count = 0;
            while (!c.Halted && count <= maxLimit)
            {
                var current = reader.NextInstruction();
                c.Execute(current, m);
                count++;
            }

            var dos = c.GetDOS();
            return (dos.StdOut.ToString(), dos.ReturnCode, l);
        }
    }

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