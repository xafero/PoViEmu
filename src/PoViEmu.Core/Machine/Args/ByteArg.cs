using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ByteArg : OpArg, IByteArg
    {
        public ByteArg(byte val, bool signed)
        {
            Value = val;
            Signed = signed;
        }

        public byte Value { get; }
        public bool Signed { get; }

        public override string ToString()
        {
            var val = Signed ? Value.HaveComplement(out _) : Value;
            return $"0x{val:x}";
        }

        public byte[] Bytes => [Value];
    }
}