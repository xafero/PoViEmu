using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class MemArg : OpArg
    {
        public MemArg(byte a, short b)
        {
            A = a;
            B = b;
        }

        public MemArg(short a, short b)
        {
            A = a;
            B = b;
        }

        public object A { get; }
        public object B { get; }

        public override string ToString()
        {
            return $"{A}:{B}";
        }
    }
}