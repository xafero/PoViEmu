using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ImplicitArg : OpArg
    {
        public ImplicitArg(int val)
        {
            Val = val;
        }

        public int Val { get; }

        public override string ToString()
        {
            return $"{Val}";
        }
    }
}