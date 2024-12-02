using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Mins
{
    public static class Flag2Tool
    {
        public static void ClearAuxiliary(this MachineState m)
        {
            m.AF = false;
        }

        public static void ClearCarry(this MachineState m)
        {
            m.CF = false;
        }

        public static void ClearOverflow(this MachineState m)
        {
            m.OF = false;
        }

        public static bool IsAuxiliarySet(this MachineState m)
        {
            return m.AF;
        }

        public static bool IsCarrySet(this MachineState m)
        {
            return m.CF;
        }

        public static void SetAuxiliary(this MachineState m)
        {
            m.AF = true;
        }

        public static void SetCarry(this MachineState m)
        {
            m.CF = true;
        }

        public static void SetAuxiliary8(this MachineState m, int result, byte a, byte b)
        {
            m.AF = ((a ^ b ^ result) & 0x10) != 0;
        }

        public static void SetAuxiliary16(this MachineState m, int result, ushort a, ushort b)
        {
            m.AF = ((a ^ b ^ result) & 0x10) != 0;
        }

        public static void SetCarry8(this MachineState m, int result)
        {
            m.CF = (result & 0x100) != 0;
        }

        public static void SetCarry16(this MachineState m, int result)
        {
            m.CF = (result & 0x10000) != 0;
        }

        public static void SetCarryInt(this MachineState m, int carry)
        {
            m.CF = carry != 0;
        }

        public static void SetCarry8S(this MachineState m, byte a, byte b)
        {
            m.CF = ((a << b) & (1 << 8)) != 0;
        }

        public static void SetCarry16S(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a << b) & (1 << 16)) != 0;
        }

        public static void SetCarry16R(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a >> (b - 1)) & 0x01) != 0;
        }

        public static void SetCarry8R(this MachineState m, byte a, byte b)
        {
            m.CF = ((a >> (b - 1)) & 0x01) != 0;
        }

        public static void SetCarry8Sh(this MachineState m, byte a)
        {
            m.CF = (a & 0x80) != 0;
        }

        public static void SetCarry16Sh(this MachineState m, ushort a)
        {
            m.CF = (a & 0x8000) != 0;
        }

        public static void SetCarry8H(this MachineState m, byte a, byte b)
        {
            m.CF = ((a >> (b - 1)) & (1 << 0)) != 0;
        }

        public static void SetCarry16H(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a >> (b - 1)) & (1 << 0)) != 0;
        }

        public static void SetCarry16N(this MachineState m, ushort b)
        {
            m.CF = b != 0;
        }

        public static void SetCarry8N(this MachineState m, byte b)
        {
            m.CF = b != 0;
        }

        public static void SetCarry8U(this MachineState m, uint result)
        {
            m.CF = result >> 8 != 0;
        }

        public static void SetCarry16U(this MachineState m, uint result)
        {
            m.CF = result >> 16 != 0;
        }

        public static void SetOverflow8Add(this MachineState m, int result, byte a, byte b)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x80) != 0;
        }

        public static void SetOverflow16Add(this MachineState m, int result, ushort a, ushort b)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x8000) != 0;
        }

        public static void SetOverflow8Sub(this MachineState m, int result, byte a, byte b)
        {
            m.OF = ((result ^ a) & (a ^ b) & 0x80) != 0;
        }

        public static void SetOverflow16Sub(this MachineState m, int result, ushort a, ushort b)
        {
            m.OF = ((result ^ a) & (a ^ b) & 0x8000) != 0;
        }

        public static void SetOverflowX16(this MachineState m, int result, ushort a, ushort b)
        {
            m.OF = ((result ^ a) & (a ^ b) & 0x8000) != 0;
        }

        public static void SetOverflow8S(this MachineState m, int result, byte a)
        {
            m.OF = ((a ^ result) & 0x80) != 0;
        }

        public static void SetOverflow16S(this MachineState m, int result, ushort a)
        {
            m.OF = ((a ^ result) & 0x8000) != 0;
        }

        public static void SetOverflow16S(this MachineState m, ushort a, int result)
        {
            m.OF = ((a ^ result) & 0x8000) != 0;
        }

        public static void SetOverflow8Sh(this MachineState m, int result, byte a, bool signed)
        {
            m.OF = !signed && ((a ^ result) & 0x80) != 0;
        }

        public static void SetOverflow16Sh(this MachineState m, int result, ushort a, bool signed)
        {
            m.OF = !signed && ((a ^ result) & 0x8000) != 0;
        }

        public static void SetOverflow8U(this MachineState m, uint result)
        {
            m.OF = result >> 8 != 0;
        }

        public static void SetOverflow16U(this MachineState m, uint result)
        {
            m.OF = result >> 16 != 0;
        }

        public static void SetOverflowCarryFlag8(this MachineState m, uint result)
        {
            m.OF = result >> 8 != 0;
            m.CF = result >> 8 != 0;
        }

        public static void SetOverflowCarryFlag16(this MachineState m, uint result)
        {
            m.OF = result >> 16 != 0;
            m.CF = result >> 16 != 0;
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

        public static void SetSign8(this MachineState m, int result)
        {
            m.SF = (result & 0x80) != 0;
        }

        public static void SetSign16(this MachineState m, int result)
        {
            m.SF = (result & 0x8000) != 0;
        }

        public static void SetZero8(this MachineState m, int result)
        {
            m.ZF = (result & 0xFF) == 0;
        }

        public static void SetZero16(this MachineState m, int result)
        {
            m.ZF = (result & 0xFFFF) == 0;
        }

        public static void SetOverflow8(this MachineState m, byte a, byte b, int result)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x80) != 0;
        }

        public static void SetAux8(this MachineState m, byte a, byte b, int result)
        {
            m.AF = ((a ^ b ^ result) & 0x10) != 0;
        }

        public static void SetOverflow16(this MachineState m, ushort a, ushort b, int result)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x8000) != 0;
        }

        public static void SetAux16(this MachineState m, ushort a, ushort b, int result)
        {
            m.AF = ((a ^ b ^ result) & 0x10) != 0;
        }

        public static void SetParity16(this MachineState m, int result)
        {
            m.PF = Flag2Tool.CalculateParity(result & 0xFF);
        }

        public static void SetCarry16(this MachineState m, ushort b)
        {
            m.CF = b != 0;
        }

        public static void SetCarryW16(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a << b) & (1 << 16)) != 0;
        }

        public static void SetCarryW8(this MachineState m, byte a, byte b)
        {
            m.CF = ((a << b) & (1 << 8)) != 0;
        }

        public static void SetCarryOne(this MachineState m, ushort a, ushort b)
        {
            m.CF = ((a >> (b - 1)) & 0x01) != 0;
        }

        public static void SetCarryOne(this MachineState m, byte a, byte b)
        {
            m.CF = ((a >> (b - 1)) & (1 << 0)) != 0;
        }

        public static void SetParityFlag8(this MachineState m, int result)
        {
            m.PF = Flag2Tool.CalculateParity(result & 0xFF);
        }

        public static void SetOverflowFlag8(this MachineState m, byte a, int result)
        {
            m.OF = ((a ^ result) & 0x80) != 0;
        }

        public static void SetAuxFlag8(this MachineState m, int result, ushort a, ushort b)
        {
            m.AF = ((a ^ b ^ result) & 0x10) != 0;
        }

        public static void SetAuxFlag8(this MachineState m, byte a, int result)
        {
            m.AF = ((a ^ 1 ^ result) & 0x10) != 0;
        }

        public static void SetAuxFlag16(this MachineState m, ushort a, int result)
        {
            m.AF = ((a ^ 1 ^ result) & 0x10) != 0;
        }

        public static void SetOverflow16(this MachineState m, int result, ushort a, ushort b)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x8000) != 0;
        }

        public static void SetOverflow8(this MachineState m, int result, byte a, byte b)
        {
            m.OF = ((result ^ a) & (result ^ b) & 0x80) != 0;
        }

        public static void SetOverflowX8(this MachineState m, int result, byte a, byte b)
        {
            m.OF = ((result ^ a) & (a ^ b) & 0x80) != 0;
        }

        public static void SetOverflowY8(this MachineState m, int result, byte a)
        {
            m.OF = ((result ^ a) & (a ^ 1) & 0x80) != 0;
        }

        public static void SetOverflowY16(this MachineState m, int result, ushort a)
        {
            m.OF = ((result ^ a) & (a ^ 1) & 0x8000) != 0;
        }

        public static void SetOverflowZ8(this MachineState m, int result, byte b)
        {
            m.OF = ((result ^ 0) & (0 ^ b) & 0x80) != 0;
        }

        public static void SetOverflowZ16(this MachineState m, int result, ushort b)
        {
            m.OF = ((result ^ 0) & (0 ^ b) & 0x8000) != 0;
        }

        public static void SetAuxX16(this MachineState m, int result, ushort b)
        {
            m.AF = ((0 ^ b ^ result) & 0x10) != 0;
        }

        public static void SetAuxX8(this MachineState m, int result, byte b)
        {
            m.AF = ((0 ^ b ^ result) & 0x10) != 0;
        }

        public static void SetCarry8(this MachineState m, byte b)
        {
            m.CF = b != 0;
        }
    }
}