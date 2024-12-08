namespace PoViEmu.Core.Hardware
{
    internal static class OutsideCompute
    {
        public static void WriteByteToPort(IPorts ports, byte nr, byte val)
        {
        }
        
        public static void WriteWordToPort(IPorts ports, byte src)
        {
        }
        
        public static byte ReadByteFromPort(IPorts ports, byte src)
        {
            byte dest = 0x42; // TODO ports[src];
            return dest;
        }

        public static byte ReadByteFromPort(IPorts ports, ushort src)
        {
            var dest = ports[src];
            return dest;
        }

        public static byte ReadByteFromPortToStr(MachineState m, ushort src)
        {
            var dest = src;
            if (m.DF == false)
                m.DI++;
            else
                m.DI--;
            return (byte)dest;
        }

        public static ushort ReadWordFromPortToStr(MachineState m, ushort src)
        {
            var dest = src;
            if (m.DF == false)
                m.DI += 2;
            else
                m.DI -= 2;
            return dest;
        }

        public static void WriteByteToPortStr(this MachineState m, ushort nr, byte val)
        {
            // TODO
        }

        public static void WriteWordToPortStr(this MachineState m, ushort nr, ushort val)
        {
            // TODO
        }
    }
}