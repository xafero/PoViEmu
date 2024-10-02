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
    }
}