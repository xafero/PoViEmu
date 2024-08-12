using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class MathXyArg : OpArg, IByteArg, ICalcArg
    {
        public MathXyArg(OpArg x, char op, OpArg y)
        {
            X = x;
            Op = op;
            Y = y;
        }

        public OpArg X { get; }
        public char Op { get; }
        public OpArg Y { get; }
        public Instruction? Parent { private get; set; }

        public override string ToString()
        {
            var xS = $"{X}";
            var yS = $"{Y}";
            return $"[{xS}{Op}{yS}]";
        }

        public byte[] Bytes => Parent.CollectBytes(X, Y);
    }
}