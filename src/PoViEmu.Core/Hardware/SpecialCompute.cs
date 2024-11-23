// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Hardware
{
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
    }
}