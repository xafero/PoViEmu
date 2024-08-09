using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ByteModArg : OpArg, IByteArg
    {
        public ByteModArg(byte val, char op)
        {
            Val = val;
            Op = op;
        }

        public byte Val { get; }
        public char Op { get; }

        public override string ToString()
        {
            return $"byte {Op}0x{Val:x}";
        }

        public byte[] Bytes => [Val];
    }
}