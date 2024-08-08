using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ByteArg : OpArg, IByteArg
    {
        public ByteArg(byte val)
        {
            Value = val;
        }

        public byte Value { get; }

        public override string ToString()
        {
            return $"0x{Value:x2}";
        }

        public byte[] Bytes => [Value];
    }
}