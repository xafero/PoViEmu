using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class MathXyzArg : OpArg, IByteArg, ICalcArg
    {
        public MathXyzArg(OpArg x, char op1, OpArg y, char op2, OpArg z)
        {
            X = x;
            Op1 = op1;
            Y = y;
            Op2 = op2;
            Z = z;
        }

        public OpArg X { get; }
        public char Op1 { get; }
        public OpArg Y { get; }
        public char Op2 { get; }
        public OpArg Z { get; }
        public Instruction? Parent { private get; set; }

        public override string ToString()
        {
            var xS = $"{X}";
            var yS = $"{Y}";
            var zS = $"{Z}";
            return $"[{xS}{Op1}{yS}{Op2}{zS}]";
        }

        public byte[] Bytes => Parent.CollectBytes(X, Y, Z);
    }
}