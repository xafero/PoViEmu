using System;

namespace PoViEmu.Base
{
    public sealed class ArrayException : Exception
    {
        public ArrayException(long address, long arrayLen)
            : base($"0x{address:X8} is not inside [0x{0:X8}:0x{arrayLen:X8}]!")
        {
            Address = address;
            ArrayLen = arrayLen;
        }

        public long Address { get; }
        public long ArrayLen { get; }
    }
}