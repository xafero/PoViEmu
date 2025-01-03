using System;
using PoViEmu.I186.ISA;

namespace PoViEmu.I186.CPU
{
    public static class MachExt
    {
        public static void Set(this MachineState m, B16Register reg, ushort val)
        {
            switch (reg)
            {
                case B16Register.AX: m.AX = val; break;
                case B16Register.BX: m.BX = val; break;
                case B16Register.CX: m.CX = val; break;
                case B16Register.DX: m.DX = val; break;
                case B16Register.DI: m.DI = val; break;
                case B16Register.SI: m.SI = val; break;
                case B16Register.BP: m.BP = val; break;
                case B16Register.IP: m.IP = val; break;
                case B16Register.SP: m.SP = val; break;
                case B16Register.CS: m.CS = val; break;
                case B16Register.DS: m.DS = val; break;
                case B16Register.ES: m.ES = val; break;
                case B16Register.SS: m.SS = val; break;
                default: throw new InvalidOperationException($"{reg} = {val:X4}");
            }
        }
        
        public static void Set(this MachineState m, B8Register reg, byte val)
        {
            switch (reg)
            {
                case B8Register.AL: m.AL = val; break;
                case B8Register.BL: m.BL = val; break;
                case B8Register.CL: m.CL = val; break;
                case B8Register.DL: m.DL = val; break;
                case B8Register.AH: m.AH = val; break;
                case B8Register.BH: m.BH = val; break;
                case B8Register.CH: m.CH = val; break;
                case B8Register.DH: m.DH = val; break;
                default: throw new InvalidOperationException($"{reg} = {val:X2}");
            }
        }
        
        public static byte Get(this MachineState m, B8Register reg)
        {
            switch (reg)
            {
                case B8Register.AL: return m.AL;
                case B8Register.BL: return m.BL;
                case B8Register.CL: return m.CL;
                case B8Register.DL: return m.DL;
                case B8Register.AH: return m.AH;
                case B8Register.BH: return m.BH;
                case B8Register.CH: return m.CH;
                case B8Register.DH: return m.DH;
                default: throw new InvalidOperationException($"{reg}");
            }
        }
        
        public static ushort Get(this MachineState m, B16Register reg)
        {
            switch (reg)
            {
                case B16Register.AX: return m.AX;
                case B16Register.BX: return m.BX;
                case B16Register.CX: return m.CX;
                case B16Register.DX: return m.DX;
                case B16Register.DI: return m.DI;
                case B16Register.SI: return m.SI;
                case B16Register.BP: return m.BP;
                case B16Register.IP: return m.IP;
                case B16Register.SP: return m.SP;
                case B16Register.CS: return m.CS;
                case B16Register.DS: return m.DS;
                case B16Register.ES: return m.ES;
                case B16Register.SS: return m.SS;
                default: throw new InvalidOperationException($"{reg}");
            }
        }
    }
}