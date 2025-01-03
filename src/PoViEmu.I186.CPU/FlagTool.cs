using PoViEmu.I186.ISA;

namespace PoViEmu.I186.CPU
{
    public static class FlagTool
    {
        public static void SetCarry(this MachineState m)
        {
            m.CF = true;
        }
        
        public static void ClearCarry(this MachineState m)
        {
            m.CF = false;
        }
        
        public static void SetOverflow16U(this MachineState m, uint result)
        {
            m.OF = result >> 16 != 0;
        }
        
        public static void SetCarry16Sh(this MachineState m, ushort a)
        {
            m.CF = (a & 0x8000) != 0;
        }
        
        public static void SetCarry16R(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a >> (b - 1)) & 0x01) != 0;
        }
        
        public static void SetOverflow16Sh(this MachineState m, int result, ushort a, bool signed)
        {
            m.OF = !signed && ((a ^ result) & 0x8000) != 0;
        }
        
        public static void SetCarry16H(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a >> (b - 1)) & (1 << 0)) != 0;
        }
        
        public static void SetOverflow16Add(this MachineState m, int result, ushort a, ushort b)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x8000) != 0;
        }
        
        public static void SetOverflow8Add(this MachineState m, int result, byte a, byte b)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x80) != 0;
        }
        
        public static void SetOverflow8U(this MachineState m, uint result)
        {
            m.OF = result >> 8 != 0;
        }
        
        public static void SetCarry8U(this MachineState m, uint result)
        {
            m.CF = result >> 8 != 0;
        }
        
        public static void SetCarry16U(this MachineState m, uint result)
        {
            m.CF = result >> 16 != 0;
        }
        
        public static void SetCarry16N(this MachineState m, ushort b)
        {
            m.CF = b != 0;
        }
        
        public static void ClearOverflow(this MachineState m)
        {
            m.OF = false;
        }
        
        public static void ClearAuxiliary(this MachineState m)
        {
            m.AF = false;
        }
        
        public static void SetAuxiliary(this MachineState m)
        {
            m.AF = true;
        }
        
        public static bool IsAuxiliarySet(this MachineState m)
        {
            return m.AF;
        }
        
        public static bool IsCarrySet(this MachineState m)
        {
            return m.CF;
        }
        
        public static void SetOverflow8Sub(this MachineState m, int result, byte a, byte b)
        {
            m.OF = ((result ^ a) & (a ^ b) & 0x80) != 0;
        }
        
        public static void SetSign8(this MachineState m, int result)
        {
            m.SF = (result & 0x80) != 0;
        }
        
        public static void SetZero8(this MachineState m, int result)
        {
            m.ZF = (result & 0xFF) == 0;
        }
        
        public static void SetAuxiliary8(this MachineState m, int result, byte a, byte b)
        {
            m.AF = ((a ^ b ^ result) & 0x10) != 0;
        }
        
        public static void SetCarry8(this MachineState m, int result)
        {
            m.CF = (result & 0x100) != 0;
        }
        
        public static void SetSign16(this MachineState m, int result)
        {
            m.SF = (result & 0x8000) != 0;
        }
        
        public static void SetOverflow16Sub(this MachineState m, int result, ushort a, ushort b)
        {
            m.OF = ((result ^ a) & (a ^ b) & 0x8000) != 0;
        }
        
        public static void SetZero16(this MachineState m, int result)
        {
            m.ZF = (result & 0xFFFF) == 0;
        }
        
        public static void SetAuxiliary16(this MachineState m, int result, ushort a, ushort b)
        {
            m.AF = ((a ^ b ^ result) & 0x10) != 0;
        }
        
        internal static bool CalculateParity(int result)
        {
            var bitsSet = 0;
            while (result != 0)
            {
                bitsSet += result & 0x01;
                result >>= 1;
            }
            return bitsSet == 0 || bitsSet % 2 == 0;
        }
        
        public static void SetParity8(this MachineState m, int result)
        {
            m.PF = CalculateParity(result & 0xFF);
        }
        
        public static void SetCarry16(this MachineState m, int result)
        {
            m.CF = (result & 0x10000) != 0;
        }
        
        public static void SetOverflow16S(this MachineState m, int result, ushort a)
        {
            m.OF = ((a ^ result) & 0x8000) != 0;
        }
        
        public static void SetCarryInt(this MachineState m, int carry)
        {
            m.CF = carry != 0;
        }
        
        public static void SetCarry16S(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a << b) & (1 << 16)) != 0;
        }
        
        public static void SetFlags(this MachineState m, Flagged value)
        {
            m.CF = value.HasFlag(Flagged.Carry);
            m.PF = value.HasFlag(Flagged.Parity);
            m.AF = value.HasFlag(Flagged.Auxiliary);
            m.ZF = value.HasFlag(Flagged.Zero);
            m.SF = value.HasFlag(Flagged.Sign);
            m.TF = value.HasFlag(Flagged.Trap);
            m.IF = value.HasFlag(Flagged.Interrupt);
            m.DF = value.HasFlag(Flagged.Direction);
            m.OF = value.HasFlag(Flagged.Overflow);
        }
    }
}