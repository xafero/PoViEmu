using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Decoding
{
    public static class DecodeExt
    {
        public static OpArg On(this Modifier mod, OpArg arg)
        {
            return new ModArg(mod, arg);
        }

        public static OpArg On(this Modifier mod, OpArg arg, OpArg sec)
        {
            return new ModArg(mod, arg, sec);
        }

        public static OpArg Box(this Register reg, byte? raw = null)
        {
            return new BoxArg(reg, raw);
        }

        public static OpArg Plus(this Register reg, Register sec, byte? raw = null)
        {
            return new RegPlusRegArg(reg, sec, raw);
        }

        public static OpArg With(this Register reg, byte? raw = null)
        {
            return new RegisterArg(reg, raw);
        }

        public static OpArg Plus(this byte? val)
        {
            // TODO
            return null;
        }

        public static OpArg Minus(this byte? val)
        {
            // TODO
            return null;
        }
    }
}