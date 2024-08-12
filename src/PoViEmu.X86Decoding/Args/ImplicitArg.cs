using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ImplicitArg : OpArg
    {
        public ImplicitArg(byte val)
        {
            Val = val;
        }

        public byte Val { get; }

        public override string ToString()
        {
            return $"{Val}";
        }
    }
}