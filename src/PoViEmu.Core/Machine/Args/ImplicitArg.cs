using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class ImplicitArg : OpArg
    {
        public ImplicitArg(byte value)
        {
            Value = value;
        }

        public byte Value { get; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}