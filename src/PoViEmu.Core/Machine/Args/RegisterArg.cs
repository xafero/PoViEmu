using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class RegisterArg : OpArg
    {
        public RegisterArg(Register value)
        {
            Value = value;
        }

        public Register Value { get; set; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}