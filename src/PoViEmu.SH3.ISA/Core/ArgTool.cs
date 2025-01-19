using PoViEmu.SH3.ISA.Ops;
using PoViEmu.SH3.ISA.Ops.Consts;
using PoViEmu.SH3.ISA.Ops.Places;
using PoViEmu.SH3.ISA.Ops.Mems;
using PoViEmu.SH3.ISA.Ops.Regs;
using AM = PoViEmu.SH3.ISA.Ops.Mems.AddressingMode;
using System.Text.RegularExpressions;

namespace PoViEmu.SH3.ISA.Core
{
    internal static class ArgTool
    {
        public static BaseOperand B(byte a, ShRegister b)
            => B(InstTool.GetReg(a), b);

        public static BaseOperand B(ShRegister a, ShRegister b)
            => new Mu8Operand(AM.Indexed, a, b, null);

        public static BaseOperand W(byte a, ShRegister b)
            => W(InstTool.GetReg(a), b);

        private static BaseOperand W(ShRegister a, ShRegister b)
            => new Mu16Operand(AM.Indexed, a, b, null);

        public static BaseOperand L(byte a, ShRegister b)
            => L(InstTool.GetReg(a), b);

        private static BaseOperand L(ShRegister a, ShRegister b)
            => new Mu32Operand(AM.Indexed, a, b, null);

        public static BaseOperand B((byte high, byte low) t) => B(t.high, t.low);

        private static BaseOperand B(byte regNo, int disp) => B(InstTool.GetReg(regNo), disp);

        public static BaseOperand B(ShRegister reg, int disp)
            => new Mu8Operand(AM.Displacement, reg, null, disp);

        public static BaseOperand W((byte high, byte low) t, int factor = 2) => W(t.high, t.low * factor);

        private static BaseOperand W(byte regNo, int disp) => W(InstTool.GetReg(regNo), disp);

        public static BaseOperand W(ShRegister reg, byte disp, int factor = 2) => W(reg, disp * factor);

        private static BaseOperand W(ShRegister reg, int disp)
            => new Mu16Operand(AM.Displacement, reg, null, disp);

        public static BaseOperand L((byte high, byte low) t, int factor = 4) => L(t.high, t.low * factor);

        private static BaseOperand L(byte regNo, int disp) => L(InstTool.GetReg(regNo), disp);

        public static BaseOperand L(ShRegister reg, byte disp, int factor = 4) => L(reg, disp * factor);

        private static BaseOperand L(ShRegister reg, int disp)
            => new Mu32Operand(AM.Displacement, reg, null, disp);

        public static BaseOperand I(byte imm) => new I8Operand((sbyte)imm);
        public static BaseOperand U(byte imm) => new U8Operand(imm);
        public static BaseOperand R(byte regNo) => new Reg32Operand(InstTool.GetReg(regNo));

        public static BaseOperand RrB(byte regNo)
            => new Mu8Operand(AM.RegIndirect, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RpB(byte regNo)
            => new Mu8Operand(AM.PostIncrement, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RmB(byte regNo)
            => new Mu8Operand(AM.PreDecrement, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RrW(byte regNo)
            => new Mu16Operand(AM.RegIndirect, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RpW(byte regNo)
            => new Mu16Operand(AM.PostIncrement, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RmW(byte regNo)
            => new Mu16Operand(AM.PreDecrement, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RrL(byte regNo)
            => new Mu32Operand(AM.RegIndirect, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RpL(byte regNo)
            => new Mu32Operand(AM.PostIncrement, InstTool.GetReg(regNo), null, null);

        public static BaseOperand RmL(byte regNo)
            => new Mu32Operand(AM.PreDecrement, InstTool.GetReg(regNo), null, null);

        public static BaseOperand Ac(ShRegister reg, byte disp, int factor = 2, int post = 4)
            => new AddressOperand(reg, (sbyte)disp * factor + post);

        public static BaseOperand Au(ShRegister reg, int disp, int factor = 2, int post = 4)
            => new AddressOperand(reg, disp * factor + post);

        public static BaseOperand Am(ShRegister reg, int disp, int factor = 4, int post = 4)
            => new AddressOperand(reg, disp * factor + post);

        public static BaseOperand Wa(ShRegister reg, byte dis, int factor = 2, int post = 4)
            => new Mu16Operand(AM.Relative, reg, null, dis * factor + post);

        public static BaseOperand La(ShRegister reg, byte dis, int factor = 4, int post = 4)
            => new Mu32Operand(AM.Relative, reg, null, dis * factor + post);

        public static BaseOperand Ar(ShRegister reg, byte regNo)
            => new AddressOperand(reg, InstTool.GetReg(regNo));

        public static BaseOperand Ar(byte regNo)
            => new AddressOperand(InstTool.GetReg(regNo));
    }
}