using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Decoding
{
    public static class DecodeExt
    {
        public static OpArg On(this Modifier mod, OpArg arg)
        {
            // TODO
            return null;
        }

        public static OpArg On(this Modifier mod, OpArg arg, OpArg sec)
        {
            // TODO
            return null;
        }

        public static OpArg Box(this Register reg, byte? raw = null)
        {
            return new BoxArg(reg, raw);
        }

        public static OpArg Plus(this Register reg, Register sec, byte? raw = null)
        {
            return new RegPlusRegArg(reg, sec, raw);
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