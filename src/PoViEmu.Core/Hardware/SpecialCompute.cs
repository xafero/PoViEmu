// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Hardware
{
    internal static class Compute
    {
        private static int ToNum(this bool value) => value ? 1 : 0;

        public static byte AddWithCarry(byte dest, byte src, bool cf)
        {
            var res = dest + src + cf.ToNum();
            return (byte)res;
        }

        public static ushort AddWithCarry(ushort dest, ushort src, bool cf)
        {
            var res = dest + src + cf.ToNum();
            return (ushort)res;
        }

        public static byte Add(byte dest, byte src)
        {
            var res = dest + src;
            return (byte)res;
        }

        public static ushort Add(ushort dest, ushort src)
        {
            var res = dest + src;
            return (ushort)res;
        }

        public static ushort Add(ushort dest, short src)
        {
            var res = dest + src;
            return (ushort)res;
        }

        public static byte LogicalAnd(byte dest, byte src)
        {
            var res = dest & src;
            return (byte)res;
        }

        public static ushort LogicalAnd(ushort dest, ushort src)
        {
            var res = dest & src;
            return (ushort)res;
        }

        public static ushort LogicalAnd(ushort dest, short src)
        {
            var res = dest & src;
            return (ushort)res;
        }

        public static void ClearCarryFlag(this MachineState m)
        {
            m.CF = false;
        }

        public static void ClearDirectionFlag(this MachineState m)
        {
            m.DF = false;
        }

        public static void ClearInterruptFlag(this MachineState m)
        {
            m.IF = false;
        }
    }

    internal static class SpecialCompute
    {
        public static void AsciiAdjustAfterAdd(this MachineState m)
        {
            if ((m.AL & 0x0F) > 9 || m.AF)
            {
                m.AX += 0x106;
                m.AF = true;
                m.CF = true;
            }
            else
            {
                m.AF = false;
                m.CF = false;
            }
            m.AL &= 0x0F;
        }

        public static void AsciiAdjustBeforeDiv(this MachineState m, byte arg)
        {
            var tmpAL = m.AL;
            var tmpAH = m.AH;
            m.AL = (byte)((tmpAL + (tmpAH * arg)) & 0xFF);
            m.AH = 0;
        }

        public static void AsciiAdjustAfterMul(this MachineState m, byte arg)
        {
            var tmpAL = m.AL;
            m.AH = (byte)(tmpAL / arg);
            m.AL = (byte)(tmpAL % arg);
        }

        public static void AsciiAdjustAfterSub(this MachineState m)
        {
            if ((m.AL & 0x0F) > 9 || m.AF)
            {
                m.AX -= 6;
                m.AH -= 1;
                m.AF = true;
                m.CF = true;
                m.AL &= 0x0F;
            }
            else
            {
                m.CF = false;
                m.AF = false;
                m.AL &= 0x0F;
            }
        }

        public static void ConvertByteToWord(this MachineState m)
        {
            var t8 = (sbyte)m.AL;
            var t16 = (short)t8;
            m.AX = (ushort)t16;
        }

        public static void ComplementCarryFlag(this MachineState m)
        {
            var tmp = !m.CF;
            m.CF = tmp;
        }
    }
}