using PoViEmu.Common;
using PoViEmu.X86Decoding.Ops;

namespace PoViEmu.X86Decoding.Args
{
    public sealed class RegisterArg : OpArg, IByteArg
    {
        public RegisterArg(Register reg, byte? raw = null)
        {
            Value = reg;
            Raw = raw;
        }

        public Register Value { get; }
        public byte? Raw { get; }

        public override string ToString()
        {
            return $"{Value.ToSmallName()}";
        }

        public byte[] Bytes => Raw == null ? [] : [Raw.Value];
    }
}