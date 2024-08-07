using PoViEmu.Core.Machine.Args;
using PoViEmu.Expert;

namespace PoViEmu.Core.Machine.Ops
{
    public abstract class OpArg
    {
        public static implicit operator OpArg(GenReg8 val)
            => new RegisterArg((Register)val);

        public static implicit operator OpArg(GenReg16 val)
            => new RegisterArg((Register)val);
        
        public static implicit operator OpArg(IdxReg val)
            => new RegisterArg((Register)val);
        
        public static implicit operator OpArg(SegReg val)
            => new RegisterArg((Register)val);
        
        public static implicit operator OpArg(FloatReg val)
            => new RegisterArg((Register)val);
        
        public static implicit operator OpArg(NumReg val)
            => new RegisterArg((Register)val);

        public static implicit operator OpArg(Register val) => new RegisterArg(val);

        public static implicit operator OpArg(int val) => new ConstantArg((byte)val);
    }
}