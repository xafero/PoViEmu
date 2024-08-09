namespace PoViEmu.Core.Machine.Args
{
    public sealed class BigSkipArg : SkipArg
    {
        public BigSkipArg(short prefix, byte real) : base(real)
        {
            Prefix = prefix;
        }

        public short Prefix { get; }

        public override string ToString()
        {
            var addr = GetSkippedAddr();
            var sum = (Prefix << 8) | addr;
            return $"0x{sum:X}".ToLowerInvariant();
        }
    }
}