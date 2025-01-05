using System;
using System.Reflection.Metadata.Ecma335;
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

    public record RegMathOperand(ShRegister Reg, int? Dis = null) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"rm{(v ? $"({Dis},{Reg})" : "")}";
        }

        public override string ToString() => $"@({Dis}, {Reg.Name()})";
    }

    public record RegOperand(ShRegister Reg) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"r{(v ? $"({Reg})" : "")}";
        }

        public override string ToString() => $"{Reg.Name()}";
    }

    public record RegRegOperand(ShRegister Reg1, ShRegister Reg2) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"rr{(v ? $"({Reg1},{Reg2})" : "")}";
        }

        public override string ToString() => $"{Reg1.Name()}:{Reg2.Name()}";
    }

    public record UImmOperand(byte Val) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"ui{(v ? $"({Val})" : "")}";
        }

        public override string ToString() => $"#{Val}";
    }

    public record SImmOperand(sbyte Val) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"si{(v ? $"({Val})" : "")}";
        }

        public override string ToString() => $"#{Val}";
    }

    public record DisplOperand(uint Val) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"d{(v ? $"({Val})" : "")}";
        }

        public override string ToString() => $"d{Val:x}";
    }
}