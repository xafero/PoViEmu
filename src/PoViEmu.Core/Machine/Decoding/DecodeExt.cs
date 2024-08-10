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
        
        public static OpArg Box(this short? reg)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
        }
        
        public static OpArg Box(this byte? reg)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
        }
        
        public static OpArg Plus(this OpArg arg, short? val)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
        }
        
        public static OpArg Minus(this OpArg arg, short? val)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
        }
        
        public static OpArg Plus(this Register arg, short? val)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
        }
        
        public static OpArg Minus(this Register arg, short? val)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
        }
        
        public static OpArg ToMem(this short? arg, short? val)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
        }
        
        public static OpArg ToMem(this byte? arg, short? val)
        {
            // TODO ?!
            return new BoxArg(Register.BX, null);
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
            return new ByteModArg(val.GetValueOrDefault(), '+');
        }

        public static OpArg Minus(this byte? val)
        {
            return new ByteModArg(val.GetValueOrDefault(), '-');
        }
    }
}