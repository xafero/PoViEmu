using System;
using PoViEmu.Base.CPU.Diff;
using PoViEmu.I186.CPU.Impl;
using PoViEmu.I186.CPU.Soft;

namespace PoViEmu.I186.CPU
{
    public static class ExecTool
    {
        public static (string Output, byte? Return, ChangeList Changes)
            Execute(byte[] bytes, int maxLimit = 1151, Action<MachineState>? act = null)
        {
            var c = new NC3022();
            var m = new MachineState();
            m.InitForCom();
            if (act == null)
                m.WriteMemory(m.CS, m.IP, bytes);
            else
                act(m);

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
}