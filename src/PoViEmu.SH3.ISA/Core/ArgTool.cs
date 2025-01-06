using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    internal static class ArgTool
    {
        public static BaseOperand B(byte n, byte d)
        {
            return new RegMathOperand(InstTool.GetReg(n), d);
        }

        public static BaseOperand W(byte n, ushort d)
        {
            return new RegMathOperand(InstTool.GetReg(n), d);
        }

        public static BaseOperand I(byte i)
        {
            return new SImmOperand((sbyte)i);
        }

        public static BaseOperand Ui(byte i)
        {
            return new UImmOperand(i);
        }

        public static BaseOperand D(uint d)
        {
            return new DisplOperand(d);
        }

        public static BaseOperand B(ShRegister r, byte d)
        {
            return new RegMathOperand(r, d);
        }

        public static BaseOperand W(ShRegister r, ushort d)
        {
            return new RegMathOperand(r, d);
        }

        public static BaseOperand L(ShRegister r, ushort d)
        {
            return new RegMathOperand(r, d);
        }

        public static BaseOperand B(ShRegister a, ShRegister b)
        {
            return new RegRegOperand(a, b);
        }

        public static BaseOperand R(byte r, 
            bool minus = false, bool plus = false, bool isRef = false)
        {
            if (minus) return new RegMathOperand(InstTool.GetReg(r));
            if (plus) return new RegMathOperand(InstTool.GetReg(r));
            return new RegOperand(InstTool.GetReg(r));
        }

        public static BaseOperand B(byte a, ShRegister b)
        {
            return new RegMathOperand(b, a);
        }

        public static BaseOperand W(byte a, ShRegister b)
        {
            return new RegMathOperand(b, a);
        }

        public static BaseOperand L(byte a, ShRegister b)
        {
            return new RegMathOperand(b, a);
        }

        public static BaseOperand L(byte a, ushort b)
        {
            return new RegMathOperand(InstTool.GetReg(a), b);
        }
    }
}