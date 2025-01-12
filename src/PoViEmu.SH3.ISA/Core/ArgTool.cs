using PoViEmu.SH3.ISA.Ops;
using PoViEmu.SH3.ISA.Ops.Consts;
using PoViEmu.SH3.ISA.Ops.Jumps;
using PoViEmu.SH3.ISA.Ops.Mems;
using PoViEmu.SH3.ISA.Ops.Regs;
using AM = PoViEmu.SH3.ISA.Ops.Mems.AddressingMode;

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

        public static BaseOperand W(ShRegister a, ShRegister b)
            => new Mu16Operand(AM.Indexed, a, b, null);

        public static BaseOperand L(byte a, ShRegister b)
            => L(InstTool.GetReg(a), b);

        public static BaseOperand L(ShRegister a, ShRegister b)
            => new Mu32Operand(AM.Indexed, a, b, null);

        public static BaseOperand B(byte regNo, int disp) => B(InstTool.GetReg(regNo), disp);

        public static BaseOperand B(ShRegister reg, int disp)
            => new Mu8Operand(AM.Displacement, reg, null, disp);

        public static BaseOperand W(byte regNo, int disp) => W(InstTool.GetReg(regNo), disp);

        public static BaseOperand W(ShRegister reg, int disp)
            => new Mu16Operand(AM.Displacement, reg, null, disp);

        public static BaseOperand L(byte regNo, int disp) => L(InstTool.GetReg(regNo), disp);

        public static BaseOperand L(ShRegister reg, int disp)
            => new Mu32Operand(AM.Displacement, reg, null, disp);

        public static BaseOperand I(byte imm) => new I8Operand((sbyte)imm);
        public static BaseOperand U(byte imm) => new U8Operand(imm);
        public static BaseOperand D(uint imm) => new NearOperand((int)imm);
        public static BaseOperand R(byte regNo) => new Reg32Operand(InstTool.GetReg(regNo));

        public static BaseOperand Rr(byte regNo)
            => new Mu32Operand(AM.RegIndirect, InstTool.GetReg(regNo), null, null);

        public static BaseOperand Rp(byte regNo)
            => new Mu32Operand(AM.PostIncrement, InstTool.GetReg(regNo), null, null);
        
        public static BaseOperand Rm(byte regNo)
            => new Mu32Operand(AM.PreDecrement, InstTool.GetReg(regNo), null, null);
    }
}