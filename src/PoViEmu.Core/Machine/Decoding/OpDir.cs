using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Flags]
    public enum OpDir : byte
    {
        Right = 0b00000000,

        Left = 0b00000010
    }
}