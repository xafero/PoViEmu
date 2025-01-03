namespace PoViEmu.I186.CPU
{
    public static class StateTool
    {
        public static void PopAll(this MachineState m)
        {
            m.DI = m.Pop();
            m.SI = m.Pop();
            m.BP = m.Pop();
            m.SP = m.Pop();
            m.BX = m.Pop();
            m.DX = m.Pop();
            m.CX = m.Pop();
            m.AX = m.Pop();
        }
        
        public static void PushAll(this MachineState m)
        {
            var tmp = m.SP;
            m.Push(m.AX);
            m.Push(m.CX);
            m.Push(m.DX);
            m.Push(m.BX);
            m.Push(tmp);
            m.Push(m.BP);
            m.Push(m.SI);
            m.Push(m.DI);
        }
        
        public static void IncOrDec(this MachineState m, byte val, bool useSi, bool useDi)
        {
            if (m.DF == false)
            {
                if (useSi) m.SI += val;
                if (useDi) m.DI += val;
            }
            else
            {
                if (useSi) m.SI -= val;
                if (useDi) m.DI -= val;
            }
        }
        
        public static void Push(this MachineState m, ushort value)
        {
            m.SP -= 2;
            /*
               var bytes = BitConverter.GetBytes(value);
                m.WriteMemory(m.SS, m.SP, bytes);
            */
            m.U16[m.SS, m.SP] = value;
        }
        
        public static ushort Pop(this MachineState m)
        {
            var value = m.U16[m.SS, m.SP];
            /*
                var bytes = m.ReadMemory(m.SS, m.SP, 2).ToArray();
                var value = BitConverter.ToUInt16(bytes, 0);
            */
            m.SP += 2;
            return value;
        }
    }
}