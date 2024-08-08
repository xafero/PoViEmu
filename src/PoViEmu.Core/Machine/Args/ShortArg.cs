using System;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class ShortArg : OpArg, IByteArg
    {
        public ShortArg(short val)
        {
            Value = val;
        }

        public short Value { get; }

        public override string ToString()
        {
            return $"0x{Value:x2}";
        }

        public byte[] Bytes => BitConverter.GetBytes(Value);
    }
}