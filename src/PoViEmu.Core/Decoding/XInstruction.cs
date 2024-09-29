using Iced.Intel;
using PoViEmu.Common;

namespace PoViEmu.Core.Decoding
{
    public record XInstruction(string Bytes, Instruction Parsed)
    {
        public override string ToString() => ToString(string.Empty);

        public string ToString(string prefix)
        {
            var (op, arg) = Parsed.FormatStr();
            var bits = Bytes.ToUpper().AddSpaceTo(16);
            var opa = op.AddSpaceTo(9);
            return $"{prefix}{Parsed.IP16:X4}   {bits}  {opa} {arg}";
        }
    }
}