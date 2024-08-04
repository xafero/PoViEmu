using System;

namespace PoViEmu.Core.Machine.Decoding
{
    [Serializable]
    public class InstructionError : Exception
    {
        public InstructionError(long pos, byte op) : base($"Unknown opcode 0x{op:X2} at offset 0x{pos:X2}!")
        {
        }
    }
}