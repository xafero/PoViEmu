using System;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public class ShortArg : OpArg, IByteArg
    {
        public ShortArg(short value)
        {
            Value = value;
        }

        public short Value { get; }

        public override string ToString()
        {
            return $"0x{Value:X}".ToLowerInvariant();
        }

        public byte[] Bytes => BitConverter.GetBytes(Value);
    }
}