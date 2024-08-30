using Iced.Intel;
using PoViEmu.Common;

namespace PoViEmu.Core.Decoding
{
    public record XInstruction(int Offset, string Bytes, Instruction Parsed)
    {
        public override string ToString()
            => $"{Offset:x4}  {$"{Bytes}".AddSpaceTo(10)}  {ToText()}";

        private string ToText()
            => Parsed.IsInvalid ? "???" : Parsed.Format();
    }
}