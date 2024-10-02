using System;

namespace PoViEmu.Core.Hardware.AckNow
{
    public static class MachExt
    {
        public static void Set(this MachineState m, EmsRegister reg, ushort val)
        {
            switch (reg)
            {
                case EmsRegister.Bank0: m.Bank0 = val; break;
                case EmsRegister.Bank1: m.Bank1 = val; break;
                case EmsRegister.Bank2: m.Bank2 = val; break;
                case EmsRegister.Bank3: m.Bank3 = val; break;
                case EmsRegister.Bank4: m.Bank4 = val; break;
                case EmsRegister.Bank5: m.Bank5 = val; break;
                case EmsRegister.Bank6: m.Bank6 = val; break;
                case EmsRegister.Frame0: m.Frame0 = val; break;
                case EmsRegister.Frame1: m.Frame1 = val; break;
                case EmsRegister.Frame2: m.Frame2 = val; break;
                case EmsRegister.Frame3: m.Frame3 = val; break;
                case EmsRegister.Frame4: m.Frame4 = val; break;
                case EmsRegister.Frame5: m.Frame5 = val; break;
                case EmsRegister.Frame6: m.Frame6 = val; break;
                case EmsRegister.Frame7: m.Frame7 = val; break;
                case EmsRegister.Frame8: m.Frame8 = val; break;
                case EmsRegister.Frame9: m.Frame9 = val; break;
                case EmsRegister.Frame10: m.Frame10 = val; break;
                case EmsRegister.Frame11: m.Frame11 = val; break;
                default: throw new InvalidOperationException($"{reg} = {val:X4}");
            }
        }

        public static void Set(this MachineState m, FlagRegister reg, bool val)
        {
            switch (reg)
            {
                case FlagRegister.TF: m.TF = val; break;
                case FlagRegister.DF: m.DF = val; break;
                case FlagRegister.IF: m.IF = val; break;
                case FlagRegister.OF: m.OF = val; break;
                case FlagRegister.SF: m.SF = val; break;
                case FlagRegister.ZF: m.ZF = val; break;
                case FlagRegister.AF: m.AF = val; break;
                case FlagRegister.PF: m.PF = val; break;
                case FlagRegister.CF: m.CF = val; break;
                default: throw new InvalidOperationException($"{reg} = {val}");
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
        
        public static ushort Get(this MachineState m, EmsRegister reg)
        {
            switch (reg)
            {
                case EmsRegister.Bank0: return m.Bank0;
                case EmsRegister.Bank1: return m.Bank1;
                case EmsRegister.Bank2: return m.Bank2;
                case EmsRegister.Bank3: return m.Bank3;
                case EmsRegister.Bank4: return m.Bank4;
                case EmsRegister.Bank5: return m.Bank5;
                case EmsRegister.Bank6: return m.Bank6;
                case EmsRegister.Frame0: return m.Frame0;
                case EmsRegister.Frame1: return m.Frame1;
                case EmsRegister.Frame2: return m.Frame2;
                case EmsRegister.Frame3: return m.Frame3;
                case EmsRegister.Frame4: return m.Frame4;
                case EmsRegister.Frame5: return m.Frame5;
                case EmsRegister.Frame6: return m.Frame6;
                case EmsRegister.Frame7: return m.Frame7;
                case EmsRegister.Frame8: return m.Frame8;
                case EmsRegister.Frame9: return m.Frame9;
                case EmsRegister.Frame10: return m.Frame10;
                case EmsRegister.Frame11: return m.Frame11;
                default: throw new InvalidOperationException($"{reg}");
            }
        }
        
        public static bool Get(this MachineState m, FlagRegister reg)
        {
            switch (reg)
            {
                case FlagRegister.TF: return m.TF;
                case FlagRegister.DF: return m.DF;
                case FlagRegister.IF: return m.IF;
                case FlagRegister.OF: return m.OF;
                case FlagRegister.SF: return m.SF;
                case FlagRegister.ZF: return m.ZF;
                case FlagRegister.AF: return m.AF;
                case FlagRegister.PF: return m.PF;
                case FlagRegister.CF: return m.CF;
                default: throw new InvalidOperationException($"{reg}");
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