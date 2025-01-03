// ReSharper disable InconsistentNaming

using PoViEmu.Base.CPU;

namespace PoViEmu.I186.CPU
{
    internal static class SpecialCompute
    {
        public static byte LoadStatusFlags(this MachineState m)
        {
            var res = (m.SF.ToNum() << 7) |
                      (m.ZF.ToNum() << 6) | 0 |
                      (m.AF.ToNum() << 4) | 0 |
                      (m.PF.ToNum() << 2) | (1 << 1) |
                      m.CF.ToNum();
            return (byte)res;
        }
        
        public static void StoreStatusFlags(this MachineState m, byte res)
        {
            m.SF = (res & 0x80) != 0;
            m.ZF = (res & 0x40) != 0;
            m.AF = (res & 0x10) != 0;
            m.PF = (res & 0x04) != 0;
            m.CF = (res & 0x01) != 0;
        }
        
        public static ushort LoadWordStr(this MachineState m, ushort src)
        {
            var res = src;
            if (m.DF == false)
                m.SI += 2;
            else
                m.SI -= 2;
            return res;
        }
        
        public static byte LoadByteStr(this MachineState m, byte src)
        {
            var res = src;
            if (m.DF == false)
                m.SI++;
            else
                m.SI--;
            return res;
        }
    }
}