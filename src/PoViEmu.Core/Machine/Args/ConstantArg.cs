using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class ConstantArg : OpArg, IByteArg
    {
        public ConstantArg(byte value)
        {
            Value = value;
        }

        public byte Value { get; }

        public override string ToString()
        {
            return $"0x{Value:x}";
        }

        public virtual byte[] Bytes => [Value];
    }
}