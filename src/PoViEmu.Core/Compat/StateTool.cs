using System;
using System.Collections.Generic;
using PoViEmu.Core.Hardware;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Compat
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

        public static string[] ToChangeLines(this ChangeList list, bool ignoreIP = false)
        {
            var bld = new List<string>();
            foreach (var e in list.Changes)
            {
                var k = e.PropertyName;
                if (ignoreIP && k == "IP")
                    continue;
                var t = $"{k} = {e.Old.Format()} --> {e.New.Format()}";
                bld.Add(t);
            }
            return bld.ToArray();
        }
    }
}