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

        public static void ConvertWordToDouble(this MachineState m)
        {
            /*
             TODO
            var raw = m.AX;
            var val = (int)raw;
            m.DX = (ushort)val;
            */
            var cwdAx = (short)m.AX;
            m.DX = (ushort)(cwdAx >= 0 ? 0x0000 : 0xFFFF);
        }

        public static void DecimalAdjustAfterAdd(this MachineState m)
        {
            var oldAL = m.AL;
            var oldCF = m.CF;
            m.CF = false;

            if ((m.AL & 0x0F) > 9 || m.AF)
            {
                m.AL = (byte)(m.AL + 6);
                m.CF = oldCF || m.AL < oldAL;
                m.AF = true;
            }
            else
            {
                m.AF = false;
            }

            if (oldAL > 0x99 || oldCF)
            {
                m.AL = (byte)(m.AL + 0x60);
                m.CF = true;
            }
            else
            {
                m.CF = false;
            }
        }

        public static void DecimalAdjustAfterSub(this MachineState m)
        {
            var oldAL = m.AL;
            var oldCF = m.CF;
            m.CF = false;

            if ((m.AL & 0x0F) > 9 || m.AF)
            {
                m.AL = (byte)(m.AL - 6);
                m.CF = oldCF || m.AL > oldAL;
                m.AF = true;
            }
            else
            {
                m.AF = false;
            }

            if (oldAL > 0x99 || oldCF)
            {
                m.AL = (byte)(m.AL - 0x60);
                m.CF = true;
            }
        }

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

        public static byte LoadByteStr(this MachineState m, byte src)
        {
            var res = src;
            if (m.DF == false)
                m.SI++;
            else
                m.SI--;
            return res;
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
        
        public static byte ScanByteStr(this MachineState m, byte src)
        {
            var res = m.AL - src;
            // TODO SetStatusFlags(temp);
            if (!m.DF)
                m.DI += 1;
            else
                m.DI -= 1;
            return (byte)res;
        }
        
        public static ushort ScanWordStr(this MachineState m, ushort src)
        {
            var res = m.AX - src;
            // TODO SetStatusFlags(temp);
            if (!m.DF)
                m.DI += 2;
            else
                m.DI -= 2;
            return (ushort)res;
        }

        public static void MoveByteStr(this MachineState m)
        {
            // DS:SI to ES:DI
        }

        public static void MoveWordStr(this MachineState m)
        {
            // DS:SI to ES:DI
        }
    }
}