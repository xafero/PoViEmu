// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Hardware
{
    internal static class Compute
    {
        public static int ToNum(this bool value) => value ? 1 : 0;

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

        public static ushort Decrement(ushort dest)
        {
            var res = dest - 1;
            return (ushort)res;
        }

        public static ushort Increment(ushort dest)
        {
            var res = dest + 1;
            return (ushort)res;
        }

        public static ushort LogicalInclOr(ushort dest, ushort src)
        {
            var res = dest | src;
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

        public static void Compare(this MachineState m, ushort src1, ushort src2)
        {
            var res = src1 - src2;
            ModifyStatus(m, res);
        }

        public static void Compare(this MachineState m, ushort src1, short src2)
        {
            var res = src1 - src2;
            ModifyStatus(m, res);
        }

        private static void ModifyStatus(MachineState m, int res)
        {
            m.ZF = res == 0;
            m.SF = res < 0;
        }

        public static void Test(this MachineState m, ushort src1, ushort src2)
        {
            var res = src1 & src2;
            ModifyStatus(m, res);
        }

        private static void SetIndexes(this MachineState m, int size)
        {
            if (m.DF == false)
            {
                m.SI = (ushort)(m.SI + size);
                m.DI = (ushort)(m.DI + size);
            }
            else
            {
                m.SI = (ushort)(m.SI - size);
                m.DI = (ushort)(m.DI - size);
            }
        }

        public static void CompareByte(this MachineState m, byte src1, byte src2)
        {
            var res = src1 - src2;
            ModifyStatus(m, res);
            SetIndexes(m, 1);
        }

        public static void CompareWord(this MachineState m, ushort src1, ushort src2)
        {
            var res = src1 - src2;
            ModifyStatus(m, res);
            SetIndexes(m, 2);
        }

        public static void DivUnsigned(this MachineState m, byte src)
        {
            var tmp = (ushort)(m.AX / src);
            m.AL = (byte)tmp;
            m.AH = (byte)(m.AX % src);
        }

        public static void DivUnsigned(this MachineState m, ushort src)
        {
            var dividend = ((uint)m.DX << 16) | m.AX;
            var tmp = dividend / src;
            m.AX = (ushort)tmp;
            m.DX = (ushort)(dividend % src);
        }

        public static void DivUnsigned(this MachineState m, short src)
        {
            var dividend = ((uint)m.DX << 16) | m.AX;
            var tmp = dividend / src;
            m.AX = (ushort)tmp;
            m.DX = (ushort)(dividend % src);
        }

        public static void DivSigned(this MachineState m, byte src)
        {
            var tmp = (sbyte)((short)m.AX / (sbyte)src);
            m.AL = (byte)tmp;
            m.AH = (byte)((short)m.AX % (sbyte)src);
        }

        public static void DivSigned(this MachineState m, ushort src)
        {
            var dividend = m.DX << 16 | m.AX;
            var tmp = (short)(dividend / (short)src);
            m.AX = (ushort)tmp;
            m.DX = (ushort)(dividend % (short)src);
        }

        public static void DivSigned(this MachineState m, short src)
        {
            var dividend = m.DX << 16 | m.AX;
            var tmp = (short)(dividend / (short)src);
            m.AX = (ushort)tmp;
            m.DX = (ushort)(dividend % (short)src);
        }

        public static void MulSigned(this MachineState m, byte src)
        {
            var tmp = (sbyte)m.AL * (sbyte)src;
            m.AX = (ushort)tmp;
            if ((tmp & 0xFF) == tmp)
            {
                m.CF = false;
                m.OF = false;
            }
            else
            {
                m.CF = true;
                m.OF = true;
            }
        }

        public static void MulSigned(this MachineState m, ushort src)
        {
            var tmp = (short)m.AX * (short)src;
            m.AX = (ushort)(tmp & 0xFFFF);
            m.DX = (ushort)((tmp >> 16) & 0xFFFF);
            if ((tmp & 0xFFFF) == tmp)
            {
                m.CF = false;
                m.OF = false;
            }
            else
            {
                m.CF = true;
                m.OF = true;
            }
        }

        public static void MulSigned(this MachineState m, short src)
        {
            var tmp = (short)m.AX * (short)src;
            m.AX = (ushort)(tmp & 0xFFFF);
            m.DX = (ushort)((tmp >> 16) & 0xFFFF);
            if ((tmp & 0xFFFF) == tmp)
            {
                m.CF = false;
                m.OF = false;
            }
            else
            {
                m.CF = true;
                m.OF = true;
            }
        }

        public static void UnsignedMul(this MachineState m, ushort src)
        {
            var result = (uint)(m.AX * src);
            m.DX = (ushort)(result >> 16);
            m.AX = (ushort)(result & 0xFFFF);
        }
        
        public static void UnsignedMul(this MachineState m, byte src)
        {
            m.AX = (ushort)(m.AL * src);
        }

        public static ushort TwoComplNeg(this MachineState m, ushort src)
        {
            m.CF = src != 0;
            var res = -src;
            return (ushort)res;
        }

        public static ushort OneComplNeg(this MachineState _, ushort src)
        {
            var res = ~src;
            return (ushort)res;
        }
    }

    public interface IPorts
    {
        byte this[byte nr] { get; }
        byte this[ushort nr] { get; }
    }

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
            var dest = ports[src];
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