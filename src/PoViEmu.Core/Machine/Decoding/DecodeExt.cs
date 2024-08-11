using System;
using System.Collections.Generic;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Decoding
{
    public static class DecodeExt
    {
        public static OpArg On(this Modifier mod, OpArg arg)
            => new ModArg(mod, arg);

        public static OpArg On(this Modifier mod, OpArg arg, OpArg sec)
            => new ModArg(mod, arg, sec);

        public static OpArg Box(this short? val)
            => new BoxArg(new ShortArg(val.GetValueOrDefault(), false));

        public static OpArg Box(this byte? val)
            => new BoxArg(new ByteArg(val.GetValueOrDefault(), false));

        public static OpArg Plus(this Register arg, short? val)
            => Plus(new RegisterArg(arg), val);

        public static OpArg Minus(this Register arg, short? val)
            => Minus(new RegisterArg(arg), val);

        public static OpArg ToMem(this short? arg, short? val)
            => new MemArg(arg.GetValueOrDefault(), val.GetValueOrDefault());

        public static OpArg ToMem(this byte? arg, short? val)
            => new MemArg(arg.GetValueOrDefault(), val.GetValueOrDefault());

        public static OpArg Box(this Register reg, byte? raw = null)
            => new BoxArg(reg, raw);

        public static OpArg Plus(this Register reg, Register sec, byte? raw = null)
            => new RegPlusRegArg(reg, sec, raw);

        public static OpArg Plus(this byte? val)
            => new ByteModArg(val.GetValueOrDefault(), '+');

        public static OpArg Minus(this byte? val)
            => new ByteModArg(val.GetValueOrDefault(), '-');

        public static OpArg Plus(this OpArg arg, short? val)
            => DoMathArg(arg, '+', new ShortArg(val.GetValueOrDefault(), true));

        public static OpArg Minus(this OpArg arg, short? val)
            => DoMathArg(arg, '-', new ShortArg(val.GetValueOrDefault(), true));

        private static OpArg DoMathArg(this OpArg arg, char op, OpArg sec)
        {
            if (arg is RegisterArg rga && sec is ShortArg sha1)
                return new MathXyArg(rga, op, sha1);

            if (arg is RegPlusRegArg rpa && sec is ShortArg sha2)
                return new MathXyzArg(rpa.A, '+', rpa.B, op, sha2);

            throw new InvalidOperationException($"{arg.GetType()} {op} {sec.GetType()}");
        }

        public static byte[] CollectBytes(this Instruction? parent, params OpArg[] args)
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
    }
}