using System;
using System.Linq;

namespace PoViEmu.X86Decoding.Core
{
    [Serializable]
    public sealed class InstructionError : Exception
    {
        public InstructionError(long pos, byte[] bytes) : base(ToMessage(pos, bytes))
        {
        }

        private static string ToMessage(long pos, byte[] bytes)
        {
            var op = bytes[0];
            var rst = string.Join(" ", bytes.Skip(1).TakeWhile(b => b != 0x00)
                .Select(b => $"0x{b:X2}"));
            return $"Unknown opcode 0x{op:X2} {rst} at offset 0x{pos:X2}!";
        }
    }
}