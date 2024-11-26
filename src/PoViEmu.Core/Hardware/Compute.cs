namespace PoViEmu.Core.Hardware
{
    internal static class Compute
    {
        public static int ToNum(this bool value) => value ? 1 : 0;

        public static ushort SubWithBorrow(ushort dest, ushort src, bool cf)
        {
            var res = dest - (src + cf.ToNum());
            return (ushort)res;
        }

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

        public static ushort Sub(ushort dest, ushort src)
        {
            var res = dest - src;
            return (ushort)res;
        }
        
        public static ushort Sub(ushort dest, short src)
        {
            var res = dest - src;
            return (ushort)res;
        }
        
        public static byte Sub(byte dest, byte src)
        {
            var res = dest - src;
            return (byte)res;
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

        public static ushort LogicalExclOr(ushort dest, ushort src)
        {
            var res = dest ^ src;
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

        public static void SetCarryFlag(this MachineState m)
        {
            m.CF = true;
        }

        public static void ClearCarryFlag(this MachineState m)
        {
            m.CF = false;
        }

        public static void SetDirectionFlag(this MachineState m)
        {
            m.DF = true;
        }

        public static void ClearDirectionFlag(this MachineState m)
        {
            m.DF = false;
        }

        public static void SetInterruptFlag(this MachineState m)
        {
            m.IF = true;
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

        public static ushort ShiftRight(ushort sarT, ushort sarV)
        {
            var res = sarT >> sarV;
            return (ushort)res;
        }

        public static ushort ShiftLeft(ushort a, ushort b)
        {
            var res = a << b;
            return (ushort)res;
        }
    }
}