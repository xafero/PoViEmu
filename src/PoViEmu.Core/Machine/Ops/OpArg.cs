using PoViEmu.Core.Machine.Args;

namespace PoViEmu.Core.Machine.Ops
{
    public abstract class OpArg
    {
        public static implicit operator OpArg(Register val) => new RegisterArg(val);

        public static implicit operator OpArg(int val) => new ConstantArg((byte)val);
    }
}