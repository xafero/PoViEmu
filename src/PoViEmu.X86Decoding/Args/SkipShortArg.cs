using System;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class SkipShortArg : SkipArg, IByteArg
    {
        public SkipShortArg(short? value, bool signed = true) : base(signed)
        {
            Value = value.GetValueOrDefault();
        }

        public short Value { get; }

        public override string ToString()
        {
            var skippedAddr = GetSkippedShort(Value);
            return $"0x{skippedAddr:x}";
        }

        public byte[] Bytes => BitConverter.GetBytes(Value);
    }
}