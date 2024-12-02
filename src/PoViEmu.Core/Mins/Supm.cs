using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Mins
{
    public static class Supm
    {
        public static byte Add8(this MachineState m, bool withCarry, byte a, byte b)
        {
            var result = a + b + (withCarry && m.IsCarrySet() ? 1 : 0);

            m.SetCarry8(result);
            m.SetParity8(result);
            m.SetAuxiliary8(result, a, b);
            m.SetZero8(result);
            m.SetSign8(result);
            m.SetOverflow8Add(result, a, b);

            return (byte)result;
        }

        public static ushort Add16(this MachineState m, bool withCarry, ushort a, ushort b)
        {
            var result = a + b + (withCarry && m.IsCarrySet() ? 1 : 0);

            m.SetCarry16(result);
            m.SetParity8(result);
            m.SetAuxiliary16(result, a, b);
            m.SetZero16(result);
            m.SetSign16(result);
            m.SetOverflow16Add(result, a, b);

            return (ushort)result;
        }

        public static byte Or8(this MachineState m, byte a, byte b)
        {
            var result = a | b;

            m.ClearCarry();
            m.SetParity8(result);
            m.SetZero8(result);
            m.SetSign8(result);
            m.ClearOverflow();

            return (byte)result;
        }

        public static ushort Or16(this MachineState m, ushort a, ushort b)
        {
            var result = a | b;

            m.ClearCarry();
            m.SetParity8(result);
            m.SetZero16(result);
            m.SetSign16(result);
            m.ClearOverflow();

            return (ushort)result;
        }

        public static byte Sub8(this MachineState m, bool withBorrow, byte a, byte b)
        {
            var result = a - (b + (withBorrow && m.IsCarrySet() ? 1 : 0));

            m.SetCarry8(result);
            m.SetParity8(result);
            m.SetAuxiliary8(result, a, b);
            m.SetZero8(result);
            m.SetSign8(result);
            m.SetOverflow8Sub(result, a, b);

            return (byte)result;
        }

        public static ushort Sub16(this MachineState m, bool withBorrow, ushort a, ushort b)
        {
            var result = a - (b + (withBorrow && m.IsCarrySet() ? 1 : 0));

            m.SetCarry16(result);
            m.SetParity8(result);
            m.SetAuxiliary16(result, a, b);
            m.SetZero16(result);
            m.SetSign16(result);
            m.SetOverflow16Sub(result, a, b);

            return (ushort)result;
        }

        public static byte And8(this MachineState m, byte a, byte b)
        {
            var result = a & b;

            m.ClearCarry();
            m.SetParity8(result);
            m.SetZero8(result);
            m.SetSign8(result);
            m.ClearOverflow();

            return (byte)result;
        }

        public static ushort And16(this MachineState m, ushort a, ushort b)
        {
            var result = a & b;

            m.ClearCarry();
            m.SetParity8(result);
            m.SetZero16(result);
            m.SetSign16(result);
            m.ClearOverflow();

            return (ushort)result;
        }

        public static void Daa(this MachineState m, bool isSubtract)
        {
            var oldAl = m.AL;

            if ((oldAl & 0x0F) > 0x09 || m.IsAuxiliarySet())
            {
                m.AL += (byte)(isSubtract ? -0x06 : 0x06);
                m.SetAuxiliary();
            }
            else
                m.ClearAuxiliary();

            if (oldAl > 0x99 || m.IsCarrySet())
            {
                m.AL += (byte)(isSubtract ? -0x60 : 0x60);
                m.SetCarry();
            }
            else
                m.ClearCarry();

            m.SetParity8(m.AL);
            m.SetZero8(m.AL);
            m.SetSign8(m.AL);
        }

        public static byte Xor8(this MachineState m, byte a, byte b)
        {
            var result = a ^ b;

            m.ClearCarry();
            m.SetParity8(result);
            m.SetZero8(result);
            m.SetSign8(result);
            m.ClearOverflow();

            return (byte)result;
        }

        public static ushort Xor16(this MachineState m, ushort a, ushort b)
        {
            var result = a ^ b;

            m.ClearCarry();
            m.SetParity8(result);
            m.SetZero16(result);
            m.SetSign16(result);
            m.ClearOverflow();

            return (ushort)result;
        }

        public static void Aaa(this MachineState m, bool isSubtract)
        {
            if ((m.AL & 0x0F) > 0x09 || m.IsAuxiliarySet())
            {
                m.AL = (byte)(m.AL + (isSubtract ? -0x06 : 0x06));
                m.AH = (byte)(m.AH + (isSubtract ? -0x01 : 0x01));

                m.SetAuxiliary();
                m.SetCarry();
            }
            else
            {
                m.ClearAuxiliary();
                m.ClearCarry();
            }

            m.AL &= 0x0F;
        }

        public static byte Inc8(this MachineState m, byte a)
        {
            var result = a + 1;

            m.SetParity8(result);
            m.SetAuxiliary16(result, a, b: 1);
            m.SetZero8(result);
            m.SetSign8(result);
            m.SetOverflow8Add(result, a, b: 1);

            return (byte)result;
        }

        public static ushort Inc16(this MachineState m, ushort a)
        {
            var result = a + 1;

            m.SetParity8(result);
            m.SetAuxiliary16(result, a, b: 1);
            m.SetZero16(result);
            m.SetSign16(result);
            m.SetOverflow16Add(result, a, b: 1);

            return (ushort)result;
        }

        public static byte Dec8(this MachineState m, byte a)
        {
            var result = a - 1;

            m.SetParity8(result);
            m.SetAuxiliary16(result, a, b: 1);
            m.SetZero8(result);
            m.SetSign8(result);
            m.SetOverflow8Sub(result, a, b: 1);

            return (byte)result;
        }

        public static ushort Dec16(this MachineState m, ushort a)
        {
            var result = a - 1;

            m.SetParity8(result);
            m.SetAuxiliary16(result, a, b: 1);
            m.SetZero16(result);
            m.SetSign16(result);
            m.SetOverflow16Sub(result, a, b: 1);

            return (ushort)result;
        }

        public static byte Rol8(this MachineState m, bool withCarry, byte a, byte b)
        {
            int result;

            if (withCarry)
            {
                result = a;
                for (var n = 0; n < b; n++)
                {
                    var carry = result & 0x80;
                    result = (result << 1) | (m.IsCarrySet() ? 0x01 : 0);
                    m.SetCarryInt(carry);
                }
                m.SetOverflow8S(result, a);
                result &= 0xFF;
            }
            else
            {
                result = (a << b) | (a >> (8 - b));
                m.SetCarry8S(a, b);
                m.SetOverflow8S(result, a);
                result &= 0xFF;
            }

            return (byte)result;
        }

        public static ushort Rol16(this MachineState m, bool withCarry, ushort a, ushort b)
        {
            int result;

            if (withCarry)
            {
                result = a;
                for (var n = 0; n < b; n++)
                {
                    var carry = result & 0x80;
                    result = (result << 1) | (m.IsCarrySet() ? 0x0001 : 0);
                    m.SetCarryInt(carry);
                }
                m.SetOverflow16S(result, a);
                result &= 0xFFFF;
            }
            else
            {
                result = (a << b) | (a >> (16 - b));
                m.SetCarry16S(a, b);
                m.SetOverflow16S(result, a);
                result &= 0xFFFF;
            }

            return (ushort)result;
        }

        public static byte Ror8(this MachineState m, bool withCarry, byte a, byte b)
        {
            int result;

            if (withCarry)
            {
                result = a;
                for (var n = 0; n < b; n++)
                {
                    var carry = result & 0x01;
                    result = (m.IsCarrySet() ? 0x80 : 0) | (result >> 1);
                    m.SetCarryInt(carry);
                }
                m.SetOverflow8S(result, a);
                result &= 0xFF;
            }
            else
            {
                result = (a >> b) | (a << (8 - b));
                m.SetCarry8R(a, b);
                m.SetOverflow8S(result, a);
                result &= 0xFF;
            }

            return (byte)result;
        }

        public static ushort Ror16(this MachineState m, bool withCarry, ushort a, ushort b)
        {
            int result;

            if (withCarry)
            {
                result = a;
                for (var n = 0; n < b; n++)
                {
                    var carry = result & 0x01;
                    result = (m.IsCarrySet() ? 0x8000 : 0) | (result >> 1);
                    m.SetCarryInt(carry);
                }
                m.SetOverflow16S(result, a);
                result &= 0xFFFF;
            }
            else
            {
                result = (a >> b) | (a << (16 - b));
                m.SetCarry16R(a, b);
                m.SetOverflow16S(result, a);
                result &= 0xFFFF;
            }

            return (ushort)result;
        }

        public static byte Shl8(this MachineState m, byte a, byte b)
        {
            var result = (a << b) & 0xFF;

            if (b != 0)
            {
                m.SetCarry8S(a, b);
                m.SetParity8(result);
                m.SetZero8(result);
                m.SetSign8(result);
                if (b == 1) m.SetOverflow8S(result, a);
            }

            return (byte)result;
        }

        public static ushort Shl16(this MachineState m, ushort a, ushort b)
        {
            var result = (a << b) & 0xFFFF;

            if (b != 0)
            {
                m.SetCarry16S(a, b);
                m.SetParity8(result);
                m.SetZero16(result);
                m.SetSign16(result);
                if (b == 1) m.SetOverflow16S(result, a);
            }

            return (ushort)result;
        }

        public static byte Shr8(this MachineState m, bool signed, byte a, byte b)
        {
            if (signed && (b & 16) != 0)
            {
                m.SetCarry8Sh(a);
                return (byte)(0 - (m.IsCarrySet() ? 1 : 0));
            }

            var result = (a >> b) & 0xFF;

            m.SetCarry8H(a, b);
            if (signed && (a & 0x80) != 0) result |= 0xFF << (8 - b);
            m.SetParity8(result);
            m.SetZero8(result);
            m.SetSign8(result);
            m.SetOverflow8Sh(result, a, signed);

            return (byte)result;
        }

        public static ushort Shr16(this MachineState m, bool signed, ushort a, ushort b)
        {
            if (signed && (b & 16) != 0)
            {
                m.SetCarry16Sh(a);
                return (ushort)(0 - (m.IsCarrySet() ? 1 : 0));
            }

            var result = (a >> b) & 0xFFFF;

            m.SetCarry16H(a, b);
            if (signed && (a & 0x8000) != 0) result |= 0xFFFF << (16 - b);
            m.SetParity8(result);
            m.SetZero16(result);
            m.SetSign16(result);
            m.SetOverflow16Sh(result, a, signed);

            return (ushort)result;
        }

        public static byte Neg8(this MachineState m, byte b)
        {
            var result = -b & 0xFF;

            m.SetCarry8N(b);
            m.SetParity8(result);
            m.SetAuxiliary8(result, a: 0, b);
            m.SetZero8(result);
            m.SetSign8(result);
            m.SetOverflow8Sub(result, a: 0, b);

            return (byte)result;
        }

        public static ushort Neg16(this MachineState m, ushort b)
        {
            var result = -b & 0xFFFF;

            m.SetCarry16N(b);
            m.SetParity8(result);
            m.SetAuxiliary16(result, a: 0, b);
            m.SetZero16(result);
            m.SetSign16(result);
            m.SetOverflow16Sub(result, a: 0, b);

            return (ushort)result;
        }

        public static ushort Mul8(this MachineState m, bool signed, byte a, byte b)
        {
            var result = (uint)(signed ? (sbyte)a * (sbyte)b : a * b);

            m.SetOverflow8U(result);
            m.SetCarry8U(result);

            return (ushort)result;
        }

        public static uint Mul16(this MachineState m, bool signed, ushort a, ushort b)
        {
            var result = (uint)(signed ? (short)a * (short)b : a * b);

            m.SetOverflow16U(result);
            m.SetCarry16U(result);

            return (uint)result;
        }

        public static ushort Div8(this MachineState m, bool signed, ushort a, byte b)
        {
            if (b == 0)
            {
                Interrupt(0);
                return a;
            }

            var quotient = signed ? (short)a / (sbyte)b : a / b;
            var remainder = signed ? (short)a % (sbyte)b : a % b;

            return (ushort)(((remainder & 0xFF) << 8) | (quotient & 0xFF));
        }

        public static uint Div16(this MachineState m, bool signed, uint a, ushort b)
        {
            if (b == 0)
            {
                Interrupt(0);
                return a;
            }

            var quotient = signed ? (int)a / (short)b : (int)(a / b);
            var remainder = signed ? (int)a % (short)b : (int)(a % b);

            return (uint)(((remainder & 0xFFFF) << 16) | (quotient & 0xFFFF));
        }

        private static void Interrupt(int nr)
        {
            throw new System.NotImplementedException($"{nr} ?!");
        }
    }
}