using PoViEmu.X86Decoding.Core;
using PoViEmu.X86Decoding.Ops;
using PoViEmu.X86Decoding.Tools;

namespace PoViEmu.X86Decoding.Args
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