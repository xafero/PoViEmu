namespace PoViEmu.Core.Machine.Args
{
    public sealed class SkipByteArg : SkipArg, IByteArg
    {
        public SkipByteArg(byte? value, bool signed = true) : base(signed)
        {
            Value = value.GetValueOrDefault();
        }

        public byte Value { get; }

        public override string ToString()
        {
            var skippedAddr = GetSkippedShort(Value);
            return $"0x{skippedAddr:x}";
        }

        public byte[] Bytes => [Value];
    }
}