using System;
using PoViEmu.Base.CPU.Diff;
using PoViEmu.SH3.CPU.Impl;
using PoViEmu.SH3.CPU.Soft;
using PoViEmu.SH3.ISA.Decoding;

namespace PoViEmu.SH3.CPU
{
    public static class ExecTool
    {
        public static (string Output, byte? Return, ChangeList Changes)
            Execute(byte[] bytes, int maxLimit = 1151, Action<MachineState>? act = null,
                Action<XInstruction, MachineState>? beforeExec = null)
        {
            var c = new SH7291();
            var m = new MachineState();
            m.InitForCom();
            if (act == null)
                m.WriteMemory(m.PC, bytes);
            else
                act(m);

            var l = m.Collect();
            var reader = new StateCodeReader(m);

            var count = 0;
            while (!c.Halted && count <= maxLimit)
            {
                var current = reader.NextInstruction();
                beforeExec?.Invoke(current, m);
                c.Execute(current, m);
                count++;
            }

            var dos = c.GetDOS();
            return (dos.StdOut.ToString(), dos.ReturnCode, l);
        }
    }
}