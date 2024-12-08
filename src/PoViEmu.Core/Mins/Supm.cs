using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Mins
{
    public static class Supm
    {
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
        
        private static void Interrupt(int nr)
        {
            throw new System.NotImplementedException($"{nr} ?!");
        }
    }
}