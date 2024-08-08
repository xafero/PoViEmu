using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class RegisterArg : OpArg
    {
        public RegisterArg(Register reg)
        {
            Value = reg;
        }

        public Register Value { get; }

        public override string ToString()
        {
            return $"{Value.ToSmallName()}";
        }
    }
}