using PoViEmu.Core.Machine.Args;

namespace PoViEmu.Core.Machine.Ops
{
    public abstract class OpArg
    {
        public static implicit operator OpArg(Register val) => new RegisterArg(val);
    }
}