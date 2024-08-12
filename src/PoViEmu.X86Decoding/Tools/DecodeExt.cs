using System;
using System.Collections.Generic;
using PoViEmu.X86Decoding.Args;
using PoViEmu.X86Decoding.Core;
using PoViEmu.X86Decoding.Ops;

namespace PoViEmu.X86Decoding.Tools
{
    public static class DecodeExt
    {
        public static OpArg On(this Modifier mod, OpArg? arg)
            => new ModArg(mod, arg!);

        public static OpArg On(this Modifier mod, OpArg arg, OpArg? sec)
            => new ModArg(mod, arg, sec);

        public static OpArg Box(this short? val)
            => new BoxArg(new ShortArg(val.GetValueOrDefault(), false));

        public static OpArg Box(this byte? val)
            => new BoxArg(new ByteArg(val.GetValueOrDefault(), false));

        public static OpArg Plus(this Register arg, byte? val)
            => Plus(new RegisterArg(arg), val);

        public static OpArg Plus(this Register arg, short? val)
            => Plus(new RegisterArg(arg), val);

        public static OpArg Minus(this Register arg, byte? val)
            => Minus(new RegisterArg(arg), val);

        public static OpArg Minus(this Register arg, short? val)
            => Minus(new RegisterArg(arg), val);

        public static OpArg ToMem(this byte? arg, byte? val)
            => new MemArg(arg.GetValueOrDefault(), val.GetValueOrDefault());

        public static OpArg ToMem(this byte? arg, short? val)
            => new MemArg(arg.GetValueOrDefault(), val.GetValueOrDefault());

        public static OpArg ToMem(this short? arg, byte? val)
            => new MemArg(arg.GetValueOrDefault(), val.GetValueOrDefault());

        public static OpArg ToMem(this short? arg, short? val)
            => new MemArg(arg.GetValueOrDefault(), val.GetValueOrDefault());

        public static OpArg Box(this Register reg, byte? raw = null)
            => new BoxArg(reg, raw);

        public static OpArg Plus(this Register reg, Register sec, byte? raw = null)
            => new RegPlusRegArg(reg, sec, raw);

        public static OpArg SignBit(this byte? val)
            => new ByteModArg(val.GetValueOrDefault());

        public static OpArg Plus(this OpArg arg, byte? val)
            => DoMathArg(arg, '+', new ByteArg(val.GetValueOrDefault(), true));

        public static OpArg Plus(this OpArg arg, short? val)
            => DoMathArg(arg, '+', new ShortArg(val.GetValueOrDefault(), true));

        public static OpArg Minus(this OpArg arg, byte? val)
            => DoMathArg(arg, '-', new ByteArg(val.GetValueOrDefault(), true));

        public static OpArg Minus(this OpArg arg, short? val)
            => DoMathArg(arg, '-', new ShortArg(val.GetValueOrDefault(), true));

        public static OpArg Signed(this Register reg, byte? val)
            => Signed(new RegisterArg(reg), val);

        public static OpArg Signed(this Register reg, short? val)
            => Signed(new RegisterArg(reg), val);

        public static OpArg Signed(this OpArg arg, byte? val)
        {
            var ba = new ByteArg(val.GetValueOrDefault(), true);
            var op = ba.SignedVal != null ? '-' : '+';
            return DoMathArg(arg, op, ba);
        }

        public static OpArg Signed(this OpArg arg, short? val)
        {
            var ba = new ShortArg(val.GetValueOrDefault(), true);
            var op = ba.SignedVal != null ? '-' : '+';
            return DoMathArg(arg, op, ba);
        }

        private static OpArg DoMathArg(this OpArg arg, char op, OpArg sec)
        {
            if (arg is RegisterArg rga && sec is ShortArg sha1)
                return new MathXyArg(rga, op, sha1);

            if (arg is RegisterArg rgb && sec is ByteArg shb1)
                return new MathXyArg(rgb, op, shb1);

            if (arg is RegPlusRegArg rpa && sec is ShortArg sha2)
                return new MathXyzArg(rpa.A, '+', rpa.B, op, sha2);

            if (arg is RegPlusRegArg rpb && sec is ByteArg shb2)
                return new MathXyzArg(rpb.A, '+', rpb.B, op, shb2);

            throw new InvalidOperationException($"{arg.GetType()} {op} {sec.GetType()}");
        }

        private static char SwitchMath(char op)
        {
            switch (op)
            {
                case '+': return '-';
                case '-': return '+';
            }
            throw new ArgumentException($"{op} ?!");
        }

        public static byte[] CollectBytes(this Instruction? parent, params OpArg?[] args)
        {
            var list = new List<byte>();
            foreach (var arg in args)
            {
                if (arg is ICalcArg calc)
                    calc.Parent = parent;
                if (arg is IByteArg bit)
                    list.AddRange(bit.Bytes);
            }
            return list.ToArray();
        }

        public static byte[] ToLittleEndian(this object obj)
        {
            return obj is short fs ? BitConverter.GetBytes(fs) :
                obj is byte bs ? [bs] :
                [];
        }

        public static SkipShortArg Skip(this short? value)
        {
            return new SkipShortArg(value);
        }

        public static SkipByteArg Skip(this byte? value)
        {
            return new SkipByteArg(value);
        }
    }
}